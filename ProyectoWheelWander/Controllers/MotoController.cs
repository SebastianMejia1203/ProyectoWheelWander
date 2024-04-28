using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoWheelWander.Controllers
{
    [Authorize]
    public class MotoController : Controller
    {
        // GET: Obtener Pagina de Administrar mis motos
        public ActionResult Index ()
        {
            return View();
        }

        // GET: Obtener Pagina de crear moto
        public ActionResult CrearMoto()
        {
            return View();
        }

        // GET: Moto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Moto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Moto/Create
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

        // GET: Moto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Moto/Edit/5
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

        // GET: Moto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Moto/Delete/5
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
