using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades.Negocio;
using Entidades.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Servicios.Interfaces;

namespace WebSeguro.Controllers.MVC
{
    public class LoginsController : Controller
    {

        private readonly IApiServicio apiServicios;


        public LoginsController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Login>();
            try
            {
                lista = await apiServicios.Listar<Login>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Logins/ListarLogin");
                return View(lista);
            }
            catch (Exception ex)
            {
                return BadRequest();
                
            }
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Login login)
        {
            Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(login,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Logins/InsertarLogin");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(login);

            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var respuesta = await apiServicios.SeleccionarAsync<Response>(id, new Uri(WebApp.BaseAddress),
                                                                  "api/Logins");
                    if (respuesta.IsSuccess)
                    {

                        var respuestalogin = JsonConvert.DeserializeObject<Login>(respuesta.Resultado.ToString());
                        var login = new Login
                        {
                            IdLogin = respuestalogin.IdLogin,
                            FechaCambio = respuestalogin.FechaCambio,
                            Clave = respuestalogin.Clave,
                            Usuario = respuestalogin.Usuario,
                            Estado = respuestalogin.Estado,
                            IdPersona = respuestalogin.IdPersona,
                            IdPerfil = respuestalogin.IdPerfil
                        };

                        return View(login);
                    }

                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Login login)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, login, new Uri(WebApp.BaseAddress),
                                                                 "api/Logins");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                    return View(login);

                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var response = await apiServicios.EliminarAsync(id, new Uri(WebApp.BaseAddress)
                                                               , "api/Logins");
                if (response.IsSuccess)
                {

                    return RedirectToAction("Index");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {


                return BadRequest();
            }
        }
    }
}