using Common.BLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IUserReposi
    {
        public IEnumerable<BLUser> FuckAll();
        BLUser GetSpecific(int id);
        void Add(BLUser user);
        void Update(BLUser user);
        void Delete(int id);
    }
}
