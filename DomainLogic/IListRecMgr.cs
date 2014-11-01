using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic
{
   public interface IListRecMgr
   {
      void Add(ListRec rec);
      void Update(ListRec rec);
      void Delete(ListRec rec);
      void LoadAll();
   }
}
