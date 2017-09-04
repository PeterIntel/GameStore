using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Infrastructure.Helpers
{
    public static class CustomHelperMethods
    {
        public static MvcHtmlString CustomCheckBox(this HtmlHelper htmlHelper, string id, string name, string value, bool isChecked)
        {
            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("id", id);
            tag.Attributes.Add("name", name);
            tag.Attributes.Add("type", "checkbox");
            tag.Attributes.Add("value", value);
            if (isChecked)
            {
                tag.Attributes.Add("checked", "checked");
            }

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfoViewModel model)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= model.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("button");
                tag.InnerHtml = i.ToString();
                if (i == model.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString LangSwitcher(this UrlHelper url, string name, ViewContext viewContext, string lang)
        {
            var routeData = viewContext.RouteData;
            var queryString = viewContext.RequestContext.HttpContext.Request.Url.Query;
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);
            if (routeValueDictionary.ContainsKey("lang"))
            {
                if (routeData.Values["lang"] as string == lang)
                {
                    li.AddCssClass("active");
                }
                else
                {
                    routeValueDictionary["lang"] = lang;
                }
            }

            a.MergeAttribute("href", url.RouteUrl(routeValueDictionary) + queryString);
            a.SetInnerText(name);
            li.InnerHtml = a.ToString();
            
            return new MvcHtmlString(li.ToString());
        }

    }
}