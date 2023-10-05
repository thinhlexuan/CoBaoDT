using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CBClient.Common;
using CBClient.Library;
using CBClient.Services;

namespace CBClient.HeThong
{
    public partial class Login : Form
    {        
        public Login()
        {
            InitializeComponent();
            FormHelper.AddEnterKeyPressAsTabEventHandler(this);
            //txtUserName.Text = "hh_dmtrucban1";
            //txtPassword.Text = "1234567";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {           
            bool validate = true;
            MainForm.Instance.Cursor = Cursors.WaitCursor;
            try
            {
                if (String.IsNullOrEmpty(txtUserName.Text) || (string.IsNullOrWhiteSpace(txtUserName.Text)))
                {
                    //this.ShowDialog();
                    validate = false;
                    throw new Exception("Chưa nhập tài khoản hoặc tài khoản nhập không đúng định dạng!");

                }
                if (String.IsNullOrEmpty(txtPassword.Text) || (string.IsNullOrWhiteSpace(txtPassword.Text)))
                {
                    validate = false;
                    throw new Exception("Chưa nhập mật khẩu hoặc mật khẩu nhập không đúng định dạng!");
                }
                if (validate)
                {                    
                    var data = await AuthenticationService.Login(txtUserName.Text.Trim(), txtPassword.Text.Trim(), null);                   
                    if (data != null && !string.IsNullOrEmpty(data.userName) && !string.IsNullOrEmpty(data.access_token))
                    {

                        var access_token = string.Format("Bearer {0}{1}", data.userClientId, data.access_token);
                        var input = new NhanVienInput();
                        input.Username = data.userName;
                        var res = await AuthenticationService.GetNhanVienByUsername(input, data.userName, access_token);
                        if (res == null)
                        {
                             throw new Exception("Cảnh báo: Không lấy được thông tin nhân viên.");                            
                        }                        
                        MainForm.Instance.Data = data;
                        AppGlobal.dmNhanVien = res;                       
                        this.DialogResult = DialogResult.OK;
                        MainForm.Instance.Cursor = Cursors.Default;
                        this.Close();
                    }
                    else
                    {
                        throw new Exception("Có lỗi trong quá trình đăng nhập!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainForm.Instance.Cursor = Cursors.Default;
            }
        }
    }
}
