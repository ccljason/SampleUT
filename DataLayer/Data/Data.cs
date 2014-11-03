using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
   public abstract class Data : IData
   {
      public int Id { get; set; }
   }

   public class CustomerData : Data
   {
      public string Property { get; set; }
      public string Email { get; set; }
   }

   public class InventData : Data
   {
      public string Property { get; set; }
      public string Category { get; set; }
   }
}
