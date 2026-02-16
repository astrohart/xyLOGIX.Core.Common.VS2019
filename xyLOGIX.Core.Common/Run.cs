using Alphaleonis.Win32.Filesystem;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using xyLOGIX.Core.Common.Interfaces;
using xyLOGIX.Core.Debug;
using xyLOGIX.Core.Extensions;

namespace xyLOGIX.Core.Common
{
    /// <summary> Methods and properties to encapsulate the execution of actions. </summary>
    public class Run : ISystem
    {
        /// <summary>
        /// Array of valid executable file extension(s) that this class will recognize and
        /// execute.
        /// </summary>
        private static readonly string[] ValidExecutableExtensions =
        {
            ".exe", ".com", ".bat", ".cmd", ".pif"
        };

        /// Empty,
        /// <see langword="static" />
        /// constructor to prohibit direct allocation of this
        /// class.
        /// </summary>
        [Log(AttributeExclude = true)]
        static Run() { }

        /// Empty,
        /// <see langword="private" />
        /// constructor to prohibit direct allocation of this class.
        [Log(AttributeExclude = true)]
        private Run() { }

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
        public void Command(
            [NotLogged] string command,
            [NotLogged] string workingDirectory = "",
            bool useShell = true
        )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(command))
                {
                    Console.WriteLine(
                        "Run.Command: *** ERROR *** Null or blank value passed for the parameter, 'command'.  Stopping..."
                    );

                    return;
                }

                Console.WriteLine(
                    "Run.Command: *** SUCCESS *** The value of the required parameter, 'command', is not blank.  Continuing..."
                );

