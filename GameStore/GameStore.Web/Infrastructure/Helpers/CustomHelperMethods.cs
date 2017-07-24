using GameStore.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Infrastructure.Helpers
{
    public static class CustomHelperMethods
    {
        public static MvcHtmlString CustomCheckBox(this HtmlHelper htmlHelper,string id, string name, string value, bool isChecked)
        {
            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("id", id);
            tag.Attributes.Add("name", name);
            tag.Attributes.Add("type", "checkbox");
            tag.Attributes.Add("value", value);
            if(isChecked)
            {
                tag.Attributes.Add("checked", "checked");
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}