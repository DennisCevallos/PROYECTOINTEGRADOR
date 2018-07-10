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
    public class PolizasController : Controller
    {
        private readonly IApiServicio apiServicios;


        public PolizasController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Poliza>();
            try
            {
                lista = await apiServicios.Listar<Poliza>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Polizas/ListarPoliza");
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
        public async Task<IActionResult> Create(Poliza poliza)
        {
            Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(poliza,new Uri(WebApp.BaseAddress),
                                                             "api/Polizas/InsertarPoliza");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(poliza);

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
                                                                  "api/Polizas");
                    if (respuesta.IsSuccess)
                    {

                        var respuestapoliza = JsonConvert.DeserializeObject<Poliza>(respuesta.Resultado.ToString());
                        var poliza = new Poliza
                        {
                            IdPoliza = respuestapoliza.IdPoliza,
                            FechaCoverturaI = respuestapoliza.FechaCoverturaI,
                            FechaCoverturaF= respuestapoliza.FechaCoverturaF,
                            NumPoliza=respuestapoliza.NumPoliza,
                            Factura=respuestapoliza.Factura,
                            TotValAsegurado=respuestapoliza.TotValAsegurado,
                            TotValPrima=respuestapoliza.TotValPrima,
                            IdPersona=respuestapoliza.IdPersona
                        };

                        return View(poliza);
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
        public async Task<IActionResult> Edit(string id, Poliza poliza)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, poliza, new Uri(WebApp.BaseAddress),
                                                                 "api/Polizas");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                    return View(poliza);

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
                                                               , "api/Polizas");
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