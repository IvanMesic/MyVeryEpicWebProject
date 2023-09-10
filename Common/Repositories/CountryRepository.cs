using AutoMapper;
using Common.BLModels;
using Common.DALModels;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Repositories
{
    public class CountryRepository : ICountryRepo
    {
        private readonly RwaMoviesContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(RwaMoviesContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<BLCountry> GetAll(int page, int pageSize)
        {
            var dbCountries = _context.Countries;
            int itemsToSkip = (page - 1) * pageSize;

            // Apply paging to the query
            var pagedQuery = dbCountries.Skip(itemsToSkip).Take(pageSize);

            // Project the results into BLCountry using AutoMapper
            var blCountries = _mapper.Map<IEnumerable<BLCountry>>(pagedQuery);
            return blCountries;
        }

        public IEnumerable<Country> GetCountriesForPage(int page, int pageSize)
        {
            return _context.Countries
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalCount()
        {
            return _context.Countries.Count();
        }
    }
}
