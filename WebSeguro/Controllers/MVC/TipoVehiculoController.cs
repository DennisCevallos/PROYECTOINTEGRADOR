using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades.Negocio;
using Entidades.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Servicios.Interfaces;

namespace WebSeguro.Controllers
{
    public class TipoVehiculoController : Controller
    {
       
         private readonly IApiServicio apiServicios;


        public TipoVehiculoController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<TipoVehiculo>();
            try
            {
                lista = await apiServicios.Listar<TipoVehiculo>(new Uri(WebApp.BaseAddress)
                                                                    , "api/TipoVehiculoes/ListarTipoVehiculo");
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
        public async Task<IActionResult> Create(TipoVehiculo tipoVehiculo)
        {
            Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(tipoVehiculo,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/TipoVehiculoes/InsertarTipoVehiculo");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(tipoVehiculo);

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
                                                                  "api/TipoVehiculoes");
                    if (respuesta.IsSuccess)
                    {

                        var respuestaTipo = JsonConvert.DeserializeObject<TipoVehiculo>(respuesta.Resultado.ToString());
                        var tipoVehiculo = new TipoVehiculo
                        {
                            IdTipoVehiculo = respuestaTipo.IdTipoVehiculo,
                            Descripcion = respuestaTipo.Descripcion,
                            Estado = respuestaTipo.Estado
                        };

                        return View(tipoVehiculo);
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
        public async Task<IActionResult> Edit(string id, TipoVehiculo tipoVehiculo)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, tipoVehiculo, new Uri(WebApp.BaseAddress),
                                                                 "api/TipoVehiculoes");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                    return View(tipoVehiculo);

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
                                                               , "api/TipoVehiculoes");
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