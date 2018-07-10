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
    public class PerfilsController : Controller
    {
            private readonly IApiServicio apiServicios;


            public PerfilsController(IApiServicio apiServicios)
            {
                this.apiServicios = apiServicios;
            }
            public async Task<IActionResult> Index()
            {

                var lista = new List<Perfil>();
                try
                {
                    lista = await apiServicios.Listar<Perfil>(new Uri(WebApp.BaseAddress)
                                                                        , "api/Perfils/ListarPerfil");
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
            public async Task<IActionResult> Create(Perfil perfil)
            {
                Response response = new Response();
                try
                {
                    response = await apiServicios.InsertarAsync(perfil,
                                                                 new Uri(WebApp.BaseAddress),
                                                                 "api/Perfils/InsertarPerfil");
                    if (response.IsSuccess)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(perfil);

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
                                                                      "api/Perfils");
                        if (respuesta.IsSuccess)
                        {

                            var respuestaperfil = JsonConvert.DeserializeObject<Perfil>(respuesta.Resultado.ToString());
                            var perfil = new Perfil
                            {
                                IdPerfil = respuestaperfil.IdPerfil,
                                Descripcion = respuestaperfil.Descripcion,
                                Estado = respuestaperfil.Estado
                            };

                            return View(perfil);
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
            public async Task<IActionResult> Edit(string id, Perfil perfil)
            {
                Response response = new Response();
                try
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        response = await apiServicios.EditarAsync(id, perfil, new Uri(WebApp.BaseAddress),
                                                                     "api/Perfils");
                        if (response.IsSuccess)
                        {

                            return RedirectToAction("Index");
                        }
                        return View(perfil);

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
                                                                   , "api/Perfils");
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