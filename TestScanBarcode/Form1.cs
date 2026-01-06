using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZXing;

namespace TestScanBarcode
{
    public partial class Form1 : Form
    {
        string connStr = @"Data Source=Azusanyu;Initial Catalog=DocCaptureTest;Integrated Security=True";

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        bool isCaptured = false; // không cho chụp  liên tục
        bool scanForOpenForm = false; // chỉ quét để mở Form 2

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load camera list
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
            StartCamera();
        }
        private void StartCamera()
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
                return;

            videoCaptureDevice = new VideoCaptureDevice(
                filterInfoCollection[comboBoxInputCam.SelectedIndex].MonikerString);

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
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

            // Cấu hình BarcodeReader chỉ 1 lần
            var barcodeReader = new BarcodeReader
            {
                AutoRotate = true,
                Options = new ZXing.Common.DecodingOptions
                {
                    TryHarder = true,
                    PossibleFormats = new System.Collections.Generic.List<ZXing.BarcodeFormat>
                    {
                        ZXing.BarcodeFormat.CODE_128,
                        ZXing.BarcodeFormat.QR_CODE,
                        ZXing.BarcodeFormat.CODE_39,
                        ZXing.BarcodeFormat.EAN_13
                    },
                    TryInverted = true // setting zxing
                }
            };

            var result = barcodeReader.Decode(bitmap);
            if (result != null && !isCaptured)
            {
                isCaptured = true;
                string barcode = result.Text;

                this.Invoke(new Action(() =>
                {
                    textOutput.Text = barcode;
                    // ===== TRƯỜNG HỢP: QUÉT ĐEER TÌM PHIẾU =====
                    if (scanForOpenForm)
                    {
                        scanForOpenForm = false;
                        
                        this.Hide();
                        using (ShowDataFormHa f2 = new ShowDataFormHa(barcode))
                        {
                            f2.ShowDialog();
                        }
                        this.Show();

                        // KHÔNG tự quét lại
                        textOutput.Clear();
                        return;
                    }
                    else
                    {
                        // LƯU ẢNH + BARCODE
                        textOutput.Invoke(new MethodInvoker(delegate ()
                        {
                            textOutput.Text = barcode;
                        }));
                        SaveImageToDatabase(bitmap, barcode);
                        MessageBox.Show("Đã tự động chụp ảnh & lưu vào database!\nBarcode: " + barcode);                    

                        // ===== RESET NHƯ BAN ĐẦU =====
                        textOutput.Clear();
                        isCaptured = false;

                        
                    }
                }));
            }
            showVideo.Image = bitmap;
        }

        // LƯU ẢNH VÀO DATABASE
        private void SaveImageToDatabase(Bitmap bitmap, string soPhieu)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imgBytes = ms.ToArray();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string fileName = soPhieu + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";

                    string sql =    @"  INSERT INTO dbo.DocCaptureImages (SoPhieu, ImageData, FileName, UploadDate)
                        VALUES (@SoPhieu, @ImageData, @FileName, GETDATE())
                    ";

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

        private void buttStop_Click(object sender, EventArgs e)
        {
            StopCamera();
            showVideo.Image = null;
            textOutput.Clear();
            isCaptured = false;
            //dừng  xong rồi cho chạy lại 
            //StartCamera();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
                videoCaptureDevice.Stop();
        }
        // lấy mã PBH 
        public class GetCodePBH
        {
            static public string maPBH;
        }


        private void buttGetCodePBH_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(textOutput.Text))
            //{
            //    string soct = textOutput.Text;

            //    this.Hide();   // Ẩn Form 1

            //    using (ShowDataFormHa f2 = new ShowDataFormHa(soct))
            //    {
            //        f2.ShowDialog();   // mở dạng modal
            //    }

            //    this.Show();  // Form 2 đóng , hiện lại Form 1
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            scanForOpenForm = true;
            isCaptured = false;        // cho phép quét
            textOutput.Clear();
            StartCamera();
        }
    }
}
