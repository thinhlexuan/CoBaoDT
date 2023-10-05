using CBClient.BLLTypes;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CBClient.Library
{
    public class FormHelper
    {
        public static CultureInfo EnCultureInfo = CultureInfo.GetCultureInfo("en-Us");
        public static string ConvertString(string strInput)
        {
            decimal number = decimal.Parse(strInput, EnCultureInfo);
            return number.ToString();
        }        
        public static void AddEnterKeyPressAsTabEventHandler(Control innerControl)
        {
            foreach (System.Windows.Forms.Control ctrl in innerControl.Controls)
            {
                AddEnterKeyPressAsTabEventHandler(ctrl);
                if (ctrl is TextBox || ctrl is CheckBox || ctrl is ComboBox ||
                    ctrl is DateTimePicker || ctrl is NumericUpDown || ctrl is MaskedTextBox)
                {
                    ctrl.KeyDown += (s, e) =>
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            e.SuppressKeyPress = true;
                            SendKeys.Send("{TAB}");
                        }
                    };
                }
            }
        }
        public static void AddOnEnterSelectAll(Form frm, Control innerCtrl)
        {
            foreach (Control ctrl in innerCtrl.Controls)
            {
                AddOnEnterSelectAll(frm, ctrl);
                if (ctrl is TextBox || ctrl is MaskedTextBox)
                {
                    ctrl.Enter += (sender, e) =>
                    {
                        if (sender is TextBox)
                            ((TextBox)sender).SelectAll();
                        if (sender is MaskedTextBox)
                        {
                            frm.BeginInvoke((MethodInvoker)delegate()
                            {
                                ((MaskedTextBox)sender).SelectAll();
                            });
                        }
                    };
                }

            }
        }
        public static void AddKeyPressEventHandlerForNumber(TextBox innerTextBox)
        {
            innerTextBox.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
        }
        public static void AddKeyPressEventHandlerForDecimal(TextBox innerTextBox)
        {
            innerTextBox.KeyPress += (s, e) =>
            {                
                double value = 0;
                if (!double.TryParse(innerTextBox.Text + e.KeyChar.ToString(), out value) && !char.IsControl(e.KeyChar)
                && e.KeyChar != '-')
                {
                    e.Handled = true;
                }
            };
        }
        public static string Encrypt(string strToEncrypt)
        {
            byte[] bytKey = System.Text.Encoding.UTF8.GetBytes("V^r!x@Z#c$a%M~b&h*K(e)$_");
            byte[] bytIV = System.Text.Encoding.UTF8.GetBytes("r~g^p$%b$");
            TripleDESCryptoServiceProvider objTriplesDES = new TripleDESCryptoServiceProvider();
            try
            {
                byte[] bytInput = Encoding.UTF8.GetBytes(strToEncrypt);
                using (MemoryStream objOutputStream = new MemoryStream())
                {
                    //Encrypt the byte array
                    CryptoStream objCryptoStream = new CryptoStream(objOutputStream,
                      objTriplesDES.CreateEncryptor(bytKey, bytIV),
                      CryptoStreamMode.Write);
                    objCryptoStream.Write(bytInput, 0, bytInput.Length);
                    objCryptoStream.FlushFinalBlock();

                    //return the byte array as a Base64 string
                    return Convert.ToBase64String(objOutputStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex.InnerException);
            }
        }

        public static string Decrypt(string strToDecrypt)
        {
            byte[] bytKey = System.Text.Encoding.UTF8.GetBytes("V^r!x@Z#c$a%M~b&h*K(e)$_");
            byte[] bytIV = System.Text.Encoding.UTF8.GetBytes("r~g^p$%b$");
            TripleDESCryptoServiceProvider objTriplesDES = new TripleDESCryptoServiceProvider();
            try
            {
                byte[] bytInput = Convert.FromBase64String(strToDecrypt);
                using (MemoryStream objOutputStream = new MemoryStream())
                {
                    //Encrypt the byte array
                    CryptoStream objCryptoStream = new CryptoStream(objOutputStream,
                      objTriplesDES.CreateDecryptor(bytKey, bytIV),
                      CryptoStreamMode.Write);
                    objCryptoStream.Write(bytInput, 0, bytInput.Length);
                    objCryptoStream.FlushFinalBlock();

                    //return the byte array as a Base64 string

                    return Encoding.UTF8.GetString(objOutputStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex.InnerException);
            }
        }
        // Bo gridview selected cell color
        public static void AddGridSelectionColors(DataGridView grid)
        {
            grid.DefaultCellStyle.SelectionBackColor = grid.DefaultCellStyle.BackColor;
            grid.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.SteelBlue;
        }
        //Doi tu chuoi thoi gian ra phut
        public static int ConvertTimeStrToMinute(string strTime)
        {
            if (string.IsNullOrWhiteSpace(strTime))
                return 0;
            if (!strTime.Contains(':'))
                throw new Exception("Kiểu giờ không hợp lệ, giờ có dạng HH:mm");
            string[] words = strTime.Split(':');
            bool _convertOK = false;
            int _hour = 0;
            _convertOK = int.TryParse(words[0], out _hour);
            if (_convertOK == false)
                throw new Exception("Kiểu giờ không hợp lệ, phần giờ phải có dạng số");
            if (_hour > 23 || _hour < 0)
                throw new Exception("Kiểu giờ không hợp lệ, phần giờ phải nằm giữa 0 và 23");
            int _minute = 0;
            _convertOK = int.TryParse(words[1], out _minute);
            if (_convertOK == false)
                throw new Exception("Kiểu giờ không hợp lệ, phần phút phải có dạng số");
            if (_minute < 0 || _minute > 59)
                if (_hour > 23 || _hour < 0)
                    throw new Exception("Kiểu giờ không hợp lệ, phần phút phải nằm giữa 0 và 59");
            return _hour * 60 + _minute;
        }
        //Ham tru thoi gian neu nhu qua ngay thi cong them 24 gio
        public static int TimeSpanInMinutes(string strFirstTime, string strSecondTime)
        {
            int firstTime, secondTime;
            try
            {
                firstTime = ConvertTimeStrToMinute(strFirstTime);
                secondTime = ConvertTimeStrToMinute(strSecondTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return TimeSpanInMinutes(firstTime, secondTime);
        }
        private static int TimeSpanInMinutes(int firstTime, int secondTime)
        {
            return firstTime <= secondTime ? secondTime - firstTime : secondTime + 1440 - firstTime;
        }
        public static decimal TimeMinutesToHour(int timeMinutes)
        {
            if (timeMinutes <= 0) return 0.00M;
            int hourInt = timeMinutes / 60;
            int minitesInt = timeMinutes % 60;
            decimal hourConvert = decimal.Round(hourInt, 2, MidpointRounding.AwayFromZero);
            if (minitesInt >= 3 && minitesInt < 9)
                hourConvert += 0.10M;
            if (minitesInt >= 9 && minitesInt < 15)
                hourConvert += 0.20M;
            if (minitesInt >= 15 && minitesInt < 21)
                hourConvert += 0.30M;
            if (minitesInt >= 21 && minitesInt < 27)
                hourConvert += 0.40M;
            if (minitesInt >= 27 && minitesInt < 33)
                hourConvert += 0.50M;
            if (minitesInt >= 33 && minitesInt < 39)
                hourConvert += 0.60M;
            if (minitesInt >= 39 && minitesInt < 46)
                hourConvert += 0.70M;
            if (minitesInt >= 46 && minitesInt < 51)
                hourConvert += 0.80M;
            if (minitesInt >= 51 && minitesInt < 56)
                hourConvert += 0.90M;
            if (minitesInt >= 56 && minitesInt < 60)
                hourConvert += 1.00M;
            return hourConvert;
        }
        public static decimal TyTrongName(decimal Klr)
        {
            decimal K54B = 1;
            if (Klr >= 0.7890M && Klr < 0.7910M) K54B = 0.7900M;           
            if (Klr >= 0.7910M && Klr < 0.7930M) K54B = 0.7920M;
            if (Klr >= 0.7930M && Klr < 0.7950M) K54B = 0.7940M;
            if (Klr >= 0.7950M && Klr < 0.7970M) K54B = 0.7960M;
            if (Klr >= 0.7970M && Klr < 0.7990M) K54B = 0.7980M;
            if (Klr >= 0.7990M && Klr < 0.8010M) K54B = 0.8000M;
            if (Klr >= 0.8010M && Klr < 0.8030M) K54B = 0.8020M;
            if (Klr >= 0.8030M && Klr < 0.8050M) K54B = 0.8040M;
            if (Klr >= 0.8050M && Klr < 0.8070M) K54B = 0.8060M;
            if (Klr >= 0.8070M && Klr < 0.8090M) K54B = 0.8080M;
            if (Klr >= 0.8090M && Klr < 0.8110M) K54B = 0.8100M;
            if (Klr >= 0.8110M && Klr < 0.8130M) K54B = 0.8120M;
            if (Klr >= 0.8130M && Klr < 0.8150M) K54B = 0.8140M;
            if (Klr >= 0.8150M && Klr < 0.8170M) K54B = 0.8160M;
            if (Klr >= 0.8170M && Klr < 0.8190M) K54B = 0.8180M;
            if (Klr >= 0.8190M && Klr < 0.8210M) K54B = 0.8200M;
            if (Klr >= 0.8210M && Klr < 0.8230M) K54B = 0.8220M;
            if (Klr >= 0.8230M && Klr < 0.8250M) K54B = 0.8240M;
            if (Klr >= 0.8250M && Klr < 0.8270M) K54B = 0.8260M;
            if (Klr >= 0.8270M && Klr < 0.8290M) K54B = 0.8280M;
            if (Klr >= 0.8290M && Klr < 0.8310M) K54B = 0.8300M;
            if (Klr >= 0.8310M && Klr < 0.8330M) K54B = 0.8320M;
            if (Klr >= 0.8330M && Klr < 0.8350M) K54B = 0.8340M;
            if (Klr >= 0.8350M && Klr < 0.8370M) K54B = 0.8360M;
            if (Klr >= 0.8370M && Klr < 0.8390M) K54B = 0.8380M;
            if (Klr >= 0.8390M && Klr < 0.8410M) K54B = 0.8400M;
            if (Klr >= 0.8410M && Klr < 0.8430M) K54B = 0.8420M;
            if (Klr >= 0.8430M && Klr < 0.8450M) K54B = 0.8440M;
            if (Klr >= 0.8450M && Klr < 0.8470M) K54B = 0.8460M;
            if (Klr >= 0.8470M && Klr < 0.8490M) K54B = 0.8480M;
            if (Klr >= 0.8490M && Klr < 0.8510M) K54B = 0.8500M;
            if (Klr >= 0.8510M && Klr < 0.8530M) K54B = 0.8520M;
            if (Klr >= 0.8530M && Klr < 0.8550M) K54B = 0.8540M;
            if (Klr >= 0.8550M && Klr < 0.8570M) K54B = 0.8560M;
            if (Klr >= 0.8570M && Klr < 0.8590M) K54B = 0.8580M;
            if (Klr >= 0.8590M && Klr < 0.8610M) K54B = 0.8600M;
            if (Klr >= 0.8610M && Klr < 0.8630M) K54B = 0.8620M;
            if (Klr >= 0.8630M && Klr < 0.8650M) K54B = 0.8640M;
            if (Klr >= 0.8650M && Klr < 0.8670M) K54B = 0.8660M;
            if (Klr >= 0.8670M && Klr < 0.8690M) K54B = 0.8680M;
            if (Klr >= 0.8690M && Klr < 0.8710M) K54B = 0.8700M;
            return K54B;
        }
        public static decimal VCFValue(decimal columnName, NL_54BASTM nL_54BASTM)
        {
            switch(columnName)
            {
                case 0.7900M: return nL_54BASTM.K790;
                case 0.7920M: return nL_54BASTM.K792;
                case 0.7940M: return nL_54BASTM.K794;
                case 0.7960M: return nL_54BASTM.K796;
                case 0.7980M: return nL_54BASTM.K798;
                case 0.8000M: return nL_54BASTM.K800;
                case 0.8020M: return nL_54BASTM.K802;
                case 0.8040M: return nL_54BASTM.K804;
                case 0.8060M: return nL_54BASTM.K806;
                case 0.8080M: return nL_54BASTM.K808;
                case 0.8100M: return nL_54BASTM.K810;
                case 0.8120M: return nL_54BASTM.K812;
                case 0.8140M: return nL_54BASTM.K814;
                case 0.8160M: return nL_54BASTM.K816;
                case 0.8180M: return nL_54BASTM.K818;
                case 0.8200M: return nL_54BASTM.K820;
                case 0.8220M: return nL_54BASTM.K822;
                case 0.8240M: return nL_54BASTM.K824;
                case 0.8260M: return nL_54BASTM.K826;
                case 0.8280M: return nL_54BASTM.K828;
                case 0.8300M: return nL_54BASTM.K830;
                case 0.8320M: return nL_54BASTM.K832;
                case 0.8340M: return nL_54BASTM.K834;
                case 0.8360M: return nL_54BASTM.K836;
                case 0.8380M: return nL_54BASTM.K838;
                case 0.8400M: return nL_54BASTM.K840;
                case 0.8420M: return nL_54BASTM.K842;
                case 0.8440M: return nL_54BASTM.K844;
                case 0.8460M: return nL_54BASTM.K846;
                case 0.8480M: return nL_54BASTM.K848;
                case 0.8500M: return nL_54BASTM.K850;
                case 0.8520M: return nL_54BASTM.K852;
                case 0.8540M: return nL_54BASTM.K854;
                case 0.8560M: return nL_54BASTM.K856;
                case 0.8580M: return nL_54BASTM.K858;
                case 0.8600M: return nL_54BASTM.K860;
                case 0.8620M: return nL_54BASTM.K862;
                case 0.8640M: return nL_54BASTM.K864;
                case 0.8660M: return nL_54BASTM.K866;
                case 0.8680M: return nL_54BASTM.K868;
                case 0.8700M: return nL_54BASTM.K870;                      
                default: return 1;
            }                
        }
        public static string NumberToText(Int64 number)
        {
            var so = new[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            var hang = new[] { "", "nghìn", "triệu", "tỉ" };
            int i, j;
            int donvi, chuc, tram;
            string str;
            string sNumber = number.ToString();
            str = "";
            i = number.ToString().Length;
            j = 0;
            if (sNumber == "0")
            {
                return so[0];
            }
            while (j <= (i - 1))
            {
                if (sNumber.Substring(0, 1) == "0")
                {
                    number = int.Parse(sNumber.Substring(i - j));
                    sNumber = number.ToString();
                }
                j = j + 1;
            }
            i = sNumber.Length;
            if (i == 0)
            {
                str = "";
            }
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = int.Parse(sNumber.Substring(i - 1, 1));
                    i = i - 1;
                    if (i > 0)
                    {
                        chuc = int.Parse(sNumber.Substring(i - 1, 1));
                    }
                    else
                    {
                        chuc = -1;
                    }
                    i = i - 1;
                    if (i > 0)
                    {
                        tram =
                          int.Parse(sNumber.Substring(i - 1, 1));
                    }
                    else
                    {
                        tram = -1;
                    }
                    i = i - 1;
                    if (donvi > 0 || chuc > 0 || tram > 0 || (j % 3 == 0))
                    {
                        if (j % 3 != 0)
                        {
                            str = hang[(j - 1) % 3 + 1] + " " + str;
                        }
                        else
                        {
                            if (true)
                            {
                                string temp;
                                int k;
                                temp = "";
                                for (k = 1; k <= j / 3; k++)
                                {
                                    temp = " tỉ" + temp;
                                }
                                str = temp + ", " + str;
                            }
                        }
                    }
                    j = j + 1;
                    if (donvi == 1)
                    {
                        if (chuc > 1)
                            str = "mốt" + " " + str;
                        else
                            str = "một" + " " + str;
                    }
                    else
                    {
                        if (donvi == 5 && chuc > 0)
                        {
                            str = "lăm" + " " + str;
                        }
                        else if (donvi > 0)
                        {
                            str = so[donvi] + " " + str;
                        }
                    }
                    if (chuc < 0)
                    {
                        break;
                    }
                    else
                    {
                        if (chuc == 0 && donvi > 0)
                        {
                            str = "lẻ" + " " + str;
                        }
                        else if (chuc == 1)
                        {
                            str = "mười" + " " + str;
                        }

                        else if (chuc > 1)
                        {
                            str = so[chuc] + " " + "mươi" + " " + str;
                        }
                    }

                    if (tram < 0)
                    {
                        break;
                    }
                    else
                    {
                        if (tram > 0 || chuc > 0 || donvi > 0)
                        {
                            str = so[tram] +
                                  " " + "trăm" + " " + str;
                        }
                    }
                }
            }
            while (str != str.Replace(", tỉ", ","))
            {
                str = str.Replace(", tỉ", ",");
            }
            while (str != str.Replace(", ,", ","))
            {
                str = str.Replace(", ,", ",");
            }
            str = str.Trim();
            str = str.Substring(0, 1).ToUpper() + str.Substring(1);
            str = str.Substring(0, str.Length - 1);
            str = str.Trim();
            //if (str.Length > 50)
            //{
            //  int index = str.IndexOf(" ", 50) + 1;
            //  str = str.Substring(0, index) +
            //        "\n" + str.Substring(index);
            //}
            //if (str.EndsWith("tỉ") || str.EndsWith("triệu") || str.EndsWith("nghìn") ||
            //    str.EndsWith("trăm") || str.EndsWith("mươi") || str.EndsWith("ười"))
            //    str += " đồng chẵn";
            //else
            //    str += " đồng";
            return str;
        }
        public static void ExportExcel(DataGridView dgView)
        {
            try
            {
              
                for (int j = 0; j <= dgView.ColumnCount - 1; j++)
                {
                    string colName = dgView.Columns[j].HeaderText;
                }
                copyAlltoClipboard(dgView);
                Microsoft.Office.Interop.Excel.Application xlexcel;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlexcel = new Microsoft.Office.Interop.Excel.Application();
                xlexcel.Visible = true;
                xlWorkBook = xlexcel.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
               
            }
            catch (Exception ex)
            {                
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private static void copyAlltoClipboard(DataGridView dgView)
        {
            dgView.SelectAll();
            DataObject dataObj = dgView.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
        public static void ShowReport(ReportViewer reportViewer, string rptResource, string rptName,
                object rptValue, List<ReportParameter> rptParamList)
        {
            try
            {
                reportViewer.Reset();
                reportViewer.LocalReport.ReportEmbeddedResource = "CBClient.Report." + rptResource + ".rdlc";

                ReportDataSource rds = new ReportDataSource();
                rds.Name = rptName;
                rds.Value = rptValue;

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(rds);

                reportViewer.LocalReport.SetParameters(rptParamList);
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer.ZoomMode = ZoomMode.PageWidth;
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

    public class DialogHelper
    {
        public static DialogResult Confirm(string msg)
        {
            return MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static void Inform(string msg)
        {
            MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Exclaim(string msg)
        {
            MessageBox.Show(msg, "Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void Error(string msg)
        {
            MessageBox.Show(msg, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }       
    }

    public class KhuDoanInfo
    {
        public string KhuDoan { get; set; }
        public string CacGa { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTH { get; set; }
        public string NguoiTH { get; set; }       
    }
    static class Funcs
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }

    public class AutoCompleteTextBox : TextBox
    {
        private ListBox _listBox;
        private bool _isAdded;
        private String[] _values;
        private String _formerValue = String.Empty;

        public AutoCompleteTextBox()
        {
            InitializeComponent();
            ResetListBox();
        }

        private void InitializeComponent()
        {
            _listBox = new ListBox();
            KeyDown += this_KeyDown;
            KeyUp += this_KeyUp;
        }

        private void ShowListBox()
        {
            if (!_isAdded)
            {
                Parent.Controls.Add(_listBox);
                _listBox.Left = Left;
                _listBox.Top = Top + Height;
                _isAdded = true;
            }
            _listBox.Visible = true;
            _listBox.BringToFront();
        }

        private void ResetListBox()
        {
            _listBox.Visible = false;
        }

        private void this_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateListBox();
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    {
                        if (_listBox.Visible)
                        {
                            InsertWord((String)_listBox.SelectedItem);
                            ResetListBox();
                            _formerValue = Text;
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if ((_listBox.Visible) && (_listBox.SelectedIndex < _listBox.Items.Count - 1))
                            _listBox.SelectedIndex++;

                        break;
                    }
                case Keys.Up:
                    {
                        if ((_listBox.Visible) && (_listBox.SelectedIndex > 0))
                            _listBox.SelectedIndex--;

                        break;
                    }
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        private void UpdateListBox()
        {
            if (Text == _formerValue) return;
            _formerValue = Text;
            String word = GetWord();

            if (_values != null && word.Length > 0)
            {
                String[] matches = Array.FindAll(_values,
                                                 x => (x.StartsWith(word, StringComparison.OrdinalIgnoreCase) && !SelectedValues.Contains(x)));
                if (matches.Length > 0)
                {
                    ShowListBox();
                    _listBox.Items.Clear();
                    Array.ForEach(matches, x => _listBox.Items.Add(x));
                    _listBox.SelectedIndex = 0;
                    _listBox.Height = 0;
                    _listBox.Width = 0;
                    Focus();
                    using (Graphics graphics = _listBox.CreateGraphics())
                    {
                        for (int i = 0; i < _listBox.Items.Count; i++)
                        {
                            _listBox.Height += _listBox.GetItemHeight(i);
                            // it item width is larger than the current one
                            // set it to the new max item width
                            // GetItemRectangle does not work for me
                            // we add a little extra space by using '_'
                            int itemWidth = (int)graphics.MeasureString(((String)_listBox.Items[i]) + "_", _listBox.Font).Width;
                            _listBox.Width = (_listBox.Width < itemWidth) ? itemWidth : _listBox.Width;
                        }
                    }
                }
                else
                {
                    ResetListBox();
                }
            }
            else
            {
                ResetListBox();
            }
        }

        private String GetWord()
        {
            String text = Text;
            int pos = SelectionStart;

            int posStart = text.LastIndexOf(' ', (pos < 1) ? 0 : pos - 1);
            posStart = (posStart == -1) ? 0 : posStart + 1;
            int posEnd = text.IndexOf(' ', pos);
            posEnd = (posEnd == -1) ? text.Length : posEnd;

            int length = ((posEnd - posStart) < 0) ? 0 : posEnd - posStart;

            return text.Substring(posStart, length);
        }

        private void InsertWord(String newTag)
        {
            String text = Text;
            int pos = SelectionStart;

            int posStart = text.LastIndexOf(' ', (pos < 1) ? 0 : pos - 1);
            posStart = (posStart == -1) ? 0 : posStart + 1;
            int posEnd = text.IndexOf(' ', pos);

            String firstPart = text.Substring(0, posStart) + newTag;
            String updatedText = firstPart + ((posEnd == -1) ? "" : text.Substring(posEnd, text.Length - posEnd));


            Text = updatedText;
            SelectionStart = firstPart.Length;
        }

        public String[] Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }

        public List<String> SelectedValues
        {
            get
            {
                String[] result = Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return new List<String>(result);
            }
        }

    }
}
