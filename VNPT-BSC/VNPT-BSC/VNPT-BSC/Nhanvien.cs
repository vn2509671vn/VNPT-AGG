using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNPT_BSC
{
    public class Nhanvien
    {
     
        public int nhanvien_id {get;set;}
        public string nhanvien_taikhoan { get; set; }
        public string nhanvien_matkhau { get; set; }
        public string nhanvien_hoten { get; set; }
        public int nhanvien_donvi_id { get; set; }
        public int nhanvien_chucvu_id { get; set; }
        public string nhanvien_donvi { get; set; }
        public string nhanvien_chucvu { get; set; }
    }
}