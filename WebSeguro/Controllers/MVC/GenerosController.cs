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
    public class GenerosController : Controller
    {

        private readonly IApiServicio apiServicios;


        public GenerosController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Genero>();
            try
            {
                lista = await apiServicios.Listar<Genero>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Generoes/ListarGenero");
                return View(lista);
            }
            catch (Exception ex)
            {
                return BadRequest();
                //hola todos
            }
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Genero genero)
        {
           Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(genero,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Generoes/InsertarGenero");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                 return View(genero);

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
                                                                  "api/Generoes");
                    if (respuesta.IsSuccess)
                    {

                        var respuestagenero = JsonConvert.DeserializeObject<Genero>(respuesta.Resultado.ToString());
                        var genero = new Genero
                        {
                            IdGenero = respuestagenero.IdGenero,
                            Descripcion = respuestagenero.Descripcion,
                            Estado = respuestagenero.Estado
                        };
                        
                        return View(genero);
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
        public async Task<IActionResult> Edit(string id, Genero genero)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, genero, new Uri(WebApp.BaseAddress),
                                                                 "api/Generoes");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                   return View(genero);

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
                                                               , "api/Generoes");
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
