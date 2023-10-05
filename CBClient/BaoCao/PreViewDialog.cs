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
    public partial class PreViewDialog : Form
    {
        public PreViewDialog(string rptResource, 
            string rptName1,object rptValue1, 
            string rptName2,object rptValue2, 
            string rptName3,object rptValue3,
            string rptName4, object rptValue4,
            List<ReportParameter> rptParamList)
        {
            InitializeComponent();
            try
            {
                reportViewer1.Reset();                
                reportViewer1.LocalReport.ReportEmbeddedResource = rptResource;

                ReportDataSource rds1 = new ReportDataSource();
                rds1.Name = rptName1;
                rds1.Value = rptValue1;
                ReportDataSource rds2 = new ReportDataSource();
                rds2.Name = rptName2;
                rds2.Value = rptValue2;
                ReportDataSource rds3 = new ReportDataSource();
                rds3.Name = rptName3;
                rds3.Value = rptValue3;
                ReportDataSource rds4 = new ReportDataSource();
                rds4.Name = rptName4;
                rds4.Value = rptValue4;

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds1);
                reportViewer1.LocalReport.DataSources.Add(rds2);
                reportViewer1.LocalReport.DataSources.Add(rds3);
                reportViewer1.LocalReport.DataSources.Add(rds4);

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
