using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.TagHelpers
{
    [HtmlTargetElement(Attributes = "if")]
    public class IfTagHelper : TagHelper
    {
        [HtmlAttributeName("if")]
        public bool RenderContent { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (RenderContent == false)
            {
                output.TagName = null;
                output.SuppressOutput();
            }
            output.Attributes.RemoveAll("if");
        }

        public override int Order => int.MinValue;
    }
}
