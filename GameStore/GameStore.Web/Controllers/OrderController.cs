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
        private static string CustomId = "1";
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

        [ActionName("history")]
        public ActionResult GetOrders()
        {
            var orders = _mapper.Map<IEnumerable<Order>, IList<OrderViewModel>>(_orderService.GetOrdersHistory());
            TempData["orders"] = orders;
            return View(new FilterOrdersViewModel() { Orders = orders});
        }

        [HttpPost]
        public ActionResult FilterOrders(FilterOrdersViewModel filterViewModel)
        {
            var filter = _mapper.Map<FilterOrdersViewModel, FilterOrders>(filterViewModel);
            IList<OrderViewModel> orders;
            if (ModelState.IsValid)
            {
                orders =
                    _mapper.Map<IEnumerable<Order>, IList<OrderViewModel>>(_orderService.GetOrdersHistory(filter));
                TempData["orders"] = orders;
            }
            else
            {
                orders = TempData["orders"] as IList<OrderViewModel>;
                TempData["orders"] = orders;
            }
            return View("history", new FilterOrdersViewModel() { Orders = orders });
        }
    }
}