using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLDaos;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.CoBaoGAs
{
    public partial class XuatDoanThongGAForm : DevComponents.DotNetBar.Metro.MetroForm    {    
        
        public XuatDoanThongGAForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            for (int i = 1; i <= 12; i++)
            {
                cboThangDT.Items.Add(i.ToString());
            }

            int year = DateTime.Today.Year;
            for (int i = year - 10; i <= year + 1; i++)
            {
                cboNamDT.Items.Add(i.ToString());
            }
            cboThangDT.SelectedText = DateTime.Today.Month.ToString();
            cboNamDT.SelectedText = year.ToString();

            var donViTT = (from ct in AppGlobal.DonviDMList
                           where ct.MaCha == "TCT"
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
        }
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            dgView.DataSource = null;
            lblBanGhi.Text = "Tổng số bản ghi: 0";
            try
            {
                base.Cursor = Cursors.WaitCursor;
                int thangDT = int.Parse(cboThangDT.Text);
                int namDT = int.Parse(cboNamDT.Text);
                string data ="&maDV=" + cboDonVi.SelectedValue;
                data += "&thangDT=" + int.Parse(cboThangDT.Text);
                data += "&namDT=" + int.Parse(cboNamDT.Text);
                if (cboDonVi.SelectedValue.ToString() == "YV")
                {
                    var obj = HttpHelper.GetList<YVXuatDT>(Configuration.UrlCBApi + "api/YenViens/YVGetXuatDTGA?" + data);
                    if (obj.Count <= 0)
                    {
                        throw new Exception("Không có dữ liệu cơ báo.");
                    }
                    var listDT = (from dt in obj
                                  group dt by new
                                  {
                                      dt.SOCB,
                                      dt.LMAY,
                                      dt.DMAY,
                                      dt.SDB1,
                                      dt.TEN1,
                                      dt.MTAU,
                                      dt.CTAC,
                                      dt.TCHAT,
                                      dt.MDOAN,
                                      dt.KDOAN,
                                      dt.SLBT,
                                      dt.SLLH,
                                      dt.SLBS,
                                      dt.SLPT,
                                      dt.NLANH,
                                      dt.PDOAN,
                                      dt.DAYCB,
                                      dt.DGKB,
                                      dt.SLRK,
                                      dt.CTY,
                                      dt.DTAU
                                  } into g
                                  select new YVXuatDT
                                  {
                                      SOCB = g.Key.SOCB,
                                      LMAY = g.Key.LMAY,
                                      DMAY = g.Key.DMAY,
                                      SDB1 = g.Key.SDB1,
                                      TEN1 = g.Key.TEN1,
                                      SDB2 = g.FirstOrDefault().SOCB,
                                      TEN2 = g.FirstOrDefault().TEN2,
                                      SDB3 = g.FirstOrDefault().SDB3,
                                      TEN3 = g.FirstOrDefault().TEN3,
                                      MTAU = g.Key.MTAU,
                                      CTAC = g.Key.CTAC,
                                      TCHAT = g.Key.TCHAT,
                                      MDOAN=g.Key.MDOAN,
                                      KDOAN = g.Key.KDOAN,
                                      SLBT = g.Key.SLBT,
                                      SLLH = g.Key.SLLH,
                                      SLBS = g.Key.SLBS,
                                      SLTT = g.Sum(x => x.SLTT),
                                      SLTC = g.Sum(x => x.SLTC),
                                      SLPT = g.Key.SLPT,
                                      NLANH = g.Key.NLANH,
                                      GAXP = g.FirstOrDefault().GAXP,
                                      DAYCB = g.Key.DAYCB,
                                      DGKB = g.Key.DGKB,
                                      DGDM = g.Sum(x => x.DGDM),
                                      DGDN = g.Sum(x => x.DGDN),
                                      DGKM = g.Sum(x => x.DGKM),
                                      DGKN = g.Sum(x => x.DGKN),
                                      DGQD = g.Sum(x => x.DGQD),
                                      GIQV = g.Sum(x => x.GIQV),
                                      GILH = g.Sum(x => x.GILH),
                                      GIDT = g.Sum(x => x.GIDT),
                                      DGXP = g.Sum(x => x.DGXP),
                                      DGDD = g.Sum(x => x.DGDD),
                                      DGCC = g.Sum(x => x.DGCC),
                                      DNXP = g.Sum(x => x.DNXP),
                                      DNDD = g.Sum(x => x.DNDD),
                                      DNCC = g.Sum(x => x.DNCC),
                                      SLRK = g.Key.SLRK,
                                      KMCH = g.Sum(x => x.KMCH),
                                      KMDW = g.Sum(x => x.KMDW),
                                      KMGH = g.Sum(x => x.KMGH),
                                      KMDY = g.Sum(x => x.KMDY),
                                      TKCH = g.Sum(x => x.TKCH),
                                      TKDW = g.Sum(x => x.TKDW),
                                      TKGH = g.Sum(x => x.TKGH),
                                      TKDY = g.Sum(x => x.TKDY),
                                      L_DC = g.FirstOrDefault().L_DC,
                                      L_TL = g.FirstOrDefault().L_TL,
                                      L_GT = g.FirstOrDefault().L_GT,
                                      T_DC = g.FirstOrDefault().T_DC,
                                      T_TL = g.FirstOrDefault().T_TL,
                                      T_GT = g.FirstOrDefault().T_GT,
                                      C_DC = g.FirstOrDefault().C_DC,
                                      C_TL = g.FirstOrDefault().C_TL,
                                      C_GT = g.FirstOrDefault().C_GT,
                                      SLRKM = g.Sum(x => x.SLRKM),
                                      SLRKN = g.Sum(x => x.SLRKN),
                                      CTY = g.Key.CTY,
                                      DAY_LT = g.FirstOrDefault().DAY_LT,
                                      DTAU = g.Key.DTAU
                                  }).ToList();
                    YVXuatDT ctOld = new YVXuatDT();
                    foreach (YVXuatDT ct in listDT)
                    {
                        ct.DMAY = ct.DMAY.Split('-')[1];
                        if (!string.IsNullOrWhiteSpace(ct.NLANH))
                        {
                            if (ct.NLANH == "TFLCA") ct.NLANH = "LC";
                            if (ct.NLANH == "TFXGA") ct.NLANH = "XG";
                            if (ct.NLANH == "TFYBI") ct.NLANH = "YB";
                            if (ct.NLANH == "TFVTR") ct.NLANH = "VT";
                            if (ct.NLANH == "TFLTH") ct.NLANH = "LT";
                            if (ct.NLANH == "TFMKH") ct.NLANH = "MK";
                            if (ct.NLANH == "TFDDA") ct.NLANH = "DD";
                            if (ct.NLANH == "TFYVI") ct.NLANH = "YV";
                            if (ct.NLANH == "TFHPH") ct.NLANH = "HP";
                            if (ct.NLANH == "TFHNO") ct.NLANH = "HN";
                            if (ct.NLANH == "TFGBA") ct.NLANH = "GB";
                            if (ct.NLANH == "TFNBI") ct.NLANH = "NB";
                            if (ct.NLANH == "TFTHO") ct.NLANH = "TH";
                            if (ct.NLANH == "TFVIN") ct.NLANH = "VI";
                            if (ct.NLANH == "TFPUT") ct.NLANH = "PT";
                            if(ct.NLANH == "TFDHO") ct.NLANH = "DH";
                            if (ct.NLANH == "TFHUE") ct.NLANH = "HU";
                            if (ct.NLANH == "TFDNA") ct.NLANH = "DN";
                            if (ct.NLANH == "TFQNG") ct.NLANH = "QN";
                            if (ct.NLANH == "TFDTR") ct.NLANH = "DT";
                            if (ct.NLANH == "TFNTR") ct.NLANH = "NT";
                            if (ct.NLANH == "TFBTH") ct.NLANH = "BT";
                            if (ct.NLANH == "TFSGO") ct.NLANH = "SG";
                            if (ct.NLANH == "TFSOT") ct.NLANH = "ST";
                        }
                        if (ct.SOCB == ctOld.SOCB)
                        {

                            ct.SLBT = 0;
                            ct.SLLH = 0;
                            ct.SLBS = 0;
                            ct.SLPT = 0;
                            ct.NLANH = string.Empty;
                            ct.DGKB = 0;
                            ct.SLRK = 0;
                        }
                        if (ct.LMAY == "D10H") ct.LMAY = "D10H-CAT";
                        if (ct.CTAC == 2) ct.CTAC = 1;
                        if (ct.CTAC == 3) ct.CTAC = 2;
                        if (ct.CTAC == 4) ct.CTAC = 3;
                        if (ct.CTAC == 5) ct.CTAC = 4;
                        if (ct.CTAC == 6) ct.CTAC = 5;
                        if (ct.CTAC == 7) ct.CTAC = 6;
                        if (ct.CTAC == 8||ct.CTAC == 9) ct.CTAC = 7;
                        if (ct.TCHAT == 5) ct.TCHAT = 3;
                        if (ct.TCHAT == 3|| ct.TCHAT == 4|| ct.TCHAT == 6) ct.TCHAT = 5;
                        if (ct.CTAC == 5)
                        {
                            if (ct.KDOAN == "DM-DD-di" || ct.KDOAN == "DM-DD-ve" || ct.KDOAN == "DM-ND-di" || ct.KDOAN == "DM-ND-ve" ||
                                ct.KDOAN == "ND-DD-di" || ct.KDOAN == "ND-DD-ve")
                                ct.CTACP = "DMDD";
                            if (ct.KDOAN == "LC-SY-di" || ct.KDOAN == "LC-SY-ve")
                                ct.CTACP = "LCSY";
                            if (ct.KDOAN == "CL-UB-di" || ct.KDOAN == "CL-UB-ve")
                                ct.CTACP = "CLCT";
                        }
                        if (ct.CTAC == 7 && ct.GAXP == "Lâm Thao") ct.CTACP = "DONLT";
                        ct.CTY = "1";
                        if (!string.IsNullOrWhiteSpace(ct.MTAU))
                        {
                            string cty = ct.MTAU.Substring(0,1);
                            if (cty == "s" || cty == "S")
                                ct.CTY = "2";
                            if (cty == "a" || cty == "A")
                                ct.CTY = "5";                           
                        }
                            ctOld = ct;
                    }

                    dgView.DataSource = listDT;
                    lblBanGhi.Text = "Tổng số " + cboDonVi.SelectedValue.ToString() + ": " + listDT.Count.ToString("N0");
                }
                if (cboDonVi.SelectedValue.ToString() == "HN")
                {                     
                    var obj = HttpHelper.GetList<HNXuatDT>(Configuration.UrlCBApi + "api/HaNois/HNGetXuatDTGA?" + data).OrderBy(x => x.daycb).ThenBy(x => x.socb).ThenBy(x => x.mtau).ThenBy(x => x.dayxp).ToList();
                    if (obj.Count <= 0)
                    {
                        throw new Exception("Không có dữ liệu cơ báo.");
                    }
                    var listDT = (from dt in obj
                                  group dt by new
                                  {
                                      dt.socb,
                                      dt.lmay,
                                      dt.dmay,
                                      dt.sdb1,
                                      dt.mtau,
                                      dt.ctac,
                                      dt.tchat,
                                      dt.kdoan,
                                      dt.pdoan,
                                      dt.daycb,
                                      dt.mghep,
                                      dt.ThangDT,
                                      dt.NamDT
                                  } into g
                                  select new HNXuatDT
                                  {
                                      socb = g.Key.socb,
                                      lmay = g.Key.lmay,
                                      dmay = g.Key.dmay,
                                      sdb1 = g.Key.sdb1,
                                      ten1 = g.FirstOrDefault().ten1,
                                      sdb2 = g.FirstOrDefault().sdb2,
                                      ten2 = g.FirstOrDefault().ten2,
                                      sdb3 = g.FirstOrDefault().sdb3,
                                      ten3 = g.FirstOrDefault().ten3,
                                      mtau = g.Key.mtau,
                                      ctac = g.Key.ctac,
                                      tchat = g.Key.tchat,
                                      kdoan = g.Key.kdoan,                                      
                                      pdoan =g.Key.pdoan,
                                      slbt = g.FirstOrDefault().slbt,
                                      sllh = g.FirstOrDefault().sllh,
                                      slbs = g.FirstOrDefault().slbs,
                                      sltt = g.Sum(x => x.sltt),
                                      sltc = g.Sum(x => x.sltc),
                                      slpt = g.FirstOrDefault().slpt,
                                      nlanh = g.FirstOrDefault().nlanh,
                                      gaxp = g.FirstOrDefault().gaxp,
                                      daycb = g.Key.daycb,
                                      dgkb = g.FirstOrDefault().dgkb,
                                      dgdm = g.Sum(x => x.dgdm),
                                      dgdn = g.Sum(x => x.dgdn),
                                      dgkm = g.Sum(x => x.dgkm),
                                      dgkn = g.Sum(x => x.dgkn),
                                      dgqd = g.Sum(x => x.dgqd),
                                      giqv = g.Sum(x => x.giqv),
                                      gilh = g.Sum(x => x.gilh),
                                      gidt = g.Sum(x => x.gidt),
                                      dgxp = g.Sum(x => x.dgxp),
                                      dgdd = g.Sum(x => x.dgdd),
                                      dgcc = g.Sum(x => x.dgcc),
                                      dnxp = g.Sum(x => x.dnxp),
                                      dndd = g.Sum(x => x.dndd),
                                      dncc = g.Sum(x => x.dncc),
                                      slrk = g.FirstOrDefault().slrk,
                                      kmch = g.Sum(x => x.kmch),
                                      kmdw = g.Sum(x => x.kmdw),
                                      kmgh = g.Sum(x => x.kmgh),
                                      kmdy = g.Sum(x => x.kmdy),
                                      tkch = g.Sum(x => x.tkch),
                                      tkdw = g.Sum(x => x.tkdw),
                                      tkgh = g.Sum(x => x.tkgh),
                                      tkdy = g.Sum(x => x.tkdy),
                                      l_dc = g.FirstOrDefault().l_dc,
                                      l_tl = g.FirstOrDefault().l_tl,
                                      l_gt = g.FirstOrDefault().l_gt,
                                      t_dc = g.FirstOrDefault().t_dc,
                                      t_tl = g.FirstOrDefault().t_tl,
                                      t_gt = g.FirstOrDefault().t_gt,
                                      c_dc = g.FirstOrDefault().c_dc,
                                      c_tl = g.FirstOrDefault().c_tl,
                                      c_gt = g.FirstOrDefault().c_gt,
                                      slrkm = g.FirstOrDefault().slrkm,
                                      slrkn = g.FirstOrDefault().slrkn,
                                      dayxp = g.FirstOrDefault().dayxp,
                                      dtau = g.FirstOrDefault().dtau,
                                      mghep = g.Key.mghep,
                                      ThangDT = g.Key.ThangDT,
                                      NamDT = g.Key.NamDT
                                  }).ToList();
                    HNXuatDT ctOld = new HNXuatDT();
                    foreach (HNXuatDT ct in listDT)
                    {
                        ct.dmay = ct.dmay=="VNR6239"? "VNR6239": ct.dmay.Split('-')[1];
                        ct.socb = "'" + ct.socb;
                        ct.sdb1 = string.IsNullOrEmpty(ct.sdb1) ? ct.sdb1 : "'" + ct.sdb1;
                        ct.sdb2 = string.IsNullOrEmpty(ct.sdb2) ? ct.sdb2 : "'" + ct.sdb2;
                        ct.sdb3 = string.IsNullOrEmpty(ct.sdb3) ? ct.sdb3 : "'" + ct.sdb3;
                        ct.mtau = string.IsNullOrEmpty(ct.mtau) ? ct.mtau : "'" + ct.mtau;
                        if (!string.IsNullOrWhiteSpace(ct.nlanh))
                        {
                            if (ct.nlanh == "TFHNO") ct.nlanh = "HN";
                            if (ct.nlanh == "TFHPH") ct.nlanh = "HP";
                            if (ct.nlanh == "TFGBA") ct.nlanh = "GB";
                            if (ct.nlanh == "TFNBI") ct.nlanh = "NB";
                            if (ct.nlanh == "TFDHO") ct.nlanh = "DH";
                            if (ct.nlanh == "TFDNA") ct.nlanh = "DN";
                            if (ct.nlanh == "TFDTR") ct.nlanh = "DT";
                            if (ct.nlanh == "TFNTR") ct.nlanh = "NT";
                            if (ct.nlanh == "TFBTH") ct.nlanh = "BT";
                            if (ct.nlanh == "TFSGO") ct.nlanh = "SG";
                            if (ct.nlanh == "TFSOT") ct.nlanh = "ST";
                        }
                        if (ct.socb == ctOld.socb)
                        {

                            ct.slbt = 0;
                            ct.sllh = 0;
                            ct.slbs = 0;
                            ct.slpt = 0;
                            ct.dgkb = 0;
                            ct.slrk = 0;
                        }
                        ctOld = ct;
                    }

                    dgView.DataSource = listDT;
                    lblBanGhi.Text = "Tổng số " + cboDonVi.SelectedValue.ToString() + ": " + listDT.Count.ToString("N0");
                }
                if (cboDonVi.SelectedValue.ToString() == "VIN")
                {
                    var obj = HttpHelper.GetList<VIXuatDT>(Configuration.UrlCBApi + "api/Vinhs/VIGetXuatDTGA?" + data);
                    if (obj.Count <= 0)
                    {
                        throw new Exception("Không có dữ liệu cơ báo.");
                    }
                    var listDT = (from dt in obj
                                  group dt by new
                                  {
                                      dt.socb,
                                      dt.lmay,
                                      dt.dmay,
                                      dt.sdb1,
                                      dt.ten1,
                                      dt.sdb2,
                                      dt.ten2,
                                      dt.sdb3,
                                      dt.ten3,
                                      dt.sdb4,
                                      dt.ten4,
                                      dt.tau,
                                      dt.cty,
                                      dt.ctac,
                                      dt.ctacp,
                                      dt.tchat,
                                      dt.kdoan,
                                      dt.slbt,
                                      dt.sll1,
                                      dt.sll2,
                                      dt.slsd,
                                      dt.slbs,
                                      dt.sltt,
                                      dt.sltc,
                                      dt.slpt,
                                      dt.kho1,
                                      dt.nlieu,
                                      dt.thnl,
                                      dt.thbt,
                                      dt.phnl,
                                      dt.phpt,
                                      dt.pdoan,
                                      dt.gaxp,
                                      dt.daycb,
                                      dt.dgkb,
                                      dt.dgdm,
                                      dt.dgdn,
                                      dt.dgkm,
                                      dt.dgkn,
                                      dt.dgqd,
                                      dt.giqv,
                                      dt.gilh,
                                      dt.gidt,
                                      dt.dgxp,
                                      dt.dgdd,
                                      dt.dgcc,
                                      dt.dnxp,
                                      dt.dndd,
                                      dt.dncc,
                                      dt.slrk,
                                      dt.kmch,
                                      dt.kmdw,
                                      dt.kmgh,
                                      dt.kmdy,
                                      dt.tkch,
                                      dt.tkdw,
                                      dt.tkgh,
                                      dt.tkdy,
                                      dt.slrkm,
                                      dt.slrkn,
                                      dt.tgtnm,
                                      dt.tgtnn,
                                      dt.ThangDT,
                                      dt.NamDT
                                  } into g
                                  select new VIXuatDT
                                  {
                                      socb = g.Key.socb,
                                      lmay = g.Key.lmay,
                                      dmay = g.Key.dmay,
                                      sdb1 = g.Key.sdb1,
                                      ten1 = g.Key.ten1,
                                      sdb2 = g.Key.sdb2,
                                      ten2 = g.Key.ten2,
                                      sdb3 = g.Key.sdb3,
                                      ten3 = g.Key.ten3,
                                      sdb4 = g.Key.sdb4,
                                      ten4 = g.Key.ten4,
                                      tau = g.Key.tau,
                                      cty = g.Key.cty,
                                      ctac = g.Key.ctac,
                                      tchat = g.Key.tchat,
                                      kdoan = g.Key.kdoan,
                                      slbt = g.Key.slbt,
                                      slbs = g.Key.slbs,
                                      sltt = g.Key.sltt,
                                      sltc = g.Key.sltc,
                                      slpt = g.Key.slpt,
                                      kho1 = g.Key.kho1,
                                      gaxp = g.Key.gaxp,
                                      daycb = g.Key.daycb,
                                      dgkb = g.Key.dgkb,
                                      dgdm = g.Key.dgdm,
                                      dgdn = g.Key.dgdn,
                                      dgkm = g.Key.dgkm,
                                      dgkn = g.Key.dgkn,
                                      dgqd = g.Key.dgqd,
                                      giqv = g.Key.giqv,
                                      gilh = g.Key.gilh,
                                      gidt = g.Key.gidt,
                                      dgxp = g.Key.dgxp,
                                      dgdd = g.Key.dgdd,
                                      dgcc = g.Key.dgcc,
                                      dnxp = g.Key.dnxp,
                                      dndd = g.Key.dndd,
                                      dncc = g.Key.dncc,
                                      slrk = g.Key.slrk,
                                      kmch = g.Key.kmch,
                                      kmdw = g.Key.kmdw,
                                      kmgh = g.Key.kmgh,
                                      kmdy = g.Key.kmdy,
                                      tkch = g.Key.tkch,
                                      tkdw = g.Key.tkdw,
                                      tkgh = g.Key.tkgh,
                                      tkdy = g.Key.tkdy,
                                      slrkm = g.Key.slrkm,
                                      slrkn = g.Key.slrkn,
                                      ThangDT = g.Key.ThangDT,
                                      NamDT = g.Key.NamDT
                                  }).ToList();
                    VIXuatDT ctOld = new VIXuatDT();
                    foreach (VIXuatDT ct in listDT)
                    {
                        ct.dmay = ct.dmay.Split('-')[1];
                        //ct.socb = "'" + ct.socb;
                        //ct.sdb1 = string.IsNullOrEmpty(ct.sdb1) ? ct.sdb1 : "'" + ct.sdb1;
                        //ct.sdb2 = string.IsNullOrEmpty(ct.sdb2) ? ct.sdb2 : "'" + ct.sdb2;
                        //ct.sdb3 = string.IsNullOrEmpty(ct.sdb3) ? ct.sdb3 : "'" + ct.sdb3;
                        if (!string.IsNullOrWhiteSpace(ct.kho1))
                        {
                            if (ct.kho1 == "TFHNO") ct.kho1 = "HN";
                            if (ct.kho1 == "TFHPH") ct.kho1 = "HP";
                            if (ct.kho1 == "TFGBA") ct.kho1 = "GB";
                            if (ct.kho1 == "TFNBI") ct.kho1 = "NB";
                            if (ct.kho1 == "TFDHO") ct.kho1 = "DH";
                            if (ct.kho1 == "TFDNA") ct.kho1 = "DN";
                            if (ct.kho1 == "TFDTR") ct.kho1 = "DT";
                            if (ct.kho1 == "TFNTR") ct.kho1 = "NT";
                            if (ct.kho1 == "TFBTH") ct.kho1 = "BT";
                            if (ct.kho1 == "TFSGO") ct.kho1 = "SG";
                            if (ct.kho1 == "TFSOT") ct.kho1 = "ST";
                        }
                        if (ct.socb == ctOld.socb)
                        {

                            ct.slbt = 0;
                            ct.slbs = 0;
                            ct.slpt = 0;
                            ct.slrk = 0;
                        }
                        ctOld = ct;
                    }

                    dgView.DataSource = listDT;
                    lblBanGhi.Text = "Tổng số " + cboDonVi.SelectedValue.ToString() + ": " + listDT.Count.ToString("N0");
                }
                if (cboDonVi.SelectedValue.ToString() == "DN")
                {
                    var obj = HttpHelper.GetList<DNXuatDT>(Configuration.UrlCBApi + "api/DaNangs/DNGetXuatDTGA?" + data).OrderBy(x => x.giodi).ThenBy(x => x.socb).ThenBy(x => x.mactau).ThenBy(x => x.ngayxp).ToList();
                    if (obj.Count <= 0)
                    {
                        throw new Exception("Không có dữ liệu cơ báo.");
                    }
                    var listDT = (from dt in obj
                                  group dt by new
                                  {
                                      dt.socb,
                                      dt.madm,
                                      dt.makd,
                                      dt.mactau,
                                      dt.matc,
                                      dt.ThangDT,
                                      dt.NamDT
                                  } into g
                                  select new DNXuatDT
                                  {
                                      socb = g.Key.socb,
                                      madm = g.Key.madm,
                                      mact = g.FirstOrDefault().mact,
                                      makd = g.Key.makd,
                                      ngaydi = g.FirstOrDefault().ngaydi,
                                      mactau = g.Key.mactau,
                                      matc = g.Key.matc,
                                      solanrk = g.FirstOrDefault().solanrk,
                                      km = g.Sum(x => x.km),
                                      tan = g.Sum(x => x.km) > 0 ? g.Sum(x => x.tkm) / g.Sum(x => x.km) : 0,
                                      tkm = g.Sum(x => x.tkm),
                                      donxp = g.Sum(x => x.donxp),
                                      dondd = g.Sum(x => x.dondd),
                                      doncc = g.Sum(x => x.doncc),
                                      dungdd = g.Sum(x => x.dungdd),
                                      dungxp = g.Sum(x => x.dungxp),
                                      dungcc = g.Sum(x => x.dungcc),
                                      lh = g.Sum(x => x.lh),
                                      qvlh = g.Sum(x => x.qvlh),
                                      dungdn = g.Sum(x => x.dungdn),
                                      dungdm = g.Sum(x => x.dungdm),
                                      nltc = g.Sum(x => x.nltc),
                                      nltt = g.Sum(x => x.nltt),
                                      gaxp = g.FirstOrDefault().gaxp,
                                      ngayxp = g.FirstOrDefault().ngayxp,
                                      ThangDT = g.Key.ThangDT,
                                      NamDT = g.Key.NamDT
                                  }).ToList();
                    DNXuatDT ctOld = new DNXuatDT();
                    foreach (DNXuatDT ct in listDT)
                    {
                        ct.madm = ct.madm.Split('-')[1];
                        ct.tan=Math.Round(ct.tan, 2);
                        ct.tanqd = ct.tan;
                        ct.tkmqd = ct.tkm;
                        ct.giodi = ct.ngaydi.ToString("HHmm");
                        ct.dungddnl = ct.dungdd > 30 ? 30 : ct.dungdd;
                        ct.dungxpnl = ct.dungxp > 30 ? 30 : ct.dungxp;
                        ct.dungccnl = ct.dungcc > 30 ? 30 : ct.dungcc;
                        ct.dctlmdmnl = ct.dungdm > 30 ? 30 : ct.dungdm;
                        ct.dctlmdnnl = ct.dungdn > 30 ? 30 : ct.dungdn;
                        ct.nltt15 = ct.nltt;
                        if (ct.socb == ctOld.socb)
                        {
                            ct.solanrk = 0;
                        }
                        ctOld = ct;
                    }

                    dgView.DataSource = listDT;
                    lblBanGhi.Text = "Tổng số " + cboDonVi.SelectedValue.ToString() + ": " + listDT.Count.ToString("N0");
                }
                if (cboDonVi.SelectedValue.ToString() == "SG")
                {
                    var obj = HttpHelper.GetList<SGXuatDT>(Configuration.UrlCBApi + "api/SaiGons/SGGetXuatDTGA?" + data).OrderBy(x => x.daycb).ThenBy(x => x.socb).ToList();
                    if (obj.Count <= 0)
                    {
                        throw new Exception("Không có dữ liệu cơ báo.");
                    }
                    SGXuatDT ctOld = new SGXuatDT();
                    foreach (SGXuatDT ct in obj)
                    {
                        ct.dmay = ct.dmay.Split('-')[1];
                        if(!string.IsNullOrWhiteSpace(ct.nlanh))
                        {
                            if (ct.nlanh == "TFGBA") ct.nlanh = "GB";
                            if (ct.nlanh == "TFDNA") ct.nlanh = "DN";
                            if (ct.nlanh == "TFDTR") ct.nlanh = "DT";
                            if (ct.nlanh == "TFNTR") ct.nlanh = "NT";
                            if (ct.nlanh == "TFBTH") ct.nlanh = "BT";                           
                            if (ct.nlanh == "TFSGO") ct.nlanh = "SG";
                            if (ct.nlanh == "TFSOT") ct.nlanh = "ST";
                        }    
                        if(ct.socb==ctOld.socb)
                        {

                            ct.slbt = 0;
                            ct.sllh = 0;
                            ct.slbs = 0;
                            ct.slbs = 0;
                            ct.slsd = 0;
                            ct.dgkb = 0;
                            ct.slrk = 0;
                        }    

                       ctOld = ct;
                    }    
                    
                    dgView.DataSource = obj;
                    lblBanGhi.Text = "Tổng số " + cboDonVi.SelectedValue.ToString() + ": " + obj.Count.ToString("N0");
                }  
                
                base.Cursor = Cursors.Default;
                btnExport.Enabled = true;
            }
            catch (Exception ex)
            {
                base.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message); 
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                Library.FormHelper.ExportExcel(dgView);
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                base.Cursor = Cursors.Default;
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
