using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChitChatRaitings.Data;
using ChitChatRaitings.Models;


namespace ChitChatRaitings.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly ChitChatRaitingsContext _context;

        public FeedbacksController(ChitChatRaitingsContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Feedback.ToListAsync());
        }


        // GET: Feedbacks + Search bar
        public async Task<IActionResult> Search()
        {
            double rateSum = 0;
            double numOfElem = 0;
            double averageRate;
            var averageString = "";
            foreach (var feedback in _context.Feedback)
            {
                rateSum += feedback.Rate;
                numOfElem++;
            }
            if (numOfElem == 0)
            {
                averageString = "Does not have any ratings yet...";
            }
            else
            {
                averageRate = rateSum / numOfElem;
                averageString = System.Math.Round(averageRate, 2).ToString();
            }
            ViewBag.AverageString = averageString;

            return View(await _context.Feedback.ToListAsync());
        }

        // POST: Feedbacks + Search bar
        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            if (query == null)
            {
                return Json(await _context.Feedback.ToListAsync());
            }
            else
            {
                var q = from Feedback in _context.Feedback
                        where Feedback.Description.Contains(query)
                        select Feedback;
                return Json(await q.ToListAsync());
            }
        }


        public async Task<IActionResult> Search2(string query)
        {
            if(query == null)
            {
                return Json(await _context.Feedback.ToListAsync());
            }
            else
            {
                var q = from Feedback in _context.Feedback
                        where Feedback.Description.Contains(query)
                        select Feedback;

                return Json(await q.ToListAsync());
            }
        }


        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Feedback == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedback
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Rate,Description,CreatedDate")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.CreatedDate = DateTime.Now;
                if(feedback.Description == null)
                {
                    feedback.Description = "";
                }
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Search));
            }
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Feedback == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Rate,Description,CreatedDate")] Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    feedback.CreatedDate = DateTime.Now;
                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Search));
            }
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Feedback == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedback
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Feedback == null)
            {
                return Problem("Entity set 'ChitChatRaitingsContext.Feedback'  is null.");
            }
            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedback.Remove(feedback);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Search));
        }

        private bool FeedbackExists(int id)
        {
          return (_context.Feedback?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
