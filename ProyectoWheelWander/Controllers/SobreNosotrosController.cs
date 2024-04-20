using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoWheelWander.Controllers
{
    public class SobreNosotrosController : Controller
    {
        // GET: SobreNosotrosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SobreNosotrosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SobreNosotrosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SobreNosotrosController/Create
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

        // GET: SobreNosotrosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SobreNosotrosController/Edit/5
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

        // GET: SobreNosotrosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SobreNosotrosController/Delete/5
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
