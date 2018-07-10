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
    public class SegurosController : Controller
    {

        private readonly IApiServicio apiServicios;


        public SegurosController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Seguro>();
            try
            {
                lista = await apiServicios.Listar<Seguro>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Seguros/ListarSeguros");
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
        public async Task<IActionResult> Create(Seguro seguro)
        {
           Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(seguro,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Seguros/InsertarSeguro");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                 return View(seguro);

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
                                                                  "api/Seguros");
                    if (respuesta.IsSuccess)
                    {

                        var respuestaSeguro = JsonConvert.DeserializeObject<Seguro>(respuesta.Resultado.ToString());
                        var seguro = new Seguro
                        {
                            IdSeguro = respuestaSeguro.IdSeguro,
                            IdPoliza = respuestaSeguro.IdPoliza,
                            IdVehiculo=respuestaSeguro.IdVehiculo,
                            ValAsegurado=respuestaSeguro.ValAsegurado,
                            Tasa=respuestaSeguro.Tasa,
                            PrimaSeguro=respuestaSeguro.PrimaSeguro

                        };
                        
                        return View(seguro);
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
        public async Task<IActionResult> Edit(string id, Seguro seguro)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, seguro, new Uri(WebApp.BaseAddress),
                                                                 "api/Seguros");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                   return View(seguro);

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
                                                               , "api/Seguros");
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
