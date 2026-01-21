using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ShowDataImg
{
    public partial class ShowIMGData : Form
    {
        string connStr = @"Data Source=Azusanyu;Initial Catalog=DocCaptureTest;Integrated Security=True";

        public ShowIMGData()
        {
            InitializeComponent();
            LoadList();

            // Bắt sự kiện click vào dòng
            dataGrShow.CellClick += dataGrShow_CellClick;
        }

        private void LoadList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string sql = @"
                        SELECT ID, SoPhieu, FileName, UploadDate
                        FROM dbo.DocCaptureImages
                        ORDER BY ID DESC
                    ";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGrShow.DataSource = dt;
                }

                dataGrShow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách: " + ex.Message);
            }
        }

        private void dataGrShow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dataGrShow.Rows[e.RowIndex].Cells["ID"].Value);

            LoadImageByID(id);
        }

        private void LoadImageByID(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string sql = "SELECT ImageData FROM dbo.DocCaptureImages WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", id);

                    byte[] imgBytes = cmd.ExecuteScalar() as byte[];

                    if (imgBytes != null)
                    {
                        using (MemoryStream ms = new MemoryStream(imgBytes))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load ảnh: " + ex.Message);
            }
        }
    }
}
