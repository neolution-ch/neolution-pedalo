// Pedalo
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// Full assembly
[assembly: SuppressMessage(
    "Reliability",
    "CA2007:Consider calling ConfigureAwait on the awaited task",
    Justification = "In application code the synchronization context that an instruction was called with can generally be kept. So we suppress this rule to not have to clutter the code with unnecessary ConfigureAwait(true) calls.")]

// Startup
[assembly: SuppressMessage(
    "Major Code Smell",
    "S1200:Classes should not be coupled to too many other classes (Single Responsibility Principle)",
    Justification = "Startup is the composition root of the application and is by design coupled to a lot of classes.",
    Scope = "type",
    Target = "~T:Pedalo.UI.Api.Startup")]
[assembly: SuppressMessage(
    "Minor Code Smell",
    "S3242:Method parameters should be declared with base types",
    Justification = "The signature of the method is pre-defined by Microsoft. It will be called by a reflection-invoke of code we do not control, so we should not change it.",
    Scope = "member",
    Target = "~M:Pedalo.UI.Api.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IWebHostEnvironment)")]

// Middleware
[assembly: SuppressMessage(
    "Minor Code Smell",
    "S3242:Method parameters should be declared with base types",
    Justification = "In middleware, dependencies are injected inside the InvokeAsync() method as parameters. Using base types instead of the specialized types could lead to failed or wrong injections.",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.UI.Api.Middleware")]

// API Endpoints
[assembly: SuppressMessage(
    "Naming",
    "CA1725:Parameter names should match base declaration",
    Justification = "If the request model would only consist of one property, instead of creating a model using the property type and name directly is allowed",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.UI.Api.Endpoints")]
[assembly: SuppressMessage(
    "Critical Code Smell",
    "S927:parameter names should match base declaration and other partial definitions",
    Justification = "If the request model would only consist of one property, instead of creating a model using the property type and name directly is allowed",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.UI.Api.Endpoints")]
[assembly: SuppressMessage(
    "Naming",
    "CA1724:Name conflicts",
    Justification = "API endpoints will not be shipped in a library, they will only be consumed via HTTP",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.UI.Api.Endpoints")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1649:File name should match first type name",
    Justification = "API endpoint models use file nesting for developer convenience",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.UI.Api.Endpoints")]
