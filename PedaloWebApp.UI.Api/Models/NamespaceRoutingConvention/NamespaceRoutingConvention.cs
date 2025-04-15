namespace PedaloWebApp.UI.Api.Models.NamespaceRoutingConvention
{
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    /// <summary>
    /// The namespace routing convention.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ApplicationModels.IControllerModelConvention" />
    public class NamespaceRoutingConvention : IControllerModelConvention
    {
        /// <summary>
        /// Called to apply the convention to the <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerModel" />.
        /// </summary>
        /// <param name="controller">The <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerModel" />.</param>
        /// <exception cref="System.ArgumentNullException">controller</exception>
        public void Apply(ControllerModel controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            var hasRouteAttributes = controller.Selectors.Any(selector =>
                                                    selector.AttributeRouteModel != null);
            if (hasRouteAttributes)
            {
                // This controller manually defined some routes, so treat this
                // as an override and not apply the convention here.
                return;
            }

            foreach (var selector in controller.Selectors)
            {
                selector.AttributeRouteModel = new AttributeRouteModel()
                {
                    Template = "/[baseroute]/[controller]",
                };
            }
        }
    }
}
