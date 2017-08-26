using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    [CustomAuthorize(RoleEnum.Manager)]
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;
        private IMapper _mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        public ActionResult GetPublishers()
        {
            return View(_mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherViewModel>>(_publisherService.Get()));
        }

        [CustomAuthorize(RoleEnum.Manager, RoleEnum.User)]
        // GET: Publisher
        public ActionResult GetPublisherDetails(string companyName)
        {
            var publisher = _publisherService.GetPublisherByCompanyName(companyName);
            return View(_mapper.Map<Publisher, PublisherViewModel>(publisher));
        }

        [ActionName("new")]
        public ActionResult AddPublisher()
        {
            return View();
        }

        [ActionName("new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPublisher(PublisherViewModel publisherViewModel)
        {
            if (ModelState.IsValid)
            {
                var publisher = _mapper.Map<PublisherViewModel, Publisher>(publisherViewModel);
                _publisherService.Add(publisher);
                return RedirectToAction("GetPublishers", new {companyName = publisherViewModel.CompanyName});
            }
            return View(publisherViewModel);
        }

        public ActionResult Edit(string key)
        {
            Publisher publisher = _publisherService.First(x => x.CompanyName == key);
            if (publisher == null)
            {
                return HttpNotFound();
            }

            var publisherViewModel = _mapper.Map<Publisher, PublisherViewModel>(publisher);
     
            return View(publisherViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PublisherViewModel publisherViewModel)
        {
            if (ModelState.IsValid)
            {
                var publisher = _mapper.Map<PublisherViewModel, Publisher>(publisherViewModel);
                _publisherService.Update(publisher);

                return RedirectToAction("getPublishers");
            }

            return View(publisherViewModel); 
        }

        public ActionResult Delete(string key)
        {
            var publisher = _publisherService.First(x => x.CompanyName == key);
            if (publisher == null)
            {
                return HttpNotFound();
            }

            return View(_mapper.Map<Publisher, PublisherViewModel>(publisher));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDelete(string id)
        {
            var publisher = _publisherService.First(x => x.Id == id);
            if (ModelState.IsValid)
            {
                _publisherService.Remove(id);

                return RedirectToAction("getPublishers");
            }

            return View(_mapper.Map<Publisher, PublisherViewModel>(publisher));
        }
    }
}