using Common.BLModels;
using Common.DALModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ITagRepository
    {
        IEnumerable<BLTag> GetAll();
        BLTag GetSpecific(int id);
        void Add(BLTag tag);
        void Update(BLTag tag);
        void Delete(int id);
    }
}
