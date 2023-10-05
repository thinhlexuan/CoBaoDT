using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CBClient.BaoCao
{
    public partial class PreViewDialogKH : Form
    {
        public PreViewDialogKH(string rptResource, string rptName,object rptValue, List<ReportParameter> rptParamList)
        {
            InitializeComponent();
            try
            {
                reportViewer1.Reset();                
                reportViewer1.LocalReport.ReportEmbeddedResource = rptResource;

                ReportDataSource rds = new ReportDataSource();
                rds.Name = rptName;
                rds.Value = rptValue;                

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);                

                reportViewer1.LocalReport.SetParameters(rptParamList);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.PageWidth;
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        private void PreViewDialog_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
        }

    }
}
