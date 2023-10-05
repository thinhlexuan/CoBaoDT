using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;



namespace CBClient.HeThong
{
    public partial class NhatKyForm : DevComponents.DotNetBar.Metro.MetroForm
    {   
        string loaiLG = string.Empty;
        public NhatKyForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            dtNgayBD.Value = DateTime.Today;
            dtNgayKT.Value = dtNgayBD.Value;

            List<BangNhatKy> listBangNK = HttpHelper.GetList<BangNhatKy>(Configuration.UrlCBApi + "api/DanhMucs/GetBangNhatKy")
                  .OrderBy(x => x.TenBang).ToList();
            
            cboTenBang.Items.Add("ALL");
            foreach (var Row in listBangNK)
                cboTenBang.Items.Add(Row.TenBang);
            cboTenBang.SelectedIndex = 0;
        }
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            try
            {                
                base.Cursor = Cursors.WaitCursor;
                string data = "?ngayBD=" + dtNgayBD.Value;
                data += "&ngayKT=" + dtNgayKT.Value.AddDays(1);
                data += "&tenBang=" + cboTenBang.Text;
                data += "&tenNV=" + txtNhanVien.Text;
                List<NhatKy> listNhatKy = HttpHelper.GetList<NhatKy>(Configuration.UrlCBApi + "api/DanhMucs/GetNhatKy" + data)
                   .OrderBy(x => x.TenBang).ThenBy(x=>x.Createddate).ToList();
                if (listNhatKy.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                dataGridView1.DataSource = listNhatKy;               
                lblTableCount.Text = "Tổng số bản ghi:" + listNhatKy.Count.ToString("N0");
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {                
                base.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void dtNgay_Validated(object sender, EventArgs e)
        {
            TimeSpan timeSpan = dtNgayKT.Value - dtNgayBD.Value;
            if ((int)timeSpan.TotalDays < 0)
                dtNgayKT.Value = dtNgayBD.Value;
        }
    }
}
