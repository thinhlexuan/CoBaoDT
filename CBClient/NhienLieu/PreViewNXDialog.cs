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

namespace CBClient.NhienLieu
{
    public partial class PreViewNXDialog : Form
    {
        public PreViewNXDialog(string rptResource, 
            string rptName1,object rptValue1, 
            string rptName2,object rptValue2,            
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

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds1);
                reportViewer1.LocalReport.DataSources.Add(rds2);               

                reportViewer1.LocalReport.SetParameters(rptParamList);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
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
