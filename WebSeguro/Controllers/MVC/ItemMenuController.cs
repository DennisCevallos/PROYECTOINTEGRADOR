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
    public class ItemMenuController : Controller
    {
        private readonly IApiServicio apiServicios;


        public ItemMenuController(IApiServicio apiServicios)
        {
            this.apiServicios = apiServicios;
        }
        public async Task<IActionResult> Index()
        {

            var lista = new List<ItemMenu>();
            try
            {
                lista = await apiServicios.Listar<ItemMenu>(new Uri(WebApp.BaseAddress)
                                                                    , "api/ItemMenus/ListarItemMenu");
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
        public async Task<IActionResult> Create(ItemMenu itemmenu)
        {
            Response response = new Response();
            try
            {
                response = await apiServicios.InsertarAsync(itemmenu,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/ItemMenus/InsertarItemMenu");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(itemmenu);

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
                                                                  "api/ItemMenus");
                    if (respuesta.IsSuccess)
                    {

                        var respuestaitemmenu = JsonConvert.DeserializeObject<ItemMenu>(respuesta.Resultado.ToString());
                        var itemmenu = new ItemMenu
                        {
                            IdSubMenu = respuestaitemmenu.IdSubMenu,
                            IdPerfil = respuestaitemmenu.IdPerfil,
                            IdMenu = respuestaitemmenu.IdMenu,
                            Estado=respuestaitemmenu.Estado
                        };

                        return View(itemmenu);
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
        public async Task<IActionResult> Edit(string id, ItemMenu itemmenu)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicios.EditarAsync(id, itemmenu, new Uri(WebApp.BaseAddress),
                                                                 "api/ItemMenus");
                    if (response.IsSuccess)
                    {

                        return RedirectToAction("Index");
                    }
                    return View(itemmenu);

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
                                                               , "api/ItemMenus");
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