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
    /// Controller class for L_User_FormControl
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LUserFormControlController
    {
        // Preload our schema..
        LUserFormControl thisSchemaLoad = new LUserFormControl();
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
        public LUserFormControlCollection FetchAll()
        {
            LUserFormControlCollection coll = new LUserFormControlCollection();
            Query qry = new Query(LUserFormControl.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LUserFormControlCollection FetchByID(object UserName)
        {
            LUserFormControlCollection coll = new LUserFormControlCollection().Where("UserName", UserName).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LUserFormControlCollection FetchByQuery(Query qry)
        {
            LUserFormControlCollection coll = new LUserFormControlCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object UserName)
        {
            return (LUserFormControl.Delete(UserName) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object UserName)
        {
            return (LUserFormControl.Destroy(UserName) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string UserName,int ControlId)
        {
            Query qry = new Query(LUserFormControl.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("UserName", UserName).AND("ControlId", ControlId);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string UserName,int ControlId)
	    {
		    LUserFormControl item = new LUserFormControl();
		    
            item.UserName = UserName;
            
            item.ControlId = ControlId;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string UserName,int ControlId)
	    {
		    LUserFormControl item = new LUserFormControl();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.UserName = UserName;
				
			item.ControlId = ControlId;
				
	        item.Save(UserName);
	    }
    }
}
