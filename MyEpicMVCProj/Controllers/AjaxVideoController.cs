using AutoMapper;
using Common.BLModels;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyEpicMVCProj.ViewModels;

namespace MyEpicMVCProj.Controllers
{
    public class AjaxVideoController : Controller
    {
        private readonly IVideoRepo _videoRepository;
        private readonly IMapper _mapper;

        public AjaxVideoController(IVideoRepo videoRepository, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public IActionResult GetAllAjaxVideos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoadVideos(int page = 1, int pageSize = 10, string? filterName = null, bool ascendingOrder = true)
        {
            var totalVideos = _videoRepository.GetTotalCount(); // Note: You might want to update this to reflect the filtered count
            var videos = _videoRepository.GetVideosForPageAndFilter(page, pageSize, filterName, ascendingOrder);
            var blVideos = _mapper.Map<IEnumerable<BLVideo>>(videos);
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);

            var videoListViewModel = new VideoListViewModel
            {
                Videos = vmVideos,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalVideos, // Note: This should reflect the filtered count if a filter is applied
            };

            return Json(videoListViewModel);
        }
    }
}
