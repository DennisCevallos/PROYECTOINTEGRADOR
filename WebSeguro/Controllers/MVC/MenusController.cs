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
    public class MenusController : Controller
    {
        private readonly IApiServicio apiServicios;


        public MenusController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<Menu>();
            try
            {
                lista = await apiServicios.Listar<Menu>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Menus/ListarMenus");
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
        public async Task<IActionResult> Create(Menu menu)
        {
            Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(menu,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Menus/InsertarMenus");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(menu);

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
                                                                  "api/Menus");
                    if (respuesta.IsSuccess)
                    {

                        var respuestamenu = JsonConvert.DeserializeObject<Menu>(respuesta.Resultado.ToString());
                        var menu = new Menu
                        {
                            IdMenu = respuestamenu.IdMenu,
                            Titulo = respuestamenu.Titulo,
                            Url = respuestamenu.Url,
                            TipoMenu = respuestamenu.TipoMenu,
                            Estado = respuestamenu.Estado
                        };

                        return View(menu);
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
        public async Task<IActionResult> Edit(string id, Menu menu)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, menu, new Uri(WebApp.BaseAddress),
                                                                 "api/Menus");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                    return View(menu);

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
                                                               , "api/Menus");
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