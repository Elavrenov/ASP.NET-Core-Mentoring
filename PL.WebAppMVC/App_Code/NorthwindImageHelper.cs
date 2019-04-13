using System;
using System.Text.Encodings.Web;
using BLL.CoreEntities.Entities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PL.WebAppMVC
{
    public static class NorthwindImageHelper
    {
        public static HtmlString NorthwindImageLink(this IHtmlHelper html, Category category, int widht, int height)
        {
            TagBuilder a = new TagBuilder("a");
            a.Attributes.Add("href", $"Categories/Image/{category.CategoryId}");
            TagBuilder img = new TagBuilder("img");
            img.Attributes.Add("width", $"{widht}px");
            img.Attributes.Add("height", $"{height}px");
            img.Attributes.Add("src", $"data:image/jpeg;base64,{Convert.ToBase64String(category.Picture)}");
            a.InnerHtml.AppendHtml(img);

            var writer = new System.IO.StringWriter();
            a.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
