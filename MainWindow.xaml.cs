using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLSV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBConnect db = new DBConnect();
        SqlDataAdapter sdaKhoa = new SqlDataAdapter();
        SqlDataAdapter sdaLop = new SqlDataAdapter();
        SqlDataAdapter sdaSinhVien = new SqlDataAdapter();

        DataSet dsKhoa = new DataSet();
        DataSet dsLop = new DataSet(); 
        DataSet dsSinhVien = new DataSet();

        DataTable dtKhoa = new DataTable();
        DataTable dtLop = new DataTable();
        DataTable dtSinhVien = new DataTable();

        List<Khoa> listKhoa = new List<Khoa>();
        List<Lop> listLop = new List<Lop>();
        List<SinhVien> listSinhVien = new List<SinhVien>();

        public MainWindow()
        {
            InitializeComponent();
            db.connect();

            //Load dữ liệu vào list Khoa
            String selsqlKhoa = "SELECT * FROM Khoa;";
            SqlCommand selcmdKhoa = new SqlCommand(selsqlKhoa, db.getConnect);
            sdaKhoa.SelectCommand = selcmdKhoa;

            String inssqlKhoa = "INSERT INTO Khoa(MaKhoa, TenKhoa) VALUES (@makhoa, @tenkhoa);";

            SqlParameter makhoa = new SqlParameter();
            makhoa.ParameterName = "@makhoa";
            makhoa.SqlDbType = SqlDbType.NVarChar;
            makhoa.SourceColumn = "MaKhoa";

            SqlParameter tenkhoa = new SqlParameter();
            tenkhoa.ParameterName = "@tenkhoa";
            tenkhoa.SqlDbType = SqlDbType.NVarChar;
            tenkhoa.SourceColumn = "TenKhoa";

            SqlCommand inscmdKhoa = new SqlCommand(inssqlKhoa, db.getConnect);
            inscmdKhoa.Parameters.Add(makhoa);
            inscmdKhoa.Parameters.Add(tenkhoa);
            sdaKhoa.InsertCommand = inscmdKhoa;

            sdaKhoa.Fill(dsKhoa);
            dtKhoa = dsKhoa.Tables[0];
            foreach (DataRow dr in dtKhoa.Rows)
            {
                Khoa k = new Khoa();
                k.MaKhoa = dr["MaKhoa"].ToString();
                k.TenKhoa = dr["TenKhoa"].ToString();
                listKhoa.Add(k);
            }
            lstKhoa.ItemsSource = listKhoa;
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            db.close();
            Application.Current.Shutdown();
        }

        private void btnThemKhoa_Click(object sender, RoutedEventArgs e)
        {
            Khoa k = new Khoa();
            k.MaKhoa = txtMaKhoa.Text;
            k.TenKhoa = txtTenKhoa.Text;
            listKhoa.Add(k);
            DataRow dr = dtKhoa.NewRow();
            dr["MaKhoa"] = k.MaKhoa;
            dr["TenKhoa"] = k.TenKhoa;
            dtKhoa.Rows.Add(dr);
            lstKhoa.ItemsSource = null;
            lstKhoa.ItemsSource= listKhoa;
            sdaKhoa.Update(dsKhoa);
        }

        private void lstKhoa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstLop.ItemsSource = null;
            dsLop = new DataSet();
            listLop = new List<Lop>();
            if (lstKhoa.SelectedIndex == -1) lstKhoa.SelectedIndex = 0;
            if (lstKhoa.SelectedIndex != -1)
            {
                Khoa k = new Khoa();
                k = (Khoa) lstKhoa.SelectedItem;
                
                String selsqlLop = "SELECT * FROM Lop WHERE MaKhoa=@makhoa;";
                
                SqlParameter makhoa = new SqlParameter();
                makhoa.ParameterName = "@makhoa";
                makhoa.SqlDbType = SqlDbType.NVarChar;
                makhoa.Value = k.MaKhoa;

                SqlCommand selcmdLop = new SqlCommand(selsqlLop, db.getConnect);
                selcmdLop.Parameters.Add(makhoa);
                sdaLop.SelectCommand = selcmdLop;

                String inssqlLop = "INSERT INTO Lop(MaLop, TenLop, SiSo, MaKhoa) VALUES (@malop, @tenlop, 0, @makhoa);";

                SqlParameter  malop = new SqlParameter();
                malop.ParameterName = "@malop";
                malop.SqlDbType = SqlDbType.NVarChar;
                malop.SourceColumn = "MaLop";

                SqlParameter tenlop = new SqlParameter();
                tenlop.ParameterName = "@tenlop";
                tenlop.SqlDbType = SqlDbType.NVarChar;
                tenlop.SourceColumn = "TenLop";

                SqlParameter makhoa1 = new SqlParameter();
                makhoa1.ParameterName = "@makhoa";
                makhoa1.SqlDbType = SqlDbType.NVarChar;
                makhoa1.SourceColumn = "MaKhoa";
                
                SqlCommand inscmdLop = new SqlCommand(inssqlLop, db.getConnect);
                inscmdLop.Parameters.Add(malop);
                inscmdLop.Parameters.Add(tenlop);
                inscmdLop.Parameters.Add(makhoa1);

                sdaLop.InsertCommand = inscmdLop;

                String updSqlLop = "UPDATE Lop SET SiSo=@ss WHERE MaLop=@ml;";
                SqlParameter ss = new SqlParameter();
                ss.ParameterName = "@ss";
                ss.SqlDbType = SqlDbType.Int;
                ss.SourceColumn = "SiSo";
                SqlParameter ml = new SqlParameter();
                ml.ParameterName = "@ml";
                ml.SqlDbType = SqlDbType.NVarChar;
                ml.SourceColumn = "MaLop";

                SqlCommand updcmdLop = new SqlCommand(updSqlLop, db.getConnect);
                sdaLop.UpdateCommand = updcmdLop;
                updcmdLop.Parameters.Add(ss);
                updcmdLop.Parameters.Add(ml);


                String delSqlLop = "DELETE FROM Lop WHERE MaLop=@ml;";
                SqlParameter ml1 = new SqlParameter();
                ml1.ParameterName = "@ml";
                ml1.SqlDbType = SqlDbType.NVarChar;
                ml1.SourceColumn = "MaLop";
                
                SqlCommand delcmdLop = new SqlCommand(delSqlLop, db.getConnect);
                sdaLop.DeleteCommand = delcmdLop;
                delcmdLop.Parameters.Add(ml1);




                sdaLop.Fill(dsLop);
                dtLop = dsLop.Tables[0];
                
                foreach (DataRow dr in dtLop.Rows)
                {
                    Lop l = new Lop();
                    l.MaLop = dr["MaLop"].ToString();
                    l.Tenlop = dr["TenLop"].ToString();
                    l.SiSo = int.Parse(dr["SiSo"].ToString());
                    l.MaKhoa = dr["Makhoa"].ToString();
                    listLop.Add(l);
                }
            }
            lstLop.ItemsSource = listLop;
            
            //lblSoSV.Content = "0";

        }

        private void btnThemLop_Click(object sender, RoutedEventArgs e)
        {
            Khoa k = new Khoa();
            k = (Khoa)lstKhoa.SelectedItem;

            Lop l = new Lop();
            l.MaLop = txtMaLop.Text;
            l.Tenlop = txtTenLop.Text;
            l.SiSo = 0;
            l.MaKhoa = k.MaKhoa;

            listLop.Add(l);
            DataRow dr = dtLop.NewRow();
            dr["MaLop"] = l.MaLop;
            dr["TenLop"] = l.Tenlop;
            dr["SiSo"] = l.SiSo;
            dr["MaKhoa"] = l.MaKhoa;

            dtLop.Rows.Add(dr);
            lstLop.ItemsSource = null;
            lstLop.ItemsSource = listLop;
            sdaLop.Update(dsLop);
        }

        private void lstLop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dsSinhVien = new DataSet();
            listSinhVien = new List<SinhVien>();
            lstSinhVien.ItemsSource = null;
            if (lstLop.SelectedIndex == -1) lstLop.SelectedIndex = 0;
            if (lstLop.SelectedIndex != -1)
            {
                Lop l = new Lop();
                l = (Lop)lstLop.SelectedItem;

                lblSoSV.Content = l.SiSo.ToString();
                String selsqlSinhVien = "SELECT * FROM SinhVien WHERE MaLop = @malop;";

                SqlParameter malop = new SqlParameter();
                malop.ParameterName = "@malop";
                malop.SqlDbType = SqlDbType.NVarChar;
                malop.Value = l.MaLop;

                SqlCommand selcmdSinhVien = new SqlCommand(selsqlSinhVien, db.getConnect);
                selcmdSinhVien.Parameters.Add(malop);
                sdaSinhVien.SelectCommand = selcmdSinhVien;

                String inssqlSinhVien = "INSERT INTO SinhVien(MaSV, HoTen, MaLop) VALUES(@masv, @hoten, @malop);";

                SqlParameter masv = new SqlParameter();
                masv.ParameterName = "@masv";
                masv.SqlDbType = SqlDbType.NVarChar;
                masv.SourceColumn = "MaSV";

                SqlParameter hoten = new SqlParameter();
                hoten.ParameterName = "@hoten";
                hoten.SqlDbType = SqlDbType.NVarChar;
                hoten.SourceColumn = "HoTen";

                SqlParameter malop1 = new SqlParameter();
                malop1.ParameterName = "@malop";
                malop1.SqlDbType = SqlDbType.NVarChar;
                malop1.SourceColumn = "MaLop";

                SqlCommand inscmdSinhVien = new SqlCommand(inssqlSinhVien, db.getConnect);
                inscmdSinhVien.Parameters.Add(masv);
                inscmdSinhVien.Parameters.Add(hoten);
                inscmdSinhVien.Parameters.Add(malop1);

                sdaSinhVien.InsertCommand = inscmdSinhVien;

                String delSqlSinhVien = "DELETE FROM SinhVien WHERE MaSV=@msv;";
                SqlParameter masv1 = new SqlParameter();
                masv1.ParameterName = "@msv";
                masv1.SqlDbType = SqlDbType.NVarChar;
                masv1.SourceColumn = "MaSV";

                SqlCommand delcmdSinhVien = new SqlCommand(delSqlSinhVien, db.getConnect);
                delcmdSinhVien.Parameters.Add(masv1);

                sdaSinhVien.DeleteCommand = delcmdSinhVien;
                
                sdaSinhVien.Fill(dsSinhVien);
                dtSinhVien = dsSinhVien.Tables[0];
                
                foreach (DataRow dr in dtSinhVien.Rows)
                {
                    SinhVien s = new SinhVien();
                    s.MaSV = dr["MaSV"].ToString();
                    s.Hoten = dr["HoTen"].ToString();
                    s.Malop = dr["MaLop"].ToString(); ;
                    listSinhVien.Add(s);
                }
                lstSinhVien.ItemsSource = listSinhVien;
            }
        }

        private void btnThemSV_Click(object sender, RoutedEventArgs e)
        { 
            Lop l = new Lop();
            l = (Lop)lstLop.SelectedItem;

            SinhVien s = new SinhVien();
            s.Malop = l.MaLop.ToString();
            s.Hoten = txtHoTen.Text;
            s.MaSV = txtMaSV.Text;

            listSinhVien.Add(s);
            DataRow dr = dtSinhVien.NewRow();
            dr["MaSV"] = s.MaSV;
            dr["HoTen"] = s.Hoten;
            dr["MaLop"] = s.Malop;

            dtSinhVien.Rows.Add(dr);
            lstSinhVien.ItemsSource = null;
            lstSinhVien.ItemsSource = listLop;
            sdaSinhVien.Update(dsSinhVien);

            lblSoSV.Content = int.Parse(lblSoSV.Content.ToString()) + 1;

            DataRow dr1 = dtLop.Rows[lstLop.SelectedIndex];
            dr1["SiSo"] = int.Parse(dr1["SiSo"].ToString()) + 1;
            sdaLop.Update(dsLop);
        }

        private void btnXoaSV_Click(object sender, RoutedEventArgs e)
        {
            //Lấy Sinh viên đang được chọn
            int sel = lstSinhVien.SelectedIndex;
            //Xoá SV đó ra khỏi dtSinhVien và cập nhật vào CSDL
            dtSinhVien.Rows[sel].Delete();
            sdaSinhVien.Update(dsSinhVien);
            //Xoá SV ra khỏi list sinh viên
            listSinhVien.RemoveAt(sel);
            lstSinhVien.ItemsSource = null;
            lstSinhVien.ItemsSource = listSinhVien;
            //Hiển thị lại số SV
            lblSoSV.Content = dtSinhVien.Rows.Count;
            //Cập nhật lại số SV của lớp
            DataRow dr1 = dtLop.Rows[lstLop.SelectedIndex];
            dr1["SiSo"] = dtSinhVien.Rows.Count;
            sdaLop.Update(dsLop);
        }

        private void lstSinhVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void btnXoaLop_Click(object sender, RoutedEventArgs e)
        {
            int sel = lstLop.SelectedIndex;
            dtLop.Rows[sel].Delete();
            sdaLop.Update(dsLop);

            listLop.RemoveAt(sel);
            lstLop.ItemsSource = null;
            lstLop.ItemsSource = listLop;
        }
    }
}
