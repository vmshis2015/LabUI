using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace LIS.DAL
{
    /// <summary>
    /// Controller class for tblHisLis_PatientInfo_VNIO
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TblHisLisPatientInfoVnioController
    {
        // Preload our schema..
        TblHisLisPatientInfoVnio thisSchemaLoad = new TblHisLisPatientInfoVnio();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public TblHisLisPatientInfoVnioCollection FetchAll()
        {
            TblHisLisPatientInfoVnioCollection coll = new TblHisLisPatientInfoVnioCollection();
            Query qry = new Query(TblHisLisPatientInfoVnio.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TblHisLisPatientInfoVnioCollection FetchByID(object Id)
        {
            TblHisLisPatientInfoVnioCollection coll = new TblHisLisPatientInfoVnioCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TblHisLisPatientInfoVnioCollection FetchByQuery(Query qry)
        {
            TblHisLisPatientInfoVnioCollection coll = new TblHisLisPatientInfoVnioCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TblHisLisPatientInfoVnio.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TblHisLisPatientInfoVnio.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string BacSyDieuTri,string Buong,string DiaChi,bool? GioiTinh,string Giuong,string IdBacSyDieuTri,short? IdKhoa,string Khoa,string MaBenhNhan,bool? NoiTru,string Phong,string TenBenhNhan,short? Tuoi,string Sophieu,DateTime? TestDate,string Datra,bool? DichVu,int? IdDoiTuongBenhNhan,string MaLanKham)
	    {
		    TblHisLisPatientInfoVnio item = new TblHisLisPatientInfoVnio();
		    
            item.BacSyDieuTri = BacSyDieuTri;
            
            item.Buong = Buong;
            
            item.DiaChi = DiaChi;
            
            item.GioiTinh = GioiTinh;
            
            item.Giuong = Giuong;
            
            item.IdBacSyDieuTri = IdBacSyDieuTri;
            
            item.IdKhoa = IdKhoa;
            
            item.Khoa = Khoa;
            
            item.MaBenhNhan = MaBenhNhan;
            
            item.NoiTru = NoiTru;
            
            item.Phong = Phong;
            
            item.TenBenhNhan = TenBenhNhan;
            
            item.Tuoi = Tuoi;
            
            item.Sophieu = Sophieu;
            
            item.TestDate = TestDate;
            
            item.Datra = Datra;
            
            item.DichVu = DichVu;
            
            item.IdDoiTuongBenhNhan = IdDoiTuongBenhNhan;
            
            item.MaLanKham = MaLanKham;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,string BacSyDieuTri,string Buong,string DiaChi,bool? GioiTinh,string Giuong,string IdBacSyDieuTri,short? IdKhoa,string Khoa,string MaBenhNhan,bool? NoiTru,string Phong,string TenBenhNhan,short? Tuoi,string Sophieu,DateTime? TestDate,string Datra,bool? DichVu,int? IdDoiTuongBenhNhan,string MaLanKham)
	    {
		    TblHisLisPatientInfoVnio item = new TblHisLisPatientInfoVnio();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.BacSyDieuTri = BacSyDieuTri;
				
			item.Buong = Buong;
				
			item.DiaChi = DiaChi;
				
			item.GioiTinh = GioiTinh;
				
			item.Giuong = Giuong;
				
			item.IdBacSyDieuTri = IdBacSyDieuTri;
				
			item.IdKhoa = IdKhoa;
				
			item.Khoa = Khoa;
				
			item.MaBenhNhan = MaBenhNhan;
				
			item.NoiTru = NoiTru;
				
			item.Phong = Phong;
				
			item.TenBenhNhan = TenBenhNhan;
				
			item.Tuoi = Tuoi;
				
			item.Sophieu = Sophieu;
				
			item.TestDate = TestDate;
				
			item.Datra = Datra;
				
			item.DichVu = DichVu;
				
			item.IdDoiTuongBenhNhan = IdDoiTuongBenhNhan;
				
			item.MaLanKham = MaLanKham;
				
	        item.Save(UserName);
	    }
    }
}
