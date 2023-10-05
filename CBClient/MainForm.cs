
using System;
using System.Deployment.Application;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;
using DevComponents.DotNetBar;

namespace CBClient
{
    public partial class MainForm : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        private LoginData _data;
        public LoginData Data
        {
            get { return _data; }
            set { _data = value; }
        }

        private static MainForm _main;
        private Assembly ass = Assembly.GetExecutingAssembly();
        internal static MainForm Instance
        {
            get { return _main; }
        }
        public MainForm()
        {
            InitializeComponent();
            _main = this;
            //Kiểm tra phiên bản clickone
            //InstallUpdateSyncWithInfo();
            ShowHelpText(string.Empty);           
            this.Text += " - PHIÊN BẢN: " + ass.GetName().Version.ToString().ToUpper();
        }
        public void ShowHelpText(string strMessage)
        {
            if (strMessage == string.Empty) strMessage = "Ấn <Alt> hoặc <F10> để lên phím tắt!. Ấn F5-nạp lại danh mục.";
            lblHelpText.Text = strMessage;
            metroStatusBar1.Refresh();
        }
        private void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("This application has detected a mandatory update from your current " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". The application will now install the update and restart.",
                            "Update Available", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            MessageBox.Show("The application has been upgraded, and will now restart.");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                            return;
                        }
                    }
                }
            }
        }

        #region Automatic Color Scheme creation based on the selected color table
        private bool m_ColorSelected = false;
        private string m_StyleName = Properties.Settings.Default.StyleName;
        public eStyle m_BaseStyle = (eStyle)Enum.Parse(typeof(eStyle), Properties.Settings.Default.StyleName);
        private void buttonStyleCustom_ExpandChange(object sender, System.EventArgs e)
        {
            if (buttonStyleCustom.Expanded)
            {
                // Remember the starting color scheme to apply if no color is selected during live-preview
                m_ColorSelected = false;
                m_BaseStyle = StyleManager.Style;
            }
            else
            {
                if (!m_ColorSelected)
                {
                    if (StyleManager.Style == eStyle.Metro)
                        StyleManager.MetroColorGeneratorParameters = DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters.Default;
                    else
                        StyleManager.ChangeStyle(m_BaseStyle, Color.Empty);
                }
            }
        }

        private void buttonStyleCustom_ColorPreview(object sender, DevComponents.DotNetBar.ColorPreviewEventArgs e)
        {
            if (StyleManager.Style == eStyle.Metro)
            {
                Color baseColor = e.Color;
                StyleManager.MetroColorGeneratorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(Color.White, baseColor);
            }
            else
                StyleManager.ColorTint = e.Color;
            m_BaseStyle = StyleManager.Style;
        }

        private void buttonStyleCustom_SelectedColorChanged(object sender, System.EventArgs e)
        {
            m_ColorSelected = true; // Indicate that color was selected for buttonStyleCustom_ExpandChange method
            buttonStyleCustom.CommandParameter = buttonStyleCustom.SelectedColor;
        }
        #endregion

        private void AppCommandTheme_Executed(object sender, EventArgs e)
        {
            ICommandSource source = sender as ICommandSource;
            if (source.CommandParameter is string)
            {
                eStyle style = (eStyle)Enum.Parse(typeof(eStyle), source.CommandParameter.ToString());
                // Using StyleManager change the style and color tinting
                if (StyleManager.IsMetro(style))
                {
                    // More customization is needed for Metro
                    // Capitalize App Button and tab

                    foreach (BaseItem item in MetroShell.Items)
                    {
                        // Ribbon Control may contain items other than tabs so that needs to be taken in account
                        RibbonTabItem tab = item as RibbonTabItem;
                        if (tab != null)
                            tab.Text = tab.Text.ToUpper();
                    }

                    metroShell1.TabStripFont = new System.Drawing.Font("Segoe UI", 9.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    if (style == eStyle.Metro)
                        StyleManager.MetroColorGeneratorParameters = DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters.DarkBlue;
                    // Adjust tab strip style
                    tabStrip1.Style = eTabStripStyle.Metro;

                    StyleManager.Style = style; // BOOM                    
                }
                else
                {
                    // If previous style was Metro we need to update other properties as well
                    if (StyleManager.IsMetro(StyleManager.Style))
                    {
                        metroShell1.TabStripFont = null;
                        // Fix capitalization App Button and tab                       
                        foreach (BaseItem item in MetroShell.Items)
                        {
                            // Ribbon Control may contain items other than tabs so that needs to be taken in account
                            RibbonTabItem tab = item as RibbonTabItem;
                            if (tab != null)
                                tab.Text = ToTitleCase(tab.Text);
                        }
                    }
                    // Adjust tab strip style
                    tabStrip1.Style = eTabStripStyle.Office2007Document;
                    StyleManager.ChangeStyle(style, Color.Empty);
                }
                m_StyleName = source.CommandParameter.ToString();
            }
            else if (source.CommandParameter is Color)
            {
                if (StyleManager.IsMetro(StyleManager.Style))
                    StyleManager.MetroColorGeneratorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(Color.White, (Color)source.CommandParameter);
                else
                    StyleManager.ColorTint = (Color)source.CommandParameter;
            }

            m_BaseStyle = StyleManager.Style;
        }

        private string ToTitleCase(string text)
        {
            if (text.Contains("&"))
            {
                int ampPosition = text.IndexOf('&');
                text = text.Replace("&", "");
                text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
                if (ampPosition > 0)
                    text = text.Substring(0, ampPosition) + "&" + text.Substring(ampPosition);
                else
                    text = "&" + text;
                return text;
            }
            else
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }

        private void TKTimer_Tick(object sender, EventArgs e)
        {
            lblItemTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");            
        }
        private void PBTimer_Tick(object sender, EventArgs e)
        {
            var phienBan = HttpHelper.GetList<PhienBan>(Configuration.UrlCBApi + "api/DanhMucs/GetPhienBan").OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (phienBan!=null)
            {
                if(phienBan.ID!= ass.GetName().Version.ToString())
                {
                    ShowHelpText("Chương trình đã có phiên bản mới: " + phienBan.ID + ". Cập nhật lúc: " + 
                        phienBan.CreatedDate.ToString("HH:mm dd.MM.yyyy") + ". Hãy thoát chương trình và vào lại để cập nhật phiên bản.");
                }    
            }    
        }
        private void commandForm_Executed(object sender, EventArgs e)
        {
            MainForm.Instance.ShowHelpText(string.Empty);
            ICommandSource source = sender as ICommandSource;
            string commandName = source.CommandParameter.ToString();
            switch (commandName)
            {
                //Nhập liệu
                case "COBAOD":
                    ShowChildForm(new NhapLieu.CoBaoDTForm());
                    break;
                case "NHAPDT":
                    ShowChildForm(new NhapLieu.DoanThongForm());
                    break;
                case "KTLOGIC":
                    ShowChildForm(new NhapLieu.KTLogicForm());
                    break;
                case "XUATCB":
                    ShowChildForm(new NhapLieu.XuatCoBaoForm());
                    break;
                case "XUATDT":
                    ShowChildForm(new NhapLieu.XuatDoanThongForm());
                    break;
                //Cơ Báo Giấy
                case "COBAOGA":
                    ShowChildForm(new CoBaoGAs.CoBaoGAForm());
                    break;
                case "DOANTHONGGA":
                    ShowChildForm(new CoBaoGAs.DoanThongGAForm());
                    break;
                case "LOGICGA":
                    ShowChildForm(new CoBaoGAs.KTLogicGAForm());
                    break;
                case "XUATCBGA":
                    ShowChildForm(new CoBaoGAs.XuatCoBaoGAForm());
                    break;
                case "XUATDTGA":
                    ShowChildForm(new CoBaoGAs.XuatDoanThongGAForm());
                    break;
                ////Danh mục
                case "DMLYTRINH":
                    ShowChildForm(new DanhMuc.LyTrinhForm());
                    break;
                case "CONGLENHSK":
                    ShowChildForm(new DanhMuc.CongLenhSKForm());
                    break;
                case "HSQDNL":
                    ShowChildForm(new DanhMuc.HSQDNLForm());
                    break;
                case "DMTAIXE":
                    ShowChildForm(new DanhMuc.TaiXeForm());
                    break;
                case "DMMACTAU":
                    ShowChildForm(new DanhMuc.MacTauForm());
                    break;
                case "DMGACD":
                    ShowChildForm(new DanhMuc.GaChuyenDonForm());
                    break;
                case "MIENPHAT":
                    ShowChildForm(new DanhMuc.MienPhatForm());
                    break;
                case "DAUMAY":
                    ShowChildForm(new DanhMuc.DauMayForm());
                    break;
                //Báo cáo
                case "BCVANDUNG":
                    ShowChildForm(new BaoCao.BCVanDungForm());
                    break;
                case "BCKTKT":
                    ShowChildForm(new BaoCao.BCKTKTForm());
                    break;
                case "BCGIODON":
                    ShowChildForm(new BaoCao.BCGioDonForm());
                    break;
                case "BCTHSPTN":
                    ShowChildForm(new BaoCao.BCTHSPTNForm());
                    break;
                case "BCCTGIODON":
                    ShowChildForm(new BaoCao.BCCTGioDonForm());
                    break;
                case "BCTHNL":
                    ShowChildForm(new BaoCao.BCTHNLForm());
                    break;
                case "BCTTNL":
                    ShowChildForm(new BaoCao.BCTTNLForm());
                    break;
                case "BCDCSPTN":
                    ShowChildForm(new BaoCao.BCTacNghiepForm());
                    break;
                case "BCDAUMO":
                    ShowChildForm(new BaoCao.BCDauMoForm());
                    break;
                case "BKTINHLUONG":
                    ShowChildForm(new BaoCao.BKTinhLuongForm());
                    break;
                case "BCHQSDDM":
                    ShowChildForm(new BaoCao.BCHQSDDMForm());
                    break;

                ////Hệ thống
                case "HTNHATKY":
                    ShowChildForm(new HeThong.NhatKyForm());
                    break;
                case "HTSUAMK":
                    HeThong.ChangePWD smkForm = new HeThong.ChangePWD();
                    smkForm.ShowDialog();
                    break;
                case "HTDANGXUAT":
                    foreach (Form child in this.MdiChildren)
                    {
                        child.Close();
                    }
                    //1. Show Login Form
                    ShowLogin();
                    break;
                //Yên Viên
                case "YVKHUDOAN":
                    ShowChildForm(new YenVien.YVKhuDoanForm());
                    break;
                case "YVNLDM":
                    ShowChildForm(new YenVien.YVNLDinhMucForm());
                    break;
                case "YVNLPDM":
                    ShowChildForm(new YenVien.YVNLPDinhMucForm());
                    break;
                case "YVDMDM":
                    ShowChildForm(new YenVien.YVDMDinhMucForm());
                    break;
                //Hà Nội
                case "HNKHUDOAN":
                    ShowChildForm(new HaNoi.HNKhuDoanForm());
                    break;
                case "HNNLDM":
                    ShowChildForm(new HaNoi.HNNLDinhMucForm());
                    break;
                case "HNNLPDM":
                    ShowChildForm(new HaNoi.HNNLPDinhMucForm());
                    break;
                case "HNDMDM":
                    ShowChildForm(new HaNoi.HNDMDinhMucForm());
                    break;
                case "HNPHIEUTHUONG":
                    ShowChildForm(new HaNoi.HNPhieuThuongForm());
                    break;
                //Vinh
                case "VIKHUDOAN":
                    ShowChildForm(new Vinh.VIKhuDoanForm());
                    break;
                case "VINLDM":
                    ShowChildForm(new Vinh.VINLDinhMucForm());
                    break;
                case "VINLDMD":
                    ShowChildForm(new Vinh.VINLDDinhMucForm());
                    break;
                case "VIHSTAN":
                    ShowChildForm(new Vinh.VIHSTanForm());
                    break;
                case "VIDMDM":
                    ShowChildForm(new Vinh.VIDMDinhMucForm());
                    break;
                //Đà Nẵng
                case "DNKHUDOAN":
                    ShowChildForm(new DaNang.DNKhuDoanForm());
                    break;
                case "DNNLDM":
                    ShowChildForm(new DaNang.DNNLDinhMucForm());
                    break;
                //Sài Gòn
                case "SGKHUDOAN":
                    ShowChildForm(new SaiGon.SGKhuDoanForm());
                    break;
                case "SGNLDM":
                    ShowChildForm(new SaiGon.SGNLDinhMucForm());
                    break;
                case "SGHSTAN":
                    ShowChildForm(new SaiGon.SGHSTanForm());
                    break;
                //Đường Sắt
                case "DSNLDM":
                    ShowChildForm(new DuongSat.DSNLDinhMucForm());
                    break;
                case "DSKEHOACH":
                    ShowChildForm(new DuongSat.DSKeHoachForm());
                    break;
                //Nhiên Liệu
                case "NLLOAIDM":
                    ShowChildForm(new NhienLieu.LoaiDauMoForm());
                    break;
                case "NLNHACC":
                    ShowChildForm(new NhienLieu.NhaCCForm());
                    break;                    
                case "NLHOPDONG":
                    ShowChildForm(new NhienLieu.HopDongForm());
                    break;
                case "NLGIA":
                    ShowChildForm(new NhienLieu.BangGiaForm());
                    break;
                case "NLNHAP":
                    ShowChildForm(new NhienLieu.PhieuNhapForm());
                    break;
                case "NLXUAT":
                    ShowChildForm(new NhienLieu.PhieuXuatForm());
                    break;
                case "NLBCNHAP":
                    ShowChildForm(new NhienLieu.BCNhapKhoForm());
                    break;
                case "NLBCXUAT":
                    ShowChildForm(new NhienLieu.BCXuatKhoForm());
                    break;
                case "NLBCTHE":
                    ShowChildForm(new NhienLieu.BCTheKhoForm());
                    break;
                case "NLBCTON":
                    ShowChildForm(new NhienLieu.BCTonKhoForm());
                    break;
                case "NLBCSO":
                    ShowChildForm(new NhienLieu.BCSoKhoForm());
                    break;
                default:               
                    break;
            }
        }
        private void ShowLogin()
        {
            metroShell1.Enabled = false;
            metroTabCoBaoGA.Visible = false;
            metroTabYenVien.Visible = false;
            metroTabHaNoi.Visible = false;
            metroTabVinh.Visible = false;
            metroTabDaNang.Visible = false;
            metroTabSaiGon.Visible = false;
            metroTabDSVN.Visible = false;
            metroTabNhienLieu.Visible = false;
            lblItemUser.Text = "Người dùng";
            lblItemCuaVe.Text = "Đơn vị";
            metroStatusBar1.Refresh();
            HeThong.Login dlg = new HeThong.Login();
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                this.Close();
                return;
            }
            base.Cursor = Cursors.WaitCursor;

            AppGlobal.LoadServiceData();
            base.Cursor = Cursors.Default;
            lblItemUser.Text = AppGlobal.User.FullName;
            lblItemCuaVe.Text = AppGlobal.User.TenDV;
            //Phân quyền người dùng
            if (AppGlobal.User != null && AppGlobal.User.Active)
            {
                metroTabCoBaoGA.Visible = AppGlobal.User.MaQH == 5 ? false : true;
                metroTabDSVN.Visible = true;
                metroTabNhienLieu.Visible = AppGlobal.User.NL == 0 ? false : true;
                if (AppGlobal.User.MaDVQL == "TCT" || AppGlobal.User.MaDVQL == "UB")
                {

                    metroTabYenVien.Visible = AppGlobal.User.MaQH > 3 ? false : true;
                    metroTabHaNoi.Visible = AppGlobal.User.MaQH > 3 ? false : true;
                    metroTabVinh.Visible = AppGlobal.User.MaQH > 3 ? false : true;
                    metroTabDaNang.Visible = AppGlobal.User.MaQH > 3 ? false : true;
                    metroTabSaiGon.Visible = AppGlobal.User.MaQH > 3 ? false : true;
                }
                else if (AppGlobal.User.MaDVQL == "YV")
                {
                    metroTabYenVien.Visible = true;
                    metroTabHaNoi.Visible = true;
                }
                else if (AppGlobal.User.MaDVQL == "HN")
                {
                    metroTabHaNoi.Visible = true;
                }
                else if (AppGlobal.User.MaDVQL == "VIN")
                {
                    metroTabVinh.Visible = true;
                }
                else if (AppGlobal.User.MaDVQL == "DN")
                {
                    metroTabDaNang.Visible = true;
                    metroTabSaiGon.Visible = true;
                }
                else if (AppGlobal.User.MaDVQL == "SG")
                {
                    metroTabSaiGon.Visible = true;
                }
            }
            else
            {
                metroTabYenVien.Visible = false;
                metroTabHaNoi.Visible = false;
                metroTabVinh.Visible = false;
                metroTabDaNang.Visible = false;
                metroTabSaiGon.Visible = false;
            }
            metroTabNhapLieu.Select();
            metroShell1.Enabled = true;
            metroStatusBar1.Refresh();
        }

        private void ShowChildForm(Form frm)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child.Name == frm.Name)
                {
                    frm = null;
                    child.Activate();
                    return;
                }
            }
            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;            
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_BaseStyle = (eStyle)Enum.Parse(typeof(eStyle), CBClient.Properties.Settings.Default.StyleName);
            StyleManager.ChangeStyle(m_BaseStyle, System.Drawing.Color.Empty);
            //1. Show Login Form
            ShowLogin();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CBClient.Properties.Settings.Default.StyleName = m_StyleName;
            CBClient.Properties.Settings.Default.Save();
        }       

        private void metroShell1_HelpButtonClick(object sender, EventArgs e)
        {
            try
            {
                var uri = "http://vtds.vn/#/trogiup";
                var psi = new System.Diagnostics.ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.FileName = uri;
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }

        
    }
}
