using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using CBClient.Library;
using System.Text.RegularExpressions;
using System.Linq;
using CBClient.BLLTypes;
using CBClient.Services;
using CBClient.BLLDaos;
using CBClient.Models;
using System.ComponentModel;

namespace CBClient.NhapLieu
{
    public partial class NhapCBDTDialog : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Properites
        public CoBao rowCoBao;
        public CoBao rowCoBaoOld;
        public CoBao rowCoBaoPrev;
        public List<CoBaoCT> listcobaoct = new List<CoBaoCT>();
        public List<CoBaoDM> listcobaodm = new List<CoBaoDM>();
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
        public NhapCBDTDialog(CoBao _rowCoBao, List<CoBaoCT> _listcobaoct, List<CoBaoDM> _listcobaodm)
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty);
            InitializeComponent();
            KeyPerssEven();
            rowCoBao = _rowCoBao;
            rowCoBaoOld = _rowCoBao;
           
            listcobaoct = _listcobaoct;
            listcobaodm = _listcobaodm;
            cobaoID = _rowCoBao.CoBaoID;            
            addNew = rowCoBao.CoBaoID <= 0 ? true : false;
            if (_listcobaoct.Count > 0 && addNew)
            {
                var row = _listcobaoct.Last();
                rowCoBao.CoBaoID = row.CoBaoID;
                rowCoBao.VaoKho = row.GioDi;
                rowCoBao.GiaoMay = rowCoBao.VaoKho;
                rowCoBao.XuongBan = rowCoBao.GiaoMay; 
            }            

        }
        private async void NhapCBDialog_Load(object sender, EventArgs e)
        {
            //if (rowCoBao.CoBaoID > 0)
            //    rowCoBao = await HttpHelper.Get<CoBao>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoID?id=" + rowCoBao.CoBaoID);
            FnTuboDulieu();
            FnAutoComplete();
            bindControlToData();
            AppGlobal.MactauList = HttpHelper.GetList<MacTau>(Configuration.UrlCBApi + "api/MacTaus/GetMacTau?CongTac=0&MacTau=")
                 .OrderBy(x => x.MacTauID).OrderBy(x => x.CongTacID).ToList();
            // Nhiên liệu ban trước               
            string data = "?NgayGM=" + rowCoBao.GiaoMay + "&LoaiMay=" + rowCoBao.LoaiMayID + "&DauMay=" + rowCoBao.DauMayID + "&CoBaoGoc=" + rowCoBao.CoBaoGoc;
            rowCoBaoPrev = await HttpHelper.Get<CoBao>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoPrev" + data);

        }
        private void NhapCBDTDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                AppGlobal.LoadServiceData();
                FnAutoComplete();
            }
        }
        private void dgCoBaoCT_SelectionChanged(object sender, EventArgs e)
        {
            if (dgCoBaoCT.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CoBaoCT cobaoct = (CoBaoCT)dgCoBaoCT.CurrentRow.DataBoundItem;
                    bindControlToDataCT(cobaoct);
                    addNewCT = false;
                    this.Cursor = Cursors.Default;
                }

                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    Library.DialogHelper.Error(ex.Message);
                }

            }
        }
        private void dgCoBaoDM_SelectionChanged(object sender, EventArgs e)
        {
            //if (dgCoBaoDM.CurrentRow != null)
            //{
            //    try
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        CoBaoDM cobaodm = (CoBaoDM)dgCoBaoDM.CurrentRow.DataBoundItem;
            //        cboLoaiDM.SelectedValue = cobaodm.LoaiDauMoID;                    
            //        txtNhanDM.Text = cobaodm.Nhan.ToString();
            //        txtLinhDM.Text = cobaodm.Linh <= 0 ? "" : cobaodm.Linh.ToString();
            //        if (cobaodm.MaTram == null)
            //        {
            //            cboTramDM.SelectedIndex = -1;
            //        }
            //        else
            //        {
            //            cboTramDM.SelectedValue =cobaodm.MaTram;                       
            //        }
            //        txtGiaoDM.Text = cobaodm.Giao.ToString();
            //        this.Cursor = Cursors.Default;
            //    }

            //    catch (Exception ex)
            //    {
            //        this.Cursor = Cursors.Default;
            //        DialogHelper.Error(ex.Message);
            //    }

            //}
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
        private void txtLenBan_Validated(object sender, EventArgs e)
        {
            rowCoBao.NgayCB = sdNgayCB.Value;
            txtNhanMay.Text = !string.IsNullOrWhiteSpace(txtNhanMay.Text) ? txtNhanMay.Text : txtLenBan.Text;
            string LenBan = txtLenBan.Text.Length == 3 ? "0" + txtLenBan.Text.Substring(0, 1) + ":" + txtLenBan.Text.Substring(1) : txtLenBan.Text.Substring(0, 2) + ":" + txtLenBan.Text.Substring(2);
            rowCoBao.LenBan = DateTime.Parse(rowCoBao.NgayCB.ToShortDateString() + " " + LenBan);
            if (rowCoBao.LenBan < rowCoBao.NgayCB) rowCoBao.LenBan = rowCoBao.LenBan.AddDays(1);
        }
        private async void txtNhanMay_Validated(object sender, EventArgs e)
        {
            txtRaKho.Text = !string.IsNullOrWhiteSpace(txtRaKho.Text) ? txtRaKho.Text : txtNhanMay.Text;
            string NhanMay = txtNhanMay.Text.Length == 3 ? "0" + txtNhanMay.Text.Substring(0, 1) + ":" + txtNhanMay.Text.Substring(1) : txtNhanMay.Text.Substring(0, 2) + ":" + txtNhanMay.Text.Substring(2);
            rowCoBao.NhanMay = DateTime.Parse(rowCoBao.LenBan.ToShortDateString() + " " + NhanMay);
            if (rowCoBao.NhanMay < rowCoBao.LenBan) rowCoBao.NhanMay = rowCoBao.NhanMay.AddDays(1);
            if (addNew)
            {
                // Nhiên liệu ban trước               
                string data = "?NgayGM=" + rowCoBao.GiaoMay + "&LoaiMay=" + rowCoBao.LoaiMayID + "&DauMay=" + rowCoBao.DauMayID + "&CoBaoGoc=" + rowCoBao.CoBaoGoc;
               rowCoBaoPrev = await HttpHelper.Get<CoBao>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoPrev" + data);
                if (rowCoBaoPrev != null)
                {
                    rowCoBao.NLBanTruoc = rowCoBaoPrev.NLBanSau;
                    txtNLBanTruoc.Text = rowCoBao.NLBanTruoc > 0 ? rowCoBao.NLBanTruoc.ToString() : string.Empty;
                    if (rowCoBao.NhanMay < rowCoBaoPrev.GiaoMay) rowCoBao.NhanMay = rowCoBaoPrev.GiaoMay;
                    txtNhanMay.Text = rowCoBao.NhanMay == null ? string.Empty : rowCoBao.NhanMay.ToString("HHmm");
                }
                
            }

        }
        private void txtRaKho_Validated(object sender, EventArgs e)
        {
            string RaKho = txtRaKho.Text.Length == 3 ? "0" + txtRaKho.Text.Substring(0, 1) + ":" + txtRaKho.Text.Substring(1) : txtRaKho.Text.Substring(0, 2) + ":" + txtRaKho.Text.Substring(2);
            rowCoBao.RaKho = DateTime.Parse(rowCoBao.NhanMay.ToShortDateString() + " " + RaKho);
            if (rowCoBao.RaKho < rowCoBao.NhanMay) rowCoBao.RaKho = rowCoBao.RaKho.AddDays(1);
        }
        private void txtVaoKho_Validated(object sender, EventArgs e)
        {
            txtGiaoMay.Text = !string.IsNullOrWhiteSpace(txtGiaoMay.Text) ? txtGiaoMay.Text : txtVaoKho.Text;
            string VaoKho = txtVaoKho.Text.Length == 3 ? "0" + txtVaoKho.Text.Substring(0, 1) + ":" + txtVaoKho.Text.Substring(1) : txtVaoKho.Text.Substring(0, 2) + ":" + txtVaoKho.Text.Substring(2);
            rowCoBao.VaoKho = DateTime.Parse(rowCoBao.RaKho.ToShortDateString() + " " + VaoKho);
            if (rowCoBao.VaoKho < rowCoBao.RaKho) rowCoBao.VaoKho = rowCoBao.VaoKho.AddDays(1);
            var row = listcobaoct.LastOrDefault();
            if(row!=null)
            {
                rowCoBao.VaoKho = DateTime.Parse(row.GioDi.ToShortDateString() + " " + VaoKho);
                if (rowCoBao.VaoKho< row.GioDi) rowCoBao.VaoKho = rowCoBao.VaoKho.AddDays(1);
            }    
        }
        private void txtGiaoMay_Validated(object sender, EventArgs e)
        {
            txtXuongBan.Text = !string.IsNullOrWhiteSpace(txtXuongBan.Text) ? txtXuongBan.Text : txtGiaoMay.Text;
            string GiaoMay = txtGiaoMay.Text.Length == 3 ? "0" + txtGiaoMay.Text.Substring(0, 1) + ":" + txtGiaoMay.Text.Substring(1) : txtGiaoMay.Text.Substring(0, 2) + ":" + txtGiaoMay.Text.Substring(2);
            rowCoBao.GiaoMay = DateTime.Parse(rowCoBao.VaoKho.ToShortDateString() + " " + GiaoMay);
            if (rowCoBao.GiaoMay < rowCoBao.VaoKho) rowCoBao.GiaoMay = rowCoBao.GiaoMay.AddDays(1);
        }
        private void txtXuongBan_Validated(object sender, EventArgs e)
        {
            string XuongBan = txtXuongBan.Text.Length == 3 ? "0" + txtXuongBan.Text.Substring(0, 1) + ":" + txtXuongBan.Text.Substring(1) : txtXuongBan.Text.Substring(0, 2) + ":" + txtXuongBan.Text.Substring(2);
            rowCoBao.XuongBan = DateTime.Parse(rowCoBao.GiaoMay.ToShortDateString() + " " + XuongBan);
            if (rowCoBao.XuongBan < rowCoBao.GiaoMay) rowCoBao.XuongBan = rowCoBao.XuongBan.AddDays(1);
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
            if (!string.IsNullOrWhiteSpace(txtKMAdd.Text) && !regexNumber.IsMatch(txtKMAdd.Text))
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Km add không đúng\r\n";
            }
            return errMessage;
        }
        private string checkHoanThanh(DoanThong dt, List<DoanThongCT> listct)
        {
            string errMessage = string.Empty;
            int intCount = 0;
            if (dt.DungKB<0)
            {
                intCount += 1;
                errMessage += intCount.ToString() + ".Giờ nhận máy cơ báo: "+ rowCoBao.SoCB+" - "+ rowCoBao.NhanMay.ToString("dd.MM.yyyy HH:mm") + " nhỏ hơn giờ giao máy của cơ báo trước: " 
                    + rowCoBaoPrev.SoCB+" - "+ rowCoBao.GiaoMay.ToString("dd.MM.yyyy HH:mm") +". Số phút: " +dt.DungKB + " phút.\r\n";
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
            FormHelper.AddKeyPressEventHandlerForNumber(txtDauMay);
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
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTanSoCT);
            FormHelper.AddKeyPressEventHandlerForDecimal(txtTanRongCT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtTinhChatCT);
            FormHelper.AddKeyPressEventHandlerForNumber(txtMayGhepCT);
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

            txtPhoXe1ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtPhoXe1ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe1ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe2ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtTaiXe2ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe2ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe2ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtPhoXe2ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe2ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe3ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtTaiXe3ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe3ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe3ID.AutoCompleteCustomSource = AppGlobal.MaTaiXeAutoComplate;
            txtPhoXe3ID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe3ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe1Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtTaiXe1Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe1Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe1Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtPhoXe1Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe1Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe2Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtTaiXe2Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe2Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe2Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtPhoXe2Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe2Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTaiXe3Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtTaiXe3Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaiXe3Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtPhoXe3Name.AutoCompleteCustomSource = AppGlobal.TenTaiXeAutoComplate;
            txtPhoXe3Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPhoXe3Name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        
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
            sdNgayXPCT.Value = sdNgayCB.Value;

            txtMacTauCT.AutoCompleteCustomSource = AppGlobal.MacTauAutoComplate;
            txtMacTauCT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMacTauCT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtGaDiCT.AutoCompleteCustomSource = AppGlobal.MaGaAutoComplate;
            txtGaDiCT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGaDiCT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtTinhChatCT.AutoCompleteCustomSource = AppGlobal.MaTinhChatAutoComplate;
            txtTinhChatCT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTinhChatCT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtMayGhepCT.AutoCompleteCustomSource = AppGlobal.MaDauMayAutoComplate;
            txtMayGhepCT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMayGhepCT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
        private async void FnTuboDulieu()
        {   
            if (listcobaoct.Count > 0 && !addNew)
            {
                try
                {                   
                    var listcobaoctOld = HttpHelper.GetList<CoBaoCT>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoCTByCoBaoGoc?id=" + rowCoBao.CoBaoGoc);
                    foreach (CoBaoCT ct in listcobaoct)//Thêm khi thiếu
                    {
                        var cbOld = listcobaoctOld.Where(x => x.NgayXP == ct.NgayXP && x.GioDen == ct.GioDen && x.GioDi == ct.GioDi
                        && x.MacTauID == ct.MacTauID && x.GaID == ct.GaID && x.TinhChatID == ct.TinhChatID).FirstOrDefault();
                        if (cbOld != null)
                        {
                            ct.ID = cbOld.ID;
                            ct.CoBaoID = cbOld.CoBaoID;
                        }
                        else
                        {
                            ct.CoBaoID = rowCoBao.CoBaoID;
                            ct.ID = 0;
                            var objcbCT = await HttpHelper.Post<CoBaoCT>(Configuration.UrlCBApi + "api/CoBaos/PostCoBaoCT", ct);
                        }
                    }
                    foreach (CoBaoCT ct in listcobaoctOld)//Xóa khi thừa
                    {
                        var cbOld = listcobaoct.Where(x => x.NgayXP == ct.NgayXP && x.GioDen == ct.GioDen && x.GioDi == ct.GioDi
                        && x.MacTauID == ct.MacTauID && x.GaID == ct.GaID && x.TinhChatID == ct.TinhChatID).FirstOrDefault();
                        if (cbOld != null) continue;
                        else
                        {
                            var objcbCT = await HttpHelper.Delete<CoBaoCT>(Configuration.UrlCBApi + "api/CoBaos/DeleteCoBaoCT?id=" + ct.ID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Library.DialogHelper.Error("Lỗi Tu bổ dữ liệu: " + ex.Message);
                    return;
                }
            }
            else if(addNew)
            {
                // Nhiên liệu ban trước
                string data = "?NgayNM=" + rowCoBao.NhanMay + "&LoaiMay=" + rowCoBao.LoaiMayID + "&DauMay=" + rowCoBao.DauMayID;
                var rowCoBaoPrev = await HttpHelper.Get<CoBao>(Configuration.UrlCBApi + "api/CoBaos/GetCoBaoPrev" + data);
                if (rowCoBaoPrev != null)
                {
                    rowCoBao.NLBanTruoc = rowCoBaoPrev.NLBanSau;
                    txtNLBanTruoc.Text = rowCoBao.NLBanTruoc > 0 ? rowCoBao.NLBanTruoc.ToString() : string.Empty;
                }                  
            }    
           
        }
        private void bindControlToData()
        {
            txtDauMay.Text = rowCoBao.DauMayID;
            txtSoCB.Text = rowCoBao.SoCB;
            sdNgayCB.Value = rowCoBao.NgayCB;
            txtRutGio.Text = rowCoBao.RutGio > 0 ? rowCoBao.RutGio.ToString() : string.Empty;
            txtChatLuong.Text = rowCoBao.ChatLuong;
            txtSLRK.Text = rowCoBao.SoLanRaKho > 0 ? FormHelper.ConvertString(rowCoBao.SoLanRaKho.ToString()) : "0.5";

            txtTaiXe1ID.Text = rowCoBao.TaiXe1ID;
            txtTaiXe1Name.Text = rowCoBao.TaiXe1Name;
            txtTaiXe1GioLT.Text = rowCoBao.TaiXe1GioLT > 0 ? rowCoBao.TaiXe1GioLT.ToString() : string.Empty;
            txtPhoXe1ID.Text = rowCoBao.PhoXe1ID;
            txtPhoXe1Name.Text = rowCoBao.PhoXe1Name;
            txtPhoXe1GioLT.Text = rowCoBao.PhoXe1GioLT > 0 ? rowCoBao.PhoXe1GioLT.ToString() : string.Empty;
            txtTaiXe2ID.Text = rowCoBao.TaiXe2ID;
            txtTaiXe2Name.Text = rowCoBao.TaiXe2Name;
            txtTaiXe2GioLT.Text = rowCoBao.TaiXe2GioLT > 0 ? rowCoBao.TaiXe2GioLT.ToString() : string.Empty;
            txtPhoXe2ID.Text = rowCoBao.PhoXe2ID;
            txtPhoXe2Name.Text = rowCoBao.PhoXe2Name;
            txtPhoXe2GioLT.Text = rowCoBao.PhoXe2GioLT > 0 ? rowCoBao.PhoXe2GioLT.ToString() : string.Empty;
            txtTaiXe3ID.Text = rowCoBao.TaiXe3ID;
            txtTaiXe3Name.Text = rowCoBao.TaiXe3Name;
            txtTaiXe3GioLT.Text = rowCoBao.TaiXe3GioLT > 0 ? rowCoBao.TaiXe3GioLT.ToString() : string.Empty;
            txtPhoXe3ID.Text = rowCoBao.PhoXe3ID;
            txtPhoXe3Name.Text = rowCoBao.PhoXe3Name;
            txtPhoXe3GioLT.Text = rowCoBao.PhoXe3GioLT > 0 ? rowCoBao.PhoXe3GioLT.ToString() : string.Empty;

            txtLenBan.Text = rowCoBao.LenBan == null ? string.Empty : rowCoBao.LenBan.ToString("HHmm");
            txtNhanMay.Text = rowCoBao.NhanMay == null ? string.Empty : rowCoBao.NhanMay.ToString("HHmm");
            txtRaKho.Text = rowCoBao.RaKho == null ? string.Empty : rowCoBao.RaKho.ToString("HHmm");
            txtVaoKho.Text = rowCoBao.VaoKho == null ? string.Empty : rowCoBao.VaoKho.ToString("HHmm");
            txtGiaoMay.Text = rowCoBao.GiaoMay == null ? string.Empty : rowCoBao.GiaoMay.ToString("HHmm");
            txtXuongBan.Text = rowCoBao.XuongBan == null ? string.Empty : rowCoBao.XuongBan.ToString("HHmm");

            txtNLBanTruoc.Text = rowCoBao.NLBanTruoc > 0 ? rowCoBao.NLBanTruoc.ToString() : "0";
            txtNLBanNhan.Text = rowCoBao.NLThucNhan > 0 ? rowCoBao.NLThucNhan.ToString() : "0";
            txtNLLinh.Text = rowCoBao.NLLinh > 0 ? rowCoBao.NLLinh.ToString() : string.Empty;
            cboTramNL.SelectedValue = rowCoBao.TramNLID;
            txtNLTrongDo.Text = rowCoBao.NLTrongDoan > 0 ? FormHelper.ConvertString(rowCoBao.NLTrongDoan.ToString()) : string.Empty;
            txtNLBanSau.Text = rowCoBao.NLBanSau > 0 ? rowCoBao.NLBanSau.ToString() :"0"; 
           
            txtSHDT.Text = rowCoBao.SHDT;
            txtMaCB.Text = rowCoBao.MaCB;
            txtDonDD.Text = rowCoBao.DonDocDuong > 0 ? FormHelper.ConvertString(rowCoBao.DonDocDuong.ToString()) : string.Empty;
            txtDungDD.Text = rowCoBao.DungDocDuong > 0 ? FormHelper.ConvertString(rowCoBao.DungDocDuong.ToString()) : string.Empty;
            txtDungNM.Text = rowCoBao.DungNoMay > 0 ? FormHelper.ConvertString(rowCoBao.DungNoMay.ToString()) : string.Empty;
            txtGhiChu.Text = rowCoBao.GhiChu;

            dgCoBaoDM.DataSource = listcobaodm;
            dgCoBaoCT.DataSource = listcobaoct;
            lblCoBaoCT.Text = "Tổng số cơ báo chi tiết:" + listcobaoct.Count.ToString("N0");
        }
        private void bindDataToControl()
        {
            try
            {
                if (addNew) rowCoBao.CoBaoID = 0;
                rowCoBao.DauMayID = txtDauMay.Text;
                rowCoBao.SoCB = txtSoCB.Text;
                rowCoBao.NgayCB = sdNgayCB.Value;
                rowCoBao.RutGio = string.IsNullOrWhiteSpace(txtRutGio.Text) ? 0 : int.Parse(txtRutGio.Text);
                rowCoBao.ChatLuong = txtChatLuong.Text.Replace(" ", "");
                rowCoBao.SoLanRaKho = string.IsNullOrWhiteSpace(txtSLRK.Text) ? 0 : decimal.Parse(txtSLRK.Text, FormHelper.EnCultureInfo);

                rowCoBao.TaiXe1ID = txtTaiXe1ID.Text;
                rowCoBao.TaiXe1Name = txtTaiXe1Name.Text;
                rowCoBao.TaiXe1GioLT = string.IsNullOrWhiteSpace(txtTaiXe1GioLT.Text) ? (short)0 : short.Parse(txtTaiXe1GioLT.Text);
                rowCoBao.PhoXe1ID = txtPhoXe1ID.Text;
                rowCoBao.PhoXe1Name = txtPhoXe1Name.Text;
                rowCoBao.PhoXe1GioLT = string.IsNullOrWhiteSpace(txtPhoXe1GioLT.Text) ? (short)0 : short.Parse(txtPhoXe1GioLT.Text);
                rowCoBao.TaiXe2ID = txtTaiXe2ID.Text;
                rowCoBao.TaiXe2Name = txtTaiXe2Name.Text;
                rowCoBao.TaiXe2GioLT = string.IsNullOrWhiteSpace(txtTaiXe2GioLT.Text) ? (short)0 : short.Parse(txtTaiXe2GioLT.Text);
                rowCoBao.PhoXe2ID = txtPhoXe2ID.Text;
                rowCoBao.PhoXe2Name = txtPhoXe2Name.Text;
                rowCoBao.PhoXe2GioLT = string.IsNullOrWhiteSpace(txtPhoXe2GioLT.Text) ? (short)0 : short.Parse(txtPhoXe2GioLT.Text);
                rowCoBao.TaiXe3ID = txtTaiXe3ID.Text;
                rowCoBao.TaiXe3Name = txtTaiXe3Name.Text;
                rowCoBao.TaiXe3GioLT = string.IsNullOrWhiteSpace(txtTaiXe3GioLT.Text) ? (short)0 : short.Parse(txtTaiXe3GioLT.Text);
                rowCoBao.PhoXe3ID = txtPhoXe3ID.Text;
                rowCoBao.PhoXe3Name = txtPhoXe3Name.Text;
                rowCoBao.PhoXe3GioLT = string.IsNullOrWhiteSpace(txtPhoXe3GioLT.Text) ? (short)0 : short.Parse(txtPhoXe3GioLT.Text);
                string LenBan = txtLenBan.Text.Length == 3 ? "0" + txtLenBan.Text.Substring(0, 1) + ":" + txtLenBan.Text.Substring(1) : txtLenBan.Text.Substring(0, 2) + ":" + txtLenBan.Text.Substring(2);
                rowCoBao.LenBan = DateTime.Parse(rowCoBao.LenBan.ToShortDateString() + " " + LenBan);
                string NhanMay = txtNhanMay.Text.Length == 3 ? "0" + txtNhanMay.Text.Substring(0, 1) + ":" + txtNhanMay.Text.Substring(1) : txtNhanMay.Text.Substring(0, 2) + ":" + txtNhanMay.Text.Substring(2);
                rowCoBao.NhanMay = DateTime.Parse(rowCoBao.NhanMay.ToShortDateString() + " " + NhanMay);
                if (rowCoBao.NhanMay < rowCoBao.LenBan) rowCoBao.NhanMay=rowCoBao.NhanMay.AddDays(1);
                string RaKho = txtRaKho.Text.Length == 3 ? "0" + txtRaKho.Text.Substring(0, 1) + ":" + txtRaKho.Text.Substring(1) : txtRaKho.Text.Substring(0, 2) + ":" + txtRaKho.Text.Substring(2);
                rowCoBao.RaKho = DateTime.Parse(rowCoBao.RaKho.ToShortDateString() + " " + RaKho);
                if (rowCoBao.RaKho < rowCoBao.NhanMay) rowCoBao.RaKho=rowCoBao.RaKho.AddDays(1);
                string VaoKho = txtVaoKho.Text.Length == 3 ? "0" + txtVaoKho.Text.Substring(0, 1) + ":" + txtVaoKho.Text.Substring(1) : txtVaoKho.Text.Substring(0, 2) + ":" + txtVaoKho.Text.Substring(2);
                rowCoBao.VaoKho = DateTime.Parse(rowCoBao.VaoKho.ToShortDateString() + " " + VaoKho);
                if (rowCoBao.VaoKho < rowCoBao.RaKho) rowCoBao.VaoKho=rowCoBao.VaoKho.AddDays(1);
                string GiaoMay = txtGiaoMay.Text.Length == 3 ? "0" + txtGiaoMay.Text.Substring(0, 1) + ":" + txtGiaoMay.Text.Substring(1) : txtGiaoMay.Text.Substring(0, 2) + ":" + txtGiaoMay.Text.Substring(2);
                rowCoBao.GiaoMay = DateTime.Parse(rowCoBao.GiaoMay.ToShortDateString() + " " + GiaoMay);
                if (rowCoBao.GiaoMay < rowCoBao.VaoKho) rowCoBao.GiaoMay=rowCoBao.GiaoMay.AddDays(1);
                string XuongBan = txtXuongBan.Text.Length == 3 ? "0" + txtXuongBan.Text.Substring(0, 1) + ":" + txtXuongBan.Text.Substring(1) : txtXuongBan.Text.Substring(0, 2) + ":" + txtXuongBan.Text.Substring(2);
                rowCoBao.XuongBan = DateTime.Parse(rowCoBao.XuongBan.ToShortDateString() + " " + XuongBan);
                if (rowCoBao.XuongBan < rowCoBao.GiaoMay) rowCoBao.XuongBan=rowCoBao.XuongBan.AddDays(1);

                rowCoBao.NLBanTruoc = string.IsNullOrWhiteSpace(txtNLBanTruoc.Text) ? 0 : int.Parse(txtNLBanTruoc.Text);
                rowCoBao.NLThucNhan = string.IsNullOrWhiteSpace(txtNLBanNhan.Text) ? 0 : int.Parse(txtNLBanNhan.Text);
                rowCoBao.NLLinh = string.IsNullOrWhiteSpace(txtNLLinh.Text) ? 0 : int.Parse(txtNLLinh.Text);
                rowCoBao.TramNLID = string.IsNullOrWhiteSpace(txtNLLinh.Text) ? string.Empty : cboTramNL.SelectedValue.ToString();
                rowCoBao.NLTrongDoan = string.IsNullOrWhiteSpace(txtNLTrongDo.Text) ? 0 : decimal.Parse(txtNLTrongDo.Text, FormHelper.EnCultureInfo);
                rowCoBao.NLBanSau = string.IsNullOrWhiteSpace(txtNLBanSau.Text) ? 0 : int.Parse(txtNLBanSau.Text);

                rowCoBao.SHDT = txtSHDT.Text;
                rowCoBao.MaCB = txtMaCB.Text;
                rowCoBao.DonDocDuong = !string.IsNullOrWhiteSpace(txtDonDD.Text) ? decimal.Parse(txtDonDD.Text, FormHelper.EnCultureInfo) : 0;
                rowCoBao.DungDocDuong = !string.IsNullOrWhiteSpace(txtDungDD.Text) ? decimal.Parse(txtDungDD.Text, FormHelper.EnCultureInfo) : 0;
                rowCoBao.DungNoMay = !string.IsNullOrWhiteSpace(txtDungNM.Text) ? decimal.Parse(txtDungNM.Text, FormHelper.EnCultureInfo) : 0;
                rowCoBao.GhiChu = txtGhiChu.Text;

                rowCoBao.Modifydate = DateTime.Now;
                rowCoBao.Modifyby = AppGlobal.User.Username;
                rowCoBao.ModifyName = AppGlobal.User.FullName;
                rowCoBao.TrangThai = "Đã hoàn thành";
                if (addNew) rowCoBao.KhoaCB = false;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nạp cơ báo: " + ex.Message);
            }
        }
        private void bindAPIToControl(BCCoBaoTTInfo info, ref Common.PartnerThanhTichInput input)
        {
            try
            {
                input.CoBaoID = rowCoBao.CoBaoGoc;
                input.CoBaoTach = rowCoBao.CoBaoID.ToString();
                input.ChatLuong = rowCoBao.ChatLuong;
                input.SoLanRaKho = rowCoBao.SoLanRaKho;
                input.GioVaoKho = rowCoBao.VaoKho;
                input.GioGiaoMay = rowCoBao.GiaoMay;
                input.GioXuongBan = rowCoBao.XuongBan;

                input.NhienLieu_BanTruoc = rowCoBao.NLBanTruoc;
                input.NhienLieu_BanNhan = rowCoBao.NLThucNhan;
                input.NhienLieu_Linh = rowCoBao.NLLinh;
                input.NhienLieu_MaTram = rowCoBao.TramNLID;
                input.NhienLieu_TrongDo = rowCoBao.NLTrongDoan;
                input.NhienLieu_BanSau = rowCoBao.NLBanSau;            

                input.SoHieuDuoiTau = rowCoBao.SHDT;
                input.MaCoBao = rowCoBao.MaCB;
                input.DonDocDuong = rowCoBao.DonDocDuong;
                input.DungDocDuong = rowCoBao.DungDocDuong;
                input.DungNoMay = rowCoBao.DungNoMay;
                if (info != null)
                {
                    input.ThanhTich_QuayVong = info.GioDM;
                    input.ThanhTich_LuHanh = info.GioLH;
                    input.ThanhTich_DonThuan = info.GioDT;
                    input.ThanhTich_Don = info.GioDon;
                    input.ThanhTich_KmChay = info.KMChay;
                    input.ThanhTich_TanKM = info.TKM;
                    input.ThanhTich_NLDinhMuc = info.DinhMuc;
                    input.ThanhTich_NLTieuThu = info.TieuThu;
                    input.ThanhTich_NLLoiLo = info.LoiLo;
                }
                input.Username = rowCoBao.Modifyby;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nạp thành tích: " + ex.Message);
            }
        }
        private void bindControlToDataCT(CoBaoCT row)
        {
            if (row == null) return;
            sdNgayXPCT.Value = row.NgayXP;
            txtGioDenCT.Text = row.GioDen.ToString("HHmm");
            txtGioDiCT.Text = row.GioDi.ToString("HHmm");
            txtGioDonCT.Text = FormHelper.ConvertString(row.GioDon.ToString());

            txtMacTauCT.Text = row.MacTauID.ToString();            
            txtGaDiCT.Text = row.GaName;

            txtTanSoCT.Text = FormHelper.ConvertString(row.Tan.ToString());
            txtTanRongCT.Text = FormHelper.ConvertString(row.TanXeRong.ToString());

            txtTinhChatCT.Text = row.TinhChatID.ToString();
            txtMayGhepCT.Text = row.MayGhepID.ToString(); ;
            txtKMAdd.Text = FormHelper.ConvertString(row.KmAdd.ToString());
        }
        private void bindDataToControlCT(ref CoBaoCT row)
        {
            row.CoBaoID = rowCoBao.CoBaoID;
            row.NgayXP = sdNgayXPCT.Value;
            if (!string.IsNullOrWhiteSpace(txtGioDenCT.Text))
            {
                string gioden = txtGioDenCT.Text.Length == 3 ? "0" + txtGioDenCT.Text.Substring(0, 1) + ":" + txtGioDenCT.Text.Substring(1) : txtGioDenCT.Text.Substring(0, 2) + ":" + txtGioDenCT.Text.Substring(2);
                row.GioDen = DateTime.Parse(row.NgayXP.ToShortDateString() + " " + gioden);
            }
            if (!string.IsNullOrWhiteSpace(txtGioDiCT.Text))
            {
                string giodi = txtGioDiCT.Text.Length == 3 ? "0" + txtGioDiCT.Text.Substring(0, 1) + ":" + txtGioDiCT.Text.Substring(1) : txtGioDiCT.Text.Substring(0, 2) + ":" + txtGioDiCT.Text.Substring(2);
                row.GioDi = DateTime.Parse(row.NgayXP.ToShortDateString() + " " + giodi);
                if (row.GioDen < row.GioDi) row.GioDi.AddDays(1);
            }
            if (!string.IsNullOrWhiteSpace(txtGioDonCT.Text))
                row.GioDon = decimal.Parse(txtGioDonCT.Text, FormHelper.EnCultureInfo);
            row.MacTauID = txtMacTauCT.Text;
            row.GaName = txtGaDiCT.Text;
            row.GaID = AppGlobal.GaDic.FirstOrDefault(x => x.Value == txtGaDiCT.Text).Key;            
            if (!string.IsNullOrWhiteSpace(txtTanSoCT.Text))
                row.Tan = decimal.Parse(txtTanSoCT.Text, FormHelper.EnCultureInfo);            
            if (!string.IsNullOrWhiteSpace(txtTanRongCT.Text))
                row.TanXeRong = decimal.Parse(txtTanRongCT.Text, FormHelper.EnCultureInfo);
            row.Tan = short.Parse(txtTinhChatCT.Text);
            row.MayGhepID = string.IsNullOrWhiteSpace(txtMayGhepCT.Text) ? string.Empty : txtMayGhepCT.Text;
            if (!string.IsNullOrWhiteSpace(txtKMAdd.Text))
                row.KmAdd = decimal.Parse(txtKMAdd.Text, FormHelper.EnCultureInfo);
        }        
        #endregion
        #region Keys_Press
        private void btnLuuCT_Click(object sender, EventArgs e)
        {
            string errMessageCT = checkValidateCT();
            if (!string.IsNullOrWhiteSpace(errMessageCT))
                Library.DialogHelper.Error(errMessageCT);
            else
            {
                CoBaoCT Row = new CoBaoCT();
                if (dgCoBaoCT.CurrentRow != null)
                {
                    Row = (CoBaoCT)dgCoBaoCT.CurrentRow.DataBoundItem;
                    bindDataToControlCT(ref Row);
                }
                dgCoBaoCT.DataSource = listcobaoct;
                lblCoBaoCT.Text = "Tổng số cơ báo chi tiết:" + listcobaoct.Count.ToString("N0");                            
            }
        }
        private void btnHuyCT_Click(object sender, EventArgs e)
        {
            dgCoBaoCT.DataSource = listcobaoct;
            lblCoBaoCT.Text = "Tổng số cơ báo chi tiết:" + listcobaoct.Count.ToString("N0");                       
        }
        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (dgCoBaoDM.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CoBaoDM cobaodm = (CoBaoDM)dgCoBaoDM.CurrentRow.DataBoundItem;
                    listcobaodm.Remove(cobaodm);
                    BindingList<CoBaoDM> lstBinding = new BindingList<CoBaoDM>();
                    foreach (CoBaoDM ct in listcobaodm)
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

        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            if (dgCoBaoDM.CurrentRow != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CoBaoDM cobaodm = (CoBaoDM)dgCoBaoDM.CurrentRow.DataBoundItem;
                    txtMaDM.Text = cobaodm.LoaiDauMoID.ToString();
                    txtTenDM.Text = cobaodm.LoaiDauMoName;
                    txtDVT.Text = cobaodm.DonViTinh;                   
                    txtNhanDM.Text =FormHelper.ConvertString(cobaodm.Nhan.ToString());
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
        private void btnLuuDM_Click(object sender, EventArgs e)
        {
            string errMessageCT = checkValidateDM();
            if (!string.IsNullOrWhiteSpace(errMessageCT))
                Library.DialogHelper.Error(errMessageCT);
            else
            {
                CoBaoDM row = new CoBaoDM();
                if (dgCoBaoDM.CurrentRow != null)//Sửa dẩu mỡ
                {
                    row = (CoBaoDM)dgCoBaoDM.CurrentRow.DataBoundItem;
                    if (txtMaDM.Text != row.LoaiDauMoID.ToString())
                    {
                        row = new CoBaoDM();
                        if (listcobaodm.Where(x => x.LoaiDauMoID == short.Parse(txtMaDM.Text)).FirstOrDefault() != null)
                        {
                            DialogHelper.Inform("Đã có dầu mỡ này.");
                            return;
                        }
                        row.CoBaoID = rowCoBao.CoBaoID;
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
                    row.CoBaoID = rowCoBao.CoBaoID;
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
                BindingList<CoBaoDM> lstBinding = new BindingList<CoBaoDM>();
                foreach (CoBaoDM ct in listcobaodm)
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
            string errMessage = checkValidate();
            if (!string.IsNullOrWhiteSpace(errMessage))
            {
                Library.DialogHelper.Error("Lỗi nhập liệu: " + errMessage);
                return;
            }
            else
            {
                CoBaoALL coBaoALL = new CoBaoALL();
                //Nạp dữ liệu
                try
                {
                    bindDataToControl();
                    //Nạp dữ liệu đoạn thống
                    DoanThong doanThong = await DoanThongDAO.bindDoanThongToCoBao(rowCoBao);
                    List<DoanThongDM> listdoanthongdm = listcobaodm.Count > 0 ? DoanThongDAO.bindDoanThongDM(rowCoBao, listcobaodm) : new List<DoanThongDM>();
                    List<DoanThongCT> listdoanthongct = listcobaoct.Count >= 0 ? DoanThongDAO.bindDoanThongCT(doanThong.DungKB,rowCoBao, listcobaoct) : new List<DoanThongCT>();
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
                    DateTime ngayCB = new DateTime(2023, 1, 1);
                    if (rowCoBao.NgayCB >= ngayCB)
                    {
                        if (rowCoBao.DvcbID == "YV")
                        {
                            rowCoBao.DvcbID = "HN";
                            rowCoBao.DvcbName = "Chi Nhánh Xí Nghiệp Đầu Máy Hà Nội";
                        }
                        if (rowCoBao.DvdmID == "YV")
                        {
                            rowCoBao.DvdmID = "HN";
                            rowCoBao.DvdmName = "Chi Nhánh Xí Nghiệp Đầu Máy Hà Nội";
                        }
                        if (rowCoBao.DvcbID == "DN")
                        {
                            rowCoBao.DvcbID = "SG";
                            rowCoBao.DvcbName = "Chi Nhánh Xí Nghiệp Đầu Máy Sài Gòn";
                        }
                        if (rowCoBao.DvdmID == "DN")
                        {
                            rowCoBao.DvdmID = "SG";
                            rowCoBao.DvdmName = "Chi Nhánh Xí Nghiệp Đầu Máy Sài Gòn";
                        }
                    }
                    //1.Phân bổ nhiên liệu
                    listdoanthongct=DoanThongDAO.fnPhanBoNhienLieu(rowCoBao, listdoanthongct);
                    //2.Định mức nhiên liệu                   
                    DinhMucDAO.YVNapNLDinhMuc(rowCoBao, listcobaoct, listdoanthongct);
                    //if (rowCoBao.DvcbID == "HN")
                    //{
                        DinhMucDAO.HNNapNLDinhMuc(rowCoBao, listdoanthongct);
                        //foreach (DoanThongCT ct in listdoanthongct)
                        //{
                        //    ct.DinhMuc = ct.TieuThu;
                        //}
                    //}
                    DinhMucDAO.VINapNLDinhMuc(rowCoBao,listcobaoct, listdoanthongct);
                    if (rowCoBao.DvcbID == "DN")
                    {
                        //DinhMucDAO.DNNapNLDinhMuc(rowCoBao, listdoanthongct);
                        foreach (DoanThongCT ct in listdoanthongct)
                        {
                            ct.DinhMuc = ct.TieuThu;
                        }
                    }
                    DinhMucDAO.SGNapNLDinhMuc(rowCoBao, listcobaoct, listdoanthongct);

                    //3. định mức dầu mỡ
                    decimal kmToTal = listdoanthongct.Sum(x => x.KMChinh + x.KMDon + x.KMGhep + x.KMDay);
                    DinhMucDAO.YVNapDMDinhMuc(rowCoBao, kmToTal, listdoanthongdm);
                    if (rowCoBao.DvcbID == "HN")
                    {
                        DinhMucDAO.HNNapDMDinhMuc(rowCoBao, kmToTal, listdoanthongdm);
                    }
                    DinhMucDAO.VINapDMDinhMuc(rowCoBao, kmToTal, listdoanthongdm);

                    coBaoALL.CoBaoID = rowCoBao.CoBaoID;
                    coBaoALL.coBaos = rowCoBao;
                    //if (listcobaoct.Count > 0)
                    coBaoALL.coBaos.coBaoCTs = listcobaoct;
                    //if (listcobaodm.Count > 0)
                    coBaoALL.coBaos.coBaoDMs = listcobaodm;                        
                    coBaoALL.doanThongs = doanThong;
                    //if (listcobaoct.Count > 0)
                    coBaoALL.doanThongs.doanThongCTs = listdoanthongct;
                    //if (listcobaodm.Count > 0)
                    coBaoALL.doanThongs.doanThongDMs = listdoanthongdm;
                }
                catch (Exception ex)
                {
                    rowCoBao = rowCoBaoOld;
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
                            var objcb = await HttpHelper.Put<CoBaoALL>(Configuration.UrlCBApi + "api/CoBaos/PutCoBaoALL?id=" + rowCoBao.CoBaoID, coBaoALL);
                            if (objcb == null) throw new Exception("Lỗi lưu sửa cơ báo: " + rowCoBao.CoBaoID + "-" + rowCoBao.SoCB);
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
                            var objcb = await HttpHelper.Post<CoBaoALL>(Configuration.UrlCBApi + "api/CoBaos/PostCoBaoALL", coBaoALL);
                            if (objcb == null) throw new Exception("Lỗi lưu thêm cơ báo: " + rowCoBao.CoBaoGoc + "-" + rowCoBao.SoCB);
                            rowCoBao.CoBaoID = objcb.CoBaoID;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Lỗi thêm cơ báo: " + ex.Message);
                        }

                    }
                    //Cập nhật thành tích về lõi QTHH
                    if (cobaoID >= 0)//Nếu lỗi=-1 thì không cập nhật lõi.
                    {
                        try
                        {
                            BCCoBaoTTInfo info = await CoBaoDAO.NapObThanhTich(rowCoBao.CoBaoID);
                            Common.PartnerThanhTichInput input = new Common.PartnerThanhTichInput();
                            bindAPIToControl(info, ref input);
                            var access_token = string.Format("Bearer {0}{1}", MainForm.Instance.Data.userClientId, MainForm.Instance.Data.access_token);
                            var res = await AuthenticationService.PartnerTCTFeedBackThanhTichByID(input, MainForm.Instance.Data.userName, access_token);
                            if (res == null)
                            {
                                throw new Exception("Không lấy được dữ liệu");
                            }
                            if (res.IsOK <= 0)
                            {
                                //var objcbd = await HttpHelper.Delete<CoBao>(Configuration.UrlCBApi + "api/CoBaos/DeleteCoBaoGocALL?id=" + rowCoBao.CoBaoGoc);
                                //if (objcbd == null) throw new Exception("Lỗi xóa cơ báo: " + rowCoBao.CoBaoGoc + "-" + rowCoBao.SoCB);
                                throw new Exception(res.msg);
                                //if (Library.DialogHelper.Confirm("Cảnh báo: " + res.msg + ".Bạn có chắc chắn cắt cơ báo: " + rowCoBao.CoBaoGoc + " không?.") == DialogResult.Yes)
                                //{
                                //    System.Diagnostics.Process.Start("http://vtds.vn/#/quanlycobao/detail/" + rowCoBao.CoBaoGoc);
                                //}
                                //rowCoBao = rowCoBaoOld;
                                //this.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            string data = "?id=" + rowCoBao.CoBaoGoc;
                            data += "&manv=" + AppGlobal.User.Username;
                            data += "&tennv=" + AppGlobal.User.FullName;
                            var objcb = await HttpHelper.Delete<CoBao>(Configuration.UrlCBApi + "api/CoBaos/DeleteCoBaoGocALL" + data);
                            if (objcb == null) throw new Exception("Lỗi xóa cơ báo: " + rowCoBao.CoBaoGoc + "-" + rowCoBao.SoCB);
                            throw new Exception("Lỗi lõi QTHH: " + ex.Message);
                        }
                    }
                    string strResult = await CoBaoDAO.NapThanhTich(rowCoBao.CoBaoID);
                    DialogHelper.Inform(strResult);
                    CoBaoDTForm.Instance.ShowThanhTich(strResult);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    rowCoBao = rowCoBaoOld;
                    Library.DialogHelper.Error(ex.Message);
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
