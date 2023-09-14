using Common.BLModels;
using Common.DALModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IVideoRepo
    {
        IEnumerable<BLVideo> GetAll(int page, int pageSize);
        BLVideo GetSpecific(int id);
        void Add(BLVideo video);
        void Update(BLVideo video);
        void Delete(int id);
        IEnumerable<Video> GetVideosForPage(int page, int pageSize);
        int GetTotalCount();
        IEnumerable<Video> GetVideosForPageAndFilter(int page, int pageSize, string? filterName = null, bool ascendingOrder = true);
        IEnumerable<Video> GetFilteredVideos(string filterName, int page, int pageSize);
        int GetTotalCount(string filterName);
    }
}
