using AutoMapper;
using Common.BLModels;
using Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEpicMVCProj.ViewModels;

namespace MyEpicMVCProj.Controllers
{
    public class TagMvcController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagMvcController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public IActionResult GetAllTags()
        {
            var blTags = _tagRepository.GetAll();
            var vmTags = _mapper.Map<IEnumerable<VMTag>>(blTags);
            return View(vmTags);
        }
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var blTag = _tagRepository.GetSpecific(id);
            var vmTag = _mapper.Map<VMTag>(blTag);
            return View(vmTag);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VMTag vmTag)
        {
            if (ModelState.IsValid)
            {
                var blTag = _mapper.Map<BLTag>(vmTag);
                _tagRepository.Add(blTag);
                return RedirectToAction("Index");
            }
            return View(vmTag);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var blTag = _tagRepository.GetSpecific(id);
            var vmTag = _mapper.Map<VMTag>(blTag);
            return View(vmTag);
        }

        [HttpPut]
        public IActionResult Save(VMTag vmTag)
        {
            if (ModelState.IsValid)
            {
                var blTag = _mapper.Map<BLTag>(vmTag);
                _tagRepository.Update(blTag);
                return RedirectToAction("Index");
            }
            return View(vmTag);
        }

        public IActionResult Delete(int id)
        {
            var blTag = _tagRepository.GetSpecific(id);
            var vmTag = _mapper.Map<VMTag>(blTag);
            return View(vmTag);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _tagRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
