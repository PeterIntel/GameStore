using System.Web.Mvc;

namespace GameStore.Web.Filters
{
    public class AddGameErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new ViewResult()
            {
                ViewName = "AddError",
                ViewData = new ViewDataDictionary(filterContext.Exception)
            };

            filterContext.ExceptionHandled = true;
        }
    }
}