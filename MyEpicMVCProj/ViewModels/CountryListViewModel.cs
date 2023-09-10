﻿namespace MyEpicMVCProj.ViewModels
{
    public class CountryListViewModel
    {
        public IEnumerable<VMCountry> Countries { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);


    }
}
