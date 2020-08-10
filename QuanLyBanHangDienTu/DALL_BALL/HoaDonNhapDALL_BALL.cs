﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALL_BALL
{
  public  class HoaDonNhapDALL_BALL
    {

        QLBanHangDataContext data = new QLBanHangDataContext();
        public List<HoaDon_Nhap> loadhoadonhap()
        {
            var ds = data.HoaDon_Nhaps.ToList();
            return ds;
                
           
        }
        public  List<ChiTietHD_Nhap> kiemtradulieuhoadonnhap(string mahdn)
        {
            return data.ChiTietHD_Nhaps.Where(t => t.MAHD_Nhap == mahdn).ToList();
        }
        public IQueryable loadhoadonhap2(string manv)
        {
            var ds = from HoaDon_Nhaps in data.HoaDon_Nhaps.Where(t=>t.MANV==manv)
                     select new
                     {
                         HoaDon_Nhaps.MAHD_Nhap,
                         HoaDon_Nhaps.NhaCungCap.TenNCC,
                         HoaDon_Nhaps.NhanVien.TenNV,
                         HoaDon_Nhaps.TinhTrangNhap,
                         HoaDon_Nhaps.MaNC,
                         HoaDon_Nhaps.NgayLapHD,
                         HoaDon_Nhaps.TongTien
                         
                     };
            return ds;
        }


      


        public IQueryable loadhoadonhapchitiet(string mahd)
        {
            var ds = from ChiTietHD_Nhaps in data.ChiTietHD_Nhaps
                     where
                       ChiTietHD_Nhaps.MAHD_Nhap == mahd
                     select new
                     {
                         ChiTietHD_Nhaps.MAHD_Nhap,
                         ChiTietHD_Nhaps.HangHoa.TenHH,
                         ChiTietHD_Nhaps.SoLuong_Nhap,
                         ChiTietHD_Nhaps.DonGia_Nhap,
                         ChiTietHD_Nhaps.Thanhtien,
                         ChiTietHD_Nhaps.DonVi,
                         ChiTietHD_Nhaps.MAHH
                      
                     };
            return ds;
        }
        public  List<ChiTietHD_Nhap> loadhoadonchitiet(string mahd)
        {
            var ds = from k in data.ChiTietHD_Nhaps.Where(t => t.MAHD_Nhap == mahd) select k;
            return ds.ToList();
        }

        
        public void them1hoadonhap(string mahdnhap,string mancc,string manv,DateTime ngaylaphoadon,string tinhtrangnhap,long tongtien)
            {
            HoaDon_Nhap iHoaDon_Nhaps = new HoaDon_Nhap
            {
                MAHD_Nhap =mahdnhap,
                MaNC = mancc,
                MANV = manv,
                NgayLapHD = ngaylaphoadon,
                TinhTrangNhap = tinhtrangnhap,
                TongTien=tongtien
            };
            data.HoaDon_Nhaps.InsertOnSubmit(iHoaDon_Nhaps);
            data.SubmitChanges();


        }
        public void them1hoadonhap1(HoaDon_Nhap hdnhap)
        {
            HoaDon_Nhap iHoaDon_Nhaps = new HoaDon_Nhap
            {
                MAHD_Nhap = hdnhap.MAHD_Nhap,
                MaNC = hdnhap.MaNC,
                MANV = hdnhap.MANV,
                NgayLapHD = hdnhap.NgayLapHD,
                TinhTrangNhap = hdnhap.TinhTrangNhap
            };
            data.HoaDon_Nhaps.InsertOnSubmit(iHoaDon_Nhaps);
            data.SubmitChanges();


        }

        public void sua1hoadonnhap(string mahdnhap, string mancc, string manv, DateTime ngaylaphoadon, string tinhtrangnhap)
        {
                             var queryHoaDon_Nhaps =
                            from HoaDon_Nhaps in data.HoaDon_Nhaps
                            where
                              HoaDon_Nhaps.MAHD_Nhap == mahdnhap
                            select HoaDon_Nhaps;
                        foreach (var HoaDon_Nhaps in queryHoaDon_Nhaps)
                        {
                            HoaDon_Nhaps.NgayLapHD = ngaylaphoadon;
                            HoaDon_Nhaps.MaNC = mancc;
                            HoaDon_Nhaps.TinhTrangNhap = tinhtrangnhap;
                            }
                            data.SubmitChanges();

        }

        public string getmatudongtanhdnhap()
        {
           
                string x = data.HoaDon_Nhaps.Max(t => t.MAHD_Nhap);
                int ma = int.Parse(x.Substring(x.Length - 3, 3));

                if (ma >= 0 && ma < 9)
                {
                    return "HDN00" + (ma + 1).ToString();
                }
                else if (ma >= 9)
                {
                    return "HDN" + (ma + 1).ToString();
                }
                else
                    return "";


        }
        public void xoanhd(string mahd) // xóa hóa đơn nhập
        {
                        var queryHoaDon_Nhaps =
             from HoaDon_Nhaps in data.HoaDon_Nhaps
             where
               HoaDon_Nhaps.MAHD_Nhap == mahd
             select HoaDon_Nhaps;
                        foreach (var del in queryHoaDon_Nhaps)
                        {
                            data.HoaDon_Nhaps.DeleteOnSubmit(del);
                        }
                        data.SubmitChanges();
        }
       
        public void xoachitiethoadonnhap(string mahh)
        {
                    var queryChiTietHD_Nhaps =
             from ChiTietHD_Nhaps in data.ChiTietHD_Nhaps
             where
               ChiTietHD_Nhaps.MAHH==mahh
             select ChiTietHD_Nhaps;
                    foreach (var del in queryChiTietHD_Nhaps)
                    {
                        data.ChiTietHD_Nhaps.DeleteOnSubmit(del);
                    }
                    data.SubmitChanges();

        }


        // them 1 chitiethodonnhap
        public void them1chitiethoadon(string mahdnhap,string mahanghoa,int soluong,double dongia,double thanhtien,string donvi,double vat)
        {
            ChiTietHD_Nhap iChiTietHD_Nhaps = new ChiTietHD_Nhap
            {
                MAHD_Nhap = mahdnhap,
                MAHH = mahanghoa,
                SoLuong_Nhap =soluong,
                DonGia_Nhap = dongia,
                Thanhtien = thanhtien,
                DonVi = donvi,
              
            };
            data.ChiTietHD_Nhaps.InsertOnSubmit(iChiTietHD_Nhaps);
            data.SubmitChanges();

        }
        //sua1chiiethoadonnhap

        public void sua1chitiethoadon(string mahdnhap, string mahanghoa, int soluong, double dongia, double thanhtien, string donvi, double vat)
        {

            var queryChiTietHD_Nhaps =
      from ChiTietHD_Nhaps in data.ChiTietHD_Nhaps
      where

        ChiTietHD_Nhaps.MAHH == mahanghoa &&
        ChiTietHD_Nhaps.MAHD_Nhap ==mahdnhap

      select ChiTietHD_Nhaps;
            foreach (var ChiTietHD_Nhaps in queryChiTietHD_Nhaps)
            {

                ChiTietHD_Nhaps.SoLuong_Nhap = soluong;
                ChiTietHD_Nhaps.DonGia_Nhap = dongia;
                ChiTietHD_Nhaps.Thanhtien = thanhtien;
               
            }
            data.SubmitChanges();
        }
        public void xoa1chitiethoadon(string mahd)
        {
                    var queryChiTietHD_Nhaps =
            from ChiTietHD_Nhaps in data.ChiTietHD_Nhaps
            where
              ChiTietHD_Nhaps.MAHD_Nhap == mahd
            select ChiTietHD_Nhaps;
                    foreach (var del in queryChiTietHD_Nhaps)
                    {
                        data.ChiTietHD_Nhaps.DeleteOnSubmit(del);
                    }
                    data.SubmitChanges();

        }

        public void updatehoadonhapkhithanhtoan(string mahdn,string tinhtrang,float vat)
        {
            HoaDon_Nhap hdn = new HoaDon_Nhap();
            hdn = data.HoaDon_Nhaps.Where(t => t.MAHD_Nhap == mahdn).FirstOrDefault();

            if (hdn != null)
            {
                hdn.TinhTrangNhap = tinhtrang;
                hdn.Vat = vat;
                data.SubmitChanges();
            }
            
        }
        
    }
}