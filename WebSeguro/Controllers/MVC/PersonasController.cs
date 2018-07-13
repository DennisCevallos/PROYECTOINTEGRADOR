using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades.Negocio;
using Entidades.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Servicios.Interfaces;

namespace WebSeguro.Controllers.MVC
{
    public class PersonasController : Controller
    {

        private readonly IApiServicio apiServicios;


        public PersonasController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Persona>();
            try
            {
                lista = await apiServicios.Listar<Persona>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Personas/ListarPersonas");
                return View(lista);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> Create()
        {
            ViewData["IdGenero"] = new SelectList(await apiServicios.Listar<Genero>(new Uri(WebApp.BaseAddress), "api/Generoes/ListarGenero"), "IdGenero", "Descripcion");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Persona persona)
        {
           Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(persona,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Personas/InsertarPersona");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                 return View(persona);

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
                                                                  "api/Personas");
                    if (respuesta.IsSuccess)
                    {

                        var respuestaPersona = JsonConvert.DeserializeObject<Persona>(respuesta.Resultado.ToString());
                        var persona = new Persona
                        {
                            IdPersona = respuestaPersona.IdPersona,
                            Identificacion = respuestaPersona.Identificacion,
                            Nombres = respuestaPersona.Nombres,
                            Apellido = respuestaPersona.Apellido,
                            Direccion = respuestaPersona.Direccion,
                            Email = respuestaPersona.Email,
                            Telefono = respuestaPersona.Telefono,
                            Celular = respuestaPersona.Celular,
                            Estado = respuestaPersona.Estado,
                        };

                        ViewData["IdGenero"] = new SelectList(await apiServicios.Listar<Genero>(new Uri(WebApp.BaseAddress), "api/Generoes/ListarGenero"), "IdGenero", "Descripcion");

                        return View(persona);
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
        public async Task<IActionResult> Edit(string id, Persona persona)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, persona, new Uri(WebApp.BaseAddress),
                                                                 "api/Personas");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                    ViewData["IdGenero"] = new SelectList(await apiServicios.Listar<Genero>(new Uri(WebApp.BaseAddress), "api/Generoes/ListarGenero"), "IdGenero", "Descripcion");
                    return View();

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
                                                               , "api/Personas");
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
