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
            if (worshipProgram == null)
            {
                return NotFound();
            }

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
        public IActionResult Edit(WorshipProgramDTO dto)
        {
            if (ModelState.IsValid)
            {
                var worshipProgram = _worshipProgramRepository.GetById(dto.WorshipProgramId);
                if (worshipProgram == null)
                {
                    return NotFound();
                }

                worshipProgram.PerformerName = dto.PerformerName;
                worshipProgram.PoetryName = dto.PoetryName;
                worshipProgram.SongName = dto.SongName;
                worshipProgram.Comment = dto.Comment;

                _worshipProgramRepository.Update(worshipProgram);
                _worshipProgramRepository.SaveChanges();

                return RedirectToAction("Index", "Worship");
            }
            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, string deleteCode)
        {
            var worshipProgram = _worshipProgramRepository.GetById(id);

            if (worshipProgram == null)
            {
                return NotFound();
            }
           
            _worshipProgramRepository.Delete(id);
            _worshipProgramRepository.SaveChanges();
        

            return RedirectToAction("Index", "Worship");
        }



        //public IActionResult Details(int id)
        //{
        //    var worshipProgram = _worshipProgramRepository.GetById(id);
        //    if (worshipProgram == null) return NotFound();

        //    var dto = new WorshipProgramDTO
        //    {
        //        WorshipProgramId = worshipProgram.WorshipProgramId,
        //        PerformerName = worshipProgram.PerformerName,
        //        PoetryName = worshipProgram.PoetryName,
        //        SongName = worshipProgram.SongName,
        //        Comment = worshipProgram.Comment,
        //        WorshipId = worshipProgram.WorshipId
        //    };

        //    return View(dto);
        //}

        //public IActionResult Edit(int id)
        //{
        //    var worshipProgram = _worshipProgramRepository.GetById(id);
        //    if (worshipProgram == null) return NotFound();

        //    var dto = new WorshipProgramDTO
        //    {
        //        WorshipProgramId = worshipProgram.WorshipProgramId,
        //        PerformerName = worshipProgram.PerformerName,
        //        PoetryName = worshipProgram.PoetryName,
        //        SongName = worshipProgram.SongName,
        //        Comment = worshipProgram.Comment,
        //        WorshipId = worshipProgram.WorshipId
        //    };

        //    return View(dto);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, WorshipProgramDTO dto)
        //{
        //    if (id != dto.WorshipProgramId) return NotFound();

        //    if (ModelState.IsValid)
        //    {
        //        var worshipProgram = new WorshipProgram
        //        {
        //            WorshipProgramId = dto.WorshipProgramId,
        //            PerformerName = dto.PerformerName,
        //            PoetryName = dto.PoetryName,
        //            SongName = dto.SongName,
        //            Comment = dto.Comment,
        //            WorshipId = dto.WorshipId
        //        };

        //        _worshipProgramRepository.Update(worshipProgram);
        //        _worshipProgramRepository.SaveChanges();
        //        return RedirectToAction(nameof(ListOfPrograms));
        //    }
        //    return View(dto);
        //}

        //public IActionResult Delete(int id)
        //{
        //    var worshipProgram = _worshipProgramRepository.GetById(id);
        //    if (worshipProgram == null) return NotFound();

        //    var dto = new WorshipProgramDTO
        //    {
        //        WorshipProgramId = worshipProgram.WorshipProgramId,
        //        PerformerName = worshipProgram.PerformerName,
        //        PoetryName = worshipProgram.PoetryName,
        //        SongName = worshipProgram.SongName,
        //        Comment = worshipProgram.Comment,
        //        WorshipId = worshipProgram.WorshipId
        //    };

        //    return View(dto);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    _worshipProgramRepository.Delete(id);
        //    _worshipProgramRepository.SaveChanges();
        //    return RedirectToAction(nameof(ListOfPrograms));
        //}
    }
}