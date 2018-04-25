using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace PhotoManager.CustomHelpers
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString TextEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string className = "form-control")
        {
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper,
                expression, new
                {
                    @class = className
                });
        }

        public static MvcHtmlString NumberEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, double stepValue = 1, string className = "form-control")
        {
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper,
                expression, new
                {
                    @class = className,
                    type = "number",
                    step = stepValue
                });
        }

        public static MvcHtmlString FileEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string acceptType = "image/jpeg", string className = "btn-add-photo")
        {
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper,
                expression, new
                {
                    @class = className,
                    type = "file",
                    accept = acceptType
                });
        }

        public static MvcHtmlString DateTimeEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string className = "form-control")
        {
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper,
                expression, new
                {
                    @class = className,
                    type = "date"
                });
        }
    }
}