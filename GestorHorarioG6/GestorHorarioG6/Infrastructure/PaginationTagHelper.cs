using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using GestorHorarioG6.Models;

namespace GestorHorarioG6.Infrastructure
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("div", Attributes = "pagination-model")]
    public class PaginationTagHelper : TagHelper
    {
        private readonly int MaxPageLinks = 8;
        private IUrlHelperFactory urlHelperFactory;

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PaginationViewModel PaginationModel { get; set; }

        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            var result = new TagBuilder("div");

            var firstArrow = new TagBuilder("a");
            firstArrow.Attributes["href"] = urlHelper.Action(PageAction, new { page = 1 });
            firstArrow.InnerHtml.Append("<<");
            firstArrow.AddCssClass("btn");
            firstArrow.AddCssClass("btn-default");
            result.InnerHtml.AppendHtml(firstArrow);

            int initialPage = PaginationModel.CurrentPage - (MaxPageLinks / 2);
            if (initialPage < 1) initialPage = 1;
            int finalPage = initialPage + MaxPageLinks - 1;
            if (finalPage > PaginationModel.TotalPages) finalPage = PaginationModel.TotalPages;

            for (int p = initialPage; p <= finalPage; p++)
            {
                var link = new TagBuilder("a");
                if (p == PaginationModel.CurrentPage)
                {
                    link = new TagBuilder("p");
                    link.InnerHtml.Append(p.ToString());
                    link.AddCssClass("btn-info");
                }
                else
                {
                    link.Attributes["href"] = urlHelper.Action(PageAction, new { page = p });
                    link.InnerHtml.Append(p.ToString());
                    link.AddCssClass("btn-default");
                }
                link.AddCssClass("btn");
                result.InnerHtml.AppendHtml(link);
            }

            var lastArrow = new TagBuilder("a");
            lastArrow.Attributes["href"] = urlHelper.Action(PageAction, new { page = (PaginationModel.TotalPages) });
            lastArrow.InnerHtml.Append(">>");
            lastArrow.AddCssClass("btn");
            lastArrow.AddCssClass("btn-default");
            result.InnerHtml.AppendHtml(lastArrow);

            output.Content.AppendHtml(result);
        }
    }
}
