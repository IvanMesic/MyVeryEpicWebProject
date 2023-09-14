using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;
using AutoMapper;
using Common.Interfaces;
using MyEpicMVCProj.ViewModels;
using Common.BLModels;

namespace MVC.Controllers
{
    public class VideoMvcController : Controller
    {
        private readonly IVideoRepo _videoRepository;
        private readonly IMapper _mapper;

        public VideoMvcController(IVideoRepo videoRepository, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoadVideos(int page = 1, int pageSize = 10, string filterName = null)
        {
            var videos = string.IsNullOrEmpty(filterName)
                ? _videoRepository.GetVideosForPage(page, pageSize)
                : _videoRepository.GetFilteredVideos(filterName, page, pageSize); // Ensure this method exists

            var blVideos = _mapper.Map<IEnumerable<BLVideo>>(videos);
            var vmVideos = _mapper.Map<IEnumerable<VMVideo>>(blVideos);

            var videoListViewModel = new VideoListViewModel
            {
                Videos = vmVideos,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = _videoRepository.GetTotalCount(filterName), // Ensure this method handles filtering
            };

            return Json(videoListViewModel);
        }

        public IActionResult Details(int id)
        {
            var blVideo = _videoRepository.GetSpecific(id);
            var vmVideo = _mapper.Map<VMVideo>(blVideo);
            return View(vmVideo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VMVideo vmVideo)
        {
            if (ModelState.IsValid)
            {
                var blVideo = _mapper.Map<BLVideo>(vmVideo);
                _videoRepository.Add(blVideo);
                return RedirectToAction("Index");
            }
            return View(vmVideo);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var blVideo = _videoRepository.GetSpecific(id);
            var vmVideo = _mapper.Map<VMVideo>(blVideo);
            return View(vmVideo);
        }

        [HttpPost]
        public IActionResult Save(VMVideo vmVideo)
        {
            if (!ModelState.IsValid)
            {
                var blVideo = _mapper.Map<BLVideo>(vmVideo);
                _videoRepository.Update(blVideo);
                return RedirectToAction("Index");
            }
            return View(vmVideo);
        }

        public IActionResult Delete(int id)
        {
            var blVideo = _videoRepository.GetSpecific(id);
            var vmVideo = _mapper.Map<VMVideo>(blVideo);
            return View(vmVideo);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _videoRepository.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
