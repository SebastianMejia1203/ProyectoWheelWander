﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Datos;
using ProyectoWheelWander.Models;
using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Models.ViewModel;
using System.Security.Claims;

namespace ProyectoWheelWander.Controllers
{
    public class UsuariosController : Controller
    {   
        UsuarioDatos _UsuarioDatos = new UsuarioDatos();

        [Authorize(Roles = "1")]
        public IActionResult listaUsuarios() {
            //la vista mostrara una lista de usuarios
            var listaUsuarios = _UsuarioDatos.GetAllUsuario();

            return View(listaUsuarios); 
        }

        // GET: Obtener Pagina de Mi Perfil
        [Authorize]
        [HttpGet("MiPerfil")]
        public IActionResult MiPerfil()
        {
            var cedulaAutenticada = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            int cedula = Convert.ToInt32(cedulaAutenticada);
            var oUsuario = _UsuarioDatos.BuscarUsuario(cedula); //busca un usuario especifico
            MiPerfilViewModel model = new MiPerfilViewModel
            {
                usuario = oUsuario,
            };
            return View(model); // Envía este modelo integral a la vista
        }

        [Authorize(Roles = "1")]
        [HttpGet("MiPerfil/{cedula}")]
        public IActionResult MiPerfil(int cedula)
        {
            var oUsuario = _UsuarioDatos.BuscarUsuario(cedula); //busca un usuario especifico
            return View(oUsuario); // Envía este modelo integral a la vista
        }

        // GET: Obtener Pagina de cuenta creada
        public ActionResult CuentaCreada()
        {
            return View();
        }

        // GET: Obtener Pagina de error de registro
        public ActionResult ErrorRegistro()
        {
            return View();
        }

        public ActionResult RecuperarCuenta()
        {
            return View();
        }
        public ActionResult RecuperarTrue()
        {
            return View();
        }
        public ActionResult RecuperarFalse()
        {
            return View();
        }


        public IActionResult Register()
        {
            if (!User.Identity!.IsAuthenticated) {
                var viewModel = new RegisterViewModel
                {
                    Usuario = new Usuario(), // Inicializa un nuevo usuario para ser llenado desde la vista
                    TiposDocumento = _UsuarioDatos.GetAllTipoDocumento() // Carga los tipos de documento desde la base de datos
                };

                return View(viewModel); // Envía este modelo integral a la vista
            }
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
                model.Usuario.FKIDTipoDocumento = (int)model.FKIDTipoDocumentoSelected; // Asegúrate de asignar el tipo de documento seleccionado al usuario
                var respuesta = _UsuarioDatos.RegistrarUsuario(model.Usuario);

                if (respuesta)
                {
                    return RedirectToAction("CuentaCreada");
                }
            
            // Vuelve a cargar los tipos de documento en caso de que la validación falle
            model.TiposDocumento = _UsuarioDatos.GetAllTipoDocumento();
            return View("ErrorRegistro");
        }

        [Authorize]
        [HttpGet("/Usuarios/UpdateUsuario")]
        public IActionResult UpdateUsuario()
        {
            var cedulaAutenticada = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            int cedula = Convert.ToInt32(cedulaAutenticada);
            var oUsuario = _UsuarioDatos.BuscarUsuario(cedula); //busca un usuario especifico
            return View(oUsuario); // Envía este modelo integral a la vista
        }

        [Authorize(Roles = "1")]
        [HttpGet("/Usuarios/UpdateUsuario/{cedula}")]
        public IActionResult UpdateUsuario(int cedula)
        {
            var oUsuario = _UsuarioDatos.BuscarUsuario(cedula); //busca un usuario especifico
            return View(oUsuario); // Envía este modelo integral a la vista
        }

        [HttpPost]
        public IActionResult UpdateUsuario(Usuario model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = _UsuarioDatos.EditarUsuario(model);

            if (respuesta)
            {
                return RedirectToAction("MiPerfil", "Usuarios");
            }
            return View();
        }

        [Authorize]
        [HttpGet("/Usuarios/DeleteUsuario")]
        public IActionResult DeleteUsuario()
        {
            var cedulaAutenticada = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            int cedula = Convert.ToInt32(cedulaAutenticada);
            var oUsuario = _UsuarioDatos.BuscarUsuario(cedula); //busca un usuario especifico
            return View(oUsuario); // Envía este modelo integral a la vista
        }

        [Authorize(Roles = "1")]
        [HttpGet("/Usuarios/DeleteUsuario/{cedula}")]
        public IActionResult DeleteUsuario(int cedula)
        {
            var oUsuario = _UsuarioDatos.BuscarUsuario(cedula); //busca un usuario especifico
            return View(oUsuario); // Envía este modelo integral a la vista
        }

        [HttpPost]
        public IActionResult DeleteUsuario(Usuario model)
        {
            var respuesta = _UsuarioDatos.DesabilitarUsuario(model.Cedula);

            if (respuesta)
            {
                if (User.IsInRole("1")){
                    return RedirectToAction("listaUsuarios");
                }
                else if (User.IsInRole("2")){
                    return RedirectToAction("Salir", "Login");
                }

            }
            return View();
        }
    }
}
