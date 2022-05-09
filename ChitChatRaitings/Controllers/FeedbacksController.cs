#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChitChatRaitings.Models;
using ChitChatRaitings.Services;

namespace ChitChatRaitings.Controllers
{
    public class FeedbacksController : Controller
    {
        private IFeedbackService _service;

        public FeedbacksController()
        {
            _service = new FeedbackService();
        }


        // GET: Feedbacks
        public IActionResult Index()
        {
            return View(_service.GetAllFeedbacks());
        }

        // GET: Feedbacks/Details/5
        public IActionResult Details(int id)
        {

            var feedback = _service.GetFeedback(id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Rate,Description")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _service.Create(feedback.Rate, feedback.Description, feedback.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public IActionResult Edit(int id)
        {

            var feedback = _service.GetFeedback(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirmed(int id, [Bind("Id,Name,Rate,Description")] Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _service.EditFeedback(feedback.Id, feedback.Rate, feedback.Description);
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public IActionResult Delete(int id)
        {
            var feedback = _service.GetFeedback(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.DeleteFeedback(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