                using (var proc = new Process())
                {
                    var workingDirectoryToUse =
                        DetermineCurrentWorkingDirectory(workingDirectory);

                    if (useShell)
                    {
                        proc.StartInfo.FileName =
                            Environment.ExpandEnvironmentVariables("%COMSPEC%");
                        proc.StartInfo.Arguments = $"/C {command}";
                    }
                    else
                    {
                        SplitExeAndArgs(command, out var exe, out var args);
                        proc.StartInfo.FileName = exe;
                        proc.StartInfo.Arguments = args;
                    }

                    proc.StartInfo.WorkingDirectory = workingDirectoryToUse;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.UseShellExecute = false;

                    Console.WriteLine(
                        $"*** FYI *** Executing the specified command: {command}"
                    );

                    Console.WriteLine(
                        $@"Run.Command: {workingDirectoryToUse}\> {command}"
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        $@"Run.Command: {workingDirectoryToUse}\> {command}"
                    );

                    Console.WriteLine("Run.Command: [no output will be read]");

                    DebugUtils.WriteLine(
                        DebugLevel.Info, "Run.Command: [no output will be read]"
                    );

                    proc.Start();
                    proc.WaitForExit();

                    Console.WriteLine(
                        $"Run.Command: [process exited with code {proc.ExitCode}]"
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
        /// As this method is an iterator, it will not actually get called
        /// until it is enumerated, say, in a <see langword="foreach" />
        /// loop, for example.
        /// <para />
        /// Uses <c>cmd /C … 2&gt;&amp;1</c> so both streams arrive in order on
        /// <c>STDOUT</c>; no lambdas → no CS1621.
        /// </remarks>
        [return: NotLogged]
        public IEnumerable<string> CommandWithOutput(
            [NotLogged] string command,
            [NotLogged] string workingDirectory = "",
            bool useShell = true
        )
        {
            if (string.IsNullOrWhiteSpace(command)) yield break;

            var workDir = DetermineCurrentWorkingDirectory(workingDirectory);

            var psi = new ProcessStartInfo
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = workDir
            };

            if (useShell)
            {
                psi.FileName =
                    Environment.ExpandEnvironmentVariables("%COMSPEC%");
                psi.Arguments = $"/C {command} 2>&1";
            }
            else
            {
                SplitExeAndArgs(command, out var exe, out var args);
                psi.FileName = exe;
                psi.Arguments = $"{args}";
            }

            using (var proc = new Process())
            {
                Console.WriteLine();
                Console.WriteLine(
                    $@"{workDir}\> {psi.FileName} {psi.Arguments}"
                );
                Console.WriteLine();

                proc.StartInfo = psi;

                if (!useShell && !DoesFileExist(psi.FileName))
                {
                    Console.WriteLine(
                        $"ERROR: Could not locate the executable, '{psi.FileName}'.  Stopping..."
                    );
                    yield break;
                }

                proc.Start();

                string line;
                while ((line = proc.StandardOutput.ReadLine()) != null)
                {
                    yield return line;
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
                        "Run.DetermineCurrentWorkingDirectory: *** ERROR *** The parameter, 'folder', was either passed a null value, or it is blank. Stopping..."
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
                        $"Run.DetermineCurrentWorkingDirectory: Result = '{result}'"
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

        /// <summary>
        /// Determines whether the file having the specified <paramref name="pathnamne" />
        /// exists on the file system.
        /// </summary>
        /// <param name="pathnamne">
        /// (Required.) A <see cref="T:System.String" /> that contains the fully-qualified
        /// pathname of a file that is to be searched for.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if the file having the specified
        /// <paramref name="pathnamne" /> exists, <see langword="false" /> otherwwise.
        /// </returns>
        [Log(AttributeExclude = true)]
        private static bool DoesFileExist([NotLogged] string pathnamne)
        {
            var result = false;

            try
            {
                /*
                 * ASSUME that the specified pathname is that of
                 * a file.  We have to check string.IsNullOrWhiteSpace()
                 * separately, since otherwise, File.Exists will raise
                 * an exception if it is passed a null or blank value.
                 */

                if (string.IsNullOrWhiteSpace(pathnamne)) return result;

                result = File.Exists(pathnamne);
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = false;
            }

            DebugUtils.WriteLine(
                DebugLevel.Debug, $"Run.DoesFileExist: Result = {result}"
            );

            return result;
        }

        /// <summary>
        /// Attempts to resolve <paramref name="pathname" /> to a fully-qualified file
        /// on the current <c>PATH</c>.
        /// </summary>
        /// <remarks>
        /// * Accepts executables with any of the four legacy extensions
        /// (<c>.bat</c>, <c>.cmd</c>, <c>.com</c>, <c>.pif</c>) or <c>.exe</c>.
        /// * When no extension is supplied, <c>.exe</c> is assumed.
        /// * Returns the original <paramref name="pathname" /> if a match is not
        /// found.
        /// </remarks>
        /// <param name="pathname">
        /// Command name exactly as supplied by the caller (e.g. <c>git</c>,
        /// <c>git.exe</c>, <c>myTool.cmd</c>).
        /// </param>
        /// <returns>
        /// The resolved, fully-qualified path when the file is found; otherwise the
        /// original <paramref name="pathname" />.
        /// </returns>
        private static string ResolveExeOnPath(string pathname)
        {
            var result = pathname;

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.ResolveExeOnPath *** INFO: Checking whether the value of the parameter, 'pathname', is blank..."
                );

                // Check whether the value of the parameter, 'pathname', is blank.
                // If this is so, then emit an error message to the log file, and
                // then terminate the execution of this method.
                if (string.IsNullOrWhiteSpace(pathname))
                {
                    // The parameter, 'pathname' was either passed a null value, or it is blank.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "Run.ResolveExeOnPath: The parameter, 'pathname' was either passed a null value, or it is blank. Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"Run.ResolveExeOnPath: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** SUCCESS *** The parameter 'pathname' is not blank.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"Run.ResolveExeOnPath: Checking whether the pathname, '{pathname}', is NOT already fully-qualified..."
                );

                // Check to see whether the specified pathname is NOT already fully-qualified.
                // If this is not the case, then write an error message to the log file,
                // and then terminate the execution of this method.
                if (Path.IsPathRooted(pathname) && File.Exists(pathname))
                {
                    // The specified pathname is already fully-qualified.  There is nothing further to do.
                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        $"*** FYI *** The pathname, '{pathname}', is already fully-qualified.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** Run.ResolveExeOnPath: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"Run.ResolveExeOnPath: *** SUCCESS *** The pathname, '{pathname}', is NOT already fully-qualified.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"*** FYI *** Attempting to resolve the pathname, '{pathname}', to a fully-qualified file on the current PATH..."
                );

                var ext = Path.GetExtension(pathname);
                var hasAllowedExt = ValidExecutableExtensions.Contains(ext);

                var baseName = pathname.Trim('"');
                var searchName = hasAllowedExt ? baseName : baseName + ".exe";

                if (pathname.Contains(@"\") || pathname.Contains("/"))
                    return pathname; // already a path (relative or abs.)

                var pathParts = Environment.GetEnvironmentVariable("PATH")
                                           ?.Split(';') ??
                                Array.Empty<string>();

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.ResolveExeOnPath: Checking whether the variable, 'pathParts', has a null reference for a value..."
                );

                // Check to see if the variable, pathParts, is null.  If it is, send an error
                // to the log file and terminate the execution of this method, returning
                // the default return value.
                if (pathParts == null)
                {
                    // the variable pathParts is required to have a valid object reference.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "Run.ResolveExeOnPath: *** ERROR ***  The variable, 'pathParts', has a null reference.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** Run.ResolveExeOnPath: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                // We can use the variable, pathParts, because it's not set to a null reference.
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.ResolveExeOnPath: *** SUCCESS *** The variable, 'pathParts', has a valid object reference for its value.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.ResolveExeOnPath *** INFO: Checking whether the array, 'pathParts', has greater than zero elements..."
                );

                // Check whether the array, 'pathParts', has greater than zero elements.  If it is empty,
                // then write an error message to the log file, and then terminate the execution of this method.
                // It is preferred for the array to have greater than zero elements.
                if (pathParts.Length <= 0)
                {
                    // The array, 'pathParts', has zero elements, and we can't proceed if this is so.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "Run.ResolveExeOnPath *** ERROR *** The array, 'pathParts', has zero elements.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** Run.ResolveExeOnPath: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"Run.ResolveExeOnPath *** SUCCESS *** {pathParts.Length} element(s) were found in the 'pathParts' array.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"*** FYI *** Searching for the file, '{searchName}', in the directory(ies) listed on the PATH:"
                );

                foreach (var dir in pathParts)
                {
                    var candidate = Path.GetFullPath(
                        Path.Combine(dir.Trim('"'), searchName)
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        $"Run.ResolveExeOnPath *** INFO: Checking whether the file having pathname, '{candidate}', exists on the file system..."
                    );

                    // Check whether a file having pathname, 'candidate', exists on the file system.
                    // If it does not, then write an error message to the log file, and then skip
                    // to the next iteration of the loop.
                    if (!DoesFileExist(candidate))
                    {
                        DebugUtils.WriteLine(
                            DebugLevel.Error,
                            $"Run.ResolveExeOnPath: *** ERROR *** The system could not locate the file having pathname, '{candidate}', on the file system.  Skipping to the next loop iteration..."
                        );

                        // skip to the next loop iteration.
                        continue;
                    }

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        $"Run.ResolveExeOnPath: *** SUCCESS *** The file having pathname, '{candidate}', was found on the file system.  Proceeding..."
                    );

                    result = candidate;
                    break;
                }
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = pathname;
            }

            DebugUtils.WriteLine(
                DebugLevel.Debug, $"Run.ResolveExeOnPath: Result = '{result}'"
            );

            return result;
        }

        /// <summary>
        /// Splits the specified <paramref name="command" /> into an executable path and
        /// its argument(s).
        /// </summary>
        /// <param name="command">
        /// (Required.) A <see cref="T:System.String" /> containing the command to be
        /// executed.
        /// </param>
        /// <param name="exePath">
        /// (Required.) A <see cref="T:System.String" /> that receives the fully-qualified
        /// pathname of the executable.
        /// </param>
        /// <param name="arguments">
        /// A <see cref="T:System.String" /> that receives the
        /// argument(s) that are assumed to have been passed to the target executable.
        /// </param>
        private static void SplitExeAndArgs(
            string command,
            [NotLogged] out string exePath,
            [NotLogged] out string arguments
        )
        {
            try
            {
                exePath = arguments = string.Empty;

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.SplitExeAndArgs: Checking whether the value of the required method parameter, 'command' parameter is null or consists solely of whitespace..."
                );

                // Check whether the value of the parameter, 'command', is blank.
                // If this is so, then emit an error message to the log file, and
                // then terminate the execution of this method.
                if (string.IsNullOrWhiteSpace(command))
                {
                    // The parameter, 'command' was either passed a null value, or it is blank.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "Run.SplitExeAndArgs: *** ERROR *** Null or blank value passed for the parameter, 'command'.  Stopping..."
                    );

                    return;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.SplitExeAndArgs: *** SUCCESS *** The value of the required parameter, 'command', is not blank.  Continuing..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"*** FYI *** Attempting to split the command, '{command}', into an executable path and its argument(s)..."
                );

                // Tokenize: either "quoted string" or unquoted token
                var parts = Regex.Matches(command, @"[\""].+?[\""]|[^ ]+")
                                 .Cast<Match>()
                                 .Select(m => m.Value.Trim())
                                 .ToArray();

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.SplitExeAndArgs: Checking whether the variable 'parts' has a null reference for a value..."
                );

                // Check to see if the variable, parts, is null. If it is,
                // send an error to the log file and quit, returning from the method.
                if (parts == null)
                {
                    // the variable parts is required to have a valid object reference.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "Run.SplitExeAndArgs: *** ERROR ***  The 'parts' variable has a null reference.  Stopping..."
                    );

                    // stop.
                    return;
                }

