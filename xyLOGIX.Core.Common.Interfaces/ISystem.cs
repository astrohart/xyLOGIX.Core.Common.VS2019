using PostSharp.Patterns.Diagnostics;
using System.Collections.Generic;

namespace xyLOGIX.Core.Common.Interfaces
{
    /// <summary>
    /// Defines the publicly-exposed methods and properties of system-command
    /// methods.
    /// </summary>
    public interface ISystem
    {
        /// <summary>
        /// Runs the specified system <paramref name="command" /> but does not
        /// return the output.
        /// </summary>
        /// <param name="command">
        /// (Required.) String containing the command to execute.
        /// May be anything you can type into the <c>cmd</c> prompt, and may have
        /// environment variables.
        /// </param>
        /// <param name="workingDirectory">
        /// (Required.) A <see cref="T:System.String" />
        /// containing a fully-qualified pathname of the working directory for running the
        /// command.
        /// </param>
        /// <remarks>
        /// If this method is passed a <see langword="null" /> or blank value for
        /// the <paramref name="command" /> parameter, it does nothing.
        /// <para />
        /// By default, no console window is displayed when the command is executed. By
        /// default, this method waits to return until the launched command has completed
        /// execution.
        /// </remarks>
        void Command(
            [NotLogged] string command,
            [NotLogged] string workingDirectory = ""
        );

        /// <summary>
        /// Executes a system <paramref name="command" /> and returns every line
        /// written to <c>STDOUT</c> and <c>STDERR</c>.
        /// </summary>
        /// <param name="command">
        /// (Required.) The command to run – anything you can type at <c>cmd</c>.
        /// Environment variables are allowed.
        /// </param>
        /// <param name="workingDirectory">
        /// Fully-qualified path to use as the working directory.
        /// Falls back to <c>Directory.GetCurrentDirectory()</c> if blank or invalid.
        /// </param>
        /// <returns>
        /// A read-only list of lines captured from the child process.
        /// </returns>
        [return: NotLogged]
        IReadOnlyList<string> CommandWithOutput(
            [NotLogged] string command,
            [NotLogged] string workingDirectory = ""
        );
    }
}