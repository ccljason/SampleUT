using DataLayer.Data;
using DomainLogic.LedgerRecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic.ListRecMgr
{
   public abstract class ListRecMgr : IListRecMgr
   {
      private List<ListRec> m_list;

      protected List<ListRec> TheList
      {
         get
         {
            if (m_list == null)
               m_list = new List<ListRec>();
            return m_list;
         }
      }


      public void Add(ListRec rec)
      {
         TheList.Add(rec);
      }

      public void Update(ListRec rec)
      {
         ListRec found = TheList.Find(r => r.Id == rec.Id);
         if (found == default(ListRec))
            return;
         else
            DoUpdate(found, rec);
      }

      public void Delete(ListRec rec)
      {
         TheList.Remove(rec);
      }

      public ListRec CreateListRec(LedgerModel ledgerModel)
      {
         ListRec listRec = CreateRec();
         MapListRec(listRec, ledgerModel);
         return listRec;
      }

      protected abstract ListRec CreateRec();

      protected abstract void MapListRec(ListRec rec, LedgerModel model);

      protected abstract void DoUpdate(ListRec to, ListRec from);


   }

   public class CustListRecMgr : ListRecMgr
   {
      #region Singleton
      private static CustListRecMgr m_listRecMgr;
      protected CustListRecMgr()
      {
      }
      public static CustListRecMgr Instance
      {
         get
         {
            if (m_listRecMgr == null)
               m_listRecMgr = new CustListRecMgr();
            return m_listRecMgr;
         }
      }
      #endregion

      protected override void MapListRec(ListRec rec, LedgerModel model)
      {
         CustLedgerModel ledgerModel = model as CustLedgerModel;
         if (ledgerModel == null)
            return;
         CustomerData ledgerModelData = ledgerModel.DataHolder as CustomerData;
         if (ledgerModelData == null)
            return;
         CustListRec listRec = rec as CustListRec;
         if (listRec == null)
            return;

         listRec.Id = ledgerModelData.Id;
         listRec.Property = ledgerModelData.Property;
         listRec.Email = ledgerModelData.Email;
      }

      protected override ListRec CreateRec()
      {
         return new CustListRec();
      }

      protected override void DoUpdate(ListRec to, ListRec from)
      {
         CustListRec toRec = to as CustListRec;
         CustListRec fromRec = from as CustListRec;
         if (toRec == null || fromRec == null)
            return;

         toRec.Property = fromRec.Property;
         toRec.Email = fromRec.Email;
      }
   }

   //public class InventListRecMgr : ListRecMgr
   //{
   //   protected override ListRecMgr CreateListRecMgr()
   //   {
   //      return new InventListRecMgr();
   //   }
   //   protected override void DoUpdate(ListRec to, ListRec from)
   //   {
   //      InventListRec toRec = to as InventListRec;
   //      InventListRec fromRec = from as InventListRec;
   //      if (toRec == null || fromRec == null)
   //         return;

   //      toRec.Property = fromRec.Property;
   //      toRec.Category = fromRec.Category;
   //   }
   //}

}
