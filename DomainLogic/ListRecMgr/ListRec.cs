using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic.ListRecMgr
{
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

}
