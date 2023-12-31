using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CBClient.Library;

namespace CBClient.HeThong
{
   public partial class ChangePWD : Form
   {
       public ChangePWD()
      {
         InitializeComponent();
      }     
      private void ChangePasswordForm_Load(object sender, EventArgs e)
      {      

         txtUserName.Text =AppGlobal.User.FullName;
         SendKeys.Send("{TAB}");      
      }

      private void btnAccept_Click(object sender, EventArgs e)
      {
         if (!IsFormValid()) return;
         //var opStatus = AppGlobal.Proxy.ChangePWD(AppGlobal.User.MaNV, Library.FormHelper.Encrypt(txtPasswordNew.Text));        
         //lblInfo.ForeColor = Color.Blue;
         //if (opStatus.IsSuccess)
         //{
         //   lblInfo.Text = "Đổi mật khẩu thành công !";
         //   AppGlobal.User.MatKhau = txtConfirmPassword.Text;               
         //}
         //else
         //{
         //   lblInfo.ForeColor = Color.Red;
         //   lblInfo.Text = "Không đổi được mật khẩu !";
         //}
      }

      #region IsFormValid
      private bool IsFormValid()
      {
         // check the fields for valid data and
         // display message boxes if neccessary  
         // if (AppGlobal.User.MatKhau != Library.FormHelper.Encrypt(txtPasswordOld.Text))
         //{
         //   lblInfo.Text = "Nhập mật khẩu không đúng.";
         //   txtPasswordOld.SelectAll();  
         //   txtPasswordOld.Focus();  
         //   return false;
         //}
         if (txtPasswordNew.Text.IndexOf(" ") > -1)
         {
            lblInfo.Text = "Mật khẩu không bao gồm dấu trắng";
            txtPasswordNew.SelectAll();
            txtPasswordNew.Focus(); 
            return false;
         }

         if (txtPasswordNew.Text.Length < 3)
         {
            lblInfo.Text = "Mật khẩu phải có ít nhất 3 ký tự";
            txtPasswordNew.SelectAll();
            txtPasswordNew.Focus();
            return false;
         }

         if (txtPasswordNew.Text != txtConfirmPassword.Text)
         {
            lblInfo.Text = "Nhập mật khẩu mới không hợp lệ.";
            txtConfirmPassword.SelectAll();
            txtConfirmPassword.Focus();
            return false;
         }         
         return true;
      }
      #endregion

      private void TextBox_Validated(object sender, EventArgs e)
      {
         lblInfo.Text = string.Empty;
         if (((Control)sender).Text.Trim().Length ==0)
         {
            lblInfo.ForeColor = Color.Red;
            lblInfo.Text = "Không được để mật khẩu trống !.";
            ((Control)sender).Focus(); 
         }
      }

      private void TextBox_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Return)
            SendKeys.Send("{TAB}");            
      }

      private void TextBox_Enter(object sender, EventArgs e)
      {
         ((TextBox)sender).SelectAll();
      }

      private void btnCancel_Click(object sender, EventArgs e)
      {
         this.Close();  
      }      

   }
}