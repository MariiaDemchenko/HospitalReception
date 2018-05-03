using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }

        public static MvcHtmlString CustomEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, bool addSelectAllItem = false, string className = "form-control")
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            Type baseEnumType = Enum.GetUnderlyingType(enumType);
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (FieldInfo field in enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
            {
                string text = field.Name;
                string value = Convert.ChangeType(field.GetValue(null), baseEnumType).ToString();
                bool selected = field.GetValue(null).Equals(metadata.Model);
                foreach (DisplayAttribute displayAttribute in field.GetCustomAttributes(true).OfType<DisplayAttribute>())
                {
                    text = displayAttribute.GetName();
                }

                items.Add(new SelectListItem()
                {
                    Text = text,
                    Value = value,
                    Selected = !addSelectAllItem && selected
                });
            }

            if (addSelectAllItem)
            {
                items.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
            }

            return htmlHelper.DropDownListFor(expression, new SelectList(items, "Value", "Text"), new { @class = className });
        }

        public static MvcHtmlString DateEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string className = "form-control")
        {
            return htmlHelper.TextBoxFor(expression, new
            {
                @class = className,
                type = "date"
            });
        }

        public static MvcHtmlString DateTimeEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string className = "form-control")
        {
            return htmlHelper.TextBoxFor(expression, new
            {
                @class = className,
                type = "datetime-local"
            });
        }

        public static MvcHtmlString HiddenFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string className = "form-control")
        {
            return htmlHelper.HiddenFor(expression, new
            {
                @class = className
            });
        }
    }
}