using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FeedbackValidationPortal.TagHelpers;

[HtmlTargetElement("rating-stars")]
public class RatingStarsTagHelper : TagHelper
{
    public int Selected { get; set; }
    public string Name { get; set; } = "Rating";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.SetAttribute("class", "rating-stars");

        var stars = Enumerable.Range(1, 5)
            .Select(value =>
                $"<label style=\"font-size:1.4rem;margin-right:.35rem;\"><input type=\"radio\" name=\"{Name}\" value=\"{value}\" {(value == Selected ? "checked" : string.Empty)} />&#9733;</label>");

        output.Content.SetHtmlContent(string.Join(string.Empty, stars));
    }
}
