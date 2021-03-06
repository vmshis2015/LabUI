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
    /// Controller class for L_Test_Status
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LTestStatusController
    {
        // Preload our schema..
        LTestStatus thisSchemaLoad = new LTestStatus();
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
        public LTestStatusCollection FetchAll()
        {
            LTestStatusCollection coll = new LTestStatusCollection();
            Query qry = new Query(LTestStatus.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LTestStatusCollection FetchByID(object TestStatusId)
        {
            LTestStatusCollection coll = new LTestStatusCollection().Where("TestStatus_ID", TestStatusId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LTestStatusCollection FetchByQuery(Query qry)
        {
            LTestStatusCollection coll = new LTestStatusCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object TestStatusId)
        {
            return (LTestStatus.Delete(TestStatusId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object TestStatusId)
        {
            return (LTestStatus.Destroy(TestStatusId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(short TestStatusId,string TestStatusName,bool? InUse)
	    {
		    LTestStatus item = new LTestStatus();
		    
            item.TestStatusId = TestStatusId;
            
            item.TestStatusName = TestStatusName;
            
            item.InUse = InUse;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(short TestStatusId,string TestStatusName,bool? InUse)
	    {
		    LTestStatus item = new LTestStatus();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.TestStatusId = TestStatusId;
				
			item.TestStatusName = TestStatusName;
				
			item.InUse = InUse;
				
	        item.Save(UserName);
	    }
    }
}
