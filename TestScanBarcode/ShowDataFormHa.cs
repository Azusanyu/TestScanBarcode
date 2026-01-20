using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace TestScanBarcode
{
    public partial class ShowDataFormHa : Form
    {
        string connStr = @"Data Source=Azusanyu;Initial Catalog=QLDA;Integrated Security=True";

        private string soct;   // biến lưu SoCT

        // Constructor nhận barcode (SoCT)
        public ShowDataFormHa(string _soct)
        {
            InitializeComponent();
            soct = _soct;   // gán vào biến toàn cục
        }

        private void ShowDataFormHa_Load(object sender, EventArgs e)
        {
            LoadDataFromBarcode();
        }

        private void LoadDataFromBarcode()
        {
            // ===== LẤY DỮ LIỆU TỪ MT32 LÊN TEXTBOX =====
            try
            {
                string query = "SELECT * FROM MT32 WHERE SoCT = @soct";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@soct", soct);

                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        // group 1
                        textCodePBH.Text = rd["SoCT"].ToString();
                        ngayLap.Text = Convert.ToDateTime(rd["NgayCT"]).ToString("dd/MM/yyyy");
                        gioLap.Text = Convert.ToDateTime(rd["Gio"]).ToString("HH:mm:ss");
                        nguoiLap.Text = rd["NguoiLap"].ToString();
                        //group 2
                        textSoKH.Text = rd["PC_SoKH"].ToString();
                        textTenKH1.Text = rd["PC_KH1"].ToString();
                        textNote.Text = rd["PC_GC"].ToString();
                        textLoaiHang.Text = rd["PC_LH"].ToString();
                        //group 3
                        comboxMaKH.Text = rd["MaKH"].ToString();
                        comboBoxNameKH.Text = rd["TenTat"].ToString();
                        //group 4
                        textSoPhieuCan.Text = rd["SoPhieuCan"].ToString();
                        textSoXe1.Text = rd["PC_SoXe1"].ToString();
                        textSoXe2.Text = rd["PC_SoXe2"].ToString();
                        textLoaiXe.Text = rd["LoaiXe"].ToString();
                        //group 5
                        textHinhThuc.Text = rd["HinhThucGH"].ToString();
                        textThuKX.Text = rd["PC_TKX"].ToString();
                        textToXH.Text = rd["PC_To"].ToString();
                        //group 6
                        textCan1.Text = rd["PC_L1"].ToString();
                        textCan2.Text = rd["PC_L2"].ToString();
                        dateCan1.Text = Convert.ToDateTime(rd["PC_L1_TG"]).ToString("dd/MM/yyyy HH:mm:ss");
                        dateCan2.Text = Convert.ToDateTime(rd["PC_L2_TG"]).ToString("dd/MM/yyyy HH:mm:ss");
                        textNgCan1.Text = rd["PC_L1_USR"].ToString();
                        textNgCan2.Text = rd["PC_L2_USR"].ToString();
                        textTrongLhang.Text = rd["PC_TL"].ToString();
                        //group 7
                        textSLTong.Text = rd["TongSL"].ToString();
                        textQDSLTong.Text = rd["TongQuyDoi"].ToString();
                        checkXuatCL.Checked = Convert.ToBoolean(rd["XuatChenhLech"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load MT32: " + ex.Message);
            }

            // ===== LẤY DỮ LIỆU CHI TIẾT DT32 LÊN GRIDVIEW =====
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string sql = @"
                    SELECT 
                        dt.DT32ID,        
                        dt.SoDH,
                        dt.MaHH,
                        dt.TenHang,
                        dt.DVT,
                        dt.Dai,
                        dt.Rong,
                        dt.SoLuong,
                        dt.SLThucNhan
                    FROM DT32 dt
                    LEFT JOIN MT32 mt ON mt.MT32ID = dt.MT32ID
                    WHERE mt.SoCT = @soct";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@soct", soct);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridViewPBH.DataSource = dt;

                    // ===== ẨN CỘT ID =====
                    dataGridViewPBH.Columns["DT32ID"].Visible = false;
                }

                dataGridViewPBH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load DT32: " + ex.Message);
            }

        }
        private void butSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // ===== UPDATE MT32 =====
                    using (SqlCommand cmdMT = new SqlCommand(
                        @"UPDATE MT32 
                  SET XuatChenhLech = @XuatCL 
                  WHERE SoCT = @SoCT",
                        conn, tran))
                    {
                        cmdMT.Parameters.Add("@XuatCL", SqlDbType.Bit).Value = checkXuatCL.Checked;
                        cmdMT.Parameters.Add("@SoCT", SqlDbType.VarChar).Value = soct;
                        cmdMT.ExecuteNonQuery();
                    }

                    // ===== UPDATE DT32 theo DT32ID (GUID) =====
                    foreach (DataGridViewRow row in dataGridViewPBH.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (row.Cells["DT32ID"].Value == null) continue;

                        Guid dt32ID = (Guid)row.Cells["DT32ID"].Value;

                        object slThucNhan = DBNull.Value;
                        if (row.Cells["SLThucNhan"].Value != null &&
                            decimal.TryParse(row.Cells["SLThucNhan"].Value.ToString(), out decimal sl))
                        {
                            slThucNhan = sl;
                        }

                        using (SqlCommand cmdDT = new SqlCommand(
                            @"UPDATE DT32 
                              SET SLThucNhan = @SLThucNhan
                              WHERE DT32ID = @DT32ID",
                            conn, tran))
                        {
                            cmdDT.Parameters.Add("@SLThucNhan", SqlDbType.Decimal).Value = slThucNhan;
                            cmdDT.Parameters.Add("@DT32ID", SqlDbType.UniqueIdentifier).Value = dt32ID;
                            cmdDT.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    MessageBox.Show("Đã cập nhật dữ liệu");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("❌ Lỗi: " + ex.Message);
                }
            }
        }



        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }
    }


}