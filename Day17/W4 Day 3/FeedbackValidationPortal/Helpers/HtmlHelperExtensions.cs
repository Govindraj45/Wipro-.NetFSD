using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FeedbackValidationPortal.Helpers;

public static class HtmlHelperExtensions
{
    public static IHtmlContent StyledInputFor<TModel, TResult>(
        this IHtmlHelper<TModel> html,
        System.Linq.Expressions.Expression<Func<TModel, TResult>> expression,
        string placeholder,
        string cssClass)
    {
        return html.TextBoxFor(expression, new { placeholder, @class = cssClass });
    }
}
