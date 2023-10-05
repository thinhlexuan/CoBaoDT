using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBClient.BLLTypes
{
    public partial class DSNLDinhMuc
    {        
        public long ID { get; set; }
        public string MaDV { get; set; }
        public string LoaiMayID { get; set; }
        public short? CongTacId { get; set; }         
        public string GhiChu { get; set; }
        public decimal? DinhMuc { get; set; }
        public string DonVi { get; set; }
        public DateTime NgayHL { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
}
