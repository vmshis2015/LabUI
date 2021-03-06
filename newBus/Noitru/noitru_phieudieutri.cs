﻿using System;
using System.Data;
using System.Transactions;
using System.Linq;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using System.Text;

using SubSonic;
using NLog;
using VNS.Properties;
using System.Collections.Generic;

namespace VNS.HIS.BusRule.Classes
{
    public class noitru_phieudieutri
    {
        private NLog.Logger log;
        public noitru_phieudieutri()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        public ActionResult SaoChepDonThuocTheoPhieuDieuTri(KcbDonthuoc objDonthuoc, NoitruPhieudieutri objTreatment,
          KcbDonthuocChitiet[] arrChitietdonthuoc)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        objDonthuoc.IdPhieudieutri = objTreatment.IdPhieudieutri;
                        objDonthuoc.IdDonthuocthaythe = -1;
                        objDonthuoc.IdKham = objTreatment.IdPhieudieutri;
                        objDonthuoc.IdBacsiChidinh = objTreatment.IdBacsi;
                        objDonthuoc.NgaySua = null;
                        objDonthuoc.NguoiSua = null;
                        objDonthuoc.NgayKedon = Convert.ToDateTime(objTreatment.NgayDieutri);
                        objDonthuoc.Noitru = 1;
                        //objDonthuoc.id = Utility.Int32Dbnull(objTreatment.PatientDeptId);
                        NoitruPhanbuonggiuong objPatientDept = NoitruPhanbuonggiuong.FetchByID(objTreatment.IdBuongGiuong);
                        if (objPatientDept != null)
                        {
                            objDonthuoc.IdKhoadieutri = Utility.Int16Dbnull(objPatientDept.IdKhoanoitru);
                            objDonthuoc.IdBuongNoitru = Utility.Int16Dbnull(objPatientDept.IdBuong);
                            objDonthuoc.IdGiuongNoitru = Utility.Int16Dbnull(objPatientDept.IdGiuong);
                        }
                        objDonthuoc.NgayXacnhan = null;
                        objDonthuoc.NgayCapphat = null;
                        objDonthuoc.DonthuocTaiquay = 0;
                        objDonthuoc.TrangThai = 0;
                        objDonthuoc.TrangthaiThanhtoan = 0;
                        objDonthuoc.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                        //objDonthuoc.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                        objDonthuoc.MotaThem = "Sao chép";
                        objDonthuoc.NguoiTao = globalVariables.UserName;
                        objDonthuoc.NgayTao = globalVariables.SysDate;
                        objDonthuoc.IpMaytao = globalVariables.gv_strIPAddress;
                        objDonthuoc.TenMaytao = globalVariables.gv_strComputerName;
                        objDonthuoc.IsNew = true;
                        objDonthuoc.Save();
                        foreach (var objChitietdonthuoc in arrChitietdonthuoc)
                        {
                            objChitietdonthuoc.IdKham = objTreatment.IdPhieudieutri;
                            objChitietdonthuoc.SoluongHuy=0;
                            objChitietdonthuoc.NgayHuy = null;
                            objChitietdonthuoc.TrangthaiHuy = 0;
                            objChitietdonthuoc.NguoiHuy = null;
                            objChitietdonthuoc.TrangThai = 0;
                            objChitietdonthuoc.SluongLinh = 0;
                            objChitietdonthuoc.SluongSua = 0;
                            objChitietdonthuoc.NgayXacnhan = null;
                            objChitietdonthuoc.IdThanhtoan = -1;
                            objChitietdonthuoc.TrangthaiThanhtoan = 0;
                            objChitietdonthuoc.NgayThanhtoan = null;
                            objChitietdonthuoc.TrangthaiChuyen = 0;

                            objChitietdonthuoc.NgaySua = null;
                            objChitietdonthuoc.NguoiSua = null;
                            objChitietdonthuoc.TileChietkhau = 0;
                            objChitietdonthuoc.TienChietkhau = 0;
                            objChitietdonthuoc.IdGoi = -1;
                            objChitietdonthuoc.TrongGoi = 0;

                            objChitietdonthuoc.BhytChitra = Utility.DecimaltoDbnull(objChitietdonthuoc.DonGia) *
                                                   Utility.DecimaltoDbnull(objChitietdonthuoc.PtramBhyt) / 100;
                            objChitietdonthuoc.BnhanChitra = Utility.DecimaltoDbnull(objChitietdonthuoc.DonGia) -
                                                 Utility.DecimaltoDbnull(objChitietdonthuoc.BhytChitra);

                            
                            objChitietdonthuoc.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);

