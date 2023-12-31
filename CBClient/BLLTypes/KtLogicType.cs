﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBClient.BLLTypes
{
    public partial class KTQuayVong
    {
        public long CoBaoID { get; set; }
        public string SoCB { get; set; }
        public DateTime NgayCB { get; set; }
        public string DauMayID { get; set; }
        public string DvcbID { get; set; }
        public string TaiXe1ID { get; set; }
        public string TaiXe1Name { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public int QuayVong { get; set; }
    }
    public partial class KTDonThuan
    {      
        public long CoBaoID { get; set; }
        public string SoCB { get; set; }
        public DateTime NgayCB { get; set; }
        public string DauMayID { get; set; }
        public string DvcbID { get; set; }
        public string TaiXe1ID { get; set; }
        public string TaiXe1Name { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public int DonThuan { get; set; }
    }
    public partial class KTVanTocKT
    {
        public long CoBaoID { get; set; }
        public string SoCB { get; set; }
        public DateTime NgayCB { get; set; }
        public string DauMayID { get; set; }
        public string DvcbID { get; set; }
        public string TaiXe1ID { get; set; }
        public string TaiXe1Name { get; set; }
        public string MacTauID { get; set; }
        public short CongTacID { get; set; }
        public string CongTacName { get; set; }
        public decimal VanToc { get; set; }
    }
    public partial class KTDungKB
    {
        public string DauMayID { get; set; }
        public int DungKB { get; set; }
        public DateTime GiaoMay { get; set; }
        public DateTime NhanMay { get; set; }
        public string SoCBGiao { get; set; }
        public string SoCBNhan { get; set; }
        public string TaiXeGiao { get; set; }
        public string TaiXeNhan { get; set; }
        public string DvGiao { get; set; }
        public string DvNhan { get; set; }
        public long CoBaoIDGiao { get; set; }
        public long CoBaoIDNhan { get; set; }
    }
    public partial class KTNhienLieu
    {
        public string DauMayID { get; set; }
        public int NLBanGiao { get; set; }
        public int NLBanNhan { get; set; }
        public DateTime GiaoMay { get; set; }
        public DateTime NhanMay { get; set; }
        public string SoCBGiao { get; set; }
        public string SoCBNhan { get; set; }
        public string TaiXeGiao { get; set; }
        public string TaiXeNhan { get; set; }
        public string DvGiao { get; set; }
        public string DvNhan { get; set; }
        public long CoBaoIDGiao { get; set; }
        public long CoBaoIDNhan { get; set; }
    }
}
