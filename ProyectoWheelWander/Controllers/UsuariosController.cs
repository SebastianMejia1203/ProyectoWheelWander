using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoWheelWander.Models;
using ProyectoWheelWander.Models.Data;

namespace ProyectoWheelWander.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly WheelWanderContext _context;

        public UsuariosController(WheelWanderContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var wheelWanderContext = _context.Usuarios.Include(u => u.FkidtipoDocumentoNavigation).Include(u => u.IdrolNavigation);
            return View(await wheelWanderContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.FkidtipoDocumentoNavigation)
                .Include(u => u.IdrolNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.SexoDescripcion = usuario.Sexo switch
            {
                1 => "Masculino",
                2 => "Femenino",
                3 => "Otro",
                _ => "No especificado",
            };

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["FkidtipoDocumento"] = new SelectList(_context.TipoDocumentos, "IdtipoDocumento", "NombreTipoDocumento");
            ViewData["Idrol"] = new SelectList(_context.RolUsuarios, "IdrolUsuario", "NombreRol");
            ViewData["Sexo"] = new SelectList(new[]
                {
                   new { Id = (byte)1, Nombre = "Masculino" },
                   new { Id = (byte)2, Nombre = "Femenino" },
                   new { Id = (byte)3, Nombre = "Otro" }
                }, "Id", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Email,Contrasena,Celular,EstadoUsuario,Sexo,Idrol,FechaRegistro,FkidtipoDocumento,UrlfotoFcedula,UrlfotoPcedula,UrlfotoFlicencia,UrlfotoPlicencia,FechaNacimiento")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Login");
            }
            ViewData["FkidtipoDocumento"] = new SelectList(_context.TipoDocumentos, "IdtipoDocumento", "NombreTipoDocumento", usuario.FkidtipoDocumento);
            ViewData["Idrol"] = new SelectList(_context.RolUsuarios, "IdrolUsuario", "NombreRol", usuario.Idrol);
            ViewData["Sexo"] = new SelectList(new[]
                {
                   new { Id = (byte)1, Nombre = "Masculino" },
                   new { Id = (byte)2, Nombre = "Femenino" },
                   new { Id = (byte)3, Nombre = "Otro" }
                }, "Id", "Nombre");
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["FkidtipoDocumento"] = new SelectList(_context.TipoDocumentos, "IdtipoDocumento", "NombreTipoDocumento", usuario.FkidtipoDocumento);
            ViewData["Idrol"] = new SelectList(_context.RolUsuarios, "IdrolUsuario", "NombreRol", usuario.Idrol);
            ViewData["Sexo"] = new SelectList(new[]
                {
                   new { Id = (byte)1, Nombre = "Masculino" },
                   new { Id = (byte)2, Nombre = "Femenino" },
                   new { Id = (byte)3, Nombre = "Otro" }
                }, "Id", "Nombre");
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Cedula,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Email,Contrasena,Celular,EstadoUsuario,Sexo,Idrol,FechaRegistro,FkidtipoDocumento,UrlfotoFcedula,UrlfotoPcedula,UrlfotoFlicencia,UrlfotoPlicencia,FechaNacimiento")] Usuario usuario)
        {
            if (id != usuario.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Cedula))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkidtipoDocumento"] = new SelectList(_context.TipoDocumentos, "IdtipoDocumento", "NombreTipoDocumento", usuario.FkidtipoDocumento);
            ViewData["Idrol"] = new SelectList(_context.RolUsuarios, "IdrolUsuario", "NombreRol", usuario.Idrol);
            ViewData["Sexo"] = new SelectList(new[]
                {
                   new { Id = (byte)1, Nombre = "Masculino" },
                   new { Id = (byte)2, Nombre = "Femenino" },
                   new { Id = (byte)3, Nombre = "Otro" }
                }, "Id", "Nombre");
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.FkidtipoDocumentoNavigation)
                .Include(u => u.IdrolNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.Cedula == id);
        }
    }
}
