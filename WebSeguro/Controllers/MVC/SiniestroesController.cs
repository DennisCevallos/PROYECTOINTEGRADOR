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
    public class SiniestroesController : Controller
    {
        private readonly IApiServicio apiServicios;


        public SiniestroesController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Siniestro>();
            try
            {
                lista = await apiServicios.Listar<Siniestro>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Siniestroes/ListarSiniestro");
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
        public async Task<IActionResult> Create(Siniestro siniestro)
        {
            Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(siniestro,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Siniestroes/InsertarSiniestro");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(siniestro);

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
                                                                  "api/Siniestroes");
                    if (respuesta.IsSuccess)
                    {

                        var respuestasiniestro = JsonConvert.DeserializeObject<Siniestro>(respuesta.Resultado.ToString());
                        var siniestro = new Siniestro
                        {
                            IdSiniestro = respuestasiniestro.IdSiniestro,
                            Fecha = respuestasiniestro.Fecha,
                            CallePrincipal = respuestasiniestro.CallePrincipal,
                            CalleSecundaria=respuestasiniestro.CalleSecundaria,
                            Referencia=respuestasiniestro.Referencia,
                            IdVehiculo=respuestasiniestro.IdVehiculo
                        };

                        return View(siniestro);
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
        public async Task<IActionResult> Edit(string id, Siniestro siniestro)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, siniestro, new Uri(WebApp.BaseAddress),
                                                                 "api/Siniestroes");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                    return View(siniestro);

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
                                                               , "api/Siniestroes");
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