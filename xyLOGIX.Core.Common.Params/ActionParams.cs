using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using xyLOGIX.Core.Common.Params.Interfaces;

namespace xyLOGIX.Core.Common.Params
{
    /// <summary>
    /// POCO that tracks a <see cref="T:System.Delegate" /> and any of its
    /// arguments.
    /// </summary>
    [ExplicitlySynchronized]
    public class ActionParams : IActionParams
    {
        /// <summary>
        /// Initializes static data or performs actions that need to be performed once only
        /// for the <see cref="T:xyLOGIX.Core.Common.Params.ActionParams" /> class.
        /// </summary>
        /// <remarks>
        /// This constructor is called automatically prior to the first instance being
        /// created or before any static members are referenced.
        /// <para />
        /// We've decorated this constructor with the <c>[Log(AttributeExclude = true)]</c>
        /// attribute in order to simplify the logging output.
        /// </remarks>
        [Log(AttributeExclude = true)]
        static ActionParams() { }

        /// <summary>
        /// Constructs a new instance of
        /// <see cref="T:xyLOGIX.Core.Common.Params.ActionParams" /> and returns a
        /// reference to it.
        /// </summary>
        [Log(AttributeExclude = true)]
        public ActionParams()
        {
            Action = null;
            Arguments = Enumerable.Empty<object>();
        }

        /// <summary>
        /// Constructs a new instance of
        /// <see cref="T:xyLOGIX.Core.Common.Params.ActionParams" /> and returns a
        /// reference to it.
        /// </summary>
        /// <param name="action">
        /// (Required.) A <see cref="T:System.Delegate" /> that
        /// specifies the code that is to be executed.
        /// </param>
        /// <param name="args">
        /// (Optional.) Collection of arguments that is to be passed to
        /// the specified <paramref name="action" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Thrown if the required
        /// parameter, <paramref name="action" />, is passed a <see langword="null" />
        /// value.
        /// </exception>
        [Log(AttributeExclude = true)]
        public ActionParams(
            [NotLogged] Delegate action,
            [NotLogged] params object[] args
        )
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Arguments = !args.Any() ? Enumerable.Empty<object>() : args;
        }

        /// <summary>
        /// Gets or sets a reference to an instance of
        /// <see cref="T:System.Delegate" /> that is to be invoked.
        /// </summary>
        public Delegate Action
        {
            [DebuggerStepThrough] get;
            [DebuggerStepThrough] set;
        }

        /// <summary>
        /// Gets or sets an enumerable collection of objects to be passed to the
        /// <see cref="P:Delegate" /> property.
        /// </summary>
        public IEnumerable<object> Arguments
        {
            [DebuggerStepThrough] get;
            [DebuggerStepThrough] set;
        }

        /// Invokes the
        /// <see cref="T:System.Delegate" />
        /// instance that is referenced by the
        /// <returns>
        /// Any data that was returned by the
        /// <see cref="P:Core.Common.Params.Interfaces.IActionParams.Action" />.
        /// </returns>
        /// <remarks>
        /// If the value of the
        /// <see cref="P:Core.Common.Params.ActionParams.Action" /> property is set to a
        /// <see langword="null" /> reference, then this method does nothing and returns
        /// <see langword="null" />.
        /// </remarks>
        [return: NotLogged]
        public object Invoke()
        {
            if (Action == null)
                return null;
            return Arguments.Any()
                ? Action.DynamicInvoke(Arguments)
                : Action.DynamicInvoke();
        }
    }
}