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

        bool isCaptured = false; // không cho chụp 2 lần liên tục

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
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBoxInputCam.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();

            isCaptured = false;
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
            // luu anh vao data
            //if (result != null && !isCaptured)
            //{
            //    isCaptured = true;
            //    string barcode = result.Text;

            //    textOutput.Invoke(new MethodInvoker(delegate ()
            //    {
            //        textOutput.Text = barcode;
            //    }));

            //    SaveImageToDatabase(bitmap, barcode);

            //    MessageBox.Show("Đã tự động chụp ảnh & lưu vào database!\nBarcode: " + barcode);

            //}
            if (result != null && !isCaptured)
            {
                isCaptured = true;
                string barcode = result.Text;

                this.Invoke(new Action(() =>
                {
                    textOutput.Text = barcode;

                    // LƯU ẢNH + BARCODE
                    SaveImageToDatabase(bitmap, barcode);

                    // DỪNG CAMERA NGAY
                    if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
                    {
                        videoCaptureDevice.SignalToStop();
                    }

                    // MỞ FORM 2
                    this.Hide();
                    using (ShowDataFormHa f2 = new ShowDataFormHa(barcode))
                    {
                        f2.ShowDialog();
                    }

                    // FORM 2 ĐÓNG QUAY LẠI FORM 1
                    this.Show();

                    // reset để quét tiếp
                    isCaptured = false;
                    textOutput.Clear();
                }));
            }

            showVideo.Image = bitmap;
        }


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
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
                showVideo.Image = null;
            }

            isCaptured = false;
            textOutput.Clear();
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


    }
}
