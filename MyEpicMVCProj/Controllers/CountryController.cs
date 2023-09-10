using AutoMapper;
using Common.BLModels;
using Common.DALModels;
using Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEpicMVCProj.ViewModels;

namespace MyEpicMVCProj.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepo _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepo countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public IActionResult GetAllCountries(int page = 1, int pageSize = 10)
        {
            var totalCountries = _countryRepository.GetTotalCount();

            var countries = _countryRepository.GetCountriesForPage(page, pageSize);

            var blCountries = _mapper.Map<IEnumerable<BLCountry>>(countries);
            var vmCountries = _mapper.Map<IEnumerable<VMCountry>>(blCountries);

            var countryListViewModel = new CountryListViewModel
            {
                Countries = vmCountries,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalCountries,
            };

            return View(countryListViewModel);
        }
    }
}
