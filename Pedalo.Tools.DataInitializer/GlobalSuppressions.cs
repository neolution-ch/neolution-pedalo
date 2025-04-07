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
