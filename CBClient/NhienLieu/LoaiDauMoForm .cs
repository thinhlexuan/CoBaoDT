using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CBClient.BLLTypes;
using CBClient.Library;
using CBClient.Models;
using CBClient.Services;

namespace CBClient.NhienLieu
{
    public partial class LoaiDauMoForm : DevComponents.DotNetBar.Metro.MetroForm
    {       
        bool bThem = false;       
        public LoaiDauMoForm()
        {
            DevComponents.DotNetBar.StyleManager.ChangeStyle(MainForm.Instance.m_BaseStyle, System.Drawing.Color.Empty); 
            InitializeComponent();
            Library.FormHelper.AddEnterKeyPressAsTabEventHandler(this);           
            ShowControl(false);
        }      
        
        private void btnTraTim_Click(object sender, EventArgs e)
        {
            fnTraTim();
        }      
        private void fnTraTim()
        {
            try
            {
                bsLoaiDM.DataSource = null;                
                base.Cursor = Cursors.WaitCursor;
                string data = "?tenDM=" + txtTenDMTT.Text.Trim();                             
                List<DMLoaiDauMo> listLoaiDM = HttpHelper.GetList<DMLoaiDauMo>(Configuration.UrlCBApi + "api/NhienLieus/NLGetLoaiDauMo" + data).ToList();                
                if (listLoaiDM.Count <= 0)
                {
                    throw new Exception("Không có dữ liệu.");

                }
                bsLoaiDM.DataSource = listLoaiDM;
                dataGridView1.Refresh();
                lblTableCount.Text = "Tổng số bản ghi:" + listLoaiDM.Count.ToString("N0");               
                base.Cursor = Cursors.Default;
                dataGridView1.Focus();
                dataGridView1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bsLoaiDM.DataSource = null;
                base.Cursor = Cursors.Default;
            }
            ShowControl(false);
        }

        private void ShowControl(bool b)
        {
            ExpTraTim.Enabled = !b;
            dataGridView1.Enabled = !b;
            txtID.Enabled = b;
            txtTenDM.Enabled = b;
            txtDonVi.Enabled = b;
            chkActive.Enabled = b;
            btnThem.Enabled = !b;
            if (b == false)
            {
                btnSua.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnXoa.Enabled = dataGridView1.CurrentRow == null ? false : true;
                btnExport.Enabled = dataGridView1.CurrentRow == null ? false : true;
            }
            else
            {
                btnSua.Enabled = !b;
                btnXoa.Enabled = !b;
                btnExport.Enabled = !b;
            }
            btnLuu.Enabled = b;
            btnHuy.Enabled = b;
        }

        private void ClearControl()
        {
            txtID.Text = "0";
            txtTenDM.ResetText();
            txtDonVi.ResetText();
            chkActive.Checked = true;
        }

        private void BindControl()
        {
            DMLoaiDauMo loaiDM = bsLoaiDM.Current as DMLoaiDauMo;
            if (loaiDM != null)
            {
                txtID.Text = loaiDM.ID.ToString();
                txtTenDM.Text = loaiDM.LoaiDauMo;
                txtDonVi.Text = loaiDM.DonViTinh;    
                chkActive.Checked = loaiDM.Active;
            }
        }

        private DMLoaiDauMo BindObject()
        {
            DMLoaiDauMo loaiDM = new DMLoaiDauMo();
            if (!bThem)
                loaiDM = bsLoaiDM.Current as DMLoaiDauMo;
            loaiDM.ID = short.Parse(txtID.Text);
            loaiDM.LoaiDauMo = txtTenDM.Text;
            loaiDM.DonViTinh = txtDonVi.Text;
            loaiDM.Active = chkActive.Checked;
            return loaiDM;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AppGlobal.User.NL < 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            ClearControl();          
            bThem = true;
            ShowControl(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần sửa.");
                return;
            }
            if (AppGlobal.User.NL < 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            BindControl();
            ShowControl(true);           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn bản ghi cần xóa.");
                return;
            }
            if (AppGlobal.User.NL < 3)
            {
                MessageBox.Show("Bạn không có quyền này.");
                return;
            }
            DMLoaiDauMo loaiDM = bsLoaiDM.Current as DMLoaiDauMo;
            if (Library.DialogHelper.Confirm("Xóa dầu mỡ này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                var opStatus = HttpHelper.Delete<NL_NhaCC>(Configuration.UrlCBApi + "api/NhienLieus/NLDeleteLoaiDauMo?id=" + loaiDM.ID);
                if (opStatus.Result.ID== loaiDM.ID)
                    bsLoaiDM.Remove(loaiDM);
                else
                    Library.DialogHelper.Error(opStatus.IsFaulted.ToString());
            }
            BindControl();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                DMLoaiDauMo loaiDM = BindObject();               
                if (bThem)
                {
                    loaiDM.CreatedBy = AppGlobal.User.Username;
                    loaiDM.CreatedName = AppGlobal.User.FullName;
                    loaiDM.CreatedDate = DateTime.Now;
                    loaiDM.ModifyBy = loaiDM.CreatedBy;
                    loaiDM.ModifyName = loaiDM.CreatedName;
                    loaiDM.ModifyDate = loaiDM.CreatedDate;
                    var objInsert = await HttpHelper.Post<DMLoaiDauMo>(Configuration.UrlCBApi + "api/NhienLieus/NLPostLoaiDauMo", loaiDM);
                    bsLoaiDM.Add(loaiDM);
                    bsLoaiDM.MoveLast();
                }
                else
                {
                    loaiDM.ModifyBy = AppGlobal.User.Username;
                    loaiDM.ModifyName= AppGlobal.User.FullName;
                    loaiDM.ModifyDate = DateTime.Now;                    
                    var objUpdate = await HttpHelper.Put<DMLoaiDauMo>(Configuration.UrlCBApi + "api/NhienLieus/NLPutLoaiDauMo?id=" + loaiDM.ID, loaiDM);
                    bsLoaiDM.EndEdit();                   
                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                Library.DialogHelper.Error(ex.Message);
            }
            ClearControl();
            bThem = false;
            ShowControl(false);
            BindControl();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearControl();
            bThem = false;
            ShowControl(false);
            BindControl();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            BindControl();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                base.Cursor = Cursors.WaitCursor;
                Library.FormHelper.ExportExcel(dataGridView1);
                base.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                base.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
