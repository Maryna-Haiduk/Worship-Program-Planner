using Microsoft.AspNetCore.Mvc;
using WorshipProgramPlannerApp.Models;
using WorshipProgramPlannerApp.Repositories;
using WorshipProgramPlannerApp.DTOs;
using System.Linq;

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
            var worshipPrograms = _worshipProgramRepository.GetAll()
                .Select(wp => new WorshipProgramDTO
                {
                    WorshipProgramId = wp.WorshipProgramId,
                    PerformerName = wp.PerformerName,
                    PoetryName = wp.PoetryName,
                    SongName = wp.SongName,
                    Comment = wp.Comment,
                    WorshipId = wp.WorshipId
                });

            return RedirectToAction("Index", "Worship");
        }


        //public IActionResult ListOfPrograms()
        //{
        //    var worshipPrograms = _worshipProgramRepository.GetAll()
        //        .Select(wp => new WorshipProgramDTO
        //        {
        //            WorshipProgramId = wp.WorshipProgramId,
        //            PerformerName = wp.PerformerName,
        //            PoetryName = wp.PoetryName,
        //            SongName = wp.SongName,
        //            Comment = wp.Comment,
        //            WorshipId = wp.WorshipId
        //        });
        //    return View(worshipPrograms);
        //}

        public IActionResult Details(int id)
        {
            var worshipProgram = _worshipProgramRepository.GetById(id);
            if (worshipProgram == null) return NotFound();

            var dto = new WorshipProgramDTO
            {
                WorshipProgramId = worshipProgram.WorshipProgramId,
                PerformerName = worshipProgram.PerformerName,
                PoetryName = worshipProgram.PoetryName,
                SongName = worshipProgram.SongName,
                Comment = worshipProgram.Comment,
                WorshipId = worshipProgram.WorshipId
            };

            return View(dto);
        }

        public IActionResult Create(int worshipId)
        {
            var dto = new WorshipProgramDTO { WorshipId = worshipId };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WorshipProgramDTO dto)
        {
            if (ModelState.IsValid)
            {
                var worshipProgram = new WorshipProgram
                {
                    PerformerName = dto.PerformerName,
                    PoetryName = dto.PoetryName,
                    SongName = dto.SongName,
                    Comment = dto.Comment,
                    WorshipId = dto.WorshipId
                };

                _worshipProgramRepository.Add(worshipProgram);
                _worshipProgramRepository.SaveChanges();
                return RedirectToAction(nameof(ListOfPrograms));
            }
            return View(dto);
        }

        public IActionResult Edit(int id)
        {
            var worshipProgram = _worshipProgramRepository.GetById(id);
            if (worshipProgram == null) return NotFound();

            var dto = new WorshipProgramDTO
            {
                WorshipProgramId = worshipProgram.WorshipProgramId,
                PerformerName = worshipProgram.PerformerName,
                PoetryName = worshipProgram.PoetryName,
                SongName = worshipProgram.SongName,
                Comment = worshipProgram.Comment,
                WorshipId = worshipProgram.WorshipId
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, WorshipProgramDTO dto)
        {
            if (id != dto.WorshipProgramId) return NotFound();

            if (ModelState.IsValid)
            {
                var worshipProgram = new WorshipProgram
                {
                    WorshipProgramId = dto.WorshipProgramId,
                    PerformerName = dto.PerformerName,
                    PoetryName = dto.PoetryName,
                    SongName = dto.SongName,
                    Comment = dto.Comment,
                    WorshipId = dto.WorshipId
                };

                _worshipProgramRepository.Update(worshipProgram);
                _worshipProgramRepository.SaveChanges();
                return RedirectToAction(nameof(ListOfPrograms));
            }
            return View(dto);
        }

        public IActionResult Delete(int id)
        {
            var worshipProgram = _worshipProgramRepository.GetById(id);
            if (worshipProgram == null) return NotFound();

            var dto = new WorshipProgramDTO
            {
                WorshipProgramId = worshipProgram.WorshipProgramId,
                PerformerName = worshipProgram.PerformerName,
                PoetryName = worshipProgram.PoetryName,
                SongName = worshipProgram.SongName,
                Comment = worshipProgram.Comment,
                WorshipId = worshipProgram.WorshipId
            };

            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _worshipProgramRepository.Delete(id);
            _worshipProgramRepository.SaveChanges();
            return RedirectToAction(nameof(ListOfPrograms));
        }
    }
}




//using Microsoft.AspNetCore.Mvc;
//using WorshipProgramPlannerApp.Repositories;
//using WorshipProgramPlannerApp.Models;

//namespace WorshipProgramPlanner.Controllers
//{
//    public class WorshipProgramController : Controller
//    {
//        private readonly IWorshipProgramRepository _worshipProgramRepository;

//        public WorshipProgramController(IWorshipProgramRepository worshipProgramRepository)
//        {
//            _worshipProgramRepository = worshipProgramRepository;
//        }

//        public IActionResult ListOfPrograms()
//        {
//            var worshipPrograms = _worshipProgramRepository.GetAll();
//            return View(worshipPrograms);
//        }

//        public IActionResult Details(int id)
//        {
//            var worshipProgram = _worshipProgramRepository.GetById(id);
//            if (worshipProgram == null) return NotFound();
//            return View(worshipProgram);
//        }

//        public IActionResult Create(int worshipId)
//        {
//            var worshipProgram = new WorshipProgram { WorshipId = worshipId };
//            return View(worshipProgram);
//        }

//        [HttpPost]
//        public IActionResult Create(WorshipProgram worshipProgram)
//        {
//            // ModelState should only require WorshipId and other relevant properties.
//            if (ModelState.IsValid)
//            {
//                _worshipProgramRepository.Add(worshipProgram);
//                _worshipProgramRepository.SaveChanges();
//                return RedirectToAction(nameof(ListOfPrograms));
//            }
//            if (!ModelState.IsValid)
//            {
//                var errors = ModelState
//                             .SelectMany(m => m.Value.Errors)
//                             .Select(e => e.ErrorMessage)
//                             .ToList();

//                foreach (var error in errors)
//                {
//                    Console.WriteLine($"Model Error: {error}");
//                }
//            }
//            return View(worshipProgram);
//        }

//        public IActionResult Edit(int id)
//        {
//            var worshipProgram = _worshipProgramRepository.GetById(id);
//            if (worshipProgram == null) return NotFound();
//            return View(worshipProgram);
//        }

//        [HttpPost]
//        public IActionResult Edit(int id, WorshipProgram worshipProgram)
//        {
//            if (id != worshipProgram.WorshipProgramId) return NotFound();

//            if (ModelState.IsValid)
//            {
//                _worshipProgramRepository.Update(worshipProgram);
//                _worshipProgramRepository.SaveChanges();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(worshipProgram);
//        }

//        public IActionResult Delete(int id)
//        {
//            var worshipProgram = _worshipProgramRepository.GetById(id);
//            if (worshipProgram == null) return NotFound();
//            return View(worshipProgram);
//        }

//        [HttpPost, ActionName("Delete")]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            _worshipProgramRepository.Delete(id);
//            _worshipProgramRepository.SaveChanges();
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
