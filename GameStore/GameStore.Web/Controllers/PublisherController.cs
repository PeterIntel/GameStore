using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    public class PublisherController : Controller
    {
        private IPublisherService _publisherService;
        private IMapper _mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }
        // GET: Publisher
        public ActionResult GetPublisherDetails(string companyName)
        {
            var publisher = _publisherService.GetPublisherByCompanyName(companyName);
            return View(_mapper.Map<Publisher, PublisherViewModel>(publisher));
        }
    }
}