                            objChitietdonthuoc.NguoiTao = globalVariables.UserName;
                            objChitietdonthuoc.NgayTao = globalVariables.SysDate;
                            objChitietdonthuoc.IpMaytao = globalVariables.gv_strIPAddress;
                            objChitietdonthuoc.TenMaytao = globalVariables.gv_strComputerName;

                            objChitietdonthuoc.IsNew = true;
                            objChitietdonthuoc.Save();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult ThemPhieudieutri(NoitruPhieudieutri objTreatment)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {


                        //if(Convert.ToDateTime(objTreatment.NgayLapPhieu))
                        SqlQuery sqlQuery = new Select().From(NoitruPhieudieutri.Schema)
                            .Where(NoitruPhieudieutri.Columns.IdPhieudieutri).IsEqualTo(objTreatment.IdPhieudieutri);
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            objTreatment.NgaySua = null;
                            objTreatment.NguoiSua = string.Empty;
                            objTreatment.IsNew = true;
                            objTreatment.Save();
                        }
                        else
                        {
                            objTreatment.TenMaysua = globalVariables.gv_strComputerName;
                            objTreatment.IpMaysua = globalVariables.gv_strIPAddress;
                            objTreatment.NgaySua = DateTime.Now;
                            objTreatment.NguoiSua = globalVariables.UserName;
                            objTreatment.MarkOld();
                            objTreatment.IsNew = false;
                            objTreatment.IsLoaded = true;
                            objTreatment.Save();
                            new Update(KcbChidinhcl.Schema)
                                .Set(KcbChidinhcl.Columns.NgayChidinh).EqualTo(objTreatment.NgayDieutri)
                                .Where(KcbChidinhcl.Columns.IdDieutri).IsEqualTo(objTreatment.IdPhieudieutri).Execute();
                            new Update(KcbDonthuoc.Schema)
                                .Set(KcbDonthuoc.Columns.NgayKedon).EqualTo(objTreatment.NgayDieutri)
                                .Where(KcbDonthuoc.Columns.IdPhieudieutri).IsEqualTo(objTreatment.IdPhieudieutri).Execute();
                        }
                        new Update(KcbLuotkham.Schema)
                               .Set(KcbLuotkham.Columns.TrangthaiNoitru).EqualTo(2)
                               .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objTreatment.IdBenhnhan)
                               .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objTreatment.MaLuotkham).Execute();

                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh tao phieu dieu tri: {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public DataSet NoitruLaythongtinphieudieutriIn(string lstIdPhieudieutri)
        {
            return SPs.NoitruLaythongtinphieudieutriIn(lstIdPhieudieutri).GetDataSet();
        }
        public DataTable NoitruLaydulieuClsThuocVtthSaochep(int IdPhieudieutri, int idbenhnhan, string maluotkham)
        {
            return SPs.NoitruLaydulieuClsThuocVtthSaochep(IdPhieudieutri, maluotkham,idbenhnhan ).GetDataSet().Tables[0];
        }
        public ActionResult ChuyentoanboVTTHvaogoi(long IdDonthuoc,int id_goi)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Update(KcbDonthuoc.Schema)
                            .Set(KcbDonthuoc.Columns.IdGoi).EqualTo(id_goi)
                            .Set(KcbDonthuoc.Columns.TrongGoi).EqualTo(1)
                            .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(IdDonthuoc).Execute();
                        new Update(KcbDonthuocChitiet.Schema)
                           .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(id_goi)
                           .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(1)
                           .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(IdDonthuoc).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult ChuyenVTTHvaogoi(long IdDonthuoc,List<long>lstchitiet, int id_goi)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        //Tạo phiếu mới
                        KcbDonthuoc objnew = KcbDonthuoc.FetchByID(IdDonthuoc);
                        objnew.IsNew = true;
                        objnew.IdGoi = id_goi;
                        objnew.Noitru = 1;
                        objnew.TrongGoi = 1;
                        objnew.Save();
                        new Update(KcbDonthuocChitiet.Schema)
                           .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(id_goi)
                           .Set(KcbDonthuocChitiet.Columns.IdDonthuoc).EqualTo(objnew.IdDonthuoc)
                           .Set(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).EqualTo(IdDonthuoc)
                           .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(1)
                           .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(lstchitiet).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }

            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult ChuyenVTTHvaogoi(long IdDonthuoc_source,long IdDonthuoc_des, List<long> lstchitiet, int id_goi)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        new Update(KcbDonthuocChitiet.Schema)
                           .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(id_goi)
                           .Set(KcbDonthuocChitiet.Columns.IdDonthuoc).EqualTo(IdDonthuoc_des)
                           .Set(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).EqualTo(IdDonthuoc_source)
                           .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(1)
                           .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(lstchitiet).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }

            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult ChuyenVTTH(List<long> lstchitiet, byte tronggoi)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        new Update(KcbDonthuocChitiet.Schema)
                           .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(tronggoi)
                           .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(lstchitiet).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }

            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult ChuyentoanboVTTHrakhoigoi(long IdDonthuoc)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Update(KcbDonthuoc.Schema)
                           .Set(KcbDonthuoc.Columns.IdGoi).EqualTo(-1)
                           .Set(KcbDonthuoc.Columns.TrongGoi).EqualTo(0)
                           .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(IdDonthuoc).Execute();

                        new Update(KcbDonthuocChitiet.Schema)
                           .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(-1)
                           .Set(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).EqualTo(-1)
                           .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                           .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(IdDonthuoc).Execute();

                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }

            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult ChuyenVTTHrakhoigoi(long IdDonthuoc_source,List<long> lstId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        
                        foreach (long _id in lstId)
                        {
                            KcbDonthuoc objKcbDonthuoc = KcbDonthuoc.FetchByID(_id);
                            if (objKcbDonthuoc == null)
                            {
                                objKcbDonthuoc = KcbDonthuoc.FetchByID(IdDonthuoc_source);
                                objKcbDonthuoc.IdGoi = -1;
                                objKcbDonthuoc.TrongGoi = 0;
                                objKcbDonthuoc.IsNew = true;
                                objKcbDonthuoc.Save();
                                new Update(KcbDonthuocChitiet.Schema)
                                   .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(-1)
                                   .Set(KcbDonthuocChitiet.Columns.IdDonthuoc).EqualTo(objKcbDonthuoc.IdDonthuoc)
                                   .Set(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).EqualTo(-1)
                                   .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                                   .Where(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).IsEqualTo(_id).Execute();
                            }
                            else
                            {
                                new Update(KcbDonthuocChitiet.Schema)
                                  .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(-1)
                                  .Set(KcbDonthuocChitiet.Columns.IdDonthuoc).EqualTo(objKcbDonthuoc.IdDonthuoc)
                                  .Set(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).EqualTo(-1)
                                  .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                                  .Where(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).IsEqualTo(_id).Execute();
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }

            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult Xoaphieudieutri(System.Collections.Generic.List<int> lstIdPhieudieutri)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        foreach (int IdPhieudieutri in lstIdPhieudieutri)
                        {
                            SPs.NoitruXoaphieudieutri(IdPhieudieutri).Execute();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public  ActionResult SaochepDonthuoc(int CurrentTreatID, KcbLuotkham objPatientExam,
         KcbDonthuocCollection lstPres, DateTime pres_date)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        foreach (KcbDonthuoc pre in lstPres)
                        {
                            int oldPreID = pre.IdDonthuoc;
                            pre.IdPhieudieutri = CurrentTreatID;
                            pre.NgayKedon = pres_date;
                            pre.NguoiTao = globalVariables.UserName;
                            //pre.TenDonthuoc = BusinessHelper.GeneratePresNameNoitru(objPatientExam.MaLuotkham,
                            //    Utility.Int32Dbnull(objPatientExam.IdBenhnhan, -1), pre.PresDate);
                            pre.TrangThai = 0;
                            //pre.PresStatus = null;
                            pre.IdDonthuocthaythe = -1;
                            pre.TrangthaiThanhtoan = 0;
                            pre.NgayThanhtoan = null;
                            pre.NguoiSua = null;
                            pre.NgaySua = null;
                            pre.IsNew = true;
                            pre.Save();
                            KcbDonthuocChitietCollection lstobjChitietdonthuoc =
                                new Select().From(KcbDonthuocChitiet.Schema)
                                    .Where(KcbDonthuocChitiet.IdDonthuocColumn)
                                    .IsEqualTo(oldPreID)
                                    .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                            foreach (KcbDonthuocChitiet _detail in lstobjChitietdonthuoc)
                            {
                                _detail.IdDonthuoc = pre.IdDonthuoc;
                                _detail.TrangThai = 0;
                                _detail.TrangthaiThanhtoan = 0;
                                _detail.SoluongHuy = 0;
                                _detail.TrangthaiHuy = 0;
                                _detail.NgayThanhtoan = null;
                                _detail.IdThanhtoan = null;
                                _detail.NguoiHuy = null;
                                _detail.NgayHuy = null;
                                _detail.SoLuong = Utility.Int32Dbnull(_detail.SluongSua, -1) < 0
                                    ? _detail.SoLuong
                                    : _detail.SluongSua.Value;
                                _detail.SluongSua = null;
                                ;
                                _detail.SluongLinh = null;
                                _detail.IsNew = true;
                                _detail.Save();
                            }
                        }

                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }

        public ActionResult SaoChepPhieuDieuTri(NoitruPhieudieutri[] lstPhieudieutri, KcbLuotkham objLuotkham, KcbChidinhclsChitiet[] arrChidinhCLSChitiet, KcbDonthuocChitiet[] arrDonthuocChitiet)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        foreach (NoitruPhieudieutri objTreatment in lstPhieudieutri)
                        {
                            objTreatment.NguoiTao = globalVariables.UserName;
                            objTreatment.NgayTao = DateTime.Now;
                            objTreatment.TthaiBosung = 0;
                            objTreatment.IdBacsi = globalVariables.gv_intIDNhanvien;
                            objTreatment.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                            objTreatment.MaLuotkham = objLuotkham.MaLuotkham;
                            objTreatment.IdBenhnhan = objTreatment.IdBenhnhan;
                            objTreatment.IdBuongGiuong = objLuotkham.IdRavien;
                            objTreatment.TrangThai = 0;
                            objTreatment.TthaiIn = 0;
                            objTreatment.IpMaytao = globalVariables.gv_strIPAddress;
                            objTreatment.TenMaytao = globalVariables.gv_strComputerName;
                            objTreatment.GioDieutri = Utility.GetFormatDateTime(globalVariables.SysDate, "hh:mm:ss");
                            objTreatment.IsNew = true;
                            objTreatment.Save();
                            if (arrChidinhCLSChitiet.Length > 0)
                            {
                                KcbChidinhcl objAssignInfo = new KcbChidinhcl();
                                objAssignInfo.IdDieutri = objTreatment.IdPhieudieutri;
                                objAssignInfo.IdBuongGiuong = objTreatment.IdBuongGiuong;
                                objAssignInfo.MaLuotkham = objTreatment.MaLuotkham;
                                objAssignInfo.IdBenhnhan = Utility.Int32Dbnull(objTreatment.IdBenhnhan);
                                objAssignInfo.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                                objAssignInfo.Noitru = 1;
                                objAssignInfo.IdKhoadieutri = objTreatment.IdKhoanoitru;
                                objAssignInfo.IdKhoaChidinh = objTreatment.IdKhoanoitru;
                                objAssignInfo.IdKham=-1;
                                objAssignInfo.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                objAssignInfo.IdPhongChidinh = objTreatment.IdKhoanoitru;
                                objAssignInfo.Barcode = string.Empty;
                                
                                objAssignInfo.NgayChidinh = objTreatment.NgayDieutri.Value;
                                objAssignInfo.NguoiTao = globalVariables.UserName;
                                objAssignInfo.NgayTao = globalVariables.SysDate;
                                objAssignInfo.IpMaytao = globalVariables.gv_strIPAddress;
                                objAssignInfo.TenMaytao = globalVariables.gv_strComputerName;

                                objAssignInfo.MaChidinh = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                                objAssignInfo.IsNew = true;
                                objAssignInfo.Save();
                               List<KcbChidinhclsChitiet> lstChidinhCLSChitiet=new List<KcbChidinhclsChitiet>();
                                foreach (KcbChidinhclsChitiet objAssignDetail in arrChidinhCLSChitiet)
                                {
                                    KcbChidinhclsChitiet objDetail = KcbChidinhclsChitiet.FetchByID(objAssignDetail.IdChitietchidinh);
                                    if (objDetail != null)
                                    {
                                        objDetail.IdChitietchidinh = -1;
                                        objDetail.IdChidinh = objAssignInfo.IdChidinh;
                                        objDetail.IdKham = -1;
                                        objDetail.TrangthaiThanhtoan = 0;
                                        objDetail.NgayThanhtoan = null;
                                        objDetail.TrangthaiHuy = 0;
                                        objDetail.TrangthaiChuyencls = 0;
                                        objDetail.TrangthaiXacnhan = 0;
                                        objDetail.ImgPath1 = string.Empty;
                                        objDetail.ImgPath2 = string.Empty;
                                        objDetail.ImgPath3 = string.Empty;
                                        objDetail.ImgPath4 = string.Empty;
                                        objDetail.TrangThai =0;
                                        objDetail.MotaThem = null;
                                        objDetail.TrangthaiBhyt = 0;
                                        objDetail.IdThanhtoan = -1;
                                        objDetail.IdKhoaThuchien =(short) objAssignInfo.IdKhoadieutri;
                                        objDetail.IdPhongThuchien = objDetail.IdKhoaThuchien;
                                        objDetail.IdGoi = -1;
                                        objDetail.IdBacsiThuchien = -1;
                                        objDetail.NgayThuchien = null;
                                        objDetail.NguoiThuchien = null;
                                        objDetail.KetLuan = null;
                                        objDetail.KetQua = null;
                                        objDetail.DeNghi = null;
                                        objDetail.MaVungkhaosat = null;
                                        objDetail.KetquaXn = "";
                                        objDetail.NguoiTao = globalVariables.UserName;
                                        objDetail.NgayTao = globalVariables.SysDate;
                                        objDetail.IpMaytao = globalVariables.gv_strIPAddress;
                                        objDetail.TenMaytao = globalVariables.gv_strComputerName;
                                        objDetail.IsNew=true;
                                        lstChidinhCLSChitiet.Add(objDetail);
                                    }
                                    new KCB_CHIDINH_CANLAMSANG().InsertAssignDetail(objAssignInfo, objLuotkham, lstChidinhCLSChitiet.ToArray<KcbChidinhclsChitiet>());

                                }
                            }
                            if (arrDonthuocChitiet.Length > 0)
                            {
                                var query = (from donthuoc in arrDonthuocChitiet.AsEnumerable()
                                             let y = donthuoc.IdDonthuoc
                                             select y).Distinct();
                                foreach (var pres_id in query.ToList())
                                {
                                    KcbDonthuoc objPresInfo = KcbDonthuoc.FetchByID(Utility.Int32Dbnull(pres_id));
                                    if (objPresInfo != null)
                                    {

                                        objPresInfo.IsNew = true;
                                        objPresInfo.Save();
                                        List<KcbDonthuocChitiet> lstDonthuocchitiet = (from donthuoc in arrDonthuocChitiet.AsEnumerable()
                                                                                       where donthuoc.IdDonthuoc == pres_id
                                                                                       select donthuoc).ToList<KcbDonthuocChitiet>();
                                        SaoChepDonThuocTheoPhieuDieuTri(objPresInfo, objTreatment, lstDonthuocchitiet.ToArray<KcbDonthuocChitiet>());
                                    }

                                }
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }
        
    }
}
