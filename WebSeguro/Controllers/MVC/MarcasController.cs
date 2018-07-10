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
    public class MarcasController : Controller
    {

        private readonly IApiServicio apiServicios;


        public MarcasController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Marca>();
            try
            {
                lista = await apiServicios.Listar<Marca>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Marcas/ListarMarcas");
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
        public async Task<IActionResult> Create(Marca marca)
        {
           Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(marca,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Marcas/InsertarMarca");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                 return View(marca);

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
                                                                  "api/Marcas");
                    if (respuesta.IsSuccess)
                    {

                        var respuestamarca = JsonConvert.DeserializeObject<Marca>(respuesta.Resultado.ToString());
                        var marca = new Marca
                        {
                            IdMarca = respuestamarca.IdMarca,
                            Descripcion = respuestamarca.Descripcion,
                            Estado = respuestamarca.Estado
                        };
                        
                        return View(marca);
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
        public async Task<IActionResult> Edit(string id, Marca marca)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, marca, new Uri(WebApp.BaseAddress),
                                                                 "api/Marcas");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                   return View(marca);

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
                                                               , "api/Marcas");
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
