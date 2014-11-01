using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public interface IData
   {
      int Id { get; set; }
   }

   public interface IDataService
   {
      void InsertData(IData data);
      void UpdateData(IData data);
      void DeleteData(IData data);
      bool LoadData(ref IData data);
   }

}
