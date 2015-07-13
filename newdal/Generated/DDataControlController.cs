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
    /// Controller class for D_DATA_CONTROL
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DDataControlController
    {
        // Preload our schema..
        DDataControl thisSchemaLoad = new DDataControl();
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
        public DDataControlCollection FetchAll()
        {
            DDataControlCollection coll = new DDataControlCollection();
            Query qry = new Query(DDataControl.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DDataControlCollection FetchByID(object DataControlId)
        {
            DDataControlCollection coll = new DDataControlCollection().Where("DataControl_ID", DataControlId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DDataControlCollection FetchByQuery(Query qry)
        {
            DDataControlCollection coll = new DDataControlCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object DataControlId)
        {
            return (DDataControl.Delete(DataControlId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object DataControlId)
        {
            return (DDataControl.Destroy(DataControlId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(decimal DeviceId,int DataTypeId,int? DataSequence,bool? ControlType,string DataName,string AliasName,string MeasureUnit,short DataPoint,string NormalLevel,string NormalLevelW,bool? DataView,bool? DataPrint,string DataType,string Description,string TestDataId,string SCondition,string SFormula,short? ForceRun)
	    {
		    DDataControl item = new DDataControl();
		    
            item.DeviceId = DeviceId;
            
            item.DataTypeId = DataTypeId;
            
            item.DataSequence = DataSequence;
            
            item.ControlType = ControlType;
            
            item.DataName = DataName;
            
            item.AliasName = AliasName;
            
            item.MeasureUnit = MeasureUnit;
            
            item.DataPoint = DataPoint;
            
            item.NormalLevel = NormalLevel;
            
            item.NormalLevelW = NormalLevelW;
            
            item.DataView = DataView;
            
            item.DataPrint = DataPrint;
            
            item.DataType = DataType;
            
            item.Description = Description;
            
            item.TestDataId = TestDataId;
            
            item.SCondition = SCondition;
            
            item.SFormula = SFormula;
            
            item.ForceRun = ForceRun;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(decimal DataControlId,decimal DeviceId,int DataTypeId,int? DataSequence,bool? ControlType,string DataName,string AliasName,string MeasureUnit,short DataPoint,string NormalLevel,string NormalLevelW,bool? DataView,bool? DataPrint,string DataType,string Description,string TestDataId,string SCondition,string SFormula,short? ForceRun)
	    {
		    DDataControl item = new DDataControl();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.DataControlId = DataControlId;
				
			item.DeviceId = DeviceId;
				
			item.DataTypeId = DataTypeId;
				
			item.DataSequence = DataSequence;
				
			item.ControlType = ControlType;
				
			item.DataName = DataName;
				
			item.AliasName = AliasName;
				
			item.MeasureUnit = MeasureUnit;
				
			item.DataPoint = DataPoint;
				
			item.NormalLevel = NormalLevel;
				
			item.NormalLevelW = NormalLevelW;
				
			item.DataView = DataView;
				
			item.DataPrint = DataPrint;
				
			item.DataType = DataType;
				
			item.Description = Description;
				
			item.TestDataId = TestDataId;
				
			item.SCondition = SCondition;
				
			item.SFormula = SFormula;
				
			item.ForceRun = ForceRun;
				
	        item.Save(UserName);
	    }
    }
}