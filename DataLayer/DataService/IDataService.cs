using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataService
{
   public interface IDataService
   {
      void InsertData(IData data);
      void UpdateData(IData data);
      void DeleteData(IData data);
      bool LoadData(ref IData data);
   }

}
