using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public static class MyHtmlHelperExtensions
    {
        public static IHtmlContent ColorfulHeading(this IHtmlHelper htmlHelper, int level, string color, string content)
        {
            level = level < 1 ? 1 : level;
            level = level > 6 ? 6 : level;
            var tagName = $"h{level}";
            var tagBuilder = new TagBuilder(tagName);
            tagBuilder.Attributes.Add("style", $"color: {color ?? "green"}");
            tagBuilder.InnerHtml.Append(content ?? string.Empty);
            return tagBuilder;
        }

        public static IHtmlContent FakeTextboxFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            var body = expression.Body as MemberExpression;
            var propertyName = body.Member.Name.ToLower(); // keep lower case
            var tagBuilder = new TagBuilder("input")
            {
                TagRenderMode = TagRenderMode.SelfClosing
            };
            tagBuilder.Attributes.Add("type", "text"); // can be omitted
            tagBuilder.Attributes.Add("name", propertyName); // key purpose 1: for name-value pair
            tagBuilder.Attributes.Add("id", propertyName); // key purpose 2: for client JavaScript and CSS
            return tagBuilder;
        }
    }
}
