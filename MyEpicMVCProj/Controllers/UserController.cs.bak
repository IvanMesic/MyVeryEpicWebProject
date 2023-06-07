using Microsoft.AspNetCore.Mvc;
using Octopus.Client.Repositories;
using Common;
using Common.Repositories;
using AutoMapper;
using Common.Interfaces;
using Common.BLModels;
using MyEpicMVCProj.Models;

namespace MyEpicMVCProj.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserReposi _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserReposi userRepo, IMapper mapper)
        {
            _userRepository = userRepo;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var blUsers = _userRepository.FuckAll();
            var vmusers = _mapper.Map<IEnumerable<VMPerson>>(blUsers);
            return View(vmusers);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var blUser = _userRepository.GetSpecific(id);
            var vmUser = _mapper.Map<VMPerson>(blUser);
            return View(vmUser);
        }

        [HttpPost]
        public IActionResult Create(VMPerson vmUser)
        {
            if (ModelState.IsValid)
            {
                var blUser = _mapper.Map<BLUser>(vmUser);
                _userRepository.Add(blUser);
                return RedirectToAction("Index");
            }
            return View(vmUser);
        }


        [HttpPut]
        public IActionResult Edit(VMPerson vmUser)
        {
            if (ModelState.IsValid)
            {
                var blUser = _mapper.Map<BLUser>(vmUser);
                _userRepository.Update(blUser);
                return RedirectToAction("Index");
            }
            return View(vmUser);
        }

        public IActionResult Delete(int id)
        {
            var blUser = _userRepository.GetSpecific(id);
            var vmUser = _mapper.Map<VMPerson>(blUser);
            return View(vmUser);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _userRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
