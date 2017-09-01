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
    public class PublisherController : BaseController
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper, IAuthentication auth) : base(auth)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [CustomAuthorize(RoleEnum.Manager)]
        public ActionResult GetPublishers()
        {
            return View(_mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherViewModel>>(_publisherService.Get()));
        }

        [AllowAnonymous]
        [ActionName("details")]
        // GET: Publisher
        public ActionResult GetPublisherDetails(string companyName)
        {
            var publisher = _publisherService.GetPublisherByCompanyName(companyName);

            return View(_mapper.Map<Publisher, PublisherViewModel>(publisher));
        }

        [CustomAuthorize(RoleEnum.Manager)]
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
                return RedirectToAction("GetPublishers");
            }

            return View(publisherViewModel);
        }

        [CustomAuthorize(RoleEnum.Manager)]
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

        [CustomAuthorize(RoleEnum.Publisher)]
        public ActionResult EditProfile()
        {
            if (CurrentUser.Publisher != null)
            {
                Publisher publisher = _publisherService.First(x => x.CompanyName == CurrentUser.Publisher.CompanyName);
                if (publisher == null)
                {
                    return HttpNotFound();
                }

                var publisherViewModel = _mapper.Map<Publisher, PublisherViewModel>(publisher);

                return View("edit", publisherViewModel);
            }

            return new HttpStatusCodeResult(403);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PublisherViewModel publisherViewModel)
        {
            if (ModelState.IsValid)
            {
                var publisher = _mapper.Map<PublisherViewModel, Publisher>(publisherViewModel);
                _publisherService.Update(publisher);

                if (CurrentUser.IsInRole(RoleEnum.Publisher))
                {
                    return RedirectToAction("details", new { companyName = publisher.CompanyName });
                }

                return RedirectToAction("getPublishers");
            }

            return View(publisherViewModel);
        }

        [CustomAuthorize(RoleEnum.Manager)]
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