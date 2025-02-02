using Microsoft.AspNetCore.Mvc;
using WorshipProgramPlannerApp.Models;
using WorshipProgramPlannerApp.Repositories;

namespace WorshipProgramPlannerApp.Controllers
{
    public class WorshipController : Controller
    {
        private readonly IWorshipRepository _worshipRepository;

        public WorshipController(IWorshipRepository worshipRepository)
        {
            _worshipRepository = worshipRepository;
        }

        public IActionResult Index()
        {
            var worships = _worshipRepository.GetAll();
            return View(worships);
        }

        public IActionResult Details(int id)
        {
            var worship = _worshipRepository.GetById(id);
            if (worship == null) return NotFound();
            return View(worship);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Worship worship)
        {
            if (ModelState.IsValid)
            {
                _worshipRepository.Add(worship);
                _worshipRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(worship);
        }

        public IActionResult Edit(int id)
        {
            var worship = _worshipRepository.GetById(id);
            if (worship == null) return NotFound();
            return View(worship);
        }

        [HttpPost]
        public IActionResult Edit(int id, Worship worship)
        {
            if (id != worship.WorshipId) return NotFound();

            if (ModelState.IsValid)
            {
                _worshipRepository.Update(worship);
                _worshipRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(worship);
        }

        public IActionResult Delete(int id)
        {
            var worship = _worshipRepository.GetById(id);
            if (worship == null) return NotFound();
            return View(worship);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _worshipRepository.Delete(id);
            _worshipRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
