// Pedalo
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// Migrations
[assembly: SuppressMessage(
    "Minor Code Smell",
    "S1192:String literals should not be duplicated",
    Justification = "Migrations are auto generated files",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.Infrastructure.Migrations")]

[assembly: SuppressMessage(
    "Naming",
    "CA1707:Identifiers should not contain underscores",
    Justification = "Migrations are auto generated files",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.Infrastructure.Migrations")]

[assembly: SuppressMessage(
    "Naming",
    "S138:Functions should not have too many lines of code",
    Justification = "Migrations are auto generated files",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.Infrastructure.Migrations")]

[assembly: SuppressMessage(
    "Naming",
    "S3254: Default parameter values should not be passed as arguments",
    Justification = "Migrations are auto generated files",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.Infrastructure.Migrations")]

[assembly: SuppressMessage(
    "Naming",
    "SA1413:Use trailing comma in multi-line initializers",
    Justification = "Migrations are auto generated files",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.Infrastructure.Migrations")]

[assembly: SuppressMessage(
    "Naming",
    "S3900: Arguments of public methods should be validated against null",
    Justification = "Migrations are auto generated files",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.Infrastructure.Migrations")]

[assembly: SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1601:Partial elements should be documented",
    Justification = "Migrations are auto generated files",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.Infrastructure.Migrations")]

[assembly: SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "Migrations are auto generated files",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.Infrastructure.Migrations")]
