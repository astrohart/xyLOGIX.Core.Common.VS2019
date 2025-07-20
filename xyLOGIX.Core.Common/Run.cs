using Alphaleonis.Win32.Filesystem;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using xyLOGIX.Core.Common.Interfaces;
using xyLOGIX.Core.Debug;
using xyLOGIX.Core.Extensions;

namespace xyLOGIX.Core.Common
{
    /// <summary> Methods and properties to encapsulate the execution of actions. </summary>
    public class Run : ISystem
    {
        /// <summary>
        /// Empty, static constructor to prohibit direct allocation of this
        /// class.
        /// </summary>
        [Log(AttributeExclude = true)]
        static Run() { }

        /// Empty, protected constructor to prohibit direct allocation of this class.
        [Log(AttributeExclude = true)]
        protected Run() { }

        /// <summary>
        /// Gets a reference to an instance of an object that is to be used for thread
        /// synchronization purposes.
        /// </summary>
        private static object SyncRoot { get; } = new object();

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

            using (var cmd = new Process())
            {
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
        public IReadOnlyList<string> CommandWithOutput(
            string command,
            string workingDirectory = ""
        )
        {
            IReadOnlyList<string> result = Enumerable.Empty<string>()
                .ToList();

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.CommandWithOutput *** INFO: Checking whether the value of the parameter, 'command', is blank..."
                );

                // Check whether the value of the parameter, 'command', is blank.
                // If this is so, then emit an error message to the log file, and
                // then terminate the execution of this method.
                if (string.IsNullOrWhiteSpace(command))
                {
                    // The parameter, 'command' was either passed a null value, or it is blank.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "Run.CommandWithOutput: The parameter, 'command', was either passed a null value, or it is blank. Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"Run.CommandWithOutput: Result = '{result.ToSetString()}'"
                    );

                    // stop.
                    return result;
                }

                // Dump the argument of the parameter, command, to the log
                DebugUtils.WriteLine(
                    DebugLevel.Debug,
                    $@"{workingDirectory.RemoveTrailingBackslashes() ?? Directory.GetCurrentDirectory().RemoveTrailingBackslashes()}\> {command}"
                );

                var buffer = new List<string>();

                using (var proc = new Process())
                {
                    proc.StartInfo.FileName =
                        Environment.ExpandEnvironmentVariables("%COMSPEC%");
                    proc.StartInfo.Arguments = $"/C {command}";
                    proc.StartInfo.WorkingDirectory =
                        string.IsNullOrWhiteSpace(workingDirectory) ||
                        !Directory.Exists(workingDirectory)
                            ? Directory.GetCurrentDirectory()
                            : workingDirectory;

                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;

                    proc.OutputDataReceived += (s, e) =>
                    {
                        if (string.IsNullOrWhiteSpace(e.Data))
                            lock (SyncRoot)
                            {
                                buffer.Add(e.Data);
                            }

                        DebugUtils.WriteLine(e.Data);
                    };
                    proc.ErrorDataReceived += (s, e) =>
                    {
                        if (e.Data == null) return;
                        lock (SyncRoot)
                        {
                            buffer.Add(e.Data);
                        }

                        DebugUtils.WriteLine(e.Data);
                    };

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        $"*** FYI *** Executing: {proc.StartInfo.FileName} {proc.StartInfo.Arguments}"
                    );

                    if (!proc.Start()) return result;

                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    proc.WaitForExit();
                }

                result = buffer.AsReadOnly();
            }
            catch (Exception ex)
            {
                DebugUtils.LogException(ex);
                result = Enumerable.Empty<string>()
                                   .ToList();
            }

            return result;
        }
    }
}