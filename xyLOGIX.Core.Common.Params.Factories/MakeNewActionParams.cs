using PostSharp.Patterns.Diagnostics;
using System;
using System.Diagnostics;
using System.Linq;
using xyLOGIX.Core.Common.Params.Interfaces;

namespace xyLOGIX.Core.Common.Params.Factories
{
    /// <summary>
    /// Creates new instance(s) of object(s) that implement the
    /// <see cref="T:Core.Common.Params.Interfaces.IActionParams" /> interface, and
    /// returns references to them.
    /// </summary>
    public static class MakeNewActionParams
    {
        /// <summary>
        /// Initializes static data or performs actions that need to be performed once only
        /// for the
        /// <see cref="T:xyLOGIX.Core.Common.Params.Factories.MakeNewActionParams" />
        /// class.
        /// </summary>
        /// <remarks>
        /// This constructor is called automatically prior to the first instance being
        /// created or before any static members are referenced.
        /// <para />
        /// We've decorated this constructor with the <c>[Log(AttributeExclude = true)]</c>
        /// attribute in order to simplify the logging output.
        /// </remarks>
        [Log(AttributeExclude = true)]
        static MakeNewActionParams() { }

        /// <summary>
        /// Dynamically invokes (late-bound) the method represented by the
        /// current delegate.
        /// </summary>
        /// <param name="self">
        /// (Required.) Reference to an instance of an object that
        /// implements the <see cref="T:Core.Common.Params.Interfaces.IActionParams" />
        /// interface.
        /// </param>
        /// <returns>
        /// <see langword="null" /> if the <paramref name="self" /> parameter or
        /// the <see cref="P:Core.Common.Params.Interfaces.IActionParams.Action" />
        /// property property of the <paramref name="self" /> parameter is set to a
        /// <see langword="null" /> reference, or if the
        /// <see cref="P:Core.Common.Params.Interfaces.IActionParams.Action" /> returns
        /// <c>void</c>; otherwise, a reference to the data returned by the code specified
        /// by the <see cref="P:Core.Common.Params.Interfaces.IActionParams.Action" />
        /// property.
        /// </returns>
        [DebuggerStepThrough]
        [return: NotLogged]
        public static object DynamicInvoke([NotLogged] this IActionParams self)
        {
            if (self?.Action == null) return null;
            return self.Arguments.Any()
                ? self.Action.DynamicInvoke()
                : self.Action.DynamicInvoke(self.Arguments);
        }

        /// Builder extension method that initializes the
        /// <see cref="P:Core.Common.Params.Interfaces.IActionParams.Delegate" />
        /// <param name="action">
        /// (Required.) Reference to an instance of
        /// <see cref="T:System.Delegate" /> that should be invoked. Reference to the same
        /// instance of the object that called this method, for fluent use.
        /// </param>
        [DebuggerStepThrough]
        [return: NotLogged]
        public static IActionParams For([NotLogged] Delegate action)
            => new ActionParams(action);

        /// <summary>
        /// Initializes the value of the
        /// <see cref="P:Core.Common.Params.Interfaces.IActionParams.Args" /> property.
        /// </summary>
        /// <param name="self">
        /// (Required.) Reference to an instance of an object that
        /// implements the <see cref="T:Core.Common.Params.Interfaces.IActionParams" />
        /// interface.
        /// </param>
        /// <param name="arguments">
        /// (Required.) Array of references to objects that should
        /// be passed when the delegate is invoked.
        /// </param>
        /// <remarks>
        /// If no <paramref name="arguments" /> are passed to this method, then
        /// the method does nothing.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">
        /// Thrown if the required
        /// parameter, <paramref name="self" />, is passed a <see langword="null" /> value.
        /// </exception>
        [DebuggerStepThrough]
        [return: NotLogged]
        public static IActionParams WithArguments(
            [NotLogged] this IActionParams self,
            [NotLogged] object[] arguments
        )
        {
            if (self == null) throw new ArgumentNullException(nameof(self));
            if (!arguments.Any()) return self;
            self.Arguments = arguments;
            return self;
        }
    }
}