﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Not applicable for tests. Will be teared down after run.", Scope = "member", Target = "~M:PedaloWebApp.TestUtilities.Fakes.FakeAppDbContextFactory.#ctor(System.Guid)")]
