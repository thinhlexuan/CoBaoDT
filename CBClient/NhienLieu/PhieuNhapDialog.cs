using System;
using System.Linq;
using System.Windows.Forms;
using CBClient.Library;
using System.Text.RegularExpressions;
using CBClient.BLLTypes;
using CBClient.Services;
using CBClient.Models;
using System.Collections.Generic;

namespace CBClient.NhienLieu
{
    public partial class PhieuNhapDialog : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Properites     
        public NL_PhieuNhap phieuNhap;
        List<NL_PhieuNhapCT> listphieuNhapCT = new List<NL_PhieuNhapCT>();
        Regex regexNumber = new Regex("^[0-9]+$");
        Regex regexNumberdigit = new Regex("^[0-2]*[.][0-9]$");
        Regex regexFloat = new Regex("^[-+]?[0-9]*[.][0-9]+$");
        Regex regexTime = new Regex("^(?:0?[0-9]|1[0-9]|2[0-3])[0-5][0-9]$");
        string[] arRaysCL = new string[] { "A", "B", "C", "D" };
        string[] arRaysDT = new string[] { "T", "T1" };
        bool addNew = true;
        bool addNewCT = true;
        public bool comPlate = false;
        private List<NL_54BASTM> list54BASTM = new List<NL_54BASTM>();
        #endregion
        public PhieuNhapDialog(NL_PhieuNhap _phieuNhap)
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty);
            InitializeComponent();
            KeyPerssEven();
            addNew = _phieuNhap.PhieuNhapID <= 0 ? true : false;
            phieuNhap = _phieuNhap;
            if (!addNew)
                listphieuNhapCT = _phieuNhap.NL_PhieuNhapCTs;

            var TramnlList = AppGlobal.DMTramnlList;
            if (AppGlobal.User.MaDVQL != "TCT")
            {
                if (AppGlobal.User.MaDVQL == "YV")
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL || x.MaDvql == "HN").ToList();
                else if (AppGlobal.User.MaDVQL == "DN")
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL || x.MaDvql == "SG").ToList();
                else
                    TramnlList = TramnlList.Where(x => x.MaDvql == AppGlobal.User.MaDVQL).ToList();
            }
            var tramNL = (from ct in TramnlList
                          select new
                          {
                              MaTram = ct.MaTram,
                              TenTram = ct.TenTram
                          }).ToList();
            tramNL = tramNL.OrderBy(x => x.MaTram).ToList();
            cboTramNL.DataSource = tramNL;
            cboTramNL.DisplayMember = "TenTram";
            cboTramNL.ValueMember = "MaTram";
            cboTramNL.SelectedIndex = -1;

            string[] arRays = new string[] { "Nhập từ nhà cung cấp", "Nhập kiểm kê", "Nhập khác" };
            cboLoaiPhieu.Items.AddRange(arRays);
            cboLoaiPhieu.SelectedIndex = -1;

            list54BASTM = HttpHelper.GetList<NL_54BASTM>(Configuration.UrlCBApi + "api/NhienLieus/NLGet54BASTM").ToList();
        }
        private void PhieuNhapDialog_Load(object sender, EventArgs e)
        {
            FnAutoComplete();
            bindControlToData();
            clearControlCT();
        }
        #region Validated_Event
        private void sdNgayNhap_Validated(object sender, EventArgs e)
        {
            try
            {
                sdNgayHoaDon.Value = sdNgayNhap.Value.Date;
            }
            catch
            {
                sdNgayHoaDon.Value = DateTime.Today;
            }
        }

        private void txtMaNCC_Validated(object sender, EventArgs e)
        {
            try
            {
                txtTenNCC.Text = AppGlobal.NLNhaccList.Where(x => x.ID == int.Parse(txtMaNCC.Text)).FirstOrDefault().TenNCC;
            }
            catch
            {
                txtTenNCC.Text = string.Empty;
            }
        }

        private void txtTenNCC_Validated(object sender, EventArgs e)
        {
            try
            {
                txtMaNCC.Text = AppGlobal.NLNhaccList.Where(x => x.TenNCC == txtTenNCC.Text).FirstOrDefault().ID.ToString();
            }
            catch
            {
                txtMaNCC.Text = string.Empty;
            }
        }

        private void txtMaHopDong_Validated(object sender, EventArgs e)
        {
            try
            {
                var hopDong = AppGlobal.HopdongList.Where(x => x.ID == int.Parse(txtMaHopDong.Text)).FirstOrDefault();
                if (hopDong != null)
                {
                    txtTenHopDong.Text = hopDong.HopDong;
                    txtTyLe.Text = hopDong.TyLe.ToString();
                }
            }
            catch
            {
                txtTenHopDong.Text = string.Empty;
                txtTyLe.Text = string.Empty;
            }
        }

        private void txtTenHopDong_Validated(object sender, EventArgs e)
        {
            try
            {
                var hopDong = AppGlobal.HopdongList.Where(x => x.HopDong == txtTenHopDong.Text).FirstOrDefault();
                if (hopDong != null)
                {
                    txtMaHopDong.Text = hopDong.ID.ToString();
                    txtTyLe.Text = hopDong.TyLe.ToString();
                }
            }
            catch
            {
                txtMaHopDong.Text = string.Empty;
                txtTyLe.Text = string.Empty;
            }
        }

        private void txtVCF_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNhietDo.Text))
                {
                    decimal nhietDo = string.IsNullOrWhiteSpace(txtNhietDo.Text) ? 0 : decimal.Parse(txtNhietDo.Text);
                    decimal vCF = string.IsNullOrWhiteSpace(txtVCF.Text) ? 0 : decimal.Parse(txtVCF.Text);
                    var objFisrt = list54BASTM.Where(x => x.NhietDo <= nhietDo).LastOrDefault();
                    var objLast = list54BASTM.Where(x => x.NhietDo >= nhietDo).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(txtTyTrong.Text))
                    {
                        decimal tyTrong = FormHelper.TyTrongName(decimal.Parse(txtTyTrong.Text));
                        if (nhietDo >= (objFisrt.NhietDo + objLast.NhietDo) / 2)                        
                            txtVCF.Text = FormHelper.ConvertString(FormHelper.VCFValue(tyTrong,objLast).ToString());                        
                        else
                            txtVCF.Text = FormHelper.ConvertString(FormHelper.VCFValue(tyTrong, objFisrt).ToString());
                    }
                }
            }
            catch
            {
                txtVCF.ResetText();
            }
        }      
        private void txtMaDM_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtMaDM.Text))
                {
                    var loaiDM = AppGlobal.DMLoaidmList.Where(x => x.ID == int.Parse(txtMaDM.Text)).FirstOrDefault();
                    if (loaiDM != null)
                    {
                        txtTenDM.Text = loaiDM.LoaiDauMo;
                        txtDVT.Text = loaiDM.DonViTinh;
                    }
                }
                else
                    throw new Exception();
            }
            catch
            {
                txtMaDM.Text = string.Empty;
                txtTenDM.Text = string.Empty;
                txtDVT.Text = string.Empty;
            }
        }

        private void txtTenDM_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtTenDM.Text))
                {
                    var loaiDM = AppGlobal.DMLoaidmList.Where(x => x.LoaiDauMo.Contains(txtTenDM.Text)).ToList();                  
                    txtMaDM.Text = loaiDM.FirstOrDefault().ID.ToString();
                    txtTenDM.Text = loaiDM.FirstOrDefault().LoaiDauMo;
                    txtDVT.Text = loaiDM.FirstOrDefault().DonViTinh;
                }
                else
                    throw new Exception();
            }
            catch
            {
                txtMaDM.Text = string.Empty;
                txtTenDM.Text = string.Empty;
                txtDVT.Text = string.Empty;
            }
        }

        private void txtSoLuong_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal VCF = 1M;
                if (!string.IsNullOrWhiteSpace(txtMaDM.Text))
                {
                    VCF = txtMaDM.Text == "0" ? decimal.Parse(txtVCF.Text) : VCF;
                }
                if (!string.IsNullOrWhiteSpace(txtSoLuong.Text))
                {
                    txtSoLuongVCF.Text = Math.Round((decimal.Parse(txtSoLuong.Text) * VCF), 4).ToString();
                    if (!string.IsNullOrWhiteSpace(txtDonGia.Text))
                    {
                        txtThanhTien.Text = Math.Round((decimal.Parse(txtSoLuongVCF.Text) * decimal.Parse(txtDonGia.Text)), 3).ToString();
                    }
                    else if (!string.IsNullOrWhiteSpace(txtThanhTien.Text))
                    {
                        txtDonGia.Text = Math.Round((decimal.Parse(txtThanhTien.Text) / decimal.Parse(txtSoLuongVCF.Text)), 3).ToString();
                    }
                }
            }
            catch
            {
               
            }
        }
        private void txtSoLuongVCF_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal VCF = 1M;
                if (!string.IsNullOrWhiteSpace(txtMaDM.Text))
                {
                    VCF = txtMaDM.Text == "0" ? decimal.Parse(txtVCF.Text) : VCF;
                }
                if (!string.IsNullOrWhiteSpace(txtSoLuongVCF.Text))
                {
                    if (decimal.Parse(txtSoLuongVCF.Text) <= 0)
                    {
                        txtSoLuongVCF.Text = Math.Round((decimal.Parse(txtSoLuong.Text) * VCF), 4).ToString();
                    }
                    txtSoLuong.Text = Math.Round((decimal.Parse(txtSoLuongVCF.Text) / VCF), 4).ToString();
                    if (!string.IsNullOrWhiteSpace(txtDonGia.Text))
                    {
                        txtThanhTien.Text = Math.Round((decimal.Parse(txtSoLuongVCF.Text) * decimal.Parse(txtDonGia.Text)), 3).ToString();
                    }
                    else if (!string.IsNullOrWhiteSpace(txtThanhTien.Text))
                    {
                        txtDonGia.Text = Math.Round((decimal.Parse(txtThanhTien.Text) / decimal.Parse(txtSoLuongVCF.Text)), 3).ToString();
                    }
                }
            }
            catch
            {

            }
        }
        private void txtDonGia_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal VCF = 1M;
                if (!string.IsNullOrWhiteSpace(txtMaDM.Text))
                {
                    VCF = txtMaDM.Text == "0" ? decimal.Parse(txtVCF.Text) : VCF;
                }
                if (!string.IsNullOrWhiteSpace(txtSoLuongVCF.Text) && !string.IsNullOrWhiteSpace(txtDonGia.Text))
                {
                    if (string.IsNullOrWhiteSpace(txtSoLuong.Text))
                    {
                        txtSoLuong.Text = Math.Round((decimal.Parse(txtSoLuongVCF.Text) / VCF), 4).ToString();
                    }
                    txtThanhTien.Text = Math.Round((decimal.Parse(txtSoLuongVCF.Text) * decimal.Parse(txtDonGia.Text)), 3).ToString();
                }
                else if (!string.IsNullOrWhiteSpace(txtSoLuong.Text) && !string.IsNullOrWhiteSpace(txtDonGia.Text))
                {
                    if (string.IsNullOrWhiteSpace(txtSoLuongVCF.Text))
                    {
                        txtSoLuongVCF.Text = Math.Round((VCF * decimal.Parse(txtVCF.Text)), 4).ToString();
                    }
                    txtThanhTien.Text = Math.Round((decimal.Parse(txtSoLuongVCF.Text) * decimal.Parse(txtDonGia.Text)), 3).ToString();
                }
            }
            catch
            {

            }
        }
        private void txtThanhTien_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal VCF = 1M;
                if (!string.IsNullOrWhiteSpace(txtMaDM.Text))
                {
                    VCF = txtMaDM.Text == "0" ? decimal.Parse(txtVCF.Text) : VCF;
                }
                if (!string.IsNullOrWhiteSpace(txtThanhTien.Text) && !string.IsNullOrWhiteSpace(txtSoLuongVCF.Text))
                {
                    if (string.IsNullOrWhiteSpace(txtSoLuong.Text))
                    {
                        txtSoLuong.Text = Math.Round((decimal.Parse(txtVCF.Text) / VCF), 4).ToString();
                    }
                    txtDonGia.Text = Math.Round((decimal.Parse(txtThanhTien.Text) / decimal.Parse(txtSoLuongVCF.Text)), 3).ToString();
                }
                else if (!string.IsNullOrWhiteSpace(txtThanhTien.Text) && !string.IsNullOrWhiteSpace(txtSoLuong.Text))
                {
                    if (string.IsNullOrWhiteSpace(txtSoLuongVCF.Text))
                    {
                        txtSoLuongVCF.Text = Math.Round((decimal.Parse(txtSoLuong.Text) * VCF), 4).ToString();
                    }
                    txtDonGia.Text = Math.Round((decimal.Parse(txtThanhTien.Text) / decimal.Parse(txtSoLuongVCF.Text)), 3).ToString();
                }
            }
            catch
            {

            }
        }        
        #endregion
        #region Validated_Function   
        private string checkValidate()
        {
            string errMessage = string.Empty;
            int intCount = 0;
            if (cboLoaiPhieu.SelectedIndex < 0)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Chưa chọn loại phiếu nhập\r\n";
            }
            if (cboTramNL.SelectedIndex<0)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Chưa chọn trạm nl\r\n";
            }          
            return errMessage;
        }
        private string checkValidateCT()
        {
            string errMessage = string.Empty;
            int intCount = 0;
            if (string.IsNullOrWhiteSpace(txtMaDM.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Chưa chọn mã dầu mỡ\r\n";
            }
            if (string.IsNullOrWhiteSpace(txtTenDM.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Chưa chọn tên dầu mỡ\r\n";
            }
            if (string.IsNullOrWhiteSpace(txtNhietDo.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Chưa nhập nhiệt độ\r\n";
            }
            else
            {
                if (decimal.Parse(txtNhietDo.Text) < -5 || decimal.Parse(txtNhietDo.Text) > 50)
                {
                    intCount += 1;
                    errMessage += intCount.ToString() + ".Nhập nhiệt độ chưa đúng\r\n";
                }
            }
            if (string.IsNullOrWhiteSpace(txtTyTrong.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Chưa nhập tỷ trọng\r\n";
            }
            else
            {
                if (decimal.Parse(txtTyTrong.Text) <= 0 || decimal.Parse(txtTyTrong.Text) > 2)
                {
                    intCount += 1;
                    errMessage += intCount.ToString() + ".Tỷ trọng phải lớn hơn 0 và nhỏ hơn hoặc bằng 2\r\n";
                }
            }
            if (string.IsNullOrWhiteSpace(txtVCF.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Chưa nhập hệ số VCF\r\n";
            }
            else
            {
                if (decimal.Parse(txtVCF.Text) <= 0 || decimal.Parse(txtVCF.Text) > 2)
                {
                    intCount += 1;
                    errMessage += intCount.ToString() + ".Hệ số VCF phải lớn hơn 0 và nhỏ hơn hoặc bằng 2\r\n";
                }
            }
            if (decimal.Parse(txtSoLuong.Text) <= 0)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Số lượng phải lớn hơn 0\r\n";
                txtSoLuong.Focus();
                txtSoLuong.SelectAll();
            }
            if (decimal.Parse(txtSoLuongVCF.Text) <= 0)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Số lượng 15oC phải lớn hơn 0\r\n";
                txtSoLuongVCF.Focus();
                txtSoLuongVCF.SelectAll();
            }
            return errMessage;
        }       
        #endregion
        #region Function
        private void KeyPerssEven()
        {
            FormHelper.AddEnterKeyPressAsTabEventHandler(this);            
            FormHelper.AddKeyPressEventHandlerForNumber(txtMaPN);            
            FormHelper.AddKeyPressEventHandlerForNumber(txtMaNCC);
            FormHelper.AddKeyPressEventHandlerForNumber(txtMaHopDong);           
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTyLe);            
            FormHelper.AddKeyPressEventHandlerForDecimal(txtVAT);

            FormHelper.AddKeyPressEventHandlerForDecimal(txtNhietDo);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTyTrong);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtVCF);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtSoLuong);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtSoLuongVCF);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDonGia);            
            FormHelper.AddKeyPressEventHandlerForDecimal(txtThanhTien);

        }
        private void FnAutoComplete()
        {
            txtMaNCC.AutoCompleteCustomSource = AppGlobal.MaNhaCCAutoComplate;
            txtMaNCC.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMaNCC.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTenNCC.AutoCompleteCustomSource = AppGlobal.TenNhaCCAutoComplate;
            txtTenNCC.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTenNCC.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtMaHopDong.AutoCompleteCustomSource = AppGlobal.MaHDAutoComplate;
            txtMaHopDong.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMaHopDong.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTenHopDong.AutoCompleteCustomSource = AppGlobal.TenHDAutoComplate;
            txtTenHopDong.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTenHopDong.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtMaDM.AutoCompleteCustomSource = AppGlobal.MaDMAutoComplate;
            txtMaDM.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMaDM.AutoCompleteMode = AutoCompleteMode.SuggestAppend;           

            txtTenDM.AutoCompleteCustomSource = AppGlobal.TenDMAutoComplate;
            txtTenDM.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTenDM.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

           
        }
        private void bindControlToData()
        {
            txtMaPN.Text = phieuNhap.PhieuNhapID.ToString();
            cboLoaiPhieu.Text = phieuNhap.LoaiPhieu;
          
            txtNguoiGiao.Text = phieuNhap.NguoiGiao;            
            txtNhietDo.Text = phieuNhap.TenTramNL;          
            txtMaNCC.Text = phieuNhap.MaNCC.ToString();
            txtTenNCC.Text = phieuNhap.TenNCC;
            txtMaHopDong.Text = phieuNhap.MaHopDong.ToString();
            txtTenHopDong.Text = phieuNhap.TenHopDong;
            txtTyLe.Text = FormHelper.ConvertString(phieuNhap.TyLe.ToString());
            txtSoHoaDon.Text = phieuNhap.SoHoaDon;
            txtVAT.Text = FormHelper.ConvertString(phieuNhap.VAT.ToString());
            txtLyDo.Text = phieuNhap.LyDo;
            if(addNew)
            {
                sdNgayNhap.Value = DateTime.Today;
                txtGioNhap.Text = DateTime.Now.ToString("HHmm");
                sdNgayHoaDon.Value = sdNgayNhap.Value;               
            }    
            else
            {
                cboTramNL.SelectedValue = phieuNhap.MaTramNL;
                cboTramNL.Text = phieuNhap.TenTramNL;
                sdNgayNhap.Value = phieuNhap.NgayNhap.Date;
                txtGioNhap.Text = phieuNhap.NgayNhap.ToString("HHmm");
                sdNgayHoaDon.Value = phieuNhap.NgayHoaDon.Date;               
            }
           
            if (phieuNhap.NL_PhieuNhapCTs != null)
            {
                listphieuNhapCT = phieuNhap.NL_PhieuNhapCTs;
                bsPhieuNhapCT.DataSource = listphieuNhapCT;
                lblTongTien.Text = "Tổng tiền phiếu nhập: " + listphieuNhapCT.Sum(x => x.ThanhTien).ToString("N0") + " VNĐ.";
            }
        }
        private void bindDataToControl()
        {
            try
            {
                phieuNhap.PhieuNhapID = string.IsNullOrWhiteSpace(txtMaPN.Text) ? 0 : long.Parse(txtMaPN.Text);
                phieuNhap.LoaiPhieu = cboLoaiPhieu.Text;
                phieuNhap.NguoiGiao = txtNguoiGiao.Text;
                phieuNhap.MaTramNL = cboTramNL.SelectedValue.ToString();
                phieuNhap.TenTramNL = cboTramNL.Text;
                string gioNhap = txtGioNhap.Text.Length == 3 ? "0" + txtGioNhap.Text.Substring(0, 1) + ":" + txtGioNhap.Text.Substring(1) : txtGioNhap.Text.Substring(0, 2) + ":" + txtGioNhap.Text.Substring(2);              
                phieuNhap.NgayNhap = DateTime.Parse(sdNgayNhap.Value.ToShortDateString() + " " + gioNhap);
                phieuNhap.MaNCC= string.IsNullOrWhiteSpace(txtMaNCC.Text) ? 0 : int.Parse(txtMaNCC.Text);
                phieuNhap.TenNCC = txtTenNCC.Text;
                phieuNhap.MaHopDong = string.IsNullOrWhiteSpace(txtMaHopDong.Text) ? 0 : int.Parse(txtMaHopDong.Text);
                phieuNhap.TenHopDong = txtTenHopDong.Text;
                phieuNhap.TyLe= string.IsNullOrWhiteSpace(txtTyLe.Text) ? 0 : decimal.Parse(txtTyLe.Text);
                phieuNhap.SoHoaDon = txtSoHoaDon.Text;
                phieuNhap.NgayHoaDon = sdNgayHoaDon.Value;               
                phieuNhap.VAT= string.IsNullOrWhiteSpace(txtVAT.Text) ? 0 : decimal.Parse(txtVAT.Text);
                phieuNhap.LyDo = txtLyDo.Text;
                if (addNew)
                {
                    phieuNhap.KhoaSo = false;
                    phieuNhap.CreatedDate = DateTime.Now;
                    phieuNhap.CreatedBy = AppGlobal.User.Username;
                    phieuNhap.CreatedName = AppGlobal.User.FullName;
                }
                phieuNhap.ModifyDate = DateTime.Now;
                phieuNhap.ModifyBy = AppGlobal.User.Username;
                phieuNhap.ModifyName = AppGlobal.User.FullName;
                phieuNhap.NL_PhieuNhapCTs = listphieuNhapCT;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nạp phiếu nhập: " + ex.Message);
            }
        }       
        private void clearControlCT()
        {
            txtMaDM.Text = string.Empty;
            txtTenDM.Text = string.Empty;
            txtDVT.Text = string.Empty;
            txtNhietDo.Text = "15";
            txtTyTrong.Text = "1";
            txtVCF.Text = "1";
            txtSoLuong.Text = "0";
            txtSoLuongVCF.Text = "0";
            txtDonGia.Text = "0";           
            txtThanhTien.Text = "0";
        }
        private void bindControlToDataCT(NL_PhieuNhapCT row)
        {
            if (row == null) return;
            txtMaDM.Text = row.MaDauMo.ToString();
            txtTenDM.Text = row.TenDauMo;
            txtDVT.Text = row.DonViTinh;
            txtNhietDo.Text = FormHelper.ConvertString(row.NhietDo.ToString());
            txtTyTrong.Text = FormHelper.ConvertString(row.TyTrong.ToString());
            txtVCF.Text = FormHelper.ConvertString(row.VCF.ToString());
            txtSoLuong.Text = FormHelper.ConvertString(row.SoLuong.ToString());
            txtSoLuongVCF.Text = FormHelper.ConvertString(row.SoLuongVCF.ToString());
            txtDonGia.Text = FormHelper.ConvertString(row.DonGia.ToString());           
            txtThanhTien.Text = FormHelper.ConvertString(row.ThanhTien.ToString());
        }
        private void bindDataToControlCT(ref NL_PhieuNhapCT row)
        {
            row.PhieuNhapID = addNew ? 0 : phieuNhap.PhieuNhapID;
            row.MaDauMo = string.IsNullOrWhiteSpace(txtMaDM.Text) ? (short)0 :short.Parse(txtMaDM.Text);
            row.TenDauMo = txtTenDM.Text;
            row.DonViTinh = txtDVT.Text;
            row.NhietDo = string.IsNullOrWhiteSpace(txtNhietDo.Text) ? 15 : decimal.Parse(txtNhietDo.Text, FormHelper.EnCultureInfo);
            row.TyTrong = string.IsNullOrWhiteSpace(txtTyTrong.Text) ? 1 : decimal.Parse(txtTyTrong.Text, FormHelper.EnCultureInfo);
            row.VCF = string.IsNullOrWhiteSpace(txtVCF.Text) ? 1 : decimal.Parse(txtVCF.Text, FormHelper.EnCultureInfo);
            row.SoLuong= string.IsNullOrWhiteSpace(txtSoLuong.Text) ? 0 : decimal.Parse(txtSoLuong.Text, FormHelper.EnCultureInfo);
            row.SoLuongVCF = string.IsNullOrWhiteSpace(txtSoLuongVCF.Text) ? 0 : decimal.Parse(txtSoLuongVCF.Text, FormHelper.EnCultureInfo);
            row.DonGia = string.IsNullOrWhiteSpace(txtDonGia.Text) ? 0 : decimal.Parse(txtDonGia.Text, FormHelper.EnCultureInfo);
            row.ThanhTien = string.IsNullOrWhiteSpace(txtThanhTien.Text) ? 0 : decimal.Parse(txtThanhTien.Text, FormHelper.EnCultureInfo);           
            row.ConLai = row.SoLuongVCF;           
            decimal tyLe = string.IsNullOrWhiteSpace(txtTyLe.Text) ? 0 : decimal.Parse(txtTyLe.Text);
            decimal vat = string.IsNullOrWhiteSpace(txtVAT.Text) ? 0 : decimal.Parse(txtVAT.Text);
            row.TyLe = (100 - tyLe) / 100;
            row.Vat = (100 + vat) / 100;
        }
        #endregion
        #region Keys_Press
        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            if (dgPhieuNhapCT.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    NL_PhieuNhapCT phieuNhapCT = bsPhieuNhapCT.Current as NL_PhieuNhapCT;
                    bindControlToDataCT(phieuNhapCT);
                    addNewCT = false;
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    DialogHelper.Error(ex.Message);
                }

            }
        }
        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (dgPhieuNhapCT.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    NL_PhieuNhapCT phieuNhapCT = bsPhieuNhapCT.Current as NL_PhieuNhapCT;
                    bsPhieuNhapCT.Remove(phieuNhapCT);
                    phieuNhap.NL_PhieuNhapCTs.Remove(phieuNhapCT);                    
                    //bsPhieuNhapCT.EndEdit();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    DialogHelper.Error(ex.Message);
                }
            }
        }
        private void btnLuuCT_Click(object sender, EventArgs e)
        {
            string errMessageCT = checkValidateCT();
            if (!string.IsNullOrWhiteSpace(errMessageCT))
                Library.DialogHelper.Error(errMessageCT);
            else
            {
                NL_PhieuNhapCT pnct = new NL_PhieuNhapCT();
                if (addNewCT)
                {
                    bindDataToControlCT(ref pnct);
                    var pnctDB = listphieuNhapCT.Where(x => x.MaDauMo == pnct.MaDauMo).FirstOrDefault();
                    if(pnctDB!=null)
                    {
                        Library.DialogHelper.Error("Đã có dầu mỡ: " + pnct.TenDauMo + " trong phiếu nhập này.");
                        clearControlCT();
                        addNewCT = true;
                        return;
                    }    
                    bsPhieuNhapCT.DataSource = null;
                    listphieuNhapCT.Add(pnct);
                    bsPhieuNhapCT.DataSource = listphieuNhapCT;
                    bsPhieuNhapCT.MoveLast();
                }
                else if (dgPhieuNhapCT.CurrentRow != null)
                {
                    pnct = bsPhieuNhapCT.Current as NL_PhieuNhapCT;
                    listphieuNhapCT.Remove(pnct);
                    bindDataToControlCT(ref pnct);
                    listphieuNhapCT.Add(pnct);
                    bsPhieuNhapCT.EndEdit();
                    
                }
                dgPhieuNhapCT.Refresh();
                lblTongTien.Text = "Tổng tiền phiếu nhập: " + listphieuNhapCT.Sum(x=>x.ThanhTien).ToString("N0") +" VNĐ.";
                clearControlCT();
                addNewCT = true;
            }
        }
        private void btnHuyCT_Click(object sender, EventArgs e)
        {
            clearControlCT();
            addNewCT = true;
        }       
        private async void btnLuuPN_Click(object sender, EventArgs e)
        {   string errMessage = checkValidate();
            if (!string.IsNullOrWhiteSpace(errMessage))
                Library.DialogHelper.Error(errMessage);
            else
            {   
                //Lưu dữ liệu về db
                try
                {
                    bindDataToControl();
                    if(phieuNhap.NL_PhieuNhapCTs.Count<=0)
                        throw new Exception("Không có dầu mỡ nào được nhập!");
                    if (!addNew)//Sửa phiếu nhập
                    { 
                        try
                        {
                            var objpn = await HttpHelper.Put<NL_PhieuNhap>(Configuration.UrlCBApi + "api/NhienLieus/NLPutPhieuNhap", phieuNhap);
                            if (objpn == null) throw new Exception(phieuNhap.PhieuNhapID + "- Ngày nhập: " + phieuNhap.NgayNhap);
                            phieuNhap = objpn;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Lỗi sửa phiếu nhập: " + ex.Message);
                        }
                    }
                    else //Thêm phiếu nhập
                    {                        
                        try
                        {                            
                            var objpn = await HttpHelper.Post<NL_PhieuNhap>(Configuration.UrlCBApi + "api/NhienLieus/NLPostPhieuNhap", phieuNhap);
                            if (objpn == null) throw new Exception(phieuNhap.PhieuNhapID + "- Ngày nhập: " + phieuNhap.NgayNhap);
                            phieuNhap = objpn;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Lỗi thêm phiếu nhập: " + ex.Message);
                        }
                    }                    
                    //DialogHelper.Inform(strResult);                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    comPlate = true;
                }
                catch (Exception ex)
                {
                    comPlate = false;
                    Library.DialogHelper.Error(ex.Message);                    
                }
            }
        }
        private void btnHuyPN_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion       
    }
}
