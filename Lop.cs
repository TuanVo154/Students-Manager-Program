using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV
{
    internal class Lop
    {
        private String _malop;
        private String _tenlop;
        private int _siso;
        private String _makhoa;

        public String MaLop
        {
            get {  return _malop; }
            set {  _malop = value; }
        }
        public String Tenlop
        { 
            get { return _tenlop; }
            set { _tenlop = value; }
        }
        public int SiSo
        {
            get { return _siso; }
            set { _siso = value; }
        }
        public String MaKhoa
        {
            get { return _makhoa; }
            set { _makhoa = value; }
        }
    }
}
