using GameStore.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
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
    }
}