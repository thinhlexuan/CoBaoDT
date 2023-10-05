using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.NhienLieu
{
    public partial class BangGiaForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;
        private List<DmtramNhienLieu> TramnlList = new List<DmtramNhienLieu>();
        public BangGiaForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddKeyPressEventHandlerForNumber(txtGioHL);
            Library.FormHelper.AddKeyPressEventHandlerForDecimal(txtDonGia);
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);

            TramnlList = AppGlobal.DMTramnlList;
            if (AppGlobal.User.MaDVQL != "TCT")
            {
                if (AppGlobal.User.MaDVQL == "YV")
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL || x.MaDvql == "HN").ToList();
                else if (AppGlobal.User.MaDVQL == "DN")
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL || x.MaDvql == "SG").ToList();
                else
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL).ToList();
            }
            var tramNLTT=(from ct in TramnlList                        
                           select new
                           {
                               MaTram = ct.MaTram,
                               TenTram = ct.TenTram
                           }).ToList();
            tramNLTT.Add(new { MaTram = "ALL", TenTram = "Tất cả" });
            tramNLTT = tramNLTT.OrderBy(x => x.MaTram).ToList();
            cboTramNLTT.DataSource = tramNLTT;
            cboTramNLTT.DisplayMember = "TenTram";
            cboTramNLTT.ValueMember = "MaTram";
            cboTramNLTT.SelectedIndex = 0;

            var tramNL = (from ct in TramnlList
                            select new
                            {
                                MaTram = ct.MaTram,
                                TenTram = ct.TenTram
                            }).OrderBy(x => x.TenTram).ToList();            
            cboTramNL.DataSource = tramNL;
            cboTramNL.DisplayMember = "TenTram";
            cboTramNL.ValueMember = "MaTram";
            cboTramNL.SelectedIndex = 0;

            var loaiDM = (from ct in AppGlobal.DMLoaidmList
                            select new
                            {
                                MaDM = (short)ct.ID,
                                TenDM = ct.LoaiDauMo
                            }).ToList();           

            var loaiDMTT = loaiDM.ToList();
            loaiDMTT.Add(new { MaDM = (short)-1, TenDM = "Tất cả" });           
            loaiDMTT = loaiDMTT.OrderBy(f => f.MaDM).ToList();
            cboLoaiDMTT.DataSource = loaiDMTT;
            cboLoaiDMTT.DisplayMember = "TenDM";
            cboLoaiDMTT.ValueMember = "MaDM";
            cboLoaiDMTT.SelectedIndex = 0;

            loaiDM = loaiDM.OrderBy(f => f.MaDM).ToList();
            cboLoaiDM.DataSource = loaiDM;
            cboLoaiDM.DisplayMember = "TenDM";
            cboLoaiDM.ValueMember = "MaDM";
            cboLoaiDM.SelectedIndex = 0;

            ShowControl(false);
        }
        private void BangGiaForm_Load(object sender, EventArgs e)
        {
            sdNgayBD.Value = DateTime.Today.AddDays(-1);
            sdNgayKT.Value = DateTime.Today;
        }
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            fnTraTim();
        }      
        private void fnTraTim()
        {
            try
            {
                bsBangGia.DataSource = null;                
                base.Cursor = Cursors.WaitCursor;
                string strTram = string.Empty;
                if(cboTramNLTT.SelectedValue.ToString()=="ALL")
                {
                    foreach(DmtramNhienLieu tr in TramnlList)
                    {
                        strTram += tr.MaTram + ",";
                    }
                    strTram = strTram.Substring(0, strTram.Length - 1);
                }
                else
                {
                    strTram = cboTramNLTT.SelectedValue.ToString();
                }    
                string data = "?maTram=" + strTram;
                data += "&maDauMo=" + cboLoaiDMTT.SelectedValue;
                data += "&ngayBD=" + sdNgayBD.Value;
                data += "&ngayKT=" + sdNgayKT.Value;
                List<NL_BangGia> listBangGia = HttpHelper.GetList<NL_BangGia>(Configuration.UrlCBApi + "api/NhienLieus/NLGetBangGia" + data).ToList();                
                if (listBangGia.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsBangGia.DataSource = listBangGia;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listBangGia.Count.ToString("N0");               
                base.Cursor = Cursors.Default;
                dataGridView1.Focus();
                dataGridView1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsBangGia.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            cboTramNL.Enabled = b;
            cboLoaiDM.Enabled = b;
            txtDonVi.Enabled = b;
            sdNgayHL.Enabled = b;
            txtGhiChu.Enabled = b;
            txtPhiepNhap.Enabled = b; 
            txtDonGia.Enabled = b;
            txtTyTrong.Enabled = b;
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
            cboTramNL.SelectedIndex = -1;
            cboLoaiDM.SelectedIndex = -1;            
            txtDonVi.ResetText();
            sdNgayHL.Value = DateTime.Today;
            txtGioHL.Text = DateTime.Now.ToString("HHmm");
            txtGhiChu.ResetText();
            txtPhiepNhap.Text = DateTime.Now.ToString("yyyyMMddHHmm");
            txtDonGia.ResetText();
            txtTyTrong.ResetText();
        }

        private void BindControl()
        {
            NL_BangGia bg = bsBangGia.Current as NL_BangGia;
            if (bg != null)
            {
                cboTramNL.SelectedValue = bg.MaTramNL;
                cboTramNL.Text = bg.TenTramNL;
                cboLoaiDM.SelectedValue = bg.MaDauMo;
                cboLoaiDM.Text = bg.TenDauMo;
                txtDonVi.Text = bg.DonViTinh;
                sdNgayHL.Value = bg.NgayHL.Date;
                txtGioHL.Text = bg.NgayHL.ToString("HHmm");
                txtGhiChu.Text = bg.GhiChu;
                txtDonGia.Text = bg.DonGia.ToString();
                txtTyTrong.Text = bg.TyTrong.ToString();
                txtPhiepNhap.Text = bg.PhieuNhapID.ToString(); 
            }
        }

        private NL_BangGia BindObject()
        {
            NL_BangGia bg = new NL_BangGia();
            if (!bThem)
                bg = bsBangGia.Current as NL_BangGia;
            bg.MaTramNL = cboTramNL.SelectedValue.ToString();
            bg.TenTramNL = cboTramNL.Text;
            bg.MaDauMo = short.Parse(cboLoaiDM.SelectedValue.ToString());
            bg.TenDauMo = cboLoaiDM.Text;
            bg.DonViTinh = txtDonVi.Text;
            string gioHL = txtGioHL.Text.Length == 3 ? "0" + txtGioHL.Text.Substring(0, 1) + ":" + txtGioHL.Text.Substring(1) : txtGioHL.Text.Substring(0, 2) + ":" + txtGioHL.Text.Substring(2);           
            bg.NgayHL= DateTime.Parse(sdNgayHL.Value.ToShortDateString() + " " + gioHL);
            bg.GhiChu = txtGhiChu.Text;
            bg.DonGia= decimal.Parse(txtDonGia.Text);
            bg.TyTrong = decimal.Parse(txtTyTrong.Text);
            bg.PhieuNhapID = long.Parse(txtPhiepNhap.Text);                     
            return bg;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
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
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
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
            //if (AppGlobal.User.NL < 3)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
            NL_BangGia bg = bsBangGia.Current as NL_BangGia;
            if (Library.DialogHelper.Confirm("Xóa đơn giá này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                string data = "?maTram=" + bg.MaTramNL;
                data += "&maDauMo=" + bg.MaDauMo;
                data += "&ngayHL=" + bg.NgayHL;                
                var opStatus = HttpHelper.Delete<NL_BangGia>(Configuration.UrlCBApi + "api/NhienLieus/NLDeleteBangGia" + data);
                if (opStatus.Result!=null)
                    bsBangGia.Remove(bg);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                NL_BangGia bg = BindObject();               
                if (bThem)
                {
                    bg.CreatedBy = AppGlobal.User.Username;
                    bg.CreatedName = AppGlobal.User.FullName;
                    bg.CreatedDate = DateTime.Now;
                    bg.ModifyBy = bg.CreatedBy;
                    bg.ModifyName = bg.CreatedName;
                    bg.ModifyDate = bg.CreatedDate;
                    var objInsert = await HttpHelper.Post<NL_BangGia>(Configuration.UrlCBApi + "api/NhienLieus/NLPostBangGia", bg);
                    bsBangGia.Add(bg);
                    bsBangGia.MoveLast();
                }
                else
                {
                    bg.ModifyBy = AppGlobal.User.Username;
                    bg.ModifyName= AppGlobal.User.FullName;
                    bg.ModifyDate = DateTime.Now;                    
                    var objUpdate = await HttpHelper.Put<NL_BangGia>(Configuration.UrlCBApi + "api/NhienLieus/NLPutBangGia", bg);
                    bsBangGia.EndEdit();                   
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

        private void sdNgayKT_Validated(object sender, EventArgs e)
        {
            if (sdNgayKT.Value < sdNgayBD.Value)
                sdNgayKT.Value = sdNgayBD.Value;
        }

        private void cboLoaiDM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLoaiDM.SelectedIndex >= 0)
                {
                    short maDauMo = short.Parse(cboLoaiDM.SelectedValue.ToString());
                    if (maDauMo == 0)
                        txtDonVi.Text = "Lít";
                    else
                        txtDonVi.Text = AppGlobal.DMLoaidmList.Where(x => x.ID == maDauMo).FirstOrDefault().DonViTinh;
                }
            }
            catch 
            {
                txtDonVi.Text = string.Empty;
            }
        }

        private void sdNgayHL_Validated(object sender, EventArgs e)
        {
            txtPhiepNhap.Text = sdNgayHL.Value.ToString("yyyyMMdd") + txtGioHL.Text;             
        }

        private void txtGioHL_Validated(object sender, EventArgs e)
        {
            txtPhiepNhap.Text = sdNgayHL.Value.ToString("yyyyMMdd") + txtGioHL.Text;
        }
    }
}
