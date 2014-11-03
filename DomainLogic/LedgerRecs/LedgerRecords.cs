using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Data;
using DataLayer.DataService;
using DomainLogic.ListRecMgr;

namespace DomainLogic.LedgerRecs
{
   public abstract class LedgerModel
   {
      private IDataService m_dataService = null;
      private IListRecMgr m_listRecMgr = null;
      protected IData m_data = null;

      public LedgerModel(IDataService ds, IListRecMgr listRecMgr)
      {
         m_dataService = ds;
         m_listRecMgr = listRecMgr;
      }

      public IData DataHolder
      {
         get
         {
            if (m_data == null)
               m_data = CreateData();
            return m_data;
         }
      }

      protected IDataService DataService
      {
         get
         {
            if (m_dataService == null)
               m_dataService = CreateDataService();
            return m_dataService;
         }
      }

      protected IListRecMgr ListRecMgr
      {
         get
         {
            if (m_listRecMgr == null)
               m_listRecMgr = GetGlobalInstance();
            return m_listRecMgr;
         }
      }

      protected abstract IListRecMgr GetGlobalInstance();

      public void Add()
      {
         DataService.InsertData(DataHolder);

         //CustListRecMgr.Instance.Add(DataHolder);
         ListRecMgr.Add(ListRecMgr.CreateListRec(this));
      }

      public void Save()
      {
         DataService.UpdateData(DataHolder);
      }

      public void Delete()
      {
         DataService.DeleteData(DataHolder);

         ListRecMgr.Delete(ListRecMgr.CreateListRec(this));
      }

      public bool Load()
      {
         IData toLoadData = DataHolder;
         return DataService.LoadData(ref toLoadData);
      }


      protected abstract Data CreateData();

      protected abstract SqlServerDataService CreateDataService();
   }

   public class CustLedgerModel : LedgerModel
   {
      public CustLedgerModel(IDataService ds, IListRecMgr lrm)
         : base(ds, lrm)
      {
      }

      protected override SqlServerDataService CreateDataService()
      {
         return new CustomerDataService();
      }

      protected override Data CreateData()
      {
         return new CustomerData();
      }

      protected override IListRecMgr GetGlobalInstance()
      {
         return CustListRecMgr.Instance;
      }

   }

}
