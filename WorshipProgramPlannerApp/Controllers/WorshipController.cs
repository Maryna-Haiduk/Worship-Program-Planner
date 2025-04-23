using Microsoft.AspNetCore.Mvc;
using WorshipProgramPlannerApp.DTOs;
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
            var currentDate = DateTime.Today;

            var worships = _worshipRepository.GetAll()
                .Where(w => w.WorshipDate >= currentDate) // Hide past worships
                .OrderBy(w => w.WorshipDate) // Sort by date (future first)
                .ToList();

            var worshipsDtos = worships.Select(c => new WorshipDTO()
            {
                WorshipId = c.WorshipId,
                WorshipName = c.WorshipName,
                WorshipDate = c.WorshipDate,
                WorshipPrograms = c.WorshipPrograms
                    .Select(v => new WorshipProgramDTO()
                    {
                        Comment = v.Comment,
                        PerformerName = v.PerformerName,
                        PoetryName = v.PoetryName,
                        SongName = v.SongName,
                        WorshipProgramId = v.WorshipProgramId
                    }).ToList()
            }).ToList();

            return View(worshipsDtos);
        }


        //public IActionResult Index()
        //{
        //    var worships = _worshipRepository.GetAll();
        //    var worshipsDtos = worships.Select(c => new WorshipDTO()
        //    {
        //        WorshipName = c.WorshipName,
        //        WorshipDate = c.WorshipDate,
        //        WorshipPrograms = c.WorshipPrograms.Select(v => new WorshipProgramDTO()
        //        {
        //            Comment = v.Comment,
        //            PerformerName = v.PerformerName,
        //            PoetryName = v.PoetryName,
        //            SongName = v.SongName
        //        }).ToList()
        //    });

        //    return View(worships);
        //}


        public IActionResult GetAllWorships(int year)
        {
            var worships = _worshipRepository.GetAll().Where(c => c.WorshipDate.Year == year)
                .OrderBy(c => c.WorshipDate); // Sorting from oldest to newest

            var worshipsDtos = worships.Select(c => new WorshipDTO()
            {
                WorshipId = c.WorshipId,
                
                WorshipName = c.WorshipName,
                WorshipDate = c.WorshipDate,
                WorshipPrograms = c.WorshipPrograms.Select(v => new WorshipProgramDTO()
                {
                    WorshipId = v.WorshipProgramId,
                    WorshipProgramId = v.WorshipProgramId,
                    PerformerName = v.PerformerName,
                    Comment = v.Comment,
                    PoetryName = v.PoetryName,
                    SongName = v.SongName
                }).ToList()
            });

            return View(worshipsDtos); // Fixed: passing worshipsDtos to the View
        }


        [HttpPost]
        public IActionResult Delete(int id, string deleteCode)
        {
            var correctCode = "55555";

            if (deleteCode != correctCode)
            {
                ModelState.AddModelError("", "Incorrect deletion code.");
                return RedirectToAction(nameof(Index));
            }

            var worship = _worshipRepository.GetById(id);
            if (worship == null)
            {
                return NotFound();
            }

            _worshipRepository.Delete(id);
            _worshipRepository.SaveChanges(); // Persist the changes
            
            return RedirectToAction(nameof(Index));
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Worship worship)
        {
            if (worship != null)
            {
                Console.WriteLine($"Worship Name: {worship.WorshipName}, Date: {worship.WorshipDate}");
            }
            if (ModelState.IsValid)
            {
                _worshipRepository.Add(worship);
                _worshipRepository.SaveChanges(); // This is required to persist changes in the database
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

       
    }
}
