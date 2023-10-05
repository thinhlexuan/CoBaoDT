using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CBClient.BLLDaos;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;
using Microsoft.Reporting.WinForms;

namespace CBClient.CoBaoGAs
{
    public partial class CoBaoGAForm : DevComponents.DotNetBar.Metro.MetroForm
    {
        private CoBaoGA rowCoBao;
        private List<CoBaoGA> listCoBao = new List<CoBaoGA>();
        private List<CoBaoGACT> listcobaoct = new List<CoBaoGACT>();
        private List<CoBaoGADM> listcobaodm = new List<CoBaoGADM>();
        private static CoBaoGAForm _form;
        bool _fistTT = true;
        internal static CoBaoGAForm Instance
        {
            get { return _form; }
        }
        public CoBaoGAForm()
        {           
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            _form = this;
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);           
        }
        private void CoBaoGAForm_Load(object sender, EventArgs e)
        {
            string[] arRays = new string[] { "Khác","Tháng", "Quý", "Sáu Tháng", "Chín Tháng", "Năm" };
            cboLoaiBC.Items.AddRange(arRays);
            cboLoaiBC.SelectedIndex = 0;
            var donViTT = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT"|| ct.MaDV == "TCT"
                           select new
                           {
                               MaDV = ct.MaDV,
                               TenDV = ct.TenTat
                           }).OrderBy(x => x.TenDV).ToList();            
            if (AppGlobal.User.MaDVQL != "TCT")
            {
                if (AppGlobal.User.MaDVQL == "YV")
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "HN").ToList();
                else if (AppGlobal.User.MaDVQL == "DN")
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL || x.MaDV == "SG").ToList();
                else
                    donViTT = donViTT.Where(x => x.MaDV == AppGlobal.User.MaDVQL).ToList();
            }
            cboDonVi.DataSource = donViTT;
            cboDonVi.DisplayMember = "TenDV";
            cboDonVi.ValueMember = "MaDV";
            cboDonVi.SelectedIndex = 0;

            arRays = new string[] { "Tất cả", "Đã chuyển", "Chưa chuyển", "Thêm mới" };            
            cboTrangthai.Items.AddRange(arRays);
            cboTrangthai.SelectedIndex = 0;

            sdNgayBD.Value = DateTime.Today.AddDays(-1);
            sdNgayKT.Value = DateTime.Today;
            tblChiTiet.RowStyles[1].Height = 0;
        }
        private void cboLoaiBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            sdNgayBD.Enabled = true;
            sdNgayKT.Enabled = false;
            if (cboLoaiBC.SelectedIndex == 0)
            {
                sdNgayKT.Enabled = true;
                sdNgayBD.Value = DateTime.Today.AddDays(-1);
                sdNgayKT.Value = DateTime.Today;
            }
            else if (cboLoaiBC.SelectedIndex == 1)
            {
                sdNgayBD.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, sdNgayBD.Value.Month, DateTime.DaysInMonth(sdNgayBD.Value.Year, sdNgayBD.Value.Month));
            }
            else if (cboLoaiBC.SelectedIndex == 2)
            {
                if (sdNgayBD.Value.Month < 4)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 3, DateTime.DaysInMonth(sdNgayBD.Value.Year, 3));
                }
                else if (sdNgayBD.Value.Month >= 4 && sdNgayBD.Value.Month < 7)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 4, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 6, DateTime.DaysInMonth(sdNgayBD.Value.Year, 6));
                }
                else if (sdNgayBD.Value.Month >= 7 && sdNgayBD.Value.Month < 10)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 7, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 9, DateTime.DaysInMonth(sdNgayBD.Value.Year, 9));
                }
                else
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 10, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
                }
            }
            else if (cboLoaiBC.SelectedIndex == 3)
            {
                if (sdNgayBD.Value.Month < 7)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 6, DateTime.DaysInMonth(sdNgayBD.Value.Year, 6));
                }
                else
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 7, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
                }
            }
            else if (cboLoaiBC.SelectedIndex == 4)
            {

                sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 9, DateTime.DaysInMonth(sdNgayBD.Value.Year, 9));
            }
            else if (cboLoaiBC.SelectedIndex == 5)
            {

                sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
            }
        }
        private void sdNgayBD_Validated(object sender, EventArgs e)
        {
            sdNgayBD.Enabled = true;
            sdNgayKT.Enabled = false;
            if (cboLoaiBC.SelectedIndex == 0)
            {
                sdNgayKT.Enabled = true;
                sdNgayKT.Value = DateTime.Today;
            }
            else if (cboLoaiBC.SelectedIndex == 1)
            {
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, sdNgayBD.Value.Month, DateTime.DaysInMonth(sdNgayBD.Value.Year, sdNgayBD.Value.Month));
            }
            else if (cboLoaiBC.SelectedIndex == 2)
            {
                if (sdNgayBD.Value.Month < 4)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 3, DateTime.DaysInMonth(sdNgayBD.Value.Year, 3));
                }
                else if (sdNgayBD.Value.Month >= 4 && sdNgayBD.Value.Month < 7)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 4, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 6, DateTime.DaysInMonth(sdNgayBD.Value.Year, 6));
                }
                else if (sdNgayBD.Value.Month >= 7 && sdNgayBD.Value.Month < 10)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 7, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 9, DateTime.DaysInMonth(sdNgayBD.Value.Year, 9));
                }
                else
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 10, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
                }
            }
            else if (cboLoaiBC.SelectedIndex == 3)
            {
                if (sdNgayBD.Value.Month < 7)
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 6, DateTime.DaysInMonth(sdNgayBD.Value.Year, 6));
                }
                else
                {
                    sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 7, 1);
                    sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
                }
            }
            else if (cboLoaiBC.SelectedIndex == 4)
            {

                sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 9, DateTime.DaysInMonth(sdNgayBD.Value.Year, 9));
            }
            else if (cboLoaiBC.SelectedIndex == 5)
            {

                sdNgayBD.Value = new DateTime(sdNgayBD.Value.Year, 1, 1);
                sdNgayKT.Value = new DateTime(sdNgayBD.Value.Year, 12, DateTime.DaysInMonth(sdNgayBD.Value.Year, 12));
            }
        }

        private void sdNgayKT_Validated(object sender, EventArgs e)
        {
            if (sdNgayKT.Value < sdNgayBD.Value)
                sdNgayKT.Value = DateTime.Today;
        }
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            FnTraTim();
        }

        private void FnTraTim()
        {
            rtxtMesage.Text = string.Empty;
            bsCoBaoGA.DataSource = null;            
           // dgCoBao.DataSource = null;
            dgCoBaoDM.DataSource = null;
            dgCoBaoCT.DataSource = null;
            progressBar1.Value = 0;           
            try
            {
                base.Cursor = Cursors.WaitCursor;
                string data = "?MaDV=" + cboDonVi.SelectedValue.ToString();               
                data += "&NgayBD=" + sdNgayBD.Value;
                data += "&NgayKT=" + sdNgayKT.Value;              
                data += "&DauMay=" + txtSHDauMay.Text;
                data += "&SoCB=" + txtSHCoBao.Text;
                data += "&TaiXe=" + txtSHTaiXe.Text;
                data += "&TrangThai=" + cboTrangthai.Text;

                listCoBao = HttpHelper.GetList<CoBaoGA>(Configuration.UrlCBApi + "api/CoBaoGAs/GetCoBaoGAView" + data);
                bsCoBaoGA.DataSource = listCoBao;
                lblCoBao.Text = "Tổng số cơ báo:" + listCoBao.Count.ToString("N0");
                if (_fistTT == true) _fistTT = false;
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                base.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
                bsCoBaoGA.DataSource = null;
                dgCoBaoDM.DataSource = null;
                dgCoBaoCT.DataSource = null;
                return;
            }
        }
        private async Task<string> checkHoanThanh (DateTime ngayCB,string soCB, string soDM)
        {
            string trangThai = string.Empty;
            try
            {
                string ngayBD = ngayCB.ToString("yyyy/MM/dd");
                string ngayKT = ngayCB.AddDays(1).ToString("yyyy/MM/dd");
                base.Cursor = Cursors.WaitCursor;
               
                var access_token = string.Format("Bearer {0}{1}", MainForm.Instance.Data.userClientId, MainForm.Instance.Data.access_token);               
                var res = await AuthenticationService.GetListCoBaoDienTuByDate(ngayBD, ngayKT, soCB, soDM, 0, MainForm.Instance.Data.userName, access_token);
                if (res == null)
                {
                    throw new Exception("Cảnh báo: Không lấy được dữ liệu");
                }
                if (res.IsOK > 0)
                {
                    trangThai = res.Data.FirstOrDefault().TenTrangThai;                    
                }                
                base.Cursor = Cursors.Default;                
            }
            catch (Exception ex)
            {               
                trangThai = ex.Message;
                base.Cursor = Cursors.Default;
            }
            return trangThai;
        }

        private async void dgCoBao_SelectionChanged(object sender, EventArgs e)
        {
            tblChiTiet.RowStyles[1].Height = 0;
            listcobaoct = new List<CoBaoGACT>();
            listcobaodm = new List<CoBaoGADM>();
            rtxtMesage.Text = string.Empty;
            dgCoBaoDM.DataSource = null;
            dgCoBaoCT.DataSource = null;
            if (bsCoBaoGA.Count == 0) return;
            if (dgCoBao.CurrentRow != null)
            {
                base.Cursor = Cursors.WaitCursor;
                try
                {                   
                    CoBaoGA rowCoBaoOld= (CoBaoGA)dgCoBao.CurrentRow.DataBoundItem;
                    rowCoBao = rowCoBaoOld;
                    long cobaoID = string.IsNullOrWhiteSpace(rowCoBao.CoBaoID.ToString()) ? 0 : rowCoBao.CoBaoID;
                    long cobaoGoc = string.IsNullOrWhiteSpace(rowCoBao.CoBaoGoc.ToString()) ? 0 : rowCoBao.CoBaoGoc;
                    string data = "cobaoID=" + cobaoID;
                    data += "&cobaoGoc=" + cobaoGoc;
                    //Nạp dầu mỡ                    
                    listcobaodm = HttpHelper.GetList<CoBaoGADM>(Configuration.UrlCBApi + "api/CoBaoGAs/GetCoBaoGADMView?" + data);                 
                    BindingList<CoBaoGADM> lstGABinding = new BindingList<CoBaoGADM>();
                    foreach (CoBaoGADM ct in listcobaodm)
                        lstGABinding.Add(ct);
                    if(lstGABinding.Count>0)
                        tblChiTiet.RowStyles[1].Height = 80;
                    dgCoBaoDM.DataSource = lstGABinding;
                    dgCoBaoDM.Refresh();
                    lblCoBaoDM.Text = "Tổng số cơ báo dầu mỡ:" + listcobaodm.Count.ToString("N0");
                    //Nạp cơ báo chi tiết                    
                    listcobaoct = HttpHelper.GetList<CoBaoGACT>(Configuration.UrlCBApi + "api/CoBaoGAs/GetCoBaoGACTView?" + data);
                    dgCoBaoCT.DataSource = listcobaoct;
                    lblCoBaoCT.Text = "Tổng số cơ báo chi tiết:" + listcobaoct.Count.ToString("N0");
                    string strResult = CoBaoDAO.NapThanhTichByCoBaoGoc(cobaoGoc);
                    if(rowCoBao.TrangThai!="Chưa chuyển")                      
                        strResult = await DoanThongGADAO.NapThanhTich(cobaoID);
                    rtxtMesage.Text = strResult;                   
                }
                catch (Exception ex)
                {                   
                    MessageBox.Show(ex.Message);
                    listcobaoct = new List<CoBaoGACT>();
                    rtxtMesage.Text = string.Empty;
                    dgCoBaoDM.DataSource = null;
                    dgCoBaoCT.DataSource = null;
                    return;                   
                }
                base.Cursor = Cursors.Default;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                CoBaoGA rowCoBaoGA = new CoBaoGA();
                if (bsCoBaoGA.Count > 0)
                {
                    bsCoBaoGA.MoveLast();
                    CoBaoGA rowCoBaoOld = (CoBaoGA)bsCoBaoGA.Current;
                    rowCoBaoGA.DauMayID = rowCoBaoOld.DauMayID;
                    rowCoBaoGA.NgayCB = rowCoBaoOld.GiaoMay;
                    rowCoBaoGA.LenBan= rowCoBaoOld.GiaoMay;
                    rowCoBaoGA.NhanMay = rowCoBaoGA.LenBan;
                    rowCoBaoGA.RaKho = rowCoBaoGA.NhanMay;
                    rowCoBaoGA.NLBanTruoc = rowCoBaoOld.NLBanSau;
                    rowCoBaoGA.NLThucNhan = rowCoBaoGA.NLBanTruoc;
                }
                else
                {
                    rowCoBaoGA.NgayCB = DateTime.Now;                    
                }
                List<CoBaoGACT> listcobaoGAct = new List<CoBaoGACT>();
                List<CoBaoGADM> listcobaoGAdm = new List<CoBaoGADM>();
                NhapCBGADialog nhapCBGADlg = new NhapCBGADialog(rowCoBaoGA, listcobaoGAct, listcobaoGAdm);
                DialogResult aFormResult = nhapCBGADlg.ShowDialog();
                dgCoBao.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private async void btnChuyen_Click(object sender, EventArgs e)
        {
            if (dgCoBao.CurrentRow != null)
            {
                try
                {
                    string trangThai = await checkHoanThanh(rowCoBao.NgayCB,rowCoBao.SoCB,rowCoBao.DauMayID);
                    if (trangThai != "Đã hoàn thành")
                    {
                        throw new Exception("Cơ báo: " + rowCoBao.CoBaoID.ToString() + " " + trangThai + ". Không chuyển được được.");
                    }    
                    if (rowCoBao.TrangThai != "Chưa chuyển")
                        throw new Exception("Cơ báo: " + rowCoBao.CoBaoID.ToString() + " " + rowCoBao.TrangThai + ". Không chuyển được được.");
                    //if (rowCoBao.KhoaCB == true)
                    //    throw new Exception("Cơ báo: " + rowCoBao.CoBaoID.ToString() + " đã khóa. Không chuyển được được.");
                    NhapCBGADialog nhapCBGADlg = new NhapCBGADialog(rowCoBao, listcobaoct, listcobaodm);
                    DialogResult aFormResult = nhapCBGADlg.ShowDialog();
                    dgCoBao.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private async void btnChuyenAll_Click(object sender, EventArgs e)
        {
            //if (AppGlobal.nhanVien == null || AppGlobal.nhanVien.MaQH == 5)
            //{
            //    MessageBox.Show("Bạn không có quyền này.");
            //    return;
            //}
            int i = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = listCoBao.Count;
            foreach (CoBaoGA cb in listCoBao)
            {
                progressBar1.Value = i;
                if (cb != null && cb.TrangThai == "Chưa chuyển")
                {
                    i++;
                    lblCoBaoDM.Text = "Chuyển cb: " + cb.SoCB + " .Của: " + i + "/" + listCoBao.Count.ToString("N0");
                    CoBaoGAALL coBaoALL = new CoBaoGAALL();
                    rowCoBao = cb;
                    rowCoBao.Createddate = DateTime.Now;
                    rowCoBao.Createdby = AppGlobal.User.Username;
                    rowCoBao.CreatedName = AppGlobal.User.FullName;
                    rowCoBao.Modifydate = rowCoBao.Createddate;
                    rowCoBao.Modifyby = rowCoBao.Createdby;
                    rowCoBao.ModifyName = rowCoBao.CreatedName;
                    rowCoBao.KhoaCB = false;
                    rowCoBao.KhoaCBdate = rowCoBao.Createddate;
                    rowCoBao.KhoaCBby = rowCoBao.Createdby;
                    rowCoBao.KhoaCBName = rowCoBao.CreatedName;
                   
                    long cobaoID = string.IsNullOrWhiteSpace(rowCoBao.CoBaoID.ToString()) ? 0 : rowCoBao.CoBaoID;
                    long cobaoGoc = string.IsNullOrWhiteSpace(rowCoBao.CoBaoGoc.ToString()) ? 0 : rowCoBao.CoBaoGoc;
                    string data = "cobaoID=" + cobaoID;
                    data += "&cobaoGoc=" + cobaoGoc;
                    try
                    {
                        //Nạp dầu mỡ                    
                        listcobaodm = HttpHelper.GetList<CoBaoGADM>(Configuration.UrlCBApi + "api/CoBaoGAs/GetCoBaoGADMView?" + data);
                        listcobaoct = HttpHelper.GetList<CoBaoGACT>(Configuration.UrlCBApi + "api/CoBaoGAs/GetCoBaoGACTView?" + data);
                        //Nạp dữ liệu đoạn thống
                        DoanThongGA doanThong = await DoanThongGADAO.bindDoanThongToCoBao(rowCoBao);
                        List<DoanThongGADM> listdoanthongdm = listcobaodm.Count > 0 ? DoanThongGADAO.bindDoanThongDM(rowCoBao, listcobaodm) : new List<DoanThongGADM>();
                        List<DoanThongGACT> listdoanthongct = listcobaoct.Count >= 0 ? DoanThongGADAO.bindDoanThongCT(doanThong.DungKB, rowCoBao, listcobaoct) : new List<DoanThongGACT>();
                        //1.Phân bổ nhiên liệu
                        listdoanthongct=DoanThongGADAO.fnPhanBoNhienLieu(rowCoBao, listdoanthongct);
                        //2.Định mức nhiên liệu                   
                        DinhMucGADAO.YVNapNLDinhMuc(rowCoBao,listcobaoct, listdoanthongct);
                        //if (rowCoBao.DvcbID == "HN")
                        //{
                        DinhMucGADAO.HNNapNLDinhMuc(rowCoBao, listdoanthongct);
                        //    foreach (DoanThongGACT ct in listdoanthongct)
                        //    {
                        //        ct.DinhMuc = ct.TieuThu;
                        //    }
                        //}
                        DinhMucGADAO.VINapNLDinhMuc(rowCoBao, listcobaoct, listdoanthongct);
                        if (rowCoBao.DvcbID == "DN")
                        {
                            //DinhMucDAO.DNNapNLDinhMuc(rowCoBao, listdoanthongct);
                            foreach (DoanThongGACT ct in listdoanthongct)
                            {
                                ct.DinhMuc = ct.TieuThu;
                            }
                        }
                        DinhMucGADAO.SGNapNLDinhMuc(rowCoBao, listcobaoct, listdoanthongct);

                        //3. định mức dầu mỡ
                        decimal kmToTal = listdoanthongct.Sum(x => x.KMChinh + x.KMDon + x.KMGhep + x.KMDay);
                        DinhMucGADAO.YVNapDMDinhMuc(rowCoBao, kmToTal, listdoanthongdm);
                        if (rowCoBao.DvcbID == "HN")
                        {
                            DinhMucGADAO.HNNapDMDinhMuc(rowCoBao, kmToTal, listdoanthongdm);
                        }
                        DinhMucGADAO.VINapDMDinhMuc(rowCoBao, kmToTal, listdoanthongdm);
                        //Gán dữ liệu cơ báo
                        rowCoBao.CoBaoID = 0;
                        coBaoALL.CoBaoID = rowCoBao.CoBaoID;
                        coBaoALL.coBaoGAs = rowCoBao;
                        coBaoALL.coBaoGAs.coBaoGACTs = listcobaoct;
                        coBaoALL.coBaoGAs.coBaoGADMs = listcobaodm;
                        //Gán dữ liệu đoạn thống
                        coBaoALL.doanThongGAs = doanThong;
                        coBaoALL.doanThongGAs.doanThongGACTs = listdoanthongct;
                        coBaoALL.doanThongGAs.doanThongGADMs = listdoanthongdm;

                        //Lưu dữ liệu về db
                        //Thêm cơ báo                            
                        if (coBaoALL.coBaoGAs.TrangThai == "Chưa chuyển")
                            coBaoALL.coBaoGAs.TrangThai = "Đã chuyển";
                        var objcb = await HttpHelper.Post<CoBaoGAALL>(Configuration.UrlCBApi + "api/CoBaoGAs/PostCoBaoGAALL", coBaoALL);
                        //if (objcb == null) throw new Exception("Lỗi lưu thêm cơ báo: " + rowCoBao.CoBaoGoc + "-" + rowCoBao.SoCB);
                        rowCoBao.CoBaoID = objcb.CoBaoID;
                    }
                    catch
                    {
                        //Library.DialogHelper.Error(ex.Message);
                        rowCoBao.TrangThai = cb.TrangThai;
                        continue;
                    }
                }
            }
            FnTraTim();
        }
        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (dgCoBao.CurrentRow != null)
            {
                try
                {
                    if (rowCoBao.TrangThai != "Thêm mới")
                    {
                        string trangThai = await checkHoanThanh(rowCoBao.NgayCB, rowCoBao.SoCB, rowCoBao.DauMayID);
                        if (trangThai != "Đã hoàn thành")
                        {
                            throw new Exception("Cơ báo: " + rowCoBao.CoBaoID.ToString() + " " + trangThai + ". Không chuyển được được.");
                        }
                    }
                    if (rowCoBao.TrangThai == "Chưa chuyển")
                        throw new Exception("Cơ báo: " + rowCoBao.CoBaoID.ToString() + " " + rowCoBao.TrangThai + ". Không sửa được được.");
                    if (rowCoBao.KhoaCB ==true)
                        throw new Exception("Cơ báo: " + rowCoBao.CoBaoID.ToString() + " đã khóa. Không sửa được được.");
                    NhapCBGADialog nhapCBGADlg = new NhapCBGADialog(rowCoBao, listcobaoct, listcobaodm);
                    DialogResult aFormResult = nhapCBGADlg.ShowDialog();
                    dgCoBao.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
        
        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgCoBao.CurrentRow != null)
            {
                try
                {
                    if (rowCoBao.TrangThai == "Chưa chuyển")
                        throw new Exception("Cơ báo: " + rowCoBao.CoBaoID.ToString() + " " + rowCoBao.TrangThai + ". Không xóa được được.");
                    if (rowCoBao.KhoaCB == true)
                        throw new Exception("Cơ báo: " + rowCoBao.CoBaoID.ToString() + " đã khóa. Không xóa được được.");
                    if (Library.DialogHelper.Confirm(".Bạn có chắc chắn xóa cơ báo: " + rowCoBao.CoBaoID + "-" + rowCoBao.SoCB + " không?.") == DialogResult.Yes)
                    {
                        string data = "?id=" + rowCoBao.CoBaoID;
                        data += "&manv=" + AppGlobal.User.Username;
                        data += "&tennv=" + AppGlobal.User.FullName;
                        var objcbd = await HttpHelper.Delete<CoBaoGA>(Configuration.UrlCBApi + "api/CoBaoGAs/DeleteCoBaoGAALL" + data);
                        if (objcbd == null) throw new Exception("Lỗi xóa cơ báo: " + rowCoBao.CoBaoID + "-" + rowCoBao.SoCB);
                        else
                        {
                            FnTraTim();
                        }    
                    }                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
        private async void btnXoaAll_Click(object sender, EventArgs e)
        {
            int i = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = listCoBao.Count;
            foreach (CoBaoGA cb in listCoBao)
            {
                if (cb != null && cb.TrangThai == "Đã chuyển")
                {
                    progressBar1.Value = i;
                    i++;
                    lblCoBaoDM.Text = "Xóa cb: " + cb.SoCB + " .Của: " + i + "/" + listCoBao.Count.ToString("N0");
                    try
                    {
                        string data = "?id=" + cb.CoBaoID;
                        data += "&manv=" + AppGlobal.User.Username;
                        data += "&tennv=" + AppGlobal.User.FullName;
                        var objcbd = await HttpHelper.Delete<CoBaoGA>(Configuration.UrlCBApi + "api/CoBaoGAs/DeleteCoBaoGAALL" + data);
                        if (objcbd == null) throw new Exception("Lỗi xóa cơ báo: " + rowCoBao.CoBaoID + "-" + rowCoBao.SoCB);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        continue;
                    }
                } 
            }
            FnTraTim();
        }
        private void btnInCB_Click(object sender, EventArgs e)
        {
            if (rowCoBao == null) return;
            ShowReport();
        }
        private void btnInRG_Click(object sender, EventArgs e)
        {
            if (rowCoBao == null) return;
            ShowReportRG();
        }
        public void ShowThanhTich(string strResult)
        {
            rtxtMesage.Text = strResult;
        }
        private async void ShowReport()
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
               
                    List<ReportParameter> rptParamList = new List<ReportParameter>();                   
                    string tenDV = "THUỘC: " + rowCoBao.DvcbName.ToUpper();
                    ReportParameter rptParam = new ReportParameter("prmLoaibc", tenDV);
                    rptParamList.Add(rptParam);

                    string rptResource = "CBClient.Report.RptCoBaoGA.rdlc";

                    string rptName1 = "CoBaoDS";
                    List<CoBaoGA> listCoBao = new List<CoBaoGA>();
                    listCoBao.Add(rowCoBao);

                    string rptName2 = "CoBaoTTDS";
                    var objthanhtich = CoBaoDAO.NapObThanhTichByCoBaoGoc(rowCoBao.CoBaoGoc);
                    if (rowCoBao.TrangThai != "Chưa chuyển")
                        objthanhtich = await DoanThongGADAO.ObjNapThanhTich(rowCoBao.CoBaoID);
                    List<BCCoBaoTTInfo> listCoBaoTT = new List<BCCoBaoTTInfo>();
                    listCoBaoTT.Add(objthanhtich);

                    string rptName3 = "CoBaoCTDS";
                    List<BCCoBaoGACTInfo> listCoBaoCT = new List<BCCoBaoGACTInfo>();
                    int soTT = 1;
                    foreach (CoBaoGACT row in listcobaoct)
                    {
                        BCCoBaoGACTInfo info = new BCCoBaoGACTInfo();
                        info.SoTT = soTT;
                        info.NgayXP = row.NgayXP;
                        info.GioDen = row.GioDen.ToString("HH:mm");
                        info.GioDi = row.GioDi.ToString("HH:mm");
                        info.GioDon = row.PhutDon.ToString();
                        info.RutGioNL = row.RutGioNL.ToString();
                        info.DungGioPT = row.DungGioPT;
                        info.MacTau = row.MacTauID;
                        info.GaDi = row.GaName;
                        info.Tan = (decimal)row.Tan;
                        info.xeTotal = (int)row.XeTotal;
                        info.TanRong = (decimal)row.TanXeRong;
                        info.xeRong = (int)row.XeRongTotal;
                        info.TinhChat = row.TinhChatName;
                        info.MayGhep = row.MayGhepID;
                        info.KmAdd = (decimal)row.KmAdd;
                        listCoBaoCT.Add(info);
                        soTT++;
                    }

                    string rptName4 = "CoBaoDMDS";
                    CBClient.BaoCao.PreViewDialog PrintDlg = new CBClient.BaoCao.PreViewDialog
                        (rptResource, rptName1, listCoBao, rptName2, listCoBaoTT, rptName3, listCoBaoCT, rptName4, listcobaodm, rptParamList);
                    DialogResult aFormResult = PrintDlg.ShowDialog();                
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
                base.Cursor = Cursors.Default;
                return;
            }
        }

        private void ShowReportRG()
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                //Lấy dữ liệu
                var rowCBDGPT = listcobaoct.Where(x => x.DungGioPT==true).OrderBy(x => x.GioDi).FirstOrDefault();
                var rowCBRGNL = listcobaoct.Where(x => x.RutGioNL>0).OrderBy(x => x.GioDi).FirstOrDefault();
                //Kiểm tra xem có dữ liệu hay không
                if (rowCBDGPT == null && rowCBRGNL==null)
                    throw new Exception("Không có dữ liệu.");
                string macTau = rowCBDGPT != null ? rowCBDGPT.MacTauID : rowCBRGNL.MacTauID;
                var rowCBCTFist = listcobaoct.Where(x => x.MacTauID == macTau).OrderBy(x => x.GioDen).FirstOrDefault();
                var rowCBCTLast = listcobaoct.Where(x => x.MacTauID == macTau).OrderByDescending(x => x.GioDen).FirstOrDefault();
                long tienDungGio = 0; long tienGoGio = 0; long tienTong = 0;
                if (rowCBDGPT != null)
                {
                    rowCBDGPT.MacTauID=rowCBDGPT.MacTauID.Replace("Ð", "Đ");
                    string data = "?NgayHL=" + rowCoBao.NgayCB;
                    data += "&LoaiPhieu=Đúng giờ";
                    data += "&MacTau=" + rowCBDGPT.MacTauID+",";
                    data += "&GaName=" + rowCBDGPT.GaName;
                    var dmDungGio = HttpHelper.Get<HNPhieuThuong>(Configuration.UrlCBApi + "api/HaNois/HNGetPhieuThuongOBJ" + data).Result;
                    if (dmDungGio != null)
                    {
                        tienDungGio = (long)dmDungGio.DonGia;
                        tienTong += tienDungGio;
                    }
                }
                if (rowCoBao.RutGio > 0)
                {
                    rowCBCTFist.MacTauID=rowCBCTFist.MacTauID.Replace("Ð", "Đ");
                    string data = "?NgayHL=" + rowCoBao.NgayCB;
                    data += "&LoaiPhieu=Gỡ giờ";
                    data += "&MacTau=" + rowCBCTFist.MacTauID + ",";
                    data += "&GaName=";
                    var dmGoGio = HttpHelper.Get<HNPhieuThuong>(Configuration.UrlCBApi + "api/HaNois/HNGetPhieuThuongOBJ" + data).Result;
                    if (dmGoGio != null)
                    {
                        tienGoGio = (long)(dmGoGio.DonGia* rowCoBao.RutGio);
                        tienTong += tienGoGio;
                    }
                }    

                    List<ReportParameter> rptParamList = new List<ReportParameter>();
                var dVQL = AppGlobal.DonviDMList.Where(x => x.MaDV == rowCoBao.DvcbID).FirstOrDefault();
                string tenDV = dVQL.TenDV.ToUpper();

                string tenDVCha = AppGlobal.DonviDMList.Where(x => x.MaDV == dVQL.MaCha).FirstOrDefault().TenDV;
                ReportParameter rptParam = new ReportParameter("prmDonvicha", tenDVCha.ToUpper());
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmDonvicon", tenDV);
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmNgayth", "Hà Nội, ngày " + DateTime.Today.ToString("dd") + " tháng " + DateTime.Today.ToString("MM") + " năm " + DateTime.Today.ToString("yyyy"));
                rptParamList.Add(rptParam);
                
                rptParam = new ReportParameter("prmDaumay", "Đầu máy số hiệu: " + rowCoBao.DauMayID+";      Thuộc " + rowCoBao.DvdmName+";");
                rptParamList.Add(rptParam);
               

                rptParam = new ReportParameter("prmMactau", "Kéo tàu số: " + rowCBCTFist.MacTauID + ";        Khu đoạn: " + rowCBCTFist.GaName +" - "+ rowCBCTLast.GaName + ";");
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmSocb", "Số cơ báo điện tử: " + rowCoBao.SoCB + " - Ngày cơ báo: "+rowCoBao.NgayCB.ToString("dd.MM.yyyy")+ ";");
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmGadi", "Chạy từ ga: " + rowCBCTFist.GaName + ";      Lúc: "+ rowCBCTFist.GioDi.ToString("HH")+" giờ "+ rowCBCTFist.GioDi.ToString("mm")+" phút, ngày "+ rowCBCTFist.GioDi.ToString("dd.MM.yyyy")+";");
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmGaden", "Đến ga: " + rowCBCTLast.GaName + ";     Lúc: " + rowCBCTLast.GioDen.ToString("HH") + " giờ " + rowCBCTLast.GioDen.ToString("mm") + " phút, ngày " + rowCBCTLast.GioDen.ToString("dd.MM.yyyy") + ";");
                rptParamList.Add(rptParam);

                string dungGio = ".....................................................................";
                if (rowCBDGPT != null)
                    dungGio = " Đúng giờ;";
                rptParam = new ReportParameter("prmDunggio", "- Đúng giờ BĐCT:" + dungGio);
                rptParamList.Add(rptParam);

                string chuDung = "(viết bẵng chữ";
                if (rowCoBao.RutGio > 0)
                    chuDung = chuDung +" " +FormHelper.NumberToText(rowCoBao.RutGio) + " phút)";
                else
                    chuDung = chuDung + ".....................................................................)";
                rptParam = new ReportParameter("prmChamgiochu", "- Thời gian rút chậm giờ: " + rowCoBao.RutGio.ToString("N0") + " phút " + chuDung);
                rptParamList.Add(rptParam);

                //Phần phiếu tính tiền                
                rptParam = new ReportParameter("prmSotienthuongdg", "1. Tiền thưởng đến đúng giờ: " + tienDungGio.ToString("N0")+" VNĐ.");
                rptParamList.Add(rptParam);
                
                rptParam = new ReportParameter("prmChutienthuongdg", "(Bằng chữ: " + FormHelper.NumberToText(tienDungGio) + " đồng)");
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmSotienthuongrg", "2. Tiền thưởng rút ngắn thời gian chậm tàu: " + tienGoGio.ToString("N0") + " VNĐ.");
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmChutienthuongrg", "(Bằng chữ: " + FormHelper.NumberToText(tienGoGio) + " đồng)");
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmSotienthuongtg", "Tổng số tiền (1+2): " + tienTong.ToString("N0") + " VNĐ.");
                rptParamList.Add(rptParam);

                rptParam = new ReportParameter("prmChutienthuongtg", "(Bằng chữ: " + FormHelper.NumberToText(tienTong) + " đồng)");
                rptParamList.Add(rptParam);

                string rptResource = "CBClient.Report.RptPhieuRutGio.rdlc";
                string rptName = "BaoCaoDS";

                List<CoBaoGA> listCoBao = new List<CoBaoGA>();
                listCoBao.Add(rowCoBao);
                BaoCao.PreViewDialogKH PrintDlg = new BaoCao.PreViewDialogKH(rptResource, rptName, listCoBao, rptParamList);
                DialogResult aFormResult = PrintDlg.ShowDialog();
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                base.Cursor = Cursors.Default;
                return;
            }
        }

    }
}
