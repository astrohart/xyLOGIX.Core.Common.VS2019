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
        /// Runs the specified system
        /// <paramref name="command" />
        /// but does not return the output.
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
        /// <param name="useShell">
        /// (Optional.) Set to <see langword="true" /> to use the Command Interpreter to
        /// execute the command; otherwise, <see langword="false" /> to directly execute
        /// the specified <paramref name="command" /> after splitting it on spaces,
        /// assuming that the first space-delimited token is the name of an executable
        /// file, and the rest of the token(s) are its argument(s).
        /// <para />
        /// The default value of this parameter is <see langword="true" />.
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
            [NotLogged] string workingDirectory = "",
            bool useShell = true
        );

        /// <summary>
        /// Runs an arbitrary <paramref name="command" /> and yields each line it
        /// writes to <c>STDOUT</c> or <c>STDERR</c> as soon as the line appears.
        /// </summary>
        /// <param name="command">
        /// (Required.) Exact command string as you would type in <c>cmd.exe</c>.
        /// Environment variables are allowed.
        /// </param>
        /// <param name="workingDirectory">
        /// Optional working directory.  Falls back to
        /// <see cref="P:System.Environment.CurrentDirectory" /> when blank or invalid.
        /// </param>
        /// <param name="useShell">
        /// (Optional.) Set to <see langword="true" /> to use the Command Interpreter to
        /// execute the command; otherwise, <see langword="false" /> to directly execute
        /// the specified <paramref name="command" /> after splitting it on spaces,
        /// assuming that the first space-delimited token is the name of an executable
        /// file, and the rest of the token(s) are its argument(s).
        /// <para />
        /// The default value of this parameter is <see langword="true" />.
        /// </param>
        /// <remarks>
        /// Uses <c>cmd /C … 2&gt;&amp;1</c> so both streams arrive in order on
        /// <c>STDOUT</c>; no lambdas → no CS1621.
        /// </remarks>
        [return: NotLogged]
        IEnumerable<string> CommandWithOutput(
            [NotLogged] string command,
            [NotLogged] string workingDirectory = "",
            bool useShell = true
        );
    }
}