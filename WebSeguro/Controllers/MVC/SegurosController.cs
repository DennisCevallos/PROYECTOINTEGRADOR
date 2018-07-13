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
                                                                    , "api/Seguroes/ListarSeguros");
                return View(lista);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> Create()
        {
            ViewData["IdPoliza"] = new SelectList(await apiServicios.Listar<Poliza>(new Uri(WebApp.BaseAddress), "api/Polizas/ListarPoliza"), "IdPoliza", "numPoliza");
            ViewData["IdVehiculo"] = new SelectList(await apiServicios.Listar<Vehiculo>(new Uri(WebApp.BaseAddress), "api/Vehiculoes/ListarVehiculos"), "IdVehiculo", "Placa");
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
                                                             "api/Seguroes/InsertarSeguros");
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
                                                                  "api/Seguroes");
                    if (respuesta.IsSuccess)
                    {

                        var respuestaseguro = JsonConvert.DeserializeObject<Seguro>(respuesta.Resultado.ToString());
                        var seguro = new Seguro
                        {
                           IdSeguro = respuestaseguro.IdSeguro,
                           ValAsegurado = respuestaseguro.ValAsegurado,
                           Tasa = respuestaseguro.Tasa,
                           PrimaSeguro = respuestaseguro.PrimaSeguro
                        };
                        ViewData["IdPoliza"] = new SelectList(await apiServicios.Listar<Poliza>(new Uri(WebApp.BaseAddress), "api/Polizas/ListarPoliza"), "IdPoliza", "numPoliza");
                        ViewData["IdVehiculo"] = new SelectList(await apiServicios.Listar<Vehiculo>(new Uri(WebApp.BaseAddress), "api/Vehiculoes/ListarVehiculos"), "IdVehiculo", "Placa");

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
                                                                 "api/Seguroes");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }

                    ViewData["IdPoliza"] = new SelectList(await apiServicios.Listar<Poliza>(new Uri(WebApp.BaseAddress), "api/Polizas/ListarPoliza"), "IdPoliza", "numPoliza");
                    ViewData["IdVehiculo"] = new SelectList(await apiServicios.Listar<Vehiculo>(new Uri(WebApp.BaseAddress), "api/Vehiculoes/ListarVehiculos"), "IdVehiculo", "Placa");
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
                                                               , "api/Seguroes");
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
