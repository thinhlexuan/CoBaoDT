using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using CBClient.Library;
using System.Text.RegularExpressions;
using System.Linq;
using CBClient.BLLTypes;
using CBClient.Services;
using CBClient.Models;
using System.ComponentModel;
using CBClient.BLLDaos;

namespace CBClient.CoBaoGAs
{
    public partial class NhapCBGADialog : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Properites
        public CoBaoGA rowCoBaoGA;
        public List<CoBaoGACT> listcobaoct = new List<CoBaoGACT>();
        public List<CoBaoGADM> listcobaodm = new List<CoBaoGADM>();
        long cobaoID = 0;
        Regex regexNumber = new Regex("^[0-9]+$");
        Regex regexNumberdigit = new Regex("^[0-2]*[.][0-9]$");
        Regex regexFloat = new Regex("^[-+]?[0-9]*[.][0-9]+$");
        Regex regexTime = new Regex("^(?:0?[0-9]|1[0-9]|2[0-3])[0-5][0-9]$");
        string[] arRaysCL = new string[] { "A", "B", "C", "D" };
        string[] arRaysDT = new string[] { "T", "T1" };
        bool addNew = true;
        bool addNewCT = true;
        #endregion
        public NhapCBGADialog(CoBaoGA _rowCoBao, List<CoBaoGACT> _listcobaoct, List<CoBaoGADM> _listcobaodm)
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty);
            InitializeComponent();
            KeyPerssEven();
            rowCoBaoGA = _rowCoBao;
            listcobaoct = _listcobaoct;
            listcobaodm = _listcobaodm;
            cobaoID = _rowCoBao.CoBaoID;
            addNew = rowCoBaoGA.CoBaoID <= 0 ? true : false;
            if (listcobaoct.Count > 0 && addNew)
            {
                var row = listcobaoct.Last();
                rowCoBaoGA.CoBaoID = row.CoBaoID;
                rowCoBaoGA.VaoKho = row.GioDi;
                rowCoBaoGA.GiaoMay = rowCoBaoGA.VaoKho;
                rowCoBaoGA.XuongBan = rowCoBaoGA.GiaoMay;

            }
        }
        private void NhapCBGADialog_Load(object sender, EventArgs e)
        {
            if (listcobaoct.Count > 0)
            {
                var row = listcobaoct.FirstOrDefault();
                clearControlCT(row);              
            }
            FnAutoComplete();
            bindControlToData();
            AppGlobal.MactauList = HttpHelper.GetList<MacTau>(Configuration.UrlCBApi + "api/MacTaus/GetMacTau?CongTac=0&MacTau=")
                .OrderBy(x => x.MacTauID).OrderBy(x => x.CongTacID).ToList();
        }
        private void NhapCBGADialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                AppGlobal.LoadServiceData();
                FnAutoComplete();
            }
        }
        #region Validated_Event  
        private void txtTaiXe1ID_Validated(object sender, EventArgs e)
        {
            try
            {
                txtTaiXe1Name.Text = AppGlobal.DMTaixeList.Where(x => x.TaiXeID == txtTaiXe1ID.Text).FirstOrDefault().TaiXeName;
            }
            catch
            {
                txtTaiXe1Name.Text = string.Empty;
            }
        }
        private void txtPhoXe1ID_Validated(object sender, EventArgs e)
        {
            try
            {
                txtPhoXe1Name.Text = AppGlobal.DMPhoxeList.Where(x => x.PhoXeID == txtPhoXe1ID.Text).FirstOrDefault().PhoXeName;
            }
            catch
            {
                txtPhoXe1Name.Text = string.Empty;
            }
        }
        private void txtTaiXe2ID_Validated(object sender, EventArgs e)
        {
            try
            {
                txtTaiXe2Name.Text = AppGlobal.DMTaixeList.Where(x => x.TaiXeID == txtTaiXe2ID.Text).FirstOrDefault().TaiXeName;
            }
            catch
            {
                txtTaiXe2Name.Text = string.Empty;
            }
        }
        private void txtPhoXe2ID_Validated(object sender, EventArgs e)
        {
            try
            {
                txtPhoXe2Name.Text = AppGlobal.DMPhoxeList.Where(x => x.PhoXeID == txtPhoXe2ID.Text).FirstOrDefault().PhoXeName;
            }
            catch
            {
                txtPhoXe2Name.Text = string.Empty;
            }
        }
        private void txtTaiXe3ID_Validated(object sender, EventArgs e)
        {
            try
            {
                txtTaiXe3Name.Text = AppGlobal.DMTaixeList.Where(x => x.TaiXeID == txtTaiXe3ID.Text).FirstOrDefault().TaiXeName;
            }
            catch
            {
                txtTaiXe3Name.Text = string.Empty;
            }
        }
        private void txtPhoXe3ID_Validated(object sender, EventArgs e)
        {
            try
            {
                txtPhoXe3Name.Text = AppGlobal.DMPhoxeList.Where(x => x.PhoXeID == txtPhoXe3ID.Text).FirstOrDefault().PhoXeName;
            }
            catch
            {
                txtPhoXe3Name.Text = string.Empty;
            }
        }
        private void txtKiemTra1ID_Validated(object sender, EventArgs e)
        {
            try
            {
                txtKiemTra1Name.Text = AppGlobal.DMTaixeList.Where(x => x.TaiXeID == txtKiemTra1ID.Text).FirstOrDefault().TaiXeName;
            }
            catch
            {
                txtKiemTra1Name.Text = string.Empty;
            }
        }
        private void txtKiemTra2ID_Validated(object sender, EventArgs e)
        {
            try
            {
                txtKiemTra2Name.Text = AppGlobal.DMTaixeList.Where(x => x.TaiXeID == txtKiemTra2ID.Text).FirstOrDefault().TaiXeName;
            }
            catch
            {
                txtKiemTra2Name.Text = string.Empty;
            }
        }
        private void txtKiemTra3ID_Validated(object sender, EventArgs e)
        {
            try
            {
                txtKiemTra3Name.Text = AppGlobal.DMTaixeList.Where(x => x.TaiXeID == txtKiemTra3ID.Text).FirstOrDefault().TaiXeName;
            }
            catch
            {
                txtKiemTra3Name.Text = string.Empty;
            }
        }
        private void sdNgayCB_Validated(object sender, EventArgs e)
        {
            rowCoBaoGA.NgayCB = sdNgayCB.Value;
            //sdNgayXPCT.Value = rowCoBaoGA.NgayCB;
        }
        private void txtLenBan_Validated(object sender, EventArgs e)
        {
            rowCoBaoGA.NgayCB = sdNgayCB.Value;
            txtNhanMay.Text = !string.IsNullOrWhiteSpace(txtNhanMay.Text) ? txtNhanMay.Text : txtLenBan.Text;
            string LenBan = txtLenBan.Text.Length == 3 ? "0" + txtLenBan.Text.Substring(0, 1) + ":" + txtLenBan.Text.Substring(1) : txtLenBan.Text.Substring(0, 2) + ":" + txtLenBan.Text.Substring(2);
            rowCoBaoGA.LenBan = DateTime.Parse(rowCoBaoGA.NgayCB.ToShortDateString() + " " + LenBan);
            if (rowCoBaoGA.LenBan < rowCoBaoGA.NgayCB) rowCoBaoGA.LenBan = rowCoBaoGA.LenBan.AddDays(1);
        }
        private void txtNhanMay_Validated(object sender, EventArgs e)
        {
            txtRaKho.Text = !string.IsNullOrWhiteSpace(txtRaKho.Text) ? txtRaKho.Text : txtNhanMay.Text;
            string NhanMay = txtNhanMay.Text.Length == 3 ? "0" + txtNhanMay.Text.Substring(0, 1) + ":" + txtNhanMay.Text.Substring(1) : txtNhanMay.Text.Substring(0, 2) + ":" + txtNhanMay.Text.Substring(2);
            rowCoBaoGA.NhanMay = DateTime.Parse(rowCoBaoGA.LenBan.ToShortDateString() + " " + NhanMay);
            if (rowCoBaoGA.NhanMay < rowCoBaoGA.LenBan) rowCoBaoGA.NhanMay = rowCoBaoGA.NhanMay.AddDays(1);
        }
        private void txtRaKho_Validated(object sender, EventArgs e)
        {
            string RaKho = txtRaKho.Text.Length == 3 ? "0" + txtRaKho.Text.Substring(0, 1) + ":" + txtRaKho.Text.Substring(1) : txtRaKho.Text.Substring(0, 2) + ":" + txtRaKho.Text.Substring(2);
            rowCoBaoGA.RaKho = DateTime.Parse(rowCoBaoGA.NhanMay.ToShortDateString() + " " + RaKho);
            if (rowCoBaoGA.RaKho < rowCoBaoGA.NhanMay) rowCoBaoGA.RaKho = rowCoBaoGA.RaKho.AddDays(1);           

        }
        private void txtVaoKho_Validated(object sender, EventArgs e)
        {
            txtGiaoMay.Text = !string.IsNullOrWhiteSpace(txtGiaoMay.Text) ? txtGiaoMay.Text : txtVaoKho.Text;
            string VaoKho = txtVaoKho.Text.Length == 3 ? "0" + txtVaoKho.Text.Substring(0, 1) + ":" + txtVaoKho.Text.Substring(1) : txtVaoKho.Text.Substring(0, 2) + ":" + txtVaoKho.Text.Substring(2);
            rowCoBaoGA.VaoKho = DateTime.Parse(rowCoBaoGA.RaKho.ToShortDateString() + " " + VaoKho);
            if (rowCoBaoGA.VaoKho < rowCoBaoGA.RaKho) rowCoBaoGA.VaoKho = rowCoBaoGA.VaoKho.AddDays(1);
            var row = listcobaoct.LastOrDefault();
            if (row != null)
            {
                rowCoBaoGA.VaoKho = DateTime.Parse(row.GioDi.ToShortDateString() + " " + VaoKho);
                if (rowCoBaoGA.VaoKho < row.GioDi) rowCoBaoGA.VaoKho = rowCoBaoGA.VaoKho.AddDays(1);
            }
        }
        private void txtGiaoMay_Validated(object sender, EventArgs e)
        {
            txtXuongBan.Text = !string.IsNullOrWhiteSpace(txtXuongBan.Text) ? txtXuongBan.Text : txtGiaoMay.Text;
            string GiaoMay = txtGiaoMay.Text.Length == 3 ? "0" + txtGiaoMay.Text.Substring(0, 1) + ":" + txtGiaoMay.Text.Substring(1) : txtGiaoMay.Text.Substring(0, 2) + ":" + txtGiaoMay.Text.Substring(2);
            rowCoBaoGA.GiaoMay = DateTime.Parse(rowCoBaoGA.VaoKho.ToShortDateString() + " " + GiaoMay);
            if (rowCoBaoGA.GiaoMay < rowCoBaoGA.VaoKho) rowCoBaoGA.GiaoMay = rowCoBaoGA.GiaoMay.AddDays(1);
        }
        private void txtXuongBan_Validated(object sender, EventArgs e)
        {
            string XuongBan = txtXuongBan.Text.Length == 3 ? "0" + txtXuongBan.Text.Substring(0, 1) + ":" + txtXuongBan.Text.Substring(1) : txtXuongBan.Text.Substring(0, 2) + ":" + txtXuongBan.Text.Substring(2);
            rowCoBaoGA.XuongBan = DateTime.Parse(rowCoBaoGA.GiaoMay.ToShortDateString() + " " + XuongBan);
            if (rowCoBaoGA.XuongBan < rowCoBaoGA.GiaoMay) rowCoBaoGA.XuongBan = rowCoBaoGA.XuongBan.AddDays(1);
        }
        private void txtNLBanTruoc_Validated(object sender, EventArgs e)
        {
            txtNLBanNhan.Text = txtNLBanTruoc.Text;
        }
        private void txtNLLinh_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNLLinh.Text))
                cboTramNL.SelectedIndex = -1;
            else
                cboTramNL.SelectedIndex = 0;
        }
        private void txtNhanDM_Validated(object sender, EventArgs e)
        {
            decimal DMGiao = 0;
            DMGiao += string.IsNullOrWhiteSpace(txtNhanDM.Text) ? 0 : decimal.Parse(txtNhanDM.Text, FormHelper.EnCultureInfo);
            DMGiao += string.IsNullOrWhiteSpace(txtLinhDM.Text) ? 0 : decimal.Parse(txtLinhDM.Text, FormHelper.EnCultureInfo);
            txtGiaoDM.Text = DMGiao <= 0 ? "" : DMGiao.ToString();
        }
        private void txtLinhDM_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLinhDM.Text))
                cboTramDM.SelectedIndex = -1;
            else
                cboTramDM.SelectedIndex = 0;
            decimal DMGiao = 0;
            DMGiao += string.IsNullOrWhiteSpace(txtNhanDM.Text) ? 0 : decimal.Parse(txtNhanDM.Text, FormHelper.EnCultureInfo);
            DMGiao += string.IsNullOrWhiteSpace(txtLinhDM.Text) ? 0 : decimal.Parse(txtLinhDM.Text, FormHelper.EnCultureInfo);
            txtGiaoDM.Text = DMGiao <= 0 ? "" : DMGiao.ToString();
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
        private void txtMacTauCT_Validated(object sender, EventArgs e)
        {
            try
            {
                txtLoaiTau.Text = AppGlobal.MactauList.Where(x => x.MacTauID == txtMacTauCT.Text).FirstOrDefault().LoaiTauID.ToString();
            }
            catch
            {
                txtLoaiTau.Text = string.Empty;
            }
        }

        #endregion
        #region Validated_Function   
        private string checkValidate()
        {
            string errMessage = string.Empty;
            int intCount = 0;
            if (!regexTime.IsMatch(txtLenBan.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ lên ban không đúng\r\n";
            }
            if (!regexTime.IsMatch(txtNhanMay.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ nhận máy không đúng\r\n";
            }
            if (!regexTime.IsMatch(txtRaKho.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ ra kho không đúng\r\n";
            }
            if (!regexTime.IsMatch(txtVaoKho.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ vào kho không đúng\r\n";
            }
            if (!regexTime.IsMatch(txtGiaoMay.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ giao máy không đúng\r\n";
            }
            if (!regexTime.IsMatch(txtXuongBan.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ xuống ban không đúng\r\n";
            }

            if (!regexNumber.IsMatch(txtNLBanTruoc.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Nhiên liệu ban trước không đúng\r\n";
            }
            if (!regexNumber.IsMatch(txtNLBanNhan.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Nhiên liệu ban nhận không đúng\r\n";
            }
            if (!string.IsNullOrWhiteSpace(txtNLLinh.Text))
            {
                if (!regexNumber.IsMatch(txtNLLinh.Text))
                {
                    intCount += 1;
                    errMessage += intCount.ToString() + ".Nhiên liệu lĩnh không đúng\r\n";
                }
                if (string.IsNullOrWhiteSpace(cboTramNL.Text))
                {
                    intCount += 1;
                    errMessage += intCount.ToString() + ".Trạm nhiên liệu không đúng\r\n";
                }
            }
            else
                cboTramNL.SelectedIndex = -1;
            //if (!string.IsNullOrWhiteSpace(txtNLTrongDo.Text) && !regexFloat.IsMatch(txtNLTrongDo.Text))
            //{
            //    intCount += 1;
            //    errMessage += intCount.ToString() + ".Nhiên liệu trong đó không đúng\r\n";
            //}
            if (!regexNumber.IsMatch(txtNLBanSau.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Nhiên liệu ban sau không đúng\r\n";
            }

            if (string.IsNullOrWhiteSpace(txtSLRK.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Số lần ra kho không đúng\r\n";
            }
            else if (decimal.Parse(txtSLRK.Text) > 2)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Số lần ra kho không đúng\r\n";
            }
            return errMessage;
        }
        private string checkValidateDM()
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
            if (!string.IsNullOrWhiteSpace(txtLinhDM.Text))
            {
                if (string.IsNullOrWhiteSpace(cboTramDM.Text))
                {
                    intCount += 1;
                    errMessage += intCount.ToString() + ".Trạm dầu mỡ không đúng\r\n";
                }
            }
            else
                cboTramDM.SelectedIndex = -1;
            return errMessage;
        }
        private string checkValidateCT()
        {
            string errMessage = string.Empty;
            int intCount = 0;
            if (sdNgayXPCT.Value > DateTime.Now)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Ngày xuất phát không đúng\r\n";
            }
            if (!string.IsNullOrWhiteSpace(txtGioDenCT.Text) && !regexTime.IsMatch(txtGioDenCT.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ đến không đúng\r\n";
            }
            if (!string.IsNullOrWhiteSpace(txtGioDiCT.Text) && !regexTime.IsMatch(txtGioDiCT.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ đi không đúng\r\n";
            }
            if (string.IsNullOrWhiteSpace(txtGioDenCT.Text) && string.IsNullOrWhiteSpace(txtGioDiCT.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Không có giờ đến hoặc giờ đi\r\n";
            }           
            if (!string.IsNullOrWhiteSpace(txtKMAdd.Text) && !regexFloat.IsMatch(txtKMAdd.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Km add không đúng\r\n";
            }
            return errMessage;
        }
        private string checkHoanThanh(DoanThongGA dt, List<DoanThongGACT> listct)
        {
            string errMessage = string.Empty;
            int intCount = 0;
            if (dt.DungKB<0)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ nhận máy nhỏ hơn giờ giao máy của cơ báo trước: "+dt.DungKB +"\r\n";
            }
            if (listct.Sum(x=>x.QuayVong)>720)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ quay vòng:"+ (listct.Sum(x => x.QuayVong)/60).ToString("N2") + " giờ lớn hơn 12 giờ\r\n";
            }
            var quayvong = listct.Where(x => x.QuayVong <0).FirstOrDefault();
            if (quayvong!=null)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ quay vòng âm: "+ quayvong.GaXPName+"-"+ quayvong.GaKTName + "\r\n";
            }
            var luhanh = listct.Where(x => x.LuHanh < 0).FirstOrDefault();
            if (luhanh != null)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ lữ hành âm: " + luhanh.GaXPName + "-" + luhanh.GaKTName + "\r\n";
            }
            var donthuan = listct.Where(x => x.DonThuan < 0).FirstOrDefault();
            if (donthuan != null)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ đơn thuần âm: " + donthuan.GaXPName + "-" + donthuan.GaKTName + "\r\n";
            }
            var giodung = listct.Where(x => x.DungDM < 0 ||x.DungDN<0||x.DungQD<0||x.DungXP<0||x.DungDD<0||x.DungKT<0||x.DungKhoDM<0||x.DungKhoDN<0).FirstOrDefault();
            if (giodung != null)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ dừng âm: " + giodung.GaXPName + "-" + giodung.GaKTName + "\r\n";
            }
            var giodon = listct.Where(x => x.DonXP < 0 || x.DonDD < 0 || x.DonKT < 0).FirstOrDefault();
            if (giodon != null)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ dồn âm: " + giodon.GaXPName + "-" + giodon.GaKTName + "\r\n";
            }
            return errMessage;
        }
        #endregion
        #region Function
        private void KeyPerssEven()
        {
            FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            //FormHelper.AddKeyPressEventHandlerForNumber(txtDauMay);
            FormHelper.AddKeyPressEventHandlerForNumber(txtSoCB);
            FormHelper.AddKeyPressEventHandlerForNumber(txtRutGio);
            FormHelper.AddKeyPressEventHandlerForNumber(txtTaiXe1ID);
            FormHelper.AddKeyPressEventHandlerForNumber(txtTaiXe1GioLT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtPhoXe1ID);
            FormHelper.AddKeyPressEventHandlerForNumber(txtPhoXe1GioLT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtTaiXe2ID);
            FormHelper.AddKeyPressEventHandlerForNumber(txtTaiXe2GioLT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtPhoXe2ID);
            FormHelper.AddKeyPressEventHandlerForNumber(txtPhoXe2GioLT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtTaiXe3ID);
            FormHelper.AddKeyPressEventHandlerForNumber(txtTaiXe3GioLT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtPhoXe3ID);
            FormHelper.AddKeyPressEventHandlerForNumber(txtPhoXe3GioLT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtKiemTra1ID);
            FormHelper.AddKeyPressEventHandlerForNumber(txtKiemTra2ID);
            FormHelper.AddKeyPressEventHandlerForNumber(txtKiemTra3ID);
            FormHelper.AddKeyPressEventHandlerForNumber(txtLenBan);
            FormHelper.AddKeyPressEventHandlerForNumber(txtNhanMay);
            FormHelper.AddKeyPressEventHandlerForNumber(txtRaKho);
            FormHelper.AddKeyPressEventHandlerForNumber(txtVaoKho);
            FormHelper.AddKeyPressEventHandlerForNumber(txtGiaoMay);
            FormHelper.AddKeyPressEventHandlerForNumber(txtXuongBan);            
            FormHelper.AddKeyPressEventHandlerForNumber(txtNLBanTruoc);
            FormHelper.AddKeyPressEventHandlerForNumber(txtNLBanNhan);
            FormHelper.AddKeyPressEventHandlerForNumber(txtNLLinh);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtNLTrongDo);
            FormHelper.AddKeyPressEventHandlerForNumber(txtNLBanSau);

            FormHelper.AddKeyPressEventHandlerForNumber(txtMaDM);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtNhanDM);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtLinhDM);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtGiaoDM);

            FormHelper.AddKeyPressEventHandlerForDecimal(txtSLRK);
            //FormHelper.AddKeyPressEventHandlerForDecimal(txtMaCB);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDonDD);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDungDD);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtDungNM);

            FormHelper.AddKeyPressEventHandlerForNumber(txtGioDenCT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtGioDiCT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtGioDonCT);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtRutGioNLCT);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTanSoCT);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTanRongCT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtTinhChatCT);
            //FormHelper.AddKeyPressEventHandlerForNumber(txtMayGhepCT);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtKMAdd);
        }
        private void FnAutoComplete()
        {
            txtDauMay.AutoCompleteCustomSource = AppGlobal.MaDauMayAutoComplate;
            txtDauMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtDauMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe1ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtTaiXe1ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe1ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe1ID.AutoCompleteCustomSource = AppGlobal.MaPhoXeAutoComplate;
            txtPhoXe1ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe1ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe2ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtTaiXe2ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe2ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe2ID.AutoCompleteCustomSource = AppGlobal.MaPhoXeAutoComplate;
            txtPhoXe2ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe2ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe3ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtTaiXe3ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe3ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe3ID.AutoCompleteCustomSource = AppGlobal.MaPhoXeAutoComplate;
            txtPhoXe3ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe3ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtKiemTra1ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtKiemTra1ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtKiemTra1ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtKiemTra2ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtKiemTra2ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtKiemTra2ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtKiemTra3ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtKiemTra3ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtKiemTra3ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe1Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtTaiXe1Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe1Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe1Name.AutoCompleteCustomSource = AppGlobal.TenPhoXeAutoComplate;
            txtPhoXe1Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe1Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe2Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtTaiXe2Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe2Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe2Name.AutoCompleteCustomSource = AppGlobal.TenPhoXeAutoComplate;
            txtPhoXe2Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe2Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe3Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtTaiXe3Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe3Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe3Name.AutoCompleteCustomSource = AppGlobal.TenPhoXeAutoComplate;
            txtPhoXe3Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe3Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtKiemTra1Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtKiemTra1Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtKiemTra1Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtKiemTra2Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtKiemTra2Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtKiemTra2Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtKiemTra3Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtKiemTra3Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtKiemTra3Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            var listtramnl = new List<DmtramNhienLieu>(AppGlobal.DMTramnlList);
            cboTramNL.DataSource = listtramnl;
            cboTramNL.DisplayMember = "TenTram";
            cboTramNL.ValueMember = "MaTram";
            cboTramNL.SelectedIndex = -1;

            var listtramdm = new List<DmtramNhienLieu>(AppGlobal.DMTramnlList);
            cboTramDM.DataSource = listtramdm;
            cboTramDM.DisplayMember = "TenTram";
            cboTramDM.ValueMember = "MaTram";
            cboTramDM.SelectedIndex = -1;

            txtMaDM.AutoCompleteCustomSource = AppGlobal.MaDMAutoComplate;
            txtMaDM.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMaDM.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTenDM.AutoCompleteCustomSource = AppGlobal.TenDMAutoComplate;
            txtTenDM.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTenDM.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            sdNgayCB.Value = DateTime.Today;
            //sdNgayXPCT.Value = sdNgayCB.Value;

            txtMacTauCT.AutoCompleteCustomSource = AppGlobal.MacTauAutoComplate;
            txtMacTauCT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMacTauCT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtGaDiCT.AutoCompleteCustomSource = AppGlobal.TenGaAutoComplate;
            txtGaDiCT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGaDiCT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTinhChatCT.AutoCompleteCustomSource = AppGlobal.MaTinhChatAutoComplate;
            txtTinhChatCT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTinhChatCT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtMayGhepCT.AutoCompleteCustomSource = AppGlobal.MaDauMayAutoComplate;
            txtMayGhepCT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMayGhepCT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }       
        private void bindControlToData()
        {
            txtDauMay.Text = rowCoBaoGA.DauMayID;
            txtSoCB.Text = rowCoBaoGA.SoCB;
            sdNgayCB.Value = rowCoBaoGA.NgayCB;
            txtRutGio.Text = rowCoBaoGA.RutGio > 0 ? rowCoBaoGA.RutGio.ToString() : string.Empty;
            txtChatLuong.Text = rowCoBaoGA.ChatLuong;
            txtSLRK.Text = rowCoBaoGA.SoLanRaKho > 0 ? FormHelper.ConvertString(rowCoBaoGA.SoLanRaKho.ToString()) : "0.5";

            txtTaiXe1ID.Text = rowCoBaoGA.TaiXe1ID;
            txtTaiXe1Name.Text = rowCoBaoGA.TaiXe1Name;
            txtTaiXe1GioLT.Text = rowCoBaoGA.TaiXe1GioLT > 0 ? rowCoBaoGA.TaiXe1GioLT.ToString() : string.Empty;
            txtPhoXe1ID.Text = rowCoBaoGA.PhoXe1ID;
            txtPhoXe1Name.Text = rowCoBaoGA.PhoXe1Name;
            txtPhoXe1GioLT.Text = rowCoBaoGA.PhoXe1GioLT > 0 ? rowCoBaoGA.PhoXe1GioLT.ToString() : string.Empty;
            txtTaiXe2ID.Text = rowCoBaoGA.TaiXe2ID;
            txtTaiXe2Name.Text = rowCoBaoGA.TaiXe2Name;
            txtTaiXe2GioLT.Text = rowCoBaoGA.TaiXe2GioLT > 0 ? rowCoBaoGA.TaiXe2GioLT.ToString() : string.Empty;
            txtPhoXe2ID.Text = rowCoBaoGA.PhoXe2ID;
            txtPhoXe2Name.Text = rowCoBaoGA.PhoXe2Name;
            txtPhoXe2GioLT.Text = rowCoBaoGA.PhoXe2GioLT > 0 ? rowCoBaoGA.PhoXe2GioLT.ToString() : string.Empty;
            txtTaiXe3ID.Text = rowCoBaoGA.TaiXe3ID;
            txtTaiXe3Name.Text = rowCoBaoGA.TaiXe3Name;
            txtTaiXe3GioLT.Text = rowCoBaoGA.TaiXe3GioLT > 0 ? rowCoBaoGA.TaiXe3GioLT.ToString() : string.Empty;
            txtPhoXe3ID.Text = rowCoBaoGA.PhoXe3ID;
            txtPhoXe3Name.Text = rowCoBaoGA.PhoXe3Name;
            txtPhoXe3GioLT.Text = rowCoBaoGA.PhoXe3GioLT > 0 ? rowCoBaoGA.PhoXe3GioLT.ToString() : string.Empty;
            txtKiemTra1ID.Text = rowCoBaoGA.KiemTra1ID;
            txtKiemTra1Name.Text = rowCoBaoGA.KiemTra1Name;
            txtKiemTra2ID.Text = rowCoBaoGA.KiemTra2ID;
            txtKiemTra2Name.Text = rowCoBaoGA.KiemTra2Name;
            txtKiemTra3ID.Text = rowCoBaoGA.KiemTra3ID;
            txtKiemTra3Name.Text = rowCoBaoGA.KiemTra3Name;

            txtLenBan.Text = rowCoBaoGA.CoBaoGoc <= 0 && rowCoBaoGA.CoBaoID<=0 ? string.Empty : rowCoBaoGA.LenBan.ToString("HHmm");
            txtNhanMay.Text = rowCoBaoGA.CoBaoGoc <= 0 && rowCoBaoGA.CoBaoID <= 0 ? string.Empty : rowCoBaoGA.NhanMay.ToString("HHmm");
            txtRaKho.Text = rowCoBaoGA.CoBaoGoc <= 0 && rowCoBaoGA.CoBaoID <= 0 ? string.Empty : rowCoBaoGA.RaKho.ToString("HHmm");
            txtVaoKho.Text = rowCoBaoGA.CoBaoGoc <= 0 && rowCoBaoGA.CoBaoID <= 0 ? string.Empty : rowCoBaoGA.VaoKho.ToString("HHmm");
            txtGiaoMay.Text = rowCoBaoGA.CoBaoGoc <= 0 && rowCoBaoGA.CoBaoID <= 0 ? string.Empty : rowCoBaoGA.GiaoMay.ToString("HHmm");
            txtXuongBan.Text = rowCoBaoGA.CoBaoGoc <= 0 && rowCoBaoGA.CoBaoID <= 0 ? string.Empty : rowCoBaoGA.XuongBan.ToString("HHmm");           

            txtNLBanTruoc.Text = rowCoBaoGA.NLBanTruoc > 0 ? rowCoBaoGA.NLBanTruoc.ToString() : "0";
            txtNLBanNhan.Text = rowCoBaoGA.NLThucNhan > 0 ? rowCoBaoGA.NLThucNhan.ToString() : "0";
            txtNLLinh.Text = rowCoBaoGA.NLLinh > 0 ? rowCoBaoGA.NLLinh.ToString() : string.Empty;
            if(rowCoBaoGA.NLLinh > 0) cboTramNL.SelectedValue = rowCoBaoGA.TramNLID;
            txtNLTrongDo.Text = rowCoBaoGA.NLTrongDoan > 0 ? FormHelper.ConvertString(rowCoBaoGA.NLTrongDoan.ToString()) : string.Empty;
            txtNLBanSau.Text = rowCoBaoGA.NLBanSau > 0 ? rowCoBaoGA.NLBanSau.ToString() : "0"; 
           
            txtSHDT.Text = rowCoBaoGA.SHDT;
            txtMaCB.Text = rowCoBaoGA.MaCB;
            txtDonDD.Text = rowCoBaoGA.DonDocDuong > 0 ? FormHelper.ConvertString(rowCoBaoGA.DonDocDuong.ToString()) : string.Empty;
            txtDungDD.Text = rowCoBaoGA.DungDocDuong > 0 ? FormHelper.ConvertString(rowCoBaoGA.DungDocDuong.ToString()) : string.Empty;
            txtDungNM.Text = rowCoBaoGA.DungNoMay > 0 ? FormHelper.ConvertString(rowCoBaoGA.DungNoMay.ToString()) : string.Empty;
            txtGhiChu.Text = rowCoBaoGA.GhiChu;

            CoBaoDMbinding.DataSource = listcobaodm;
            CoBaoCTbinding.DataSource = listcobaoct;
            lblCoBaoCT.Text = "Tổng số cơ báo chi tiết:" + listcobaoct.Count.ToString("N0");
        }
        private void bindDataToControl()
        {
            try
            {
                if (addNew) rowCoBaoGA.CoBaoID = 0;
                rowCoBaoGA.DauMayID = txtDauMay.Text;
                if (rowCoBaoGA.CoBaoGoc<=0)
                {
                    DMDauMay _dmDauMay = AppGlobal.DMDaumayList.Where(x => x.DauMaySo == txtDauMay.Text).FirstOrDefault();
                    rowCoBaoGA.LoaiMayID = _dmDauMay.PhanLoai;
                    rowCoBaoGA.DvdmID = _dmDauMay.MaCtquanLy;
                    rowCoBaoGA.DvdmName = AppGlobal.DonviDMList.Where(x => x.MaDV == rowCoBaoGA.DvdmID).FirstOrDefault().TenDV;
                }
                rowCoBaoGA.SoCB = txtSoCB.Text;
                rowCoBaoGA.NgayCB = sdNgayCB.Value;
                rowCoBaoGA.RutGio = string.IsNullOrWhiteSpace(txtRutGio.Text) ? 0 : int.Parse(txtRutGio.Text);
                rowCoBaoGA.ChatLuong = txtChatLuong.Text.Replace(" ", "");
                rowCoBaoGA.SoLanRaKho = string.IsNullOrWhiteSpace(txtSLRK.Text) ? 0 : decimal.Parse(txtSLRK.Text, FormHelper.EnCultureInfo);

                rowCoBaoGA.TaiXe1ID = txtTaiXe1ID.Text;
                rowCoBaoGA.TaiXe1Name = txtTaiXe1Name.Text;
                rowCoBaoGA.TaiXe1GioLT = string.IsNullOrWhiteSpace(txtTaiXe1GioLT.Text) ? (short)0 : short.Parse(txtTaiXe1GioLT.Text);
                rowCoBaoGA.PhoXe1ID = txtPhoXe1ID.Text;
                rowCoBaoGA.PhoXe1Name = txtPhoXe1Name.Text;
                rowCoBaoGA.PhoXe1GioLT = string.IsNullOrWhiteSpace(txtPhoXe1GioLT.Text) ? (short)0 : short.Parse(txtPhoXe1GioLT.Text);
                rowCoBaoGA.TaiXe2ID = txtTaiXe2ID.Text;
                rowCoBaoGA.TaiXe2Name = txtTaiXe2Name.Text;
                rowCoBaoGA.TaiXe2GioLT = string.IsNullOrWhiteSpace(txtTaiXe2GioLT.Text) ? (short)0 : short.Parse(txtTaiXe2GioLT.Text);
                rowCoBaoGA.PhoXe2ID = txtPhoXe2ID.Text;
                rowCoBaoGA.PhoXe2Name = txtPhoXe2Name.Text;
                rowCoBaoGA.PhoXe2GioLT = string.IsNullOrWhiteSpace(txtPhoXe2GioLT.Text) ? (short)0 : short.Parse(txtPhoXe2GioLT.Text);
                rowCoBaoGA.TaiXe3ID = txtTaiXe3ID.Text;
                rowCoBaoGA.TaiXe3Name = txtTaiXe3Name.Text;
                rowCoBaoGA.TaiXe3GioLT = string.IsNullOrWhiteSpace(txtTaiXe3GioLT.Text) ? (short)0 : short.Parse(txtTaiXe3GioLT.Text);
                rowCoBaoGA.PhoXe3ID = txtPhoXe3ID.Text;
                rowCoBaoGA.PhoXe3Name = txtPhoXe3Name.Text;
                rowCoBaoGA.PhoXe3GioLT = string.IsNullOrWhiteSpace(txtPhoXe3GioLT.Text) ? (short)0 : short.Parse(txtPhoXe3GioLT.Text);
                rowCoBaoGA.KiemTra1ID = txtKiemTra1ID.Text;
                rowCoBaoGA.KiemTra1Name = txtKiemTra1Name.Text;
                rowCoBaoGA.KiemTra2ID = txtKiemTra2ID.Text;
                rowCoBaoGA.KiemTra2Name = txtKiemTra2Name.Text;
                rowCoBaoGA.KiemTra3ID = txtKiemTra3ID.Text;
                rowCoBaoGA.KiemTra3Name = txtKiemTra3Name.Text;
                if (rowCoBaoGA.CoBaoGoc <= 0)
                {
                    var listTX = AppGlobal.DMTaixeList.Where(x => x.TaiXeID == txtTaiXe1ID.Text).ToList();
                    if (AppGlobal.User.MaDVQL != "TCT")
                    {
                        string maDV = AppGlobal.User.MaDVQL;
                        if (AppGlobal.User.MaDVQL == "YV") maDV += ",HN";
                        else if (AppGlobal.User.MaDVQL == "DN") maDV += ",SG";
                        DMTaiXe dmTaiXe = listTX.Where(x => maDV.Contains(x.DonViID)).FirstOrDefault();
                        rowCoBaoGA.DvcbID = dmTaiXe.DonViID;
                        rowCoBaoGA.DvcbName = dmTaiXe.DonViName;
                    }
                    else
                    {
                        DMTaiXe dmTaiXe = listTX.FirstOrDefault();
                        rowCoBaoGA.DvcbID = dmTaiXe.DonViID;
                        rowCoBaoGA.DvcbName = dmTaiXe.DonViName;
                    }    
                }

                string LenBan = txtLenBan.Text.Length == 3 ? "0" + txtLenBan.Text.Substring(0, 1) + ":" + txtLenBan.Text.Substring(1) : txtLenBan.Text.Substring(0, 2) + ":" + txtLenBan.Text.Substring(2);                
                rowCoBaoGA.LenBan = DateTime.Parse(rowCoBaoGA.LenBan.ToShortDateString() + " " + LenBan);
                if (rowCoBaoGA.LenBan < rowCoBaoGA.NgayCB) rowCoBaoGA.LenBan = rowCoBaoGA.LenBan.AddDays(1);
                string NhanMay = txtNhanMay.Text.Length == 3 ? "0" + txtNhanMay.Text.Substring(0, 1) + ":" + txtNhanMay.Text.Substring(1) : txtNhanMay.Text.Substring(0, 2) + ":" + txtNhanMay.Text.Substring(2);
                rowCoBaoGA.NhanMay = DateTime.Parse(rowCoBaoGA.NhanMay.ToShortDateString() + " " + NhanMay);
                if (rowCoBaoGA.NhanMay < rowCoBaoGA.LenBan) rowCoBaoGA.NhanMay= rowCoBaoGA.NhanMay.AddDays(1);
                string RaKho = txtRaKho.Text.Length == 3 ? "0" + txtRaKho.Text.Substring(0, 1) + ":" + txtRaKho.Text.Substring(1) : txtRaKho.Text.Substring(0, 2) + ":" + txtRaKho.Text.Substring(2);
                rowCoBaoGA.RaKho = DateTime.Parse(rowCoBaoGA.RaKho.ToShortDateString() + " " + RaKho);
                if (rowCoBaoGA.RaKho < rowCoBaoGA.NhanMay) rowCoBaoGA.RaKho= rowCoBaoGA.RaKho.AddDays(1);
                string VaoKho = txtVaoKho.Text.Length == 3 ? "0" + txtVaoKho.Text.Substring(0, 1) + ":" + txtVaoKho.Text.Substring(1) : txtVaoKho.Text.Substring(0, 2) + ":" + txtVaoKho.Text.Substring(2);
                rowCoBaoGA.VaoKho = DateTime.Parse(rowCoBaoGA.VaoKho.ToShortDateString() + " " + VaoKho);
                if (rowCoBaoGA.VaoKho < rowCoBaoGA.RaKho) rowCoBaoGA.VaoKho= rowCoBaoGA.VaoKho.AddDays(1);
                string GiaoMay = txtGiaoMay.Text.Length == 3 ? "0" + txtGiaoMay.Text.Substring(0, 1) + ":" + txtGiaoMay.Text.Substring(1) : txtGiaoMay.Text.Substring(0, 2) + ":" + txtGiaoMay.Text.Substring(2);
                rowCoBaoGA.GiaoMay = DateTime.Parse(rowCoBaoGA.GiaoMay.ToShortDateString() + " " + GiaoMay);
                if (rowCoBaoGA.GiaoMay < rowCoBaoGA.VaoKho) rowCoBaoGA.GiaoMay= rowCoBaoGA.GiaoMay.AddDays(1);
                string XuongBan = txtXuongBan.Text.Length == 3 ? "0" + txtXuongBan.Text.Substring(0, 1) + ":" + txtXuongBan.Text.Substring(1) : txtXuongBan.Text.Substring(0, 2) + ":" + txtXuongBan.Text.Substring(2);
                rowCoBaoGA.XuongBan = DateTime.Parse(rowCoBaoGA.XuongBan.ToShortDateString() + " " + XuongBan);
                if (rowCoBaoGA.XuongBan < rowCoBaoGA.GiaoMay) rowCoBaoGA.XuongBan= rowCoBaoGA.XuongBan.AddDays(1);

                rowCoBaoGA.NLBanTruoc = string.IsNullOrWhiteSpace(txtNLBanTruoc.Text) ? 0 : int.Parse(txtNLBanTruoc.Text);
                rowCoBaoGA.NLThucNhan = string.IsNullOrWhiteSpace(txtNLBanNhan.Text) ? 0 : int.Parse(txtNLBanNhan.Text);
                rowCoBaoGA.NLLinh = string.IsNullOrWhiteSpace(txtNLLinh.Text) ? 0 : int.Parse(txtNLLinh.Text);
                rowCoBaoGA.TramNLID = string.IsNullOrWhiteSpace(txtNLLinh.Text) ? string.Empty : cboTramNL.SelectedValue.ToString();
                rowCoBaoGA.NLTrongDoan = string.IsNullOrWhiteSpace(txtNLTrongDo.Text) ? 0 : decimal.Parse(txtNLTrongDo.Text, FormHelper.EnCultureInfo);
                rowCoBaoGA.NLBanSau = string.IsNullOrWhiteSpace(txtNLBanSau.Text) ? 0 : int.Parse(txtNLBanSau.Text);

                rowCoBaoGA.SHDT = txtSHDT.Text;
                rowCoBaoGA.MaCB = txtMaCB.Text;
                rowCoBaoGA.DonDocDuong = !string.IsNullOrWhiteSpace(txtDonDD.Text) ? decimal.Parse(txtDonDD.Text, FormHelper.EnCultureInfo) : 0;
                rowCoBaoGA.DungDocDuong = !string.IsNullOrWhiteSpace(txtDungDD.Text) ? decimal.Parse(txtDungDD.Text, FormHelper.EnCultureInfo) : 0;
                rowCoBaoGA.DungNoMay = !string.IsNullOrWhiteSpace(txtDungNM.Text) ? decimal.Parse(txtDungNM.Text, FormHelper.EnCultureInfo) : 0;
                rowCoBaoGA.GhiChu = txtGhiChu.Text;

                if (listcobaoct.Count > 0)
                {
                    rowCoBaoGA.GaID = listcobaoct.FirstOrDefault().GaID;
                    rowCoBaoGA.GaName = listcobaoct.FirstOrDefault().GaName;
                }

                rowCoBaoGA.Modifydate = DateTime.Now;
                rowCoBaoGA.Modifyby = AppGlobal.User.Username;
                rowCoBaoGA.ModifyName = AppGlobal.User.FullName;
                if (addNew)
                {
                    rowCoBaoGA.Createddate = rowCoBaoGA.Modifydate;
                    rowCoBaoGA.Createdby = rowCoBaoGA.Modifyby;
                    rowCoBaoGA.CreatedName = rowCoBaoGA.ModifyName;

                    rowCoBaoGA.KhoaCB = false;
                    rowCoBaoGA.KhoaCBdate = rowCoBaoGA.Modifydate;
                    rowCoBaoGA.KhoaCBby = rowCoBaoGA.Modifyby;
                    rowCoBaoGA.KhoaCBName = rowCoBaoGA.ModifyName;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nạp cơ báo: " + ex.Message);
            }
        }       
        private void clearControlCT(CoBaoGACT row)
        {
            sdNgayXPCT.Value = rowCoBaoGA.NgayCB;
            sdNgayXPCT.Value = row == null? rowCoBaoGA.NgayCB : row.NgayXP;           
            txtGioDenCT.Text = string.Empty;
            txtGioDiCT.Text = string.Empty;
            txtGioDonCT.Text = string.Empty;

            txtMacTauCT.Text = row == null ? string.Empty : row.MacTauID.ToString();
            txtGaDiCT.Text = string.Empty;
            txtLoaiTau.Text = row == null ? string.Empty : row.LoaiTauID.ToString();
            txtRutGioNLCT.Text = string.Empty;
            chkDungGioPT.Checked = false;

            txtTanSoCT.Text = row == null ? string.Empty : FormHelper.ConvertString(row.Tan.ToString());
            txtXeTotal.Text = row == null ? string.Empty : row.XeTotal.ToString();
            txtTanRongCT.Text = row == null ? string.Empty : FormHelper.ConvertString(row.TanXeRong.ToString());
            txtXeRongTotal.Text = row == null ? string.Empty : row.XeRongTotal.ToString();

            txtTinhChatCT.Text = row == null ? string.Empty : row.TinhChatID.ToString();
            txtMayGhepCT.Text = row == null ? string.Empty : row.MayGhepID.ToString(); ;
            txtKMAdd.Text = string.Empty;
        }
        private void bindControlToDataCT(CoBaoGACT row)
        {
            if (row == null) return;
            sdNgayXPCT.Value = row.NgayXP;
            txtGioDenCT.Text = row.GioDen.ToString("HHmm");
            txtGioDiCT.Text = row.GioDi.ToString("HHmm");
            txtGioDonCT.Text = FormHelper.ConvertString(row.PhutDon.ToString());

            txtMacTauCT.Text = row.MacTauID.ToString();   
            txtRutGioNLCT.Text= FormHelper.ConvertString(row.RutGioNL.ToString());
            chkDungGioPT.Checked = row.DungGioPT;
            txtGaDiCT.Text = row.GaName;
            txtLoaiTau.Text = row.LoaiTauID.ToString();

            txtTanSoCT.Text = row.Tan > 0 ? FormHelper.ConvertString(row.Tan.ToString()) : string.Empty;
            txtXeTotal.Text = row.XeTotal > 0 ? row.XeTotal.ToString() : string.Empty;
            txtTanRongCT.Text = row.TanXeRong > 0 ? FormHelper.ConvertString(row.TanXeRong.ToString()) : string.Empty;
            txtXeRongTotal.Text = row.XeRongTotal > 0 ? row.XeRongTotal.ToString() : string.Empty;

            txtTinhChatCT.Text = row.TinhChatID.ToString();
            txtMayGhepCT.Text = row.MayGhepID.ToString(); ;
            txtKMAdd.Text = row.KmAdd > 0 ? FormHelper.ConvertString(row.KmAdd.ToString()) : string.Empty;
        }
        private void bindDataToControlCT(ref CoBaoGACT row)
        {
            row.CoBaoID = rowCoBaoGA.CoBaoID;
            row.NgayXP = sdNgayXPCT.Value;
            
            if (!string.IsNullOrWhiteSpace(txtGioDenCT.Text))
            {
                string gioden = txtGioDenCT.Text.Length == 3 ? "0" + txtGioDenCT.Text.Substring(0, 1) + ":" + txtGioDenCT.Text.Substring(1) : txtGioDenCT.Text.Substring(0, 2) + ":" + txtGioDenCT.Text.Substring(2);                
                row.GioDen = DateTime.Parse(rowCoBaoGA.RaKho.ToShortDateString() + " " + gioden);
                if (rowCoBaoGA.RaKho > row.GioDen) row.GioDen = row.GioDen.AddDays(1);
            }
            if (!string.IsNullOrWhiteSpace(txtGioDiCT.Text))
            {
                string giodi = txtGioDiCT.Text.Length == 3 ? "0" + txtGioDiCT.Text.Substring(0, 1) + ":" + txtGioDiCT.Text.Substring(1) : txtGioDiCT.Text.Substring(0, 2) + ":" + txtGioDiCT.Text.Substring(2);
                row.GioDi = DateTime.Parse(rowCoBaoGA.RaKho.ToShortDateString() + " " + giodi);
                if (row.GioDen > row.GioDi) row.GioDi = row.GioDi.AddDays(1);
            }
            if (!string.IsNullOrWhiteSpace(txtGioDonCT.Text))
                row.PhutDon = decimal.Parse(txtGioDonCT.Text, FormHelper.EnCultureInfo);
            row.MacTauID = txtMacTauCT.Text;
            try
            {
                var dmMacTau = AppGlobal.MactauList.Where(x => x.MacTauID == txtMacTauCT.Text).FirstOrDefault();
                row.LoaiTauID = (short)dmMacTau.LoaiTauID;
                row.LoaiTauName = dmMacTau.LoaiTauName;
                row.CongTyID = dmMacTau.CongTyID;
                row.CongTyName = dmMacTau.CongTyName;
                row.TuyenID = (short)dmMacTau.TuyenID;
                row.TuyenName = dmMacTau.TuyenName;
                var ngayXP= listcobaoct.Where(x => x.MacTauID == txtMacTauCT.Text).FirstOrDefault();
                if (ngayXP != null)
                    row.NgayXP = ngayXP.NgayXP;
                else
                    row.NgayXP = rowCoBaoGA.NgayCB;
            }
            catch
            {

            }
            if (!string.IsNullOrWhiteSpace(txtRutGioNLCT.Text))
                row.RutGioNL = decimal.Parse(txtRutGioNLCT.Text, FormHelper.EnCultureInfo);
            row.DungGioPT = chkDungGioPT.Checked;
            row.GaName = txtGaDiCT.Text;
            row.GaID = AppGlobal.GaDic.FirstOrDefault(x => x.Value == txtGaDiCT.Text).Key;            
            if (!string.IsNullOrWhiteSpace(txtTanSoCT.Text))
                row.Tan = decimal.Parse(txtTanSoCT.Text, FormHelper.EnCultureInfo);
            if (!string.IsNullOrWhiteSpace(txtXeTotal.Text))
                row.XeTotal = int.Parse(txtXeTotal.Text);
            if (!string.IsNullOrWhiteSpace(txtTanRongCT.Text))
                row.TanXeRong = decimal.Parse(txtTanRongCT.Text, FormHelper.EnCultureInfo);
            if (!string.IsNullOrWhiteSpace(txtXeRongTotal.Text))
                row.XeRongTotal = int.Parse(txtXeRongTotal.Text);
            if (!string.IsNullOrWhiteSpace(txtTinhChatCT.Text))
            {
                row.TinhChatID = short.Parse(txtTinhChatCT.Text);
                row.TinhChatName = AppGlobal.TinhChatDic.FirstOrDefault(x => x.Key == short.Parse(txtTinhChatCT.Text)).Value;
            }
            else
            {
                row.TinhChatID = 1;
                row.TinhChatName = AppGlobal.TinhChatDic.FirstOrDefault(x => x.Key == 1).Value;
            }            
            row.MayGhepID = string.IsNullOrWhiteSpace(txtMayGhepCT.Text) ? string.Empty : txtMayGhepCT.Text;
            if (!string.IsNullOrWhiteSpace(txtKMAdd.Text))
                row.KmAdd = decimal.Parse(txtKMAdd.Text, FormHelper.EnCultureInfo);
        }
        #endregion
        #region Keys_Press
        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            if (dgCoBaoCT.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CoBaoGACT cobaoct = (CoBaoGACT)dgCoBaoCT.CurrentRow.DataBoundItem;
                    bindControlToDataCT(cobaoct);
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
            if (dgCoBaoCT.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CoBaoGACT cobaoct = (CoBaoGACT)dgCoBaoCT.CurrentRow.DataBoundItem;
                    listcobaoct.Remove(cobaoct);
                    CoBaoCTbinding.DataSource = null;
                    listcobaoct = listcobaoct.OrderBy(x => x.GioDi).ToList();                   
                    CoBaoCTbinding.DataSource = listcobaoct;                    
                    CoBaoCTbinding.EndEdit();                  
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
                CoBaoGACT ct = new CoBaoGACT();
                if(addNewCT)
                {
                    bindDataToControlCT(ref ct);
                    listcobaoct.Add(ct);
                }
                else if (dgCoBaoCT.CurrentRow != null)
                {
                    ct = (CoBaoGACT)dgCoBaoCT.CurrentRow.DataBoundItem;
                    bindDataToControlCT(ref ct);
                }
                CoBaoCTbinding.DataSource = null;
                listcobaoct = listcobaoct.OrderBy(x => x.GioDi).ToList();
                CoBaoCTbinding.DataSource = listcobaoct;
                CoBaoCTbinding.EndEdit();
                lblCoBaoCT.Text = "Tổng số cơ báo chi tiết:" + listcobaoct.Count.ToString("N0");
                clearControlCT(ct);
                addNewCT = true;
            }
        }
        private void btnHuyCT_Click(object sender, EventArgs e)
        {
            listcobaoct = listcobaoct.OrderBy(x => x.GioDi).ToList();
            CoBaoCTbinding.DataSource = listcobaoct;
            CoBaoCTbinding.CancelEdit();
            lblCoBaoCT.Text = "Tổng số cơ báo chi tiết:" + listcobaoct.Count.ToString("N0");
            CoBaoGACT ct = new CoBaoGACT();
            clearControlCT(ct);
        }
        private void btnSuaDM_Click(object sender, EventArgs e)
        {
            if (dgCoBaoDM.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CoBaoGADM cobaodm = (CoBaoGADM)dgCoBaoDM.CurrentRow.DataBoundItem;
                    txtMaDM.Text = cobaodm.LoaiDauMoID.ToString();
                    txtTenDM.Text = cobaodm.LoaiDauMoName;
                    txtDVT.Text = cobaodm.DonViTinh;
                    txtNhanDM.Text = FormHelper.ConvertString(cobaodm.Nhan.ToString());
                    txtLinhDM.Text = cobaodm.Linh <= 0 ? "" : FormHelper.ConvertString(cobaodm.Linh.ToString());
                    if (cobaodm.MaTram == null)
                    {
                        cboTramDM.SelectedIndex = -1;
                    }
                    else
                    {
                        cboTramDM.SelectedValue = cobaodm.MaTram;
                    }
                    txtGiaoDM.Text = FormHelper.ConvertString(cobaodm.Giao.ToString());
                    this.Cursor = Cursors.Default;
                }

                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    DialogHelper.Error(ex.Message);
                }

            }
        }
        private void btnXoaDM_Click(object sender, EventArgs e)
        {
            if (dgCoBaoDM.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CoBaoGADM cobaodm = (CoBaoGADM)dgCoBaoDM.CurrentRow.DataBoundItem;
                    listcobaodm.Remove(cobaodm);
                    BindingList<CoBaoGADM> lstBinding = new BindingList<CoBaoGADM>();
                    foreach (CoBaoGADM ct in listcobaodm)
                        lstBinding.Add(ct);
                    dgCoBaoDM.DataSource = lstBinding;
                    dgCoBaoDM.Refresh();
                    this.Cursor = Cursors.Default;
                }

                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    DialogHelper.Error(ex.Message);
                }
            }
        }       
        private void btnLuuDM_Click(object sender, EventArgs e)
        {
            string errMessageCT = checkValidateDM();
            if (!string.IsNullOrWhiteSpace(errMessageCT))
                Library.DialogHelper.Error(errMessageCT);
            else
            {
                CoBaoGADM row = new CoBaoGADM();
                if (dgCoBaoDM.CurrentRow != null)//Sửa dẩu mỡ
                {
                    row = (CoBaoGADM)dgCoBaoDM.CurrentRow.DataBoundItem;
                    if (txtMaDM.Text != row.LoaiDauMoID.ToString())
                    {
                        row = new CoBaoGADM();
                        if (listcobaodm.Where(x => x.LoaiDauMoID == short.Parse(txtMaDM.Text)).FirstOrDefault() != null)
                        {
                            DialogHelper.Inform("Đã có định mức này.");
                            return;
                        }
                        row.CoBaoID = rowCoBaoGA.CoBaoID;
                        row.LoaiDauMoID = short.Parse(txtMaDM.Text);
                        row.LoaiDauMoName = txtTenDM.Text;
                        row.DonViTinh = txtDVT.Text;
                        row.Nhan = !string.IsNullOrWhiteSpace(txtNhanDM.Text) ? decimal.Parse(txtNhanDM.Text, FormHelper.EnCultureInfo) : 0;
                        row.Linh = !string.IsNullOrWhiteSpace(txtLinhDM.Text) ? decimal.Parse(txtLinhDM.Text, FormHelper.EnCultureInfo) : 0;
                        if (!string.IsNullOrWhiteSpace(cboTramDM.Text))
                        {
                            row.MaTram = AppGlobal.DMTramnlList.Where(x => x.TenTram == cboTramDM.Text).FirstOrDefault().MaTram;
                            row.TenTram = cboTramDM.Text;
                        }
                        row.Giao = !string.IsNullOrWhiteSpace(txtGiaoDM.Text) ? decimal.Parse(txtGiaoDM.Text, FormHelper.EnCultureInfo) : 0;
                        listcobaodm.Add(row);
                    }
                    else
                    {
                        row.Nhan = !string.IsNullOrWhiteSpace(txtNhanDM.Text) ? decimal.Parse(txtNhanDM.Text, FormHelper.EnCultureInfo) : 0;
                        row.Linh = !string.IsNullOrWhiteSpace(txtLinhDM.Text) ? decimal.Parse(txtLinhDM.Text, FormHelper.EnCultureInfo) : 0;
                        if (!string.IsNullOrWhiteSpace(cboTramDM.Text))
                        {
                            row.MaTram = AppGlobal.DMTramnlList.Where(x => x.TenTram == cboTramDM.Text).FirstOrDefault().MaTram;
                            row.TenTram = cboTramDM.Text;
                        }
                        row.Giao = !string.IsNullOrWhiteSpace(txtGiaoDM.Text) ? decimal.Parse(txtGiaoDM.Text, FormHelper.EnCultureInfo) : 0;
                    }
                }
                else//Thêm dẩu mỡ
                {
                    if (listcobaodm.Where(x => x.LoaiDauMoID == short.Parse(txtMaDM.Text)).FirstOrDefault() != null)
                    {
                        DialogHelper.Inform("Đã có định mức này.");
                        return;
                    }
                    row.CoBaoID = rowCoBaoGA.CoBaoID;
                    row.LoaiDauMoID = short.Parse(txtMaDM.Text);
                    row.LoaiDauMoName = txtTenDM.Text;
                    row.DonViTinh = txtDVT.Text;
                    row.Nhan = !string.IsNullOrWhiteSpace(txtNhanDM.Text) ? decimal.Parse(txtNhanDM.Text, FormHelper.EnCultureInfo) : 0;
                    row.Linh = !string.IsNullOrWhiteSpace(txtLinhDM.Text) ? decimal.Parse(txtLinhDM.Text, FormHelper.EnCultureInfo) : 0;
                    if (!string.IsNullOrWhiteSpace(cboTramDM.Text))
                    {
                        row.MaTram = AppGlobal.DMTramnlList.Where(x => x.TenTram == cboTramDM.Text).FirstOrDefault().MaTram;
                        row.TenTram = cboTramDM.Text;
                    }
                    row.Giao = !string.IsNullOrWhiteSpace(txtGiaoDM.Text) ? decimal.Parse(txtGiaoDM.Text, FormHelper.EnCultureInfo) : 0;
                    listcobaodm.Add(row);
                }
                BindingList<CoBaoGADM> lstBinding = new BindingList<CoBaoGADM>();
                foreach (CoBaoGADM ct in listcobaodm)
                    lstBinding.Add(ct);
                dgCoBaoDM.DataSource = lstBinding;
                dgCoBaoDM.Refresh();
            }            
        }
        private void btnHuyDM_Click(object sender, EventArgs e)
        {
            dgCoBaoDM.DataSource = listcobaodm;
            
        }
        private async void btnLuuCB_Click(object sender, EventArgs e)
        {
            string trangThaiOld = rowCoBaoGA.TrangThai;

            string errMessage = checkValidate();
            if (!string.IsNullOrWhiteSpace(errMessage))
                Library.DialogHelper.Error("Lỗi nhập liệu: " + errMessage);
            else
            {
                CoBaoGAALL coBaoALL = new CoBaoGAALL();
                //Nạp dữ liệu
                try
                {
                    bindDataToControl();
                    //Nạp dữ liệu đoạn thống
                    DoanThongGA doanThong = await DoanThongGADAO.bindDoanThongToCoBao(rowCoBaoGA);

                    List<DoanThongGADM> listdoanthongdm = listcobaodm.Count > 0 ? DoanThongGADAO.bindDoanThongDM(rowCoBaoGA, listcobaodm) : new List<DoanThongGADM>();
                    List<DoanThongGACT> listdoanthongct = listcobaoct.Count >= 0 ? DoanThongGADAO.bindDoanThongCT(doanThong.DungKB,rowCoBaoGA, listcobaoct) : new List<DoanThongGACT>();
                    errMessage = checkHoanThanh(doanThong, listdoanthongct);
                    if (!string.IsNullOrWhiteSpace(errMessage))
                    {
                        Library.DialogHelper.Error("Lỗi nhập liệu:\r\n" + errMessage);
                        if (Library.DialogHelper.Confirm("Cơ báo này hoàn thành lỗi, có tiếp tục hoàn thành?") == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                    //Nạp dữ liệu định mức
                    //1.Phân bổ nhiên liệu
                    DateTime ngayCB = new DateTime(2023, 1, 1);
                    if (rowCoBaoGA.NgayCB >= ngayCB)
                    {
                        if (rowCoBaoGA.DvcbID == "YV")
                        {
                            rowCoBaoGA.DvcbID = "HN";
                            rowCoBaoGA.DvcbName = "Chi Nhánh Xí Nghiệp Đầu Máy Hà Nội";
                        }
                        if (rowCoBaoGA.DvdmID == "YV")
                        {
                            rowCoBaoGA.DvdmID = "HN";
                            rowCoBaoGA.DvdmName = "Chi Nhánh Xí Nghiệp Đầu Máy Hà Nội";
                        }
                        if (rowCoBaoGA.DvcbID == "DN")
                        {
                            rowCoBaoGA.DvcbID = "SG";
                            rowCoBaoGA.DvcbName = "Chi Nhánh Xí Nghiệp Đầu Máy Sài Gòn";
                        }
                        if (rowCoBaoGA.DvdmID == "DN")
                        {
                            rowCoBaoGA.DvdmID = "SG";
                            rowCoBaoGA.DvdmName = "Chi Nhánh Xí Nghiệp Đầu Máy Sài Gòn";
                        }
                    }
                    listdoanthongct=DoanThongGADAO.fnPhanBoNhienLieu(rowCoBaoGA, listdoanthongct);
                    //2.Định mức nhiên liệu                   
                    DinhMucGADAO.YVNapNLDinhMuc(rowCoBaoGA, listcobaoct, listdoanthongct);
                    //if (rowCoBaoGA.DvcbID == "HN")
                    //{
                    DinhMucGADAO.HNNapNLDinhMuc(rowCoBaoGA, listdoanthongct);
                    //    foreach (DoanThongGACT ct in listdoanthongct)
                    //    {
                    //        ct.DinhMuc = ct.TieuThu;
                    //    }
                    //}
                    DinhMucGADAO.VINapNLDinhMuc(rowCoBaoGA,listcobaoct, listdoanthongct);
                    if (rowCoBaoGA.DvcbID == "DN")
                    {
                        //DinhMucDAO.DNNapNLDinhMuc(rowCoBao, listdoanthongct);
                        foreach (DoanThongGACT ct in listdoanthongct)
                        {
                            ct.DinhMuc = ct.TieuThu;
                        }
                    }
                    DinhMucGADAO.SGNapNLDinhMuc(rowCoBaoGA, listcobaoct, listdoanthongct);

                    //3. định mức dầu mỡ
                    decimal kmToTal = listdoanthongct.Sum(x => x.KMChinh + x.KMDon + x.KMGhep + x.KMDay);
                    DinhMucGADAO.YVNapDMDinhMuc(rowCoBaoGA, kmToTal, listdoanthongdm);
                    if (rowCoBaoGA.DvcbID == "HN")
                    {
                        DinhMucGADAO.HNNapDMDinhMuc(rowCoBaoGA, kmToTal, listdoanthongdm);
                    }
                    DinhMucGADAO.VINapDMDinhMuc(rowCoBaoGA, kmToTal, listdoanthongdm);

                    coBaoALL.CoBaoID = rowCoBaoGA.CoBaoID;
                    coBaoALL.coBaoGAs = rowCoBaoGA;
                    //if (listcobaoct.Count > 0)
                    coBaoALL.coBaoGAs.coBaoGACTs = listcobaoct;
                    //if (listcobaodm.Count > 0)
                    coBaoALL.coBaoGAs.coBaoGADMs = listcobaodm;
                    coBaoALL.doanThongGAs = doanThong;
                    //if (listcobaoct.Count > 0)                   
                    coBaoALL.doanThongGAs.doanThongGACTs = listdoanthongct;
                    //if (listcobaodm.Count > 0)
                    coBaoALL.doanThongGAs.doanThongGADMs = listdoanthongdm;
                }
                catch (Exception ex)
                {
                    Library.DialogHelper.Error("Lỗi nạp dữ liệu: " + ex.Message);
                    return;
                }
                //Lưu dữ liệu về db
                try
                {
                    if (!addNew)
                    {
                        //Sửa cơ báo
                        try
                        {
                            var objcb = await HttpHelper.Put<CoBaoGAALL>(Configuration.UrlCBApi + "api/CoBaoGAs/PutCoBaoGAALL?id=" + rowCoBaoGA.CoBaoID, coBaoALL);
                            if (objcb == null) throw new Exception("Lỗi lưu sửa cơ báo: " + rowCoBaoGA.CoBaoID + "-" + rowCoBaoGA.SoCB);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Lỗi sửa cơ báo: " + ex.Message);
                        }
                    }
                    else
                    {
                        //Thêm cơ báo
                        try
                        {
                            if (coBaoALL.coBaoGAs.TrangThai == "Chưa chuyển")
                            {
                                coBaoALL.coBaoGAs.TrangThai = "Đã chuyển";
                            }
                            else
                            {
                                coBaoALL.coBaoGAs.TrangThai = "Thêm mới";
                                rowCoBaoGA.Createddate = DateTime.Now;
                                rowCoBaoGA.Createdby = AppGlobal.User.Username;
                                rowCoBaoGA.CreatedName = AppGlobal.User.FullName;
                            }
                            var objcb = await HttpHelper.Post<CoBaoGAALL>(Configuration.UrlCBApi + "api/CoBaoGAs/PostCoBaoGAALL", coBaoALL);
                            if (objcb == null) throw new Exception("Lỗi lưu thêm cơ báo: " + rowCoBaoGA.CoBaoGoc + "-" + rowCoBaoGA.SoCB);
                            rowCoBaoGA.CoBaoID = objcb.CoBaoID;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Lỗi thêm cơ báo: " + ex.Message);
                        }
                    }
                    string strResult = await DoanThongGADAO.NapThanhTich(rowCoBaoGA.CoBaoID);
                    DialogHelper.Inform(strResult);
                    CoBaoGAForm.Instance.ShowThanhTich(strResult);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    Library.DialogHelper.Error(ex.Message);
                    rowCoBaoGA.TrangThai = trangThaiOld;
                }
            }
        }
        private void btnHuyCB_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
        
    }
}
