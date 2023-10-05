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
    public partial class PhieuXuatDialog : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Properites     
        public NL_PhieuXuat phieuXuat;
        List<NL_PhieuXuatCT> listphieuXuatCT = new List<NL_PhieuXuatCT>();      
        Regex regexNumber = new Regex("^[0-9]+$");
        Regex regexNumberdigit = new Regex("^[0-2]*[.][0-9]$");
        Regex regexFloat = new Regex("^[-+]?[0-9]*[.][0-9]+$");
        Regex regexTime = new Regex("^(?:0?[0-9]|1[0-9]|2[0-3])[0-5][0-9]$");
        string[] arRaysCL = new string[] { "A", "B", "C", "D" };
        string[] arRaysDT = new string[] { "T", "T1" };
        bool addNew = true;
        bool addNewCT = true;
        public bool comPlate = false;
        long bangGiaID = 0;
        decimal soLuong = 0;
        private List<NL_54BASTM> list54BASTM = new List<NL_54BASTM>();
        #endregion
        public PhieuXuatDialog(NL_PhieuXuat _phieuXuat)
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty);
            InitializeComponent();
            KeyPerssEven();
            addNew = _phieuXuat.PhieuXuatID <= 0 ? true : false;
            phieuXuat = _phieuXuat;
            if(!addNew)
                listphieuXuatCT = _phieuXuat.NL_PhieuXuatCTs;


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

            string[] arRays = new string[] { "Xuất cho đầu máy", "Xuất kiểm kê", "Xuất khác" };
            cboLoaiPhieu.Items.AddRange(arRays);
            cboLoaiPhieu.SelectedIndex = -1;
            list54BASTM = HttpHelper.GetList<NL_54BASTM>(Configuration.UrlCBApi + "api/NhienLieus/NLGet54BASTM").ToList();
        }
        private void PhieuXuatDialog_Load(object sender, EventArgs e)
        {
            FnAutoComplete();                      
            bindControlToData();
            clearControlCT();
        }
        #region Validated_Event       
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
                            txtVCF.Text = FormHelper.ConvertString(FormHelper.VCFValue(tyTrong, objLast).ToString());
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
        private void txtDauMay_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtDauMay.Text))
                {
                    var dauMay = AppGlobal.DMDaumayList.Where(x => x.DauMaySo.Contains(txtDauMay.Text.ToUpper())).ToList();
                    if (dauMay.Count != 1)
                        throw new Exception();
                    txtDauMay.Text = dauMay.FirstOrDefault().DauMaySo;
                    txtLoaiMay.Text = dauMay.FirstOrDefault().PhanLoai;
                    if(cboLoaiPhieu.SelectedIndex==0)
                        fnCoBao();
                }
            }
            catch
            {
                if(cboLoaiPhieu.SelectedIndex==0)
                {
                    Library.DialogHelper.Error("Nhập số hiệu đầu máy chưa đúng");
                    txtDauMay.Focus();
                    txtDauMay.SelectAll();
                } 
                else
                    txtLoaiMay.Text = string.Empty;
            }
        }
        private async void fnCoBao()
        {
            string gioXuat = txtGioXuat.Text.Length == 3 ? "0" + txtGioXuat.Text.Substring(0, 1) + ":" + txtGioXuat.Text.Substring(1) : txtGioXuat.Text.Substring(0, 2) + ":" + txtGioXuat.Text.Substring(2);
            DateTime ngayCB = DateTime.Parse(sdNgayXuat.Value.ToShortDateString() + " " + gioXuat);
            string data = "?ngayCB=" + ngayCB;
            data += "&dauMay=" + txtDauMay.Text;  
            var coBaoHT = await HttpHelper.Get<CoBao>(Configuration.UrlCBApi + "api/NhienLieus/GetCoBaoOBJ" + data);
            if (coBaoHT != null)
            {
                txtSoChungTu.Text = coBaoHT.SoCB;
                txtNguoiNhan.Text = coBaoHT.TaiXe1ID + "-" + coBaoHT.TaiXe1Name;
                txtLyDo.Text = "Xuất cho tài kế: " + coBaoHT.SoCB;
            }
            else
            {
                string ngayBD = sdNgayXuat.Value.AddDays(-2).ToString("yyyy/MM/dd");
                string ngayKT = sdNgayXuat.Value.AddDays(1).ToString("yyyy/MM/dd");
                var access_token = string.Format("Bearer {0}{1}", MainForm.Instance.Data.userClientId, MainForm.Instance.Data.access_token);
                var res = await AuthenticationService.GetListCoBaoDienTuByDate(ngayBD, ngayKT, string.Empty, txtDauMay.Text, 0, MainForm.Instance.Data.userName, access_token);
                if (res.IsOK > 0)
                {
                    var coBao = res.Data.Where(x => x.GioNhanMay <= ngayCB && x.GioVaoKho >= ngayCB).FirstOrDefault();
                    if (coBao != null)
                    {
                        txtSoChungTu.Text = coBao.SoCoBao;
                        txtNguoiNhan.Text = coBao.TaiXe1_MaSo + "-" + coBao.TaiXe1_Ten;
                        txtLyDo.Text = "Xuất cho tài kế: " + coBao.SoCoBao;
                    }
                    else
                    {
                        txtSoChungTu.Text = string.Empty;
                        txtNguoiNhan.Text = string.Empty;
                        txtLyDo.Text = string.Empty;
                    }
                }
                else
                {
                    data = "?ngayCB=" + ngayCB;
                    data += "&dauMay=" + txtDauMay.Text;
                    var coBaoGA = await HttpHelper.Get<CoBaoGA>(Configuration.UrlCBApi + "api/NhienLieus/GetCoBaoGAOBJ" + data);
                    if (coBaoGA != null)
                    {
                        txtSoChungTu.Text = coBaoGA.SoCB;
                        txtNguoiNhan.Text = coBaoGA.TaiXe1ID + "-" + coBaoGA.TaiXe1Name;
                        txtLyDo.Text = "Xuất cho tài kế: " + coBaoGA.SoCB;
                    }
                    else
                    {
                        txtSoChungTu.Text = string.Empty;
                        txtNguoiNhan.Text = string.Empty;
                        txtLyDo.Text = string.Empty;
                    }
                }
            }
        }
        private void txtMaDM_Validated(object sender, EventArgs e)
        {
            if (addNewCT) soLuong = 0;
            try
            {
                if (!string.IsNullOrWhiteSpace(txtMaDM.Text))
                {
                    var loaiDM = AppGlobal.DMLoaidmList.Where(x => x.ID == int.Parse(txtMaDM.Text)).FirstOrDefault();
                    if (loaiDM != null)
                    {
                        string gioXuat = txtGioXuat.Text.Length == 3 ? "0" + txtGioXuat.Text.Substring(0, 1) + ":" + txtGioXuat.Text.Substring(1) : txtGioXuat.Text.Substring(0, 2) + ":" + txtGioXuat.Text.Substring(2);
                        DateTime ngayHL = DateTime.Parse(sdNgayXuat.Value.ToShortDateString() + " " + gioXuat);
                        txtTenDM.Text = loaiDM.LoaiDauMo;
                        txtDVT.Text = loaiDM.DonViTinh;
                        string data = "?maTram=" + cboTramNL.SelectedValue.ToString();
                        data += "&maDauMo=" + txtMaDM.Text;
                        data += "&ngayHL=" + ngayHL;
                        soLuong += HttpHelper.GetDecimal<decimal>(Configuration.UrlCBApi + "api/NhienLieus/NLGetConLaiOBJ" + data).Result;
                        if (addNewCT && soLuong <= 0)
                        {
                            txtTenDM.Text = string.Empty;
                            txtDVT.Text = string.Empty;
                            return;
                        }
                        txtConLai.Text = soLuong.ToString();
                        var donGia = HttpHelper.Get<NL_BangGia>(Configuration.UrlCBApi + "api/NhienLieus/NLGetBangGiaOBJ" + data).Result;
                        if (donGia != null)
                        {
                            bangGiaID = donGia.PhieuNhapID;
                            txtTyTrong.Text = donGia.TyTrong.ToString();
                            txtDonGia.Text = donGia.DonGia.ToString();
                        }
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
            if (addNewCT) soLuong = 0;
            try
            {
                if (!string.IsNullOrWhiteSpace(txtTenDM.Text))
                {
                    var loaiDM = AppGlobal.DMLoaidmList.Where(x => x.LoaiDauMo.ToUpper().Contains(txtTenDM.Text.ToUpper()));                  
                    txtMaDM.Text = loaiDM.FirstOrDefault().ID.ToString();
                    txtTenDM.Text = loaiDM.FirstOrDefault().LoaiDauMo;
                    txtDVT.Text = loaiDM.FirstOrDefault().DonViTinh;
                    string gioXuat = txtGioXuat.Text.Length == 3 ? "0" + txtGioXuat.Text.Substring(0, 1) + ":" + txtGioXuat.Text.Substring(1) : txtGioXuat.Text.Substring(0, 2) + ":" + txtGioXuat.Text.Substring(2);
                    DateTime ngayHL = DateTime.Parse(sdNgayXuat.Value.ToShortDateString() + " " + gioXuat);
                    string data = "?maTram=" + cboTramNL.SelectedValue.ToString();
                    data += "&maDauMo=" + txtMaDM.Text;
                    data += "&ngayHL=" + ngayHL;
                    soLuong += HttpHelper.GetDecimal<decimal>(Configuration.UrlCBApi + "api/NhienLieus/NLGetConLaiOBJ" + data).Result;
                    if (addNewCT && soLuong <= 0)
                    {
                        txtMaDM.Text = string.Empty;
                        txtDVT.Text = string.Empty;
                        return;
                    }
                    txtConLai.Text = soLuong.ToString();
                    var donGia = HttpHelper.Get<NL_BangGia>(Configuration.UrlCBApi + "api/NhienLieus/NLGetBangGiaOBJ" + data).Result;
                    if (donGia != null)
                    {
                        bangGiaID = donGia.PhieuNhapID;
                        txtTyTrong.Text = donGia.TyTrong.ToString();
                        txtDonGia.Text = donGia.DonGia.ToString();
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
        private void txtSoLuong_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal VCF = 1M;
                if(!string.IsNullOrWhiteSpace(txtMaDM.Text))
                {
                    VCF = txtMaDM.Text == "0" ? decimal.Parse(txtVCF.Text) : VCF;
                }
                if (!string.IsNullOrWhiteSpace(txtSoLuong.Text))
                {                    
                    txtSoLuongVCF.Text = Math.Round((decimal.Parse(txtSoLuong.Text) * VCF), 4).ToString();
                    if (decimal.Parse(txtSoLuongVCF.Text) > soLuong)
                    {
                        DialogHelper.Error("Số lượng xuất lớn hơn còn lại.");
                        txtSoLuong.ResetText();
                        txtSoLuongVCF.ResetText();
                        txtSoLuongVCF.Focus();
                        return;
                    }
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
                    if(decimal.Parse(txtSoLuongVCF.Text)<=0)
                    {
                        txtSoLuongVCF.Text = Math.Round((decimal.Parse(txtSoLuong.Text) * VCF), 4).ToString();
                    }    
                    txtSoLuong.Text = Math.Round((decimal.Parse(txtSoLuongVCF.Text) / VCF), 4).ToString();
                    if (decimal.Parse(txtSoLuongVCF.Text) > soLuong)
                    {
                        DialogHelper.Error("Số lượng xuất lớn hơn còn lại.");
                        txtSoLuong.ResetText();
                        txtSoLuongVCF.ResetText();
                        txtSoLuongVCF.Focus();
                        return;
                    }
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
                        txtSoLuong.Text = Math.Round((decimal.Parse(txtVCF.Text)/VCF), 4).ToString();
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
            if (cboLoaiPhieu.SelectedIndex<0)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Chưa chọn loại phiếu xuất\r\n";
            }
            if (cboTramNL.SelectedIndex<0)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Chưa chọn trạm nl\r\n";
            }           
            if(cboLoaiPhieu.SelectedIndex==0)
            {
                if (string.IsNullOrWhiteSpace(txtDauMay.Text))
                {
                    intCount += 1;
                    errMessage += intCount.ToString() + ".Chưa chọn sh đầu máy\r\n";
                }
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
            FormHelper.AddKeyPressEventHandlerForNumber(txtMaPX);

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
            txtDauMay.AutoCompleteCustomSource = AppGlobal.MaDauMayAutoComplate;
            txtDauMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtDauMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;            

            txtLoaiMay.AutoCompleteCustomSource = AppGlobal.MaLoaiMayAutoComplate;
            txtLoaiMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtLoaiMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtMaDM.AutoCompleteCustomSource = AppGlobal.MaDMAutoComplate;
            txtMaDM.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMaDM.AutoCompleteMode = AutoCompleteMode.SuggestAppend;           

            txtTenDM.AutoCompleteCustomSource = AppGlobal.TenDMAutoComplate;
            txtTenDM.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTenDM.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

           
        }
        private void bindControlToData()
        {
            txtMaPX.Text = phieuXuat.PhieuXuatID.ToString();
            cboLoaiPhieu.Text = phieuXuat.LoaiPhieu;
           
            txtSoChungTu.Text = phieuXuat.SoChungTu;
            txtNguoiNhan.Text = phieuXuat.NguoiNhan;                       
            txtDauMay.Text = phieuXuat.DauMayID;
            txtLoaiMay.Text = phieuXuat.LoaiMayID;           
            txtLyDo.Text = phieuXuat.LyDo;
            if(addNew)
            {
                sdNgayXuat.Value = DateTime.Today;
                txtGioXuat.Text = DateTime.Now.ToString("HHmm");                
            }    
            else
            {
                cboTramNL.SelectedValue = phieuXuat.MaTramNL;
                cboTramNL.Text = phieuXuat.TenTramNL;
                sdNgayXuat.Value = phieuXuat.NgayXuat.Date;
                txtGioXuat.Text = phieuXuat.NgayXuat.ToString("HHmm");              
            }
           
            if (phieuXuat.NL_PhieuXuatCTs != null)
            {
                listphieuXuatCT = phieuXuat.NL_PhieuXuatCTs;
                bsPhieuXuatCT.DataSource = listphieuXuatCT;
                lblTongTien.Text = "Tổng tiền phiếu xuất: " + listphieuXuatCT.Sum(x => x.ThanhTien).ToString("N0") + " NVĐ.";
            }
        }
        private void bindDataToControl()
        {
            try
            {
                phieuXuat.PhieuXuatID = string.IsNullOrWhiteSpace(txtMaPX.Text) ? 0 : long.Parse(txtMaPX.Text);
                phieuXuat.LoaiPhieu = cboLoaiPhieu.Text;
                phieuXuat.SoChungTu = txtSoChungTu.Text;
                phieuXuat.NguoiNhan = txtNguoiNhan.Text;
                phieuXuat.MaTramNL = cboTramNL.SelectedValue.ToString();
                phieuXuat.TenTramNL = cboTramNL.Text;
                string gioXuat = txtGioXuat.Text.Length == 3 ? "0" + txtGioXuat.Text.Substring(0, 1) + ":" + txtGioXuat.Text.Substring(1) : txtGioXuat.Text.Substring(0, 2) + ":" + txtGioXuat.Text.Substring(2);              
                phieuXuat.NgayXuat = DateTime.Parse(sdNgayXuat.Value.ToShortDateString() + " " + gioXuat);
                phieuXuat.DauMayID= txtDauMay.Text;
                phieuXuat.LoaiMayID = txtLoaiMay.Text;              
                phieuXuat.LyDo = txtLyDo.Text;
                if (addNew)
                {
                    phieuXuat.KhoaSo = false;
                    phieuXuat.CreatedDate = DateTime.Now;
                    phieuXuat.CreatedBy = AppGlobal.User.Username;
                    phieuXuat.CreatedName = AppGlobal.User.FullName;
                }
                phieuXuat.ModifyDate = DateTime.Now;
                phieuXuat.ModifyBy = AppGlobal.User.Username;
                phieuXuat.ModifyName = AppGlobal.User.FullName;
                phieuXuat.NL_PhieuXuatCTs = listphieuXuatCT;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nạp phiếu xuất: " + ex.Message);
            }
        }       
        private void clearControlCT()
        {
            txtMaDM.Text = string.Empty;
            txtTenDM.Text = string.Empty;
            txtDVT.Text = string.Empty;
            txtConLai.Text = "0";
            txtNhietDo.Text = "15";
            txtTyTrong.Text = "1";
            txtVCF.Text = "1";
            txtSoLuong.Text = "0";
            txtSoLuongVCF.Text = "0";
            txtDonGia.Text = "0";
            txtThanhTien.Text = "0";           
        }
        private void bindControlToDataCT(NL_PhieuXuatCT row)
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
        private void bindDataToControlCT(ref NL_PhieuXuatCT row)
        {
            row.PhieuXuatID = addNew ? 0 : phieuXuat.PhieuXuatID;
            row.MaDauMo = string.IsNullOrWhiteSpace(txtMaDM.Text) ? (short)0 :short.Parse(txtMaDM.Text);
            row.TenDauMo = txtTenDM.Text;
            row.DonViTinh = txtDVT.Text;
            row.NhietDo = string.IsNullOrWhiteSpace(txtNhietDo.Text) ? 15 : decimal.Parse(txtNhietDo.Text, FormHelper.EnCultureInfo);
            row.TyTrong = string.IsNullOrWhiteSpace(txtTyTrong.Text) ? 1 : decimal.Parse(txtTyTrong.Text, FormHelper.EnCultureInfo);
            row.VCF = string.IsNullOrWhiteSpace(txtVCF.Text) ? 1 : decimal.Parse(txtVCF.Text, FormHelper.EnCultureInfo);
            row.SoLuong= string.IsNullOrWhiteSpace(txtSoLuong.Text) ? 0 : decimal.Parse(txtSoLuong.Text, FormHelper.EnCultureInfo);
            row.SoLuongVCF = string.IsNullOrWhiteSpace(txtSoLuongVCF.Text) ? 0 : decimal.Parse(txtSoLuongVCF.Text, FormHelper.EnCultureInfo);
            row.PhieuNhapID = 0;
            row.DonGia = string.IsNullOrWhiteSpace(txtDonGia.Text) ? 0 : decimal.Parse(txtDonGia.Text, FormHelper.EnCultureInfo);
            row.BangGiaID = bangGiaID;
            row.ThanhTien = string.IsNullOrWhiteSpace(txtThanhTien.Text) ? 0 : decimal.Parse(txtThanhTien.Text, FormHelper.EnCultureInfo);           
        }
        #endregion
        #region Keys_Press
        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            if (dgPhieuXuatCT.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    NL_PhieuXuatCT phieuXuatCT = bsPhieuXuatCT.Current as NL_PhieuXuatCT;
                    bindControlToDataCT(phieuXuatCT);
                    bangGiaID = phieuXuatCT.BangGiaID;
                    soLuong += phieuXuatCT.SoLuong;
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
            if (dgPhieuXuatCT.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    NL_PhieuXuatCT phieuXuatCT = bsPhieuXuatCT.Current as NL_PhieuXuatCT;
                    bsPhieuXuatCT.Remove(phieuXuatCT);
                    phieuXuat.NL_PhieuXuatCTs.Remove(phieuXuatCT);                    
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
                NL_PhieuXuatCT pxct = new NL_PhieuXuatCT();
                if (addNewCT)
                {
                    bindDataToControlCT(ref pxct);
                    bsPhieuXuatCT.DataSource = null;
                    listphieuXuatCT.Add(pxct);
                    bsPhieuXuatCT.DataSource = listphieuXuatCT;
                    bsPhieuXuatCT.MoveLast();
                }
                else if (dgPhieuXuatCT.CurrentRow != null)
                {
                    pxct = bsPhieuXuatCT.Current as NL_PhieuXuatCT;
                    long phieuNhapID = pxct.PhieuNhapID;
                    listphieuXuatCT.Remove(pxct);
                    bindDataToControlCT(ref pxct);
                    pxct.PhieuNhapID = phieuNhapID;
                    listphieuXuatCT.Add(pxct);
                    bsPhieuXuatCT.EndEdit();
                    
                }
                dgPhieuXuatCT.Refresh();
                lblTongTien.Text = "Tổng tiền phiếu xuất: " + listphieuXuatCT.Sum(x => x.ThanhTien).ToString("N0") + " NVĐ.";
                clearControlCT();
                addNewCT = true;
            }
        }
        private void btnHuyCT_Click(object sender, EventArgs e)
        {
            clearControlCT();
            addNewCT = true;
        }       
        private async void btnLuuPX_Click(object sender, EventArgs e)
        {   string errMessage = checkValidate();
            if (!string.IsNullOrWhiteSpace(errMessage))
                Library.DialogHelper.Error(errMessage);
            else
            {   
                //Lưu dữ liệu về db
                try
                {
                    bindDataToControl();
                    if (phieuXuat.NL_PhieuXuatCTs.Count <= 0)
                        throw new Exception("Không có dầu mỡ nào được xuất!");
                    if (!addNew)//Sửa phiếu xuất
                    { 
                        try
                        {
                            var objpx = await HttpHelper.Put<NL_PhieuXuat>(Configuration.UrlCBApi + "api/NhienLieus/NLPutPhieuXuat", phieuXuat);
                            if (objpx == null) throw new Exception(phieuXuat.PhieuXuatID + "- Ngày xuất: " + phieuXuat.NgayXuat);
                            phieuXuat = objpx;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Lỗi sửa phiếu xuất: " + ex.Message);
                        }
                    }
                    else //Thêm phiếu xuất
                    {                        
                        try
                        {                            
                            var objpx = await HttpHelper.Post<NL_PhieuXuat>(Configuration.UrlCBApi + "api/NhienLieus/NLPostPhieuXuat", phieuXuat);
                            if (objpx == null) throw new Exception(phieuXuat.PhieuXuatID + "- Ngày xuất: " + phieuXuat.NgayXuat);
                            phieuXuat = objpx;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Lỗi thêm phiếu xuất: " + ex.Message);
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
        private void btnHuyPX_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion      
    }
}
