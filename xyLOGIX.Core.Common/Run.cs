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
        protected Run()
        { }

        /// Gets a reference to the one and only instance of
        /// <see cref="T:xyLOGIX.Core.Common.Run" />
        /// .
        public static ISystem System { [DebuggerStepThrough] get; } = new Run();

        /// <summary>
        /// Gets a reference to an instance of an object that is to be used for thread
        /// synchronization purposes.
        /// </summary>
        private static object SyncRoot { get; } = new object();

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

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Allocating a new buffer to hold the output of the child process..."
                );

                var buffer = new List<string>();

                using (var proc = new Process())
                {
                    proc.StartInfo.FileName =
                        Environment.ExpandEnvironmentVariables("%COMSPEC%");
                    proc.StartInfo.Arguments = $"/C {command}";
                    proc.StartInfo.WorkingDirectory =
                        DetermineCurrentWorkingDirectory(workingDirectory);

                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;

                    proc.OutputDataReceived += (s, e) =>
                    {
                        if (e.Data == null) return;       // end-of-stream
                            lock (SyncRoot)
                            {
                                buffer.Add(e.Data);
                            }

                        DebugUtils.WriteLine(DebugLevel.Debug, e.Data);
                    };
                    proc.ErrorDataReceived += (s, e) =>
                    {
                        if (e.Data == null) return;        // end-of-stream
                        lock (SyncRoot)
                        {
                            buffer.Add(e.Data);
                        }

                        DebugUtils.WriteLine(DebugLevel.Debug, e.Data);
                    };

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "*** FYI *** Executing the specified command..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $@"{DetermineCurrentWorkingDirectory(workingDirectory)}\> {command}"
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "Run.CommandWithOutput: Attempting to spawn the child process..."
                    );

                    // Check whether a new process was started.  If this is not the case, then
                    // write an error message to the log file, and then terminate the execution
                    //  of this method, returning the default return value, which is the empty
                    // collection of strings.
                    if (!proc.Start())
                    {
                        // No new process was started.  This is not desirable.
                        DebugUtils.WriteLine(
                            DebugLevel.Error,
                            "Run.CommandWithOutput: No new process was started. Stopping..."
                        );

                        DebugUtils.WriteLine(
                            DebugLevel.Debug,
                            $"Run.CommandWithOutput: Result = '{result.ToSetString()}'"
                        );

                        return result;
                    }

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "Run.CommandWithOutput: *** SUCCESS *** The new process was started successfully.  Proceeding..."
                    );

                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    proc.WaitForExit();
                }

                result = buffer.ToArray();
            }
            catch (Exception ex)
            {
                DebugUtils.LogException(ex);

                result = Array.Empty<string>();
            }

            DebugUtils.WriteLine(
                result.Count > 0 ? DebugLevel.Info : DebugLevel.Error,
                result.Count > 0
                    ? $"*** SUCCESS *** {result.Count} line(s) of STDOUT and STDERR output were obtained from spawning the child process.  Proceeding..."
                    : "*** ERROR *** Zero line(s) of output were obtained from spawning the child process.  Stopping..."
            );

            return result;
        }

        /// <summary>
        /// Determines the current working directory, optionally using a specified
        /// <paramref name="folder" />.
        /// </summary>
        /// <remarks>
        /// This method logs informational and error messages during its
        /// execution.
        /// <para />
        /// If an exception occurs, it logs the exception details and defaults to returning
        /// the current directory.
        /// </remarks>
        /// <param name="folder">
        /// (Optional.) A <see cref="T:System.String" /> containing
        /// the fully-qualified pathname of a folder to use as the working directory for
        /// spawning a process.
        /// <para />
        /// If this parameter is <see langword="null" />, blank, or the
        /// <see cref="F:System.String.Empty" /> value, the method returns the current
        /// directory of the application.
        /// </param>
        /// <returns>
        /// The effective working directory path, with trailing backslashes
        /// removed. If <paramref name="folder" /> is null or empty, the current directory
        /// of the application is returned.
        /// </returns>
        private static string DetermineCurrentWorkingDirectory(
            [NotLogged] string folder = ""
        )
        {
            var result = Directory.GetCurrentDirectory()
                                  .RemoveTrailingBackslashes();

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.DetermineCurrentWorkingDirectory *** INFO: Checking whether the value of the parameter, 'folder', is blank..."
                );

                // Check whether the value of the parameter, 'folder', is blank.
                // If this is so, then emit an error message to the log file, and
                // then terminate the execution of this method.
                if (string.IsNullOrWhiteSpace(folder))
                {
                    // The parameter, 'folder' was either passed a null value, or it is blank.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "Run.DetermineCurrentWorkingDirectory: The parameter, 'folder', was either passed a null value, or it is blank. Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"Run.DetermineCurrentWorkingDirectory: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** SUCCESS *** The parameter 'folder', is not blank.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"Run.DetermineCurrentWorkingDirectory *** INFO: Checking whether the folder with path, '{folder}', exists on the file system..."
                );

                // Check whether a folder having the path, 'folder', exists on the file system.
                // If it does not, then write an error message to the log file, and then terminate
                // the execution of this method, returning the default return value.
                if (!Directory.Exists(folder))
                {
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        $"Run.DetermineCurrentWorkingDirectory: *** ERROR *** The system could not locate the folder having the path, '{folder}', on the file system.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** Run.DetermineCurrentWorkingDirectory: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"Run.DetermineCurrentWorkingDirectory: *** SUCCESS *** The folder with path, '{folder}', was found on the file system.  Proceeding..."
                );

                result = folder.RemoveTrailingBackslashes();
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = Directory.GetCurrentDirectory()
                                  .RemoveTrailingBackslashes();
            }

            DebugUtils.WriteLine(
                DebugLevel.Debug,
                $"Run.DetermineCurrentWorkingDirectory: Result = '{result}'"
            );

            return result;
        }
    }
}