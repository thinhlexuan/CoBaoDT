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
    public partial class MacTauForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        string MacTauID = string.Empty;
        public MacTauForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            var congTacTT = (from ct in AppGlobal.CongtacList
                           select new
                           {
                               CongTacID = (short)ct.CongTacId,
                               CongTacName = ct.CongTacName
                           }).ToList();
            congTacTT.Add(new { CongTacID = (short)0, CongTacName = "Tất cả công tác" });
            var lisTT = congTacTT.OrderBy(f => f.CongTacID).ToList();
            cboCongTacTT.DataSource = lisTT;
            cboCongTacTT.DisplayMember = "CongTacName";
            cboCongTacTT.ValueMember = "CongTacID";
            cboCongTacTT.SelectedIndex = 0;

            var loaiTau = (from ct in AppGlobal.LoaitauList
                           select new
                           {
                               LoaiTauID = (short)ct.LoaiTauID,
                               LoaiTauName = ct.LoaiTauName
                           }).ToList();
            cboLoaiTau.DataSource = loaiTau;
            cboLoaiTau.DisplayMember = "LoaiTauName";
            cboLoaiTau.ValueMember = "LoaiTauID";
            cboLoaiTau.SelectedIndex = -1;

            var congTac = (from ct in AppGlobal.CongtacList
                             select new
                             {
                                 CongTacID = (short)ct.CongTacId,
                                 CongTacName = ct.CongTacName
                             }).ToList();
            cboCongTac.DataSource = congTac;
            cboCongTac.DisplayMember = "CongTacName";
            cboCongTac.ValueMember = "CongTacID";
            cboCongTac.SelectedIndex = -1;

            var congTy = (from ct in AppGlobal.CongtyList
                           select new
                           {
                               CongTyID = ct.CongTyID,
                               CongTyName = ct.CongTyName
                           }).ToList();
            cboCongTy.DataSource = congTy;
            cboCongTy.DisplayMember = "CongTyName";
            cboCongTy.ValueMember = "CongTyID";
            cboCongTy.SelectedIndex = -1;

            var tuyen = (from ct in AppGlobal.DMTuyenmapList
                         select new
                           {
                               TuyenID = (short)ct.TuyenId,
                               TuyenName = ct.TuyenName
                           }).ToList();
            cboTuyen.DataSource = tuyen;
            cboTuyen.DisplayMember = "TuyenName";
            cboTuyen.ValueMember = "TuyenID";
            cboTuyen.SelectedIndex = -1;

            if (AppGlobal.User.MaDVQL == "TCT" && AppGlobal.User.MaQH <3)
            {
                fnCapNhat();
            }
            ShowControl(false);
        }      
        
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            fnTraTim();
        }
        private async void fnCapNhat()
        {
            if (DialogHelper.Confirm("Cập nhật lại danh sách mác tầu không?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {

                    base.Cursor = Cursors.WaitCursor;
                    List<DMMacTau> DMMactauList = HttpHelper.GetList<DMMacTau>(Configuration.UrlCBApi + "api/DanhMucs/GetDMMacTau");
                    List<MacTau> listMacTau = HttpHelper.GetList<MacTau>(Configuration.UrlCBApi + "api/MacTaus/GetMacTauNotInDTCT")
                       .OrderBy(x => x.MacTauID).ToList();
                    if (listMacTau.Count > 0)
                    {
                        string macTauID = string.Empty;
                        foreach (MacTau mt in listMacTau)
                        {
                            if (macTauID != mt.MacTauID.ToUpper())
                            {
                                var dmmt = DMMactauList.Where(x => x.MacTauID == mt.MacTauID).FirstOrDefault();
                                if (dmmt != null)
                                {
                                    mt.LoaiTauID = dmmt.LoaiTauID;
                                    mt.LoaiTauName = dmmt.LoaiTauName;
                                    mt.CongTyID = dmmt.CongTyID;
                                    mt.CongTyName = dmmt.CongTyName;
                                    mt.TuyenID = dmmt.TuyenID;
                                    mt.TuyenName = dmmt.TuyenName;
                                }
                                mt.CreatedDate = DateTime.Now;
                                mt.CreatedBy = AppGlobal.User.Username;
                                mt.CreatedName = AppGlobal.User.FullName;
                                mt.ModifyBy = mt.CreatedBy;
                                mt.ModifyName = mt.CreatedName;
                                mt.ModifyDate = mt.CreatedDate;                               
                                var objInsert = await HttpHelper.Post<MacTau>(Configuration.UrlCBApi + "api/MacTaus/PostMacTau", mt);                                
                            }
                            macTauID = mt.MacTauID.ToUpper();
                        }
                        lblTableCount.Text = "Tổng số bản ghi:" + listMacTau.Count.ToString("N0");
                    }
                    base.Cursor = Cursors.Default;                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    base.Cursor = Cursors.Default;
                }
            }
        }
        private async void fnTraTim()
        {
            try
            {
                bsMacTau.DataSource = null;                
                base.Cursor = Cursors.WaitCursor;
                string data = "?CongTac=" + cboCongTacTT.SelectedValue.ToString();
                data += "&MacTau=" + txtMacTauTT.Text.Trim();                
                List<MacTau> listMacTau = HttpHelper.GetList<MacTau>(Configuration.UrlCBApi + "api/MacTaus/GetMacTau" + data)
                   .OrderBy(x=>x.MacTauID).OrderBy(x => x.CongTacID).ToList();                
                if (listMacTau.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsMacTau.DataSource = listMacTau;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listMacTau.Count.ToString("N0");               
                base.Cursor = Cursors.Default;
                dataGridView1.Focus();
                dataGridView1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsMacTau.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            txtMacTau.Enabled = b;
            cboLoaiTau.Enabled = b;
            cboCongTac.Enabled = b;
            cboCongTy.Enabled = b;
            cboTuyen.Enabled = b;
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
            MacTauID = string.Empty;
            txtMacTau.ResetText();
            cboLoaiTau.SelectedIndex = -1;
            cboCongTac.SelectedIndex = -1;
            cboCongTy.SelectedIndex = -1;
            cboTuyen.SelectedIndex = -1;
        }

        private void BindControl()
        {
            MacTau mt = bsMacTau.Current as MacTau;
            if (mt != null)
            {
                MacTauID = mt.MacTauID;
                txtMacTau.Text = mt.MacTauID;
                cboLoaiTau.SelectedValue = string.IsNullOrWhiteSpace(mt.LoaiTauID.ToString())?0: mt.LoaiTauID;
                cboLoaiTau.Text = mt.LoaiTauName;
                cboCongTac.SelectedValue = string.IsNullOrWhiteSpace(mt.CongTacID.ToString()) ? 0 : mt.CongTacID;
                cboCongTac.Text = mt.CongTacName;
                //cboCongTy.SelectedValue = string.IsNullOrWhiteSpace(mt.CongTyID.ToString()) ? "TCTÐSVN" : mt.CongTyID;
                cboCongTy.Text = mt.CongTyName;
                cboTuyen.SelectedValue = string.IsNullOrWhiteSpace(mt.TuyenID.ToString()) ? 0 : mt.TuyenID;
                cboTuyen.Text = mt.TuyenName;
            }
        }

        private MacTau BindObject()
        {
            MacTau mt = new MacTau();
            if (!bThem)
                mt = bsMacTau.Current as MacTau;
            mt.MacTauID =  txtMacTau.Text;  
            mt.LoaiTauID= short.Parse(cboLoaiTau.SelectedValue.ToString());
            mt.LoaiTauName = cboLoaiTau.Text;
            mt.CongTacID = short.Parse(cboCongTac.SelectedValue.ToString());
            mt.CongTacName = cboCongTac.Text;
            mt.CongTyID = cboCongTy.SelectedValue.ToString();
            mt.CongTyName = cboCongTy.Text;
            mt.TuyenID= short.Parse(cboTuyen.SelectedValue.ToString());
            mt.TuyenName = cboTuyen.Text;
            return mt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.MaQH > 3 || AppGlobal.User.MaDVQL != "TCT")
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
            if (AppGlobal.User.MaQH > 3 || AppGlobal.User.MaDVQL != "TCT")
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
            if (AppGlobal.User.MaQH > 3 || AppGlobal.User.MaDVQL != "TCT")
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            MacTau mt = bsMacTau.Current as MacTau;
            if (Library.DialogHelper.Confirm("Xóa mác tầu này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<MacTau>(Configuration.UrlCBApi + "api/MacTaus/DeleteMacTau?id=" + MacTauID);
                if (opStatus.Result.MacTauID== MacTauID)
                    bsMacTau.Remove(mt);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                MacTau mt = BindObject();               
                if (bThem)
                {
                    mt.CreatedBy = AppGlobal.User.Username;
                    mt.CreatedName = AppGlobal.User.FullName;
                    mt.CreatedDate = DateTime.Now;
                    mt.ModifyBy = mt.CreatedBy;
                    mt.ModifyName = mt.CreatedName;
                    mt.ModifyDate = mt.CreatedDate;
                    var objInsert = await HttpHelper.Post<MacTau>(Configuration.UrlCBApi + "api/MacTaus/PostMacTau", mt);
                    bsMacTau.Add(mt);
                    bsMacTau.MoveLast();
                }
                else
                {
                    mt.ModifyBy = AppGlobal.User.Username;
                    mt.ModifyName= AppGlobal.User.FullName;
                    mt.ModifyDate = DateTime.Now;                    
                    var objUpdate = await HttpHelper.Put<MacTau>(Configuration.UrlCBApi + "api/MacTaus/PutMacTau?id=" + MacTauID, mt);
                    bsMacTau.EndEdit();                   
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
    }
}
