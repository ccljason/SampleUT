using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace DomainLogic
{
   public abstract class LedgerModel
   {
      protected IData m_data = null;

      public IData DataHolder
      {
         get
         {
            if (m_data == null)
               m_data = CreateData();
            return m_data;
         }
      }

      public void Add()
      {
         SqlServerDataService ds = GetDataService();
         ds.InsertData(DataHolder);


         //CustListRecMgr.Instance.Add(DataHolder);
      }

      public void Save()
      {
         SqlServerDataService ds = GetDataService();
         ds.UpdateData(DataHolder);
      }

      public void Delete()
      {
         SqlServerDataService ds = GetDataService();
         ds.DeleteData(DataHolder);
      }

      public bool Load()
      {
         SqlServerDataService ds = GetDataService();
         IData toLoadData = DataHolder;
         return ds.LoadData(ref toLoadData);
      }


      protected abstract Data CreateData();

      protected abstract SqlServerDataService GetDataService();
   }

   public class CustLedgerModel : LedgerModel
   {
      protected override SqlServerDataService GetDataService()
      {
         return new CustomerDataService();
      }

      protected override Data CreateData()
      {
         return new CustomerData();
      }
   }

}
