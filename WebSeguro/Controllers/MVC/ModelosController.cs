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
    public class ModelosController : Controller
    {

        private readonly IApiServicio apiServicios;


        public ModelosController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Modelo>();
            try
            {
                lista = await apiServicios.Listar<Modelo>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Modeloes/ListarModelo");
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
        public async Task<IActionResult> Create(Modelo modelo)
        {
            Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(modelo,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Modeloes/InsertarModelo");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(modelo);

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
                                                                  "api/Modeloes");
                    if (respuesta.IsSuccess)
                    {

                        var respuestaModelo = JsonConvert.DeserializeObject<Modelo>(respuesta.Resultado.ToString());
                        var modelo = new Modelo
                        {
                            IdModelo = respuestaModelo.IdModelo,
                            Descripcion = respuestaModelo.Descripcion,
                            Estado = respuestaModelo.Estado
                        };

                        return View(modelo);
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
        public async Task<IActionResult> Edit(string id, Modelo modelo)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, modelo, new Uri(WebApp.BaseAddress),
                                                                 "api/Modeloes");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                    return View(modelo);

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
                                                               , "api/Modeloes");
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