using Microsoft.AspNetCore.Mvc;
using WorshipProgramPlannerApp.Repositories;
using WorshipProgramPlannerApp.Models;
using WorshipProgramPlannerApp.Repositories;

namespace WorshipProgramPlanner.Controllers
{
    public class WorshipProgramController : Controller
    {
        private readonly IWorshipProgramRepository _worshipProgramRepository;

        public WorshipProgramController(IWorshipProgramRepository worshipProgramRepository)
        {
            _worshipProgramRepository = worshipProgramRepository;
        }

        public IActionResult ListOfPrograms()
        {
            var worshipPrograms = _worshipProgramRepository.GetAll();
            return View(worshipPrograms);
        }

        public IActionResult Details(int id)
        {
            var worshipProgram = _worshipProgramRepository.GetById(id);
            if (worshipProgram == null) return NotFound();
            return View(worshipProgram);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(WorshipProgram worshipProgram)
        {
            if (ModelState.IsValid)
            {
                _worshipProgramRepository.Add(worshipProgram);
                _worshipProgramRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(worshipProgram);
        }

        public IActionResult Edit(int id)
        {
            var worshipProgram = _worshipProgramRepository.GetById(id);
            if (worshipProgram == null) return NotFound();
            return View(worshipProgram);
        }

        [HttpPost]
        public IActionResult Edit(int id, WorshipProgram worshipProgram)
        {
            if (id != worshipProgram.WorshipProgramId) return NotFound();

            if (ModelState.IsValid)
            {
                _worshipProgramRepository.Update(worshipProgram);
                _worshipProgramRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(worshipProgram);
        }

        public IActionResult Delete(int id)
        {
            var worshipProgram = _worshipProgramRepository.GetById(id);
            if (worshipProgram == null) return NotFound();
            return View(worshipProgram);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _worshipProgramRepository.Delete(id);
            _worshipProgramRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
