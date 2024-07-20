using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace xyLOGIX.Core.Common.Params.Interfaces
{
    /// <summary>
    /// Defines the publicly-exposed methods and properties of a POCO that
    /// carries information about a <see cref="T:System.Delegate" /> and its arguments
    /// (if any).
    /// </summary>
    public interface IActionParams
    {
        /// <summary>
        /// Gets or sets a reference to an instance of
        /// <see cref="T:System.Delegate" /> that is to be invoked.
        /// </summary>
        Delegate Action
        {
            [DebuggerStepThrough] get;
            [DebuggerStepThrough] set;
        }

        /// Gets or sets an array of objects to be passed to the
        /// <see cref="P:Core.Common.Params.ActionParams.Delegate" />
        /// property.
        IEnumerable<object> Arguments
        {
            [DebuggerStepThrough] get;
            [DebuggerStepThrough] set;
        }

        /// Invokes the
        /// <see cref="T:System.Delegate" />
        /// instance that is referenced by the
        /// <returns>
        /// Any data that was returned by the
        /// <see cref="P:xyLOGIX.Core.Common.Params.Interfaces.IActionParams.Action" />.
        /// </returns>
        object Invoke();
    }
}