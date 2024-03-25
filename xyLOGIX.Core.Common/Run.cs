using Alphaleonis.Win32.Filesystem;
using System;
using System.Diagnostics;
using xyLOGIX.Core.Common.Interfaces;

namespace xyLOGIX.Core.Common
{
    /// <summary> Methods and properties to encapsulate the execution of actions. </summary>
    public class Run : ISystem
    {
        /// <summary>
        /// Empty, static constructor to prohibit direct allocation of this
        /// class.
        /// </summary>
        static Run() { }

        /// Empty, protected constructor to prohibit direct allocation of this class.
        protected Run() { }

        /// Gets a reference to the one and only instance of
        /// <see cref="T:xyLOGIX.Core.Common.Run" />
        /// .
        public static ISystem System { [DebuggerStepThrough] get; } = new Run();

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
        /// <remarks>
        /// If this method is passed a <see langword="null" /> or blank value for
        /// the <paramref name="command" /> parameter, it does nothing.
        /// <para />
        /// By default, no console window is displayed when the command is executed. By
        /// default, this method waits to return until the launched command has completed
        /// execution.
        /// </remarks>
        public void Command(string command, string workingDirectory = "")
        {
            if (string.IsNullOrWhiteSpace(command))
                return; // nothing to run

            using var cmd = new Process();
            cmd.StartInfo.FileName =
                Environment.ExpandEnvironmentVariables("%COMSPEC%");
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.Arguments = $"/C {command}";
            cmd.StartInfo.WorkingDirectory =
                string.IsNullOrWhiteSpace(workingDirectory) ||
                !Directory.Exists(workingDirectory)
                    ? Directory.GetCurrentDirectory()
                    : workingDirectory;
            if (!cmd.Start()) return;
            cmd.WaitForExit();
        }
    }
}