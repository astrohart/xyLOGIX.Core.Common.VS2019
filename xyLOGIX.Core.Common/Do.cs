using PostSharp.Patterns.Diagnostics;
using System;
using System.Threading;
using xyLOGIX.Core.Common.Params.Factories;
using xyLOGIX.Core.Common.Params.Interfaces;

namespace xyLOGIX.Core.Common
{
    /// <summary> Exposes static methods to perform actions. </summary>
    public static class Do
    {
        /// <summary>
        /// Executes the specified <paramref name="action" /> in a separate
        /// worker thread until the <paramref name="action" /> succeeds.
        /// </summary>
        /// <param name="action">
        /// (Required.) A <see cref="T:System.Delegate" /> that
        /// points to the code that should be executed.
        /// </param>
        /// <param name="args">(Optional.) Arguments to be passed to the executed code.</param>
        public static void UntilSucceeds(
            [NotLogged] Delegate action,
            [NotLogged] params object[] args
        )
        {
            if (action == null) return; // do nothing with null input
            var t = new Thread(UntilSucceedsThread);
            t.Start(
                MakeNewActionParams.For(action)
                                   .WithArguments(args)
            );
        }

        /// <summary>
        /// Casts the specified <paramref name="arg" /> to an instance of an object that
        /// implements the <see cref="T:Core.Common.Params.Interfaces.IActionParams" />
        /// interface, that is, if the cast is possible; otherwise, <see langword="null" />
        /// is returned.
        /// <param name="arg">
        /// (Required.) Reference to the instance of the object that is
        /// to be cast.
        /// </param>
        /// <returns>
        /// Reference to an instance of an object that implements the
        /// <see cref="T:Core.Common.Params.Interfaces.IActionParams" /> interface, unless
        /// the object refers to data not of that type; in which case,
        /// <see langword="null" /> is returned.
        /// </returns>
        /// </summary>
        private static IActionParams ToActionParams([NotLogged] this object arg)
        {
            IActionParams result = default;
            if (arg == null)
                return result;
            if (arg is IActionParams actionParams)
                result = actionParams;
            return result;
        }

        /// Thread to execute the specified code until it stops throwing exceptions. (Required.) Reference to an instance of an object that interface that contains the metadata on the code to be executed.
        private static void UntilSucceedsThread([NotLogged] object arg)
        {
            var actionParams = arg.ToActionParams();
            if (actionParams?.Action == null) return;
            var succeeded = false;
            while (!succeeded)
            {
                Thread.Sleep(500);
                try
                {
                    actionParams.DynamicInvoke();
                    succeeded = true;
                }
                catch
                {
                    succeeded = false;
                }
            }
        }
    }
}