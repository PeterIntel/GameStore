using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IAuthentication authentication, IOrderService orderService, IMapper mapper) : base(authentication)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        // GET: Order
        [HttpGet]
        [ActionName("busket")]
        [CustomAuthorize(RoleEnum.User)]
        public ActionResult GetOrderDetails(string cultureCode)
        {
            var order = _orderService.GetOrderByCustomerId(CurrentUser.Id, cultureCode);

            return View(_mapper.Map<Order, OrderViewModel>(order));
        }

        [ActionName("buy")]
        [HttpPost]
        [CustomAuthorize(RoleEnum.User)]
        public ActionResult AddGameToOrder(string gameKey, string cultureCode)
        {
            _orderService.AddGameToCustomerOrder(gameKey, CurrentUser.Id, cultureCode);

            return RedirectToAction("busket");
        }
        
        [HttpPost]
        [CustomAuthorize(RoleEnum.Manager)]
        public ActionResult AddOneGameToOrder(OrderDetailsViewModel details)
        {
            _orderService.AddGameToOrder(details.GameId, details.OrderId);

            return RedirectToAction("edit", new { key = details.OrderId});
        }

        [HttpPost]
        [CustomAuthorize(RoleEnum.Manager)]
        public ActionResult DeleteOneGameFromOrder(OrderDetailsViewModel details)
        {
            _orderService.DeleteGameFromOrder(details.GameId, details.OrderId);

            return RedirectToAction("edit", new { key = details.OrderId });
        }

        [ActionName("history")]
        [CustomAuthorize(RoleEnum.Manager)]
        public ActionResult GetHistoryOrders()
        {
            return View(GetOrders(_orderService.GetOrdersHistory()));
        }

        [ActionName("history")]
        [HttpPost]
        public ActionResult FilterHistoryOrders(FilterOrdersViewModel filterViewModel)
        {
            var filter = _mapper.Map<FilterOrdersViewModel, FilterOrders>(filterViewModel);

            return View(FilterOrders(_orderService.GetOrdersHistory(filter)));
        }

        [CustomAuthorize(RoleEnum.Manager)]
        public ActionResult GetCurrentOrders()
        {
            return View(GetOrders(_orderService.GetCurrentOrders()));
        }

        [ActionName("GetCurrentOrders")]
        [HttpPost]
        public ActionResult FilterCurrentOrders(FilterOrdersViewModel filterViewModel)
        {
            var filter = _mapper.Map<FilterOrdersViewModel, FilterOrders>(filterViewModel);

            return View(FilterOrders(_orderService.GetCurrentOrders(filter)));
        }

        [HttpPost]
        [CustomAuthorize(RoleEnum.User)]
        public ActionResult Pay(string orderId)
        {
            var order = _orderService.First(x => x.Id == orderId, CurrentLanguageCode);
            if (order.OrderDetails == null || order.OrderDetails.Count == 0)
            {
                ModelState.AddModelError("", @"The busket have no games");
            }

            if (ModelState.IsValid)
            {
                order.Status = CompletionStatus.Paid;
                _orderService.Update(order, CurrentLanguageCode);

                return RedirectToAction("games", "game");
            }

            return View("busket", _mapper.Map<Order, OrderViewModel>(order));
        }

        [ActionName("edit")]
        [CustomAuthorize(RoleEnum.Manager)]
        public ActionResult Edit(string key)
        {
            var order = _orderService.First(x => x.Id == key, CurrentLanguageCode);

            return View(_mapper.Map<Order, OrderViewModel>(order));
        }

        [ActionName("edit")]
        [HttpPost]
        public ActionResult Edit(OrderViewModel model)
        {
            var order = _mapper.Map<OrderViewModel, Order>(model);
            _orderService.Update(order, CurrentLanguageCode);

            return RedirectToAction("GetCurrentOrders");
        }

        private FilterOrdersViewModel GetOrders(IEnumerable<Order> orders)
        {
            var ordersViewModel = _mapper.Map<IEnumerable<Order>, IList<OrderViewModel>>(orders);
            TempData["orders"] = orders;

            return new FilterOrdersViewModel() { Orders = ordersViewModel };
        }

        private FilterOrdersViewModel FilterOrders(IEnumerable<Order> orders)
        {
            IList<OrderViewModel> ordersViewModel;
            if (ModelState.IsValid)
            {
                ordersViewModel =
                    _mapper.Map<IEnumerable<Order>, IList<OrderViewModel>>(orders);
                TempData["orders"] = ordersViewModel;
            }
            else
            {
                ordersViewModel = TempData["orders"] as IList<OrderViewModel>;
                TempData["orders"] = ordersViewModel;
            }

            return new FilterOrdersViewModel() { Orders = ordersViewModel };
        }
    }
}