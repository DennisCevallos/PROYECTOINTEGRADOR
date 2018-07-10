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
    public class ColorsController : Controller
    {

        private readonly IApiServicio apiServicios;


        public ColorsController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Color>();
            try
            {
                lista = await apiServicios.Listar<Color>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Colors/ListarColors");
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
        public async Task<IActionResult> Create(Color color)
        {
           Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(color,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Colors/InsertarColor");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                 return View(color);

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
                                                                  "api/Colors");
                    if (respuesta.IsSuccess)
                    {

                        var respuestacolor = JsonConvert.DeserializeObject<Color>(respuesta.Resultado.ToString());
                        var color = new Color
                        {
                            IdColor = respuestacolor.IdColor,
                            Descripcion = respuestacolor.Descripcion,
                            Estado = respuestacolor.Estado
                        };
                        
                        return View(color);
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
        public async Task<IActionResult> Edit(string id, Color color)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, color, new Uri(WebApp.BaseAddress),
                                                                 "api/Colors");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                   return View(color);

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
                                                               , "api/Colors");
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
