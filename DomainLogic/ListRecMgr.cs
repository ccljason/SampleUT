using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic
{
   /// <summary>
   /// 
   /// </summary>
   public abstract class ListRec
   {
      public int Id { get; set; }
      public string Property { get; set; }
   }

   public class CustListRec : ListRec
   {
      public string Email { get; set; } 
   }

   public class InventListRec : ListRec
   {
      public string Category { get; set; }
   }

   public abstract class ListRecMgr : IListRecMgr
   {
      private List<ListRec> m_list;

      private ListRecMgr m_listRecMgr;
      protected ListRecMgr()
      {
      }
      public static ListRecMgr Instance
      {
         get
         {
            if (m_listRecMgr == null)
               m_listRecMgr = CreateListRecMgr();
            return m_listRecMgr;
         }
      }

      protected abstract ListRecMgr CreateListRecMgr();

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

      public void LoadAll()
      {
      }

      public ListRec CreateListRec(LedgerModel ledgerModel)
      {
         ListRec listRec = CreateRec();
         return listRec;
      }

      protected abstract ListRec CreateRec();
      protected abstract void MapListRec(ListRec rec, LedgerModel model);

      protected abstract void DoUpdate(ListRec to, ListRec from);


   }

   public class CustListRecMgr : ListRecMgr
   {
      protected override void MapListRec(ListRec rec, LedgerModel model)
      {
         CustLedgerModel ledgerModel = model as CustLedgerModel;
         CustListRec listRec = rec as CustListRec;
         if (ledgerModel == null || listRec == null)
            return;

         listRec.Id = ledgerModel.DataHolder.Id;
         //listRec.Property = ledgerModel.DataHolder.
      }
      protected override ListRec CreateRec()
      {
         return new CustListRec();
      }

      protected override ListRecMgr CreateListRecMgr()
      {
         return new CustListRecMgr();
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
