﻿namespace PedaloWebApp.UI.Api.Extensions
{
    using Neolution.Extensions.Configuration.GoogleSecrets;

    /// <summary>
    /// WebBuilder extensions for Google Secrets
    /// </summary>
    public static class GoogleSecretsExtensions
    {
        /// <summary>
        /// Gets the value of the environment variable of the google secrets project which decides where to load secrets from.
        /// </summary>
        /// <value>
        ///     The string with the project name
        /// </value>
        private static string GoogleSecretsProjectName => Environment.GetEnvironmentVariable("GOOGLE_SECRETS_PROJECT") ?? "default-project-goes-here";

        /// <summary>
        /// Gets a value indicating whether to load google secrets or not
        /// </summary>
        /// <value>
        ///     <c>true</c> to load google secrets; otherwise not.
        /// </value>
        private static bool LoadGoogleSecrets => bool.TryParse(Environment.GetEnvironmentVariable("LOAD_GOOGLE_SECRETS"), out var result) && result;

        /// <summary>
        /// Adds the Google secrets.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void AddGoogleSecrets(this WebApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // Configure app configuration to add Google Secrets if applicable
            if (LoadGoogleSecrets)
            {
                builder.Configuration.AddGoogleSecrets(options =>
                {
                    options.ProjectName = GoogleSecretsProjectName;
                });
            }
        }
    }
}
