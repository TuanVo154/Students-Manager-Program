using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV
{
    internal class Khoa
    {
        private String _makhoa;
        private String _tenkhoa;

        public String MaKhoa{
            get { return _makhoa; }
            set { _makhoa = value; }
        }

        public String TenKhoa
        {
            get { return _tenkhoa; }
            set { _tenkhoa = value; }
        }
    }
}
