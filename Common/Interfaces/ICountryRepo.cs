using Common.BLModels;
using Common.DALModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ICountryRepo
    {
        IEnumerable<BLCountry> GetAll(int page, int pageSize);
        IEnumerable<Country> GetCountriesForPage(int page, int pageSize);
        int GetTotalCount();

    }
}
