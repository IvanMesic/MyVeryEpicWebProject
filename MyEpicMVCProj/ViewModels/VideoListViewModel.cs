﻿namespace MyEpicMVCProj.ViewModels
{
    public class VideoListViewModel
    {
        public IEnumerable<VMVideo> Videos { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public int TotalItems { get; set; }
    }
}