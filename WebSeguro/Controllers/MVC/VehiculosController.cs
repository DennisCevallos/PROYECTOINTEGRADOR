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
    public class VehiculosController : Controller
    {

        private readonly IApiServicio apiServicios;


        public VehiculosController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Vehiculo>();
            try
            {
                lista = await apiServicios.Listar<Vehiculo>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Vehiculoes/ListarVehiculos");
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
        public async Task<IActionResult> Create(Vehiculo vehiculo)
        {
            Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(vehiculo,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Vehiculoes/InsertarVehiculo");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(vehiculo);

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
                                                                  "api/Vehiculoes");
                    if (respuesta.IsSuccess)
                    {

                        var respuestaVehiculo = JsonConvert.DeserializeObject<Vehiculo>(respuesta.Resultado.ToString());
                        var vehiculo = new Vehiculo
                        {
                            IdVehiculo = respuestaVehiculo.IdVehiculo,
                            IdMarca = respuestaVehiculo.IdMarca,
                            IdModelo = respuestaVehiculo.IdModelo,
                            Placa = respuestaVehiculo.Placa,
                            Chasis = respuestaVehiculo.Chasis,
                            IdColor = respuestaVehiculo.IdColor,
                            Observaciones = respuestaVehiculo.Observaciones,
                            IdTipoVehiculo = respuestaVehiculo.IdTipoVehiculo,
                            Estado = respuestaVehiculo.Estado,
                            AnioDeFabricacion = respuestaVehiculo.AnioDeFabricacion,
                            Url = respuestaVehiculo.Url
                        };

                        return View(vehiculo);
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
        public async Task<IActionResult> Edit(string id, Vehiculo vehiculo)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, vehiculo, new Uri(WebApp.BaseAddress),
                                                                 "api/Vehiculoes");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                    return View(vehiculo);

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
                                                               , "api/Vehiculoes");
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