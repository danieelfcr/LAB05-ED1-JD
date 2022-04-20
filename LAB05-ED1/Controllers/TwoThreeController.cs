using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB05_ED1.Controllers
{
    public class TwoThreeController : Controller
    {
        // GET: TwoThreeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TwoThreeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TwoThreeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TwoThreeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TwoThreeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TwoThreeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TwoThreeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TwoThreeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
