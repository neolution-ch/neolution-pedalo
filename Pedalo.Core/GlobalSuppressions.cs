// Pedalo
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// Entities
[assembly: SuppressMessage(
    "Performance",
    "CA1819:Properties should not return arrays",
    Justification = "There is no other alternative to store binary data with Entity Framework.",
    Scope = "namespaceanddescendants",
    Target = "Pedalo.Core.Domain.Entities")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.SpacingRules",
    "SA1011:Closing square brackets should be spaced correctly",
    Justification = "Field is nullable",
    Scope = "member",
    Target = "~P:Pedalo.Core.Domain.Entities.Document.FileData")]

// DbContext
[assembly: SuppressMessage(
    "Major Code Smell",
    "S1200:Classes should not be coupled to too many other classes (Single Responsibility Principle)",
    Justification = "High coupling is natural in DbContexts. And since we do not use Bounded Contexts, splitting the class is not feasible.",
    Scope = "type",
    Target = "~T:Pedalo.Core.Domain.AppDbContext")]

// Translations
[assembly: SuppressMessage(
    "Naming",
    "CA1707:Identifiers should not contain underscores",
    Justification = "Allowed for translations for better grouping.",
    Scope = "type",
    Target = "~T:Pedalo.Core.Application.Translations.TranslationCodeId")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1602:Enumeration items should be documented",
    Justification = "Allowed for translations for better overview.",
    Scope = "type",
    Target = "~T:Pedalo.Core.Application.Translations.TranslationCodeId")]