                // We can use the variable, parts, because it's not set to a null reference.
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.SplitExeAndArgs: *** SUCCESS *** The 'parts' variable has a valid object reference for its value.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.SplitExeAndArgs *** INFO: Checking whether the array, 'parts', has greater than zero elements..."
                );

                // Check whether the array, 'parts', has greater than zero elements.  If it is empty,
                // then write an error message to the log file, and then terminate the execution of this method.
                // It is preferred for the array to have greater than zero elements.
                if (parts.Length <= 0)
                {
                    // The array, 'parts', has zero elements, and we can't proceed if this is so.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "Run.SplitExeAndArgs *** ERROR *** The array, 'parts', has zero elements.  Stopping..."
                    );

                    // stop.
                    return;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"Run.SplitExeAndArgs *** SUCCESS *** {parts.Length} element(s) were found in the 'parts' array.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Trying to parse the pathname of the `.exe` file from the command..."
                );

                // If the command starts with a quoted token, take that as the exe.
                var trimmedCommand = command.TrimStart();

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "Run.SplitExeAndArgs: Checking whether the variable, 'trimmedCommand', has a null reference for a value, or is blank..."
                );

                // Check to see if the required variable, 'trimmedCommand', is null or blank. If it is, 
                // then send an  error to the log file and then terminate the execution of this
                // method.
                if (string.IsNullOrWhiteSpace(trimmedCommand))
                {
                    // the variable trimmedCommand is required.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "Run.SplitExeAndArgs: *** ERROR *** The variable, 'trimmedCommand', has a null reference or is blank.  Stopping..."
                    );

                    // stop.
                    return;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"Run.SplitExeAndArgs: *** SUCCESS *** {trimmedCommand.Length} B of data appear to be present in the variable, 'trimmedCommand'.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Checking whether the 'trimmedCommand' variable starts with a double-quote character..."
                );

                if (trimmedCommand.StartsWith("\""))
                {
                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "*** FYI *** The 'trimmedCommand' variable starts with a double-quote character.  Parsing the quoted `.exe` path from it..."
                    );

                    // first token should be the quoted exe path
                    var exeCandidate = parts[0]
                                       .Trim()
                                       .Trim('"');

                    // Dump the variable exeCandidate to the log
                    DebugUtils.WriteLine(
                        DebugLevel.Debug, $"exeCandidate = '{exeCandidate}'"
                    );

                    exePath = ResolveExeOnPath(exeCandidate);

                    // Dump the variable exePath to the log
                    DebugUtils.WriteLine(
                        DebugLevel.Debug, $"exePath = '{exePath}'"
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "Run.SplitExeAndArgs: Checking whether the variable, 'exePath', has a null reference for a value, or is blank..."
                    );

                    // Check to see if the required variable, 'exePath', is null or blank. If it is, 
                    // then send an  error to the log file and then terminate the execution of this
                    // method.
                    if (string.IsNullOrWhiteSpace(exePath))
                    {
                        // the variable exePath is required.
                        DebugUtils.WriteLine(
                            DebugLevel.Error,
                            "Run.SplitExeAndArgs: *** ERROR *** The variable, 'exePath', has a null reference or is blank.  Stopping..."
                        );

                        // stop.
                        return;
                    }

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        $"Run.SplitExeAndArgs: *** SUCCESS *** {exePath.Length} B of data appear to be present in the variable, 'exePath'.  Proceeding..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "*** FYI *** Seeing if there were any command-line argument(s) passed to the `.exe`..."
                    );

                    arguments = parts.Length > 1
                        ? string.Join(" ", parts.Skip(1))
                        : string.Empty;

                    // Dump the variable arguments to the log
                    DebugUtils.WriteLine(
                        DebugLevel.Debug, $"arguments = '{arguments}'"
                    );

                    return;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** The pathname of the application does not appear to be quoted. Iteratively parsing it until we have the whole thing (in case the pathname has spaces)..."
                );

                // For unquoted input, try progressively longer prefixes of tokens as the exe path.
                // This handles cases like: C:\Program Files\Git\cmd\git.exe status --porcelain
                var found = false;

                for (var i = 0; i < parts.Length && !found; i++)
                {
                    var candidate = string.Join(" ", parts.Take(i + 1))
                                          .Trim();

                    // Dump the variable candidate to the log
                    DebugUtils.WriteLine(
                        DebugLevel.Debug, $"candidate = '{candidate}'"
                    );

                    var candidateTrim = candidate.Trim('"');

                    // Dump the variable candidateTrim to the log
                    DebugUtils.WriteLine(
                        DebugLevel.Debug, $"candidateTrim = '{candidateTrim}'"
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        $"*** FYI *** Attempting to resolve the candidate, '{candidateTrim}', to a fully-qualified file on the current PATH..."
                    );

                    // Try to resolve candidate (may return same string if unresolved)
                    var resolved = ResolveExeOnPath(candidateTrim);

                    // Dump the variable resolved to the log
                    DebugUtils.WriteLine(
                        DebugLevel.Debug, $"resolved = '{resolved}'"
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "*** INFO: Checking whether the variable, 'resolved', appears to have a null or blank value..."
                    );

                    // Check to see if the required variable, 'resolved', appears to have a null 
                    // or blank value. If it does, then send an error to the log file and then 
                    // skip to the next loop iteration.
                    if (string.IsNullOrWhiteSpace(resolved))
                    {
                        // The variable, 'resolved', appears to have a null or blank value.  This is not desirable.
                        DebugUtils.WriteLine(
                            DebugLevel.Error,
                            "*** ERROR: The variable, 'resolved', appears to have a null or blank value.  Skipping to the next candidate..."
                        );

                        // skip to the next iteration of this loop.
                        continue;
                    }

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "*** SUCCESS *** The variable, 'resolved', seems to have a non-blank value.  Proceeding..."
                    );

                    // If either the candidate (as given) exists or the resolved path exists, accept it.

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "*** Run.SplitExeAndArgs: Checking whether the candidate or resolved string(s) contain a pathname that exists..."
                    );

                    // Check to see whether the candidate or resolved string(s) contain a pathname that exists.
                    // If this is not the case, then write an error message to the log file,
                    // and then skip to the next iteration of the loop.
                    if (!DoesFileExist(candidateTrim) &&
                        !DoesFileExist(resolved))
                    {
                        // The candidate and resolved string(s) both do not provide sufficient information to find the application that is to be run.  This is not desirable.
                        DebugUtils.WriteLine(
                            DebugLevel.Error,
                            "*** ERROR *** The candidate and resolved string(s) both do not provide sufficient information to find the application that is to be run.  Skipping to the next candidate..."
                        );

                        // skip to the next loop iteration.
                        continue;
                    }

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "Run.SplitExeAndArgs: *** SUCCESS *** The candidate or resolved string(s) contain a pathname that exists.  Proceeding..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "*** FYI *** Attempting to use the candidate or resolved string(s) as the executable path..."
                    );

                    exePath = DoesFileExist(resolved)
                        ? resolved
                        : candidateTrim;

                    // Dump the variable exePath to the log
                    DebugUtils.WriteLine(
                        DebugLevel.Debug, $"exePath = '{exePath}'"
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "*** FYI *** Attempting to parse any command-line argument(s) passed to the `.exe`..."
                    );

                    arguments = parts.Length > i + 1
                        ? string.Join(" ", parts.Skip(i + 1))
                        : string.Empty;

                    // Dump the variable arguments to the log
                    DebugUtils.WriteLine(
                        DebugLevel.Debug, $"arguments = '{arguments}'"
                    );

                    found = true;
                    break;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** As a fallback, using the original simple logic (resolve first token) if nothing matched above..."
                );

                // Fallback: use original simple logic (resolve first token) if nothing matched above.
                if (found) return;

                exePath = parts.Length > 0
                    ? ResolveExeOnPath(
                        parts[0]
                            .Trim('"')
                    )
                    : string.Empty;


                arguments = parts.Length > 1
                    ? string.Join(" ", parts.Skip(1))
                    : string.Empty;
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                exePath = arguments = string.Empty;
            }

            // Dump the variable exePath to the log
            DebugUtils.WriteLine(DebugLevel.Debug, $"exePath = '{exePath}'");

            // Dump the variable arguments to the log
            DebugUtils.WriteLine(
                DebugLevel.Debug, $"arguments = '{arguments}'"
            );
        }
    }
}