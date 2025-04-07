namespace Pedalo.UI.Api.Models.CustomRouteToken
{
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    /// <summary>
    /// The custom route token.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ApplicationModels.IApplicationModelConvention" />
    public class CustomRouteToken : IApplicationModelConvention
    {
        /// <summary>
        /// The token regex
        /// </summary>
        private readonly string tokenRegex;

        /// <summary>
        /// The value generator
        /// </summary>
        private readonly Func<ControllerModel, string?> valueGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRouteToken"/> class.
        /// </summary>
        /// <param name="tokenName">Name of the token.</param>
        /// <param name="valueGenerator">The value generator.</param>
        public CustomRouteToken(string tokenName, Func<ControllerModel, string?> valueGenerator)
        {
            this.tokenRegex = $@"(\[{tokenName}])(?<!\[\1(?=]))";
            this.valueGenerator = valueGenerator;
        }

        /// <summary>
        /// Called to apply the convention to the <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModel" />.
        /// </summary>
        /// <param name="application">The <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModel" />.</param>
        public void Apply(ApplicationModel application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            foreach (var controller in application.Controllers)
            {
                string? tokenValue = this.valueGenerator(controller);
                this.UpdateSelectors(controller.Selectors, tokenValue);
                this.UpdateSelectors(controller.Actions.SelectMany(a => a.Selectors), tokenValue);
            }
        }

        /// <summary>
        /// Updates the selectors.
        /// </summary>
        /// <param name="selectors">The selectors.</param>
        /// <param name="tokenValue">The token value.</param>
        private void UpdateSelectors(IEnumerable<SelectorModel> selectors, string? tokenValue)
        {
            foreach (var selector in selectors.Where(s => s.AttributeRouteModel != null))
            {
                if (selector.AttributeRouteModel != null)
                {
                    selector.AttributeRouteModel.Template = this.InsertTokenValue(selector.AttributeRouteModel.Template, tokenValue);
                    selector.AttributeRouteModel.Name = this.InsertTokenValue(selector.AttributeRouteModel.Name, tokenValue);
                }
            }
        }

        /// <summary>
        /// Inserts the token value.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="tokenValue">The token value.</param>
        /// <returns>The new string</returns>
        private string? InsertTokenValue(string? template, string? tokenValue)
        {
            if (template is null || tokenValue is null)
            {
                return template;
            }

            return Regex.Replace(template, this.tokenRegex, tokenValue);
        }
    }
}
