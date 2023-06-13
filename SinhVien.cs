using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV
{
    internal class SinhVien
    {
        private String _masv;
        private String _hoten;
        private String _malop;
        public String MaSV
        { 
            get { return _masv; } 
            set { _masv = value; }
        }
        public String Hoten
        {
            get { return _hoten; }
            set { _hoten = value; }
        }
        public String Malop
        {
            get { return _malop; }
            set { _malop = value; }
        }
    }
}
