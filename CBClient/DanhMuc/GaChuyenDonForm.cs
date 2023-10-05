using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.DanhMuc
{
    public partial class GaChuyenDonForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;        
        public GaChuyenDonForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            txtGaName.AutoCompleteCustomSource = AppGlobal.TenGaAutoComplate;
            txtGaName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGaName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ShowControl(false);
        }      
        
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            fnTraTim();
        }
       
        private void fnTraTim()
        {
            try
            {
                bsGaChuyenDon.DataSource = null;                
                base.Cursor = Cursors.WaitCursor;
                string data = "?ngayHL=" + sdNgayHLTT.Value;
                data += "&gaName=" + txtGaTT.Text.Trim();                
                var query = HttpHelper.GetList<GaChuyenDon>(Configuration.UrlCBApi + "api/GaChuyenDons/GetGaChuyenDon" + data)
                   .OrderBy(x=>x.GaName).OrderBy(x => x.NgayHL).ToList();                
                if (query.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                List<GaChuyenDon> listGaChuyenDon=(from x in query
                        group x by new { x.GaId } into g
                        select new GaChuyenDon
                        {
                            GaId = g.Key.GaId,
                            GaName = g.FirstOrDefault().GaName,
                            NgayHL = g.LastOrDefault().NgayHL,
                            Active = g.LastOrDefault().Active,
                            GhiChu = g.LastOrDefault().GhiChu,
                            CreatedDate = g.LastOrDefault().CreatedDate,
                            CreatedBy = g.LastOrDefault().CreatedBy,
                            CreatedName = g.LastOrDefault().CreatedName,
                            ModifyDate = g.LastOrDefault().ModifyDate,
                            ModifyBy = g.LastOrDefault().ModifyBy,
                            ModifyName = g.LastOrDefault().ModifyName
                        }).ToList();

                bsGaChuyenDon.DataSource = listGaChuyenDon;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listGaChuyenDon.Count.ToString("N0");               
                base.Cursor = Cursors.Default;
                dataGridView1.Focus();
                dataGridView1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsGaChuyenDon.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            //txtGaID.Enabled = b;
            txtGaName.Enabled = b;
            sdNgayHL.Enabled = b;
            chkActive.Enabled = b;
            txtGhiChu.Enabled = b;
            btnThem.Enabled = !b;
            if (b == false)
            {
                btnSua.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnXoa.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnExport.Enabled = dataGridView1.CurrentRow == null ? false : true;
            }
            else
            {
                btnSua.Enabled = !b;
                btnXoa.Enabled = !b;
                btnExport.Enabled = !b;
            }
            btnLuu.Enabled = b;
            btnHuy.Enabled = b;
        }

        private void ClearControl()
        {
           
            txtGaID.ResetText();           
            txtGaName.ResetText();
            sdNgayHL.Value = DateTime.Today;
            chkActive.Checked = true;
            txtGhiChu.ResetText();
            txtGaName.Focus();
        }

        private void BindControl()
        {
            GaChuyenDon ga = bsGaChuyenDon.Current as GaChuyenDon;
            if (ga != null)
            {                
                txtGaID.Text = ga.GaId.ToString();
                txtGaName.Text = ga.GaName;
                sdNgayHL.Value = ga.NgayHL;
                chkActive.Checked = ga.Active;
                txtGhiChu.Text = ga.GhiChu;                          
            }
        }

        private GaChuyenDon BindObject()
        {
            GaChuyenDon ga = new GaChuyenDon();
            if (!bThem)
                ga = bsGaChuyenDon.Current as GaChuyenDon;
            ga.GaId = int.Parse(txtGaID.Text);            
            ga.GaName = txtGaName.Text;
            ga.NgayHL = sdNgayHL.Value;
            ga.Active = chkActive.Checked;
            ga.GhiChu = txtGhiChu.Text;
            return ga;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            ClearControl();          
            bThem = true;
            ShowControl(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần sửa.");
                return;
            }
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            BindControl();
            ShowControl(true);           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần xóa.");
                return;
            }
            if (AppGlobal.User.MaQH > 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            GaChuyenDon ga = bsGaChuyenDon.Current as GaChuyenDon;
            if (Library.DialogHelper.Confirm("Xóa ga chuyên dồn này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                string data = "?ngayHL=" + ga.NgayHL;
                data += "&gaId=" + ga.GaId;
                var opStatus = HttpHelper.Delete<GaChuyenDon>(Configuration.UrlCBApi + "api/GaChuyenDons/DeleteGaChuyenDon" + data);
                if (opStatus.Result.GaId== ga.GaId && opStatus.Result.NgayHL==ga.NgayHL)
                    bsGaChuyenDon.Remove(ga);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                GaChuyenDon ga = BindObject();               
                if (bThem)
                {
                    ga.CreatedBy = AppGlobal.User.Username;
                    ga.CreatedName = AppGlobal.User.FullName;
                    ga.CreatedDate = DateTime.Now;
                    ga.ModifyBy = ga.CreatedBy;
                    ga.ModifyName = ga.CreatedName;
                    ga.ModifyDate = ga.CreatedDate;
                    var objInsert = await HttpHelper.Post<GaChuyenDon>(Configuration.UrlCBApi + "api/GaChuyenDons/PostGaChuyenDon", ga);
                    bsGaChuyenDon.Add(ga);
                    bsGaChuyenDon.MoveLast();
                }
                else
                {
                    ga.ModifyBy = AppGlobal.User.Username;
                    ga.ModifyName= AppGlobal.User.FullName;
                    ga.ModifyDate = DateTime.Now;                    
                    var objUpdate = await HttpHelper.Put<GaChuyenDon>(Configuration.UrlCBApi + "api/GaChuyenDons/PutGaChuyenDon",ga);
                    bsGaChuyenDon.EndEdit();                   
                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                Library.DialogHelper.Error(ex.Message);
            }
            ClearControl();
            bThem = false;
            ShowControl(false);
            BindControl();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearControl();
            bThem = false;
            ShowControl(false);
            BindControl();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            BindControl();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                Library.FormHelper.ExportExcel(dataGridView1);
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                base.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void txtGaName_Validated(object sender, EventArgs e)
        {
            try
            {
                txtGaID.Text = AppGlobal.GaDic.FirstOrDefault(x => x.Value == txtGaName.Text).Key.ToString();               
            }
            catch
            {
                MessageBox.Show("Không tìm thấy ga");
                txtGaID.Text = string.Empty;
                txtGaName.Text = string.Empty;
                txtGaName.Focus();

            }
        }
    }
}
