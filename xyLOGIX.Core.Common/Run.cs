using Alphaleonis.Win32.Filesystem;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using xyLOGIX.Core.Common.Interfaces;
using xyLOGIX.Core.Debug;
using xyLOGIX.Core.Extensions;

namespace xyLOGIX.Core.Common
{
    /// <summary> Methods and properties to encapsulate the execution of actions. </summary>
    public class Run : ISystem
    {
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
        public void Command(
            [NotLogged] string command,
            [NotLogged] string workingDirectory = ""
        )
        {
            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.Command: Checking whether the value of the required method parameter, 'command' parameter is null or consists solely of whitespace..."
                );

                if (string.IsNullOrWhiteSpace(command))
                {
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "Run.Command: *** ERROR *** Null or blank value passed for the parameter, 'command'.  Stopping..."
                    );

                    return;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.Command: *** SUCCESS *** The value of the required parameter, 'command', is not blank.  Continuing..."
                );

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

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        $"*** FYI *** Executing the specified command: {command}"
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $@"Run.Command: {cmd.StartInfo.WorkingDirectory}\> {command}"
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        "Run.Command: [no output will be read]"
                    );

                    cmd.Start();
                    cmd.WaitForExit();

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"Run.Command: [process exited with code {cmd.ExitCode}]"
                    );
                }
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
            }
        }

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
        /// <remarks>
        /// Uses <c>cmd /C … 2&gt;&amp;1</c> so both streams arrive in order on
        /// <c>STDOUT</c>; no lambdas → no CS1621.
        /// </remarks>
        [return: NotLogged]
        public IEnumerable<string> CommandWithOutput(
            [NotLogged] string command,
            [NotLogged] string workingDirectory = ""
        )
        {
            if (string.IsNullOrWhiteSpace(command)) yield break;

            using (var proc = new Process())
            {
                proc.StartInfo.FileName =
                    Environment.ExpandEnvironmentVariables("%COMSPEC%");
                proc.StartInfo.Arguments = $"/C {command} 2>&1";
                proc.StartInfo.WorkingDirectory =
                    DetermineCurrentWorkingDirectory(workingDirectory);

                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.RedirectStandardOutput = true;

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Executing the specified command..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Debug,
                    $@"{DetermineCurrentWorkingDirectory(workingDirectory)}\> {command}"
                );

                proc.Start();

                string line;
                while ((line = proc.StandardOutput.ReadLine()) != null)
                {
                    yield return line;

                    DebugUtils.WriteLine(DebugLevel.Debug, line);
                }

                proc.WaitForExit();
            }
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