using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers.Api
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IApiAuthentication authentication, IOrderService orderService, IMapper mapper) : base(authentication)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ActionName("busket")]
        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.User)]
        public IHttpActionResult Get(string contentType)
        {
            var order = _orderService.GetOrderByCustomerId(CurrentUser.User.Id, CurrentLanguage);

            return Serialize(_mapper.Map<Order, OrderViewModel>(order), contentType);
        }

        [ActionName("buy")]
        [HttpPost]
        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.User)]
        public IHttpActionResult AddGameToOrder(string gameKey, string contentType)
        {
            _orderService.AddGameToCustomerOrder(gameKey, CurrentUser.User.Id, CurrentLanguage);

            return Ok();
        }

        [HttpPost]
        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult AddOneGameToOrder(OrderDetailsViewModel details)
        {
            _orderService.AddGameToOrder(details.GameId, details.OrderId);

            return Ok();
        }

        [HttpPost]
        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult DeleteOneGameFromOrder(OrderDetailsViewModel details)
        {
            _orderService.DeleteGameFromOrder(details.GameId, details.OrderId);

            return Ok();
        }

        [ActionName("history")]
        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult GetHistoryOrders(string contentType)
        {
            return Serialize(GetOrders(_orderService.GetOrdersHistory()), contentType);
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        [ActionName("history")]
        [HttpPost]
        public IHttpActionResult FilterHistoryOrders(FilterOrdersViewModel filterViewModel, string contentType)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var filter = _mapper.Map<FilterOrdersViewModel, FilterOrders>(filterViewModel);

            return Serialize(FilterOrders(_orderService.GetOrdersHistory(filter)), contentType);
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult GetCurrentOrders(string contentType)
        {
            return Serialize(GetOrders(_orderService.GetCurrentOrders()), contentType);
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        [ActionName("GetCurrentOrders")]
        [HttpPost]
        public IHttpActionResult FilterCurrentOrders(FilterOrdersViewModel filterViewModel, string contentType)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var filter = _mapper.Map<FilterOrdersViewModel, FilterOrders>(filterViewModel);

            return Serialize(FilterOrders(_orderService.GetCurrentOrders(filter)), contentType);
        }

        [HttpPost]
        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        public IHttpActionResult Pay(string orderId, string contentType)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, CreateError());
            }

            var order = _orderService.First(x => x.Id == orderId, CurrentLanguage);

            if (order.OrderDetails == null || order.OrderDetails.Count == 0)
            {
                ModelState.AddModelError("", @"The busket have no games");

                return BadRequest(ModelState);
            }


            order.Status = CompletionStatus.Paid;
            _orderService.Update(order, CurrentLanguage);

            return Serialize(_mapper.Map<Order, OrderViewModel>(order), contentType);
        }

        [CustomApiAuthorize(AuthorizationMode.Allow, RoleEnum.Manager)]
        [HttpPost]
        public IHttpActionResult Put(OrderViewModel model)
        {
            var order = _mapper.Map<OrderViewModel, Order>(model);
            _orderService.Update(order, CurrentLanguage);

            return Ok();
        }

        private FilterOrdersViewModel GetOrders(IEnumerable<Order> orders)
        {
            var ordersViewModel = _mapper.Map<IEnumerable<Order>, IList<OrderViewModel>>(orders);

            return new FilterOrdersViewModel() { Orders = ordersViewModel };
        }

        private FilterOrdersViewModel FilterOrders(IEnumerable<Order> orders)
        {
            IList<OrderViewModel> ordersViewModel = _mapper.Map<IEnumerable<Order>, IList<OrderViewModel>>(orders);

            return new FilterOrdersViewModel() { Orders = ordersViewModel };
        }
    }
}
