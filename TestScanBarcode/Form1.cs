using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks; // Cần thêm cái này để chạy đa luồng
using System.Windows.Forms;
using ZXing;

namespace TestScanBarcode
{
    public partial class Form1 : Form
    {
        string connStr = @"Data Source=Azusanyu;Initial Catalog=DocCaptureTest;Integrated Security=True";

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        // --- CÁC BIẾN CỜ QUẢN LÝ TRẠNG THÁI ---
        private volatile bool _isReading = false; // Cờ kiểm soát luồng đọc (quan trọng để tăng tốc)
        bool isCaptured = false; // Đã chụp thành công chưa
        bool scanForOpenForm = false; // Chế độ quét

        // --- KHỞI TẠO READER 1 LẦN DUY NHẤT ---
        private readonly BarcodeReader _reader = new BarcodeReader
        {
            AutoRotate = false, // Tắt tự xoay để tăng tốc (sẽ xử lý thủ công)
            Options = new ZXing.Common.DecodingOptions
            {
                TryHarder = false, // Tắt chế độ quét sâu
                PossibleFormats = new List<ZXing.BarcodeFormat> { ZXing.BarcodeFormat.CODE_128 },
                TryInverted = false
            }
        };

        // Định nghĩa vùng quét (Ví dụ: Một hình chữ nhật ở giữa màn hình)
        private Rectangle? _cropRect = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
            {
                comboBoxInputCam.Items.Add(device.Name);
            }

            if (comboBoxInputCam.Items.Count > 0)
                comboBoxInputCam.SelectedIndex = 0;
            else
                MessageBox.Show("Không tìm thấy camera nào!");
        }

        private void buttStar_Click(object sender, EventArgs e)
        {
            isCaptured = false;
            textOutput.Clear();
            StartCamera();
        }

        private void StartCamera()
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
                return;

            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBoxInputCam.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void StopCamera()
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.SignalToStop();
                videoCaptureDevice.WaitForStop();
                videoCaptureDevice.NewFrame -= VideoCaptureDevice_NewFrame;
            }
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // 1. Lấy ảnh gốc để hiển thị
            Bitmap originalBitmap = (Bitmap)eventArgs.Frame.Clone();

            // Tính toán vùng cắt (ROI) 
            if (_cropRect == null)
            {
                int w = originalBitmap.Width;
                int h = originalBitmap.Height;
                int rectW = (int)(w * 0.5);   // rộng 50% màn hình
                int rectH = (int)(h * 0.25);  // cao 25% màn hình

                int marginX = 20; // cách lề trái 20px
                int marginY = 20; // cách lề trên 20px

                _cropRect = new Rectangle(
                    marginX,
                    marginY,
                    rectW,
                    rectH
                );

            }

            // Vẽ khung đỏ lên hình hiển thị để người dùng biết chỗ đặt mã vạch
            using (Graphics g = Graphics.FromImage(originalBitmap))
            {
                g.DrawRectangle(new Pen(Color.Red, 3), _cropRect.Value);
            }

            showVideo.Image = originalBitmap;
            if (isCaptured || _isReading)
            {
                return;
            }
            _isReading = true;
            Bitmap bitmapToProcess = (Bitmap)eventArgs.Frame.Clone();
            Task.Run(() => ProcessFrame(bitmapToProcess));
        }

        // Hàm xử lý ảnh
        private void ProcessFrame(Bitmap bitmap)
        {
            try
            {
                Bitmap croppedBitmap = bitmap.Clone(_cropRect.Value, bitmap.PixelFormat);

                var source = new ZXing.BitmapLuminanceSource(croppedBitmap);
                croppedBitmap.Dispose();

                var result = _reader.Decode(source);

                if (result == null)
                {
                    result = _reader.Decode(source.rotateCounterClockwise());
                }

                if (result != null)
                {
                  
                    this.Invoke(new Action(() =>
                    {
                        if (isCaptured) return;

                        HandleBarcodeResult(result.Text, bitmap);
                    }));
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                bitmap.Dispose();
                _isReading = false;
            }
        }

        private void HandleBarcodeResult(string barcode, Bitmap evidenceImage)
        {
            isCaptured = true;
            textOutput.Text = barcode;
            Console.Beep(); // Bíp báo hiệu

            if (scanForOpenForm)
            {
                scanForOpenForm = false;
                StopCamera(); // Nên dừng camera khi mở form khác

                this.Hide();
                using (ShowDataFormHa f2 = new ShowDataFormHa(barcode))
                {
                    f2.ShowDialog();
                }
                this.Show();

                // Reset lại để quét tiếp nếu cần
                StartCamera(); 
            }
            else
            {
                // Lưu vào DB
                SaveImageToDatabase(evidenceImage, barcode);
                MessageBox.Show("Đã lưu! Barcode: " + barcode);

                // Reset
                textOutput.Clear();
                isCaptured = false;
                _isReading = false; // Cho phép đọc tiếp
            }
        }

        private void SaveImageToDatabase(Bitmap bitmap, string soPhieu)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] imgBytes = ms.ToArray();

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string fileName = soPhieu + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";
                        string sql = @"INSERT INTO dbo.DocCaptureImages (SoPhieu, ImageData, FileName, UploadDate)
                                       VALUES (@SoPhieu, @ImageData, @FileName, GETDATE())";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar).Value = soPhieu;
                            cmd.Parameters.Add("@ImageData", SqlDbType.VarBinary).Value = imgBytes;
                            cmd.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = fileName;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu ảnh: " + ex.Message);
            }
        }

        private void buttStop_Click(object sender, EventArgs e)
        {
            StopCamera();
            showVideo.Image = null;
            textOutput.Clear();
            isCaptured = false;
            _isReading = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            scanForOpenForm = true;
            isCaptured = false;
            _isReading = false;
            textOutput.Clear();
            StartCamera();
        }
    }
}