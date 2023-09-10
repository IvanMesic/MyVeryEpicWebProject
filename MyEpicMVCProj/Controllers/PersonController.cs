using Microsoft.AspNetCore.Mvc;
using MyEpicMVCProj.Models;
namespace MyEpicMVCProj.Controllers
{
    public class PersonController : Controller
    {
        private static List<VMPerson> _persons = new List<VMPerson>();

        public IActionResult Index()
        {
            ViewData["persons"] = _persons;

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(VMPerson person)
        {
            if (!_persons.Any())
            {
                person.Id = 1;
            }
            else
            {
                var maxId = _persons?.Max(x => x.Id) ?? 0;
                person.Id = maxId + 1;
            }
            _persons.Add(person);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var person = _persons.FirstOrDefault(x => x.Id == id);
            ViewData["person"] = person;

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public IActionResult Edit(VMPerson person)
        {
            var editPerson = _persons.FirstOrDefault(x => x.Id == person.Id);
            editPerson.FirstName = person.FirstName;
            editPerson.LastName = person.LastName;
            editPerson.Email = person.Email;

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var person = _persons.FirstOrDefault(x => x.Id == id);
            ViewData["person"] = person;

            return View();
        }

        [HttpPost]
        public IActionResult Delete(VMPerson person)
        {
            var deletePerson = _persons.FirstOrDefault(x => x.Id == person.Id);
            _persons.Remove(deletePerson);

            return RedirectToAction("Index");
        }


    }
}
