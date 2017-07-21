using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private static int CustomId = 1;
        private IOrderService _orderService;
        private IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        // GET: Order
        [HttpGet]
        [ActionName("busket")]
        public ActionResult GetOrderDetails()
        {
            var order = _orderService.GetOrderByCustomerId(CustomId);
            return View(_mapper.Map<Order, OrderViewModel>(order));
        }

        [ActionName("buy")]
        [HttpPost]
        public ActionResult AddGameToOrder(string gamekey)
        {
            _orderService.AddGameToOrder(gamekey, CustomId);
            return RedirectToAction("busket");
        }
    }
}