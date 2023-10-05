using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CBClient.Controls
{
   public partial class SmartDate : TextBox
   {
      // string[] split = txtDateTime.Text.Split(new char[] {'.'});  
      // cac truong hop:
      //1.num             :split.length=1 : dayadd or daysub
      //2.dotnum          :split.length=2 : weekday so 1:CN, :T2, 2:T3 ...7:T7
      //3.numdot          :split.length=2 : nearest future day      
      //4.numdotnum       :split.length=2 : examp: d.m
      //4.numdotnumdot    :split.length=3 : examp: d.m. giong nhu d.m
      //5.numdotnumdotnum :split.length=3 : examp: d.m.y 
      private enum dateStyle
      {
         num = 1, dotnum = 2, numdot = 3,
         numdotnum = 4, numdotnumdotnum = 5, wrongformat = 6
      }

      private DateTime _datetime = new DateTime();
      private string stringFormat = "dd.MM.yyyy";
      public SmartDate()
      {
         _datetime = DateTime.Now;
         base.Text = _datetime.ToString(stringFormat);

         base.Font = new Font("Microsoft Sans Serif", 10F,
            FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
         base.ForeColor = System.Drawing.Color.Navy;
      }    

      public override string ToString()
      {
         return base.Text;
      }

      protected override void OnCreateControl()
      {
         //base.OnCreateControl();
         this.Value = DateTime.Now.Date; 
      }
         
      #region Properties
      public string Text
      {
         get { return base.Text; }
         set
         {
            if (string.IsNullOrEmpty(value))
               base.Text = _datetime.ToString(stringFormat);
            else
               base.Text = ToSmartDateTime(value);

            _datetimeOld = _datetime;
         }
      } 

      private bool _isTime;
      public bool IsTime
      {
         get { return _isTime; }
         set
         {
            _isTime = value;
            //_datetime = DateTime.Now;  
            stringFormat = (_isTime) ? "HH:mm" : "dd.MM.yyyy";
            base.Text = _datetime.ToString(stringFormat);
         }
      }

      private DateTime _datetimeOld;
      public DateTime Value
      {
         get {
            base.Text = _datetime.ToString(stringFormat);
            return _datetime; }
         set
         {
            _datetime = value;
            _datetimeOld = _datetime;             
            base.Text = _datetime.ToString(stringFormat);
         }
      }

      public string DMY
      {
         get { return _datetime.ToString("dd/MM/yyyy"); }
      }
      public string MDY
      {
         get { return _datetime.ToString("MM/dd/yyyy"); }
      }
      public string YMD
      {
         get { return _datetime.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo); }
      }

      #endregion

      #region DateStyleEnum
      // string[] split = txtDateTime.Text.Split(new char[] {'.'});  
      // cac truong hop:
      //1.num             :split.length=1 : dayadd or daysub
      //2.dotnum          :split.length=2 : weekday
      //3.numdot          :split.length=2 : nearest future day      
      //4.numdotnum       :split.length=2 : examp: d.m
      //4.numdotnumdot    :split.length=3 : examp: d.m. giong nhu 4.
      //5.numdotnumdotnum :split.length=3 : examp: d.m.y 

      private dateStyle DateStyleEnum(string dateString)
      {
         dateStyle _dateStyle = dateStyle.wrongformat;

         int posadd = dateString.LastIndexOf('+');
         int possub = dateString.LastIndexOf('-');
         int postdot = dateString.LastIndexOf(':');

         if (posadd > 0 || possub > 0 || postdot >= 0 ||
            ((posadd == 0 || possub == 0) && dateString.Length == 1))
            return _dateStyle;

         string[] split = dateString.Split(new char[] { '.' });
         switch (split.Length)
         {
            case 1:
               _dateStyle = dateStyle.num;
               break;
            case 2:
               if (split[1] == "")//numdot
               {
                  if (DateTime.DaysInMonth(_datetime.Year,
                       _datetime.Month) >= int.Parse(split[0]))
                     _dateStyle = dateStyle.numdot;
               }
               else
               {
                  if (split[0] == "")//dotnum
                  {
                     if (int.Parse(split[1]) >= 1 && int.Parse(split[1]) <= 7)
                        _dateStyle = dateStyle.dotnum;
                  }
                  else //numdotnum
                  {
                     if (int.Parse(split[1]) <= 12 && int.Parse(split[1])>0)
                        if (DateTime.DaysInMonth(_datetime.Year,
                             int.Parse(split[1])) >= int.Parse(split[0]))
                           _dateStyle = dateStyle.numdotnum;
                  }
               }
               break;
            case 3:
               if(split[0].Length >0 && split[1].Length >0)
                  if (int.Parse(split[1]) <= 12)
                  {
                     if (split[2] == "")//numdotnum
                     {
                        if (DateTime.DaysInMonth(_datetime.Year,
                                int.Parse(split[1])) >= int.Parse(split[0]))
                           _dateStyle = dateStyle.numdotnum;
                     }
                     else//numdotnumdotnum
                     {
                        int _yy = _datetime.Year;
                        if (split[2].Length == 4 && int.Parse(split[2]) <= 9999)
                           _yy = int.Parse(split[2]);
                        else if (split[2].Length < 4)
                        {
                           int len = _yy.ToString().Length - split[2].Length;
                           string year = _yy.ToString().Substring(0, len) + split[2];
                           _yy = int.Parse(year);
                        }
                        if (DateTime.DaysInMonth(_yy,
                           int.Parse(split[1])) >= int.Parse(split[0]))
                           _dateStyle = dateStyle.numdotnumdotnum;
                     }
                  }
               break;
         }
         return _dateStyle;
      }
      #endregion

      private string ToSmartDateTime(string ValueString)
      {
         _datetime = DateTime.Now.Date;  
         if (_isTime)
            return ToSmartTime(ValueString);
         else
            return ToSmartDate(ValueString);
      }

      #region ToSmartDate
      private string ToSmartDate(string dateString)
      {
         int _dd = _datetime.Day;
         int _mm = _datetime.Month;
         int _yy = _datetime.Year;

         string[] split = dateString.Split(new char[] { '.' });
         dateStyle _dateStyle = DateStyleEnum(dateString);
         switch (_dateStyle)
         {
            case dateStyle.num:
               _datetime = _datetime.AddDays(int.Parse(dateString));
               return _datetime.ToString(stringFormat);

            case dateStyle.dotnum:
               int _numday = int.Parse(split[1]) - 1; //tu CN-T7 ung voi _numday=0-6               
               if (_numday < 0 || _numday > 6) break;

               int _numdaycur = (int)_datetime.DayOfWeek;
               int _dayadd = 0;
               if (_numday >= _numdaycur)
                  _dayadd = _numday - _numdaycur;
               else
                  _dayadd = 6 - (_numdaycur - _numday) + 1;

               return _datetime.AddDays(_dayadd).ToString(stringFormat);

            case dateStyle.numdot:
               if (_dd > int.Parse(split[0]))
               {
                  _mm += 1;
                  if (_mm > 12)
                  {
                     _mm = 1;
                     _yy += 1;
                  }
               }
               _dd = int.Parse(split[0]);
               break;
            case dateStyle.numdotnum:
               _dd = int.Parse(split[0]);
               _mm = int.Parse(split[1]);
               DateTime _daynew = new DateTime(_yy, _mm, _dd);
               if (_datetime > _daynew)
                  _yy += 1;
               break;
            case dateStyle.numdotnumdotnum:
               _dd = int.Parse(split[0]);
               _mm = int.Parse(split[1]);
               if (split[2].Length == 4 && int.Parse(split[2]) <= 9999)
                  _yy = int.Parse(split[2]);
               else if (split[2].Length < 4)
               {
                  int len = _yy.ToString().Length - split[2].Length;
                  string year = _yy.ToString().Substring(0, len) + split[2];
                  _yy = int.Parse(year);
               }
               break;
            case dateStyle.wrongformat:
               break;
         }

         if (_dd <= 0) _dd = 1;
         if (_mm <= 0) _mm = 1;
         if (_yy <= 0) _yy = DateTime.MinValue.Year;

         _datetime = new DateTime(_yy, _mm, _dd);
         return _datetime.ToString(stringFormat);
      }

      #endregion

      #region ToSmartTime
      private string ToSmartTime(string timeString)
      {
         string[] split = timeString.Split(new char[] { '.', ':' });
         if (split.Length == 2)
         {
            int _dd = _datetime.Day;
            int _mm = _datetime.Month;
            int _yy = _datetime.Year;
            int _hour = _datetime.Hour;
            int _minute = _datetime.Minute;

            if (split[0] != "" && split[1] != "")
            {
               if (int.Parse(split[0]) < 24 && int.Parse(split[1]) < 60)
               {
                  _hour = int.Parse(split[0]);
                  _minute = int.Parse(split[1]);
               }
            }
            _datetime = new DateTime(_yy, _mm, _dd, _hour, _minute, 0);
         }
         return _datetime.ToString(stringFormat);
      }

      #endregion

      #region Events

      protected override void OnEnter(EventArgs e)
      {
         base.SelectAll();
         base.OnEnter(e);
      }

      protected override void OnLeave(EventArgs e)
      {
         _datetime = _datetimeOld;
         if (string.IsNullOrEmpty(base.Text))
            base.Text = _datetime.ToString(stringFormat);
         else
            base.Text = ToSmartDateTime(base.Text);

         base.OnLeave(e);
      }

      protected override void OnKeyDown(KeyEventArgs e)
      {
         //if (e.KeyCode == Keys.Return)
         //   SendKeys.Send("{TAB}");

         if (base.ReadOnly) return;

         if (!_isTime && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
         {
            int iday = (e.KeyCode == Keys.Up) ? 1 : -1;

            _datetime = _datetime.AddDays(iday);
            base.Text = _datetime.ToString(stringFormat);
         }
         base.OnKeyDown(e);
      }

      protected override void OnKeyPress(KeyPressEventArgs e)
      {
         //// Ignore all non-control and non-numeric key presses.
         ////e.KeyChar !=116 neu dung ca ky tu T

         if (((_isTime && e.KeyChar != 46 && e.KeyChar != 58) ||
           (!_isTime && e.KeyChar != 43 && e.KeyChar != 45 && e.KeyChar != 46))
           && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            e.Handled = true;

         base.OnKeyPress(e);
      }
      #endregion
   }
}
