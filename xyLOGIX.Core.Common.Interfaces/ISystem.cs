namespace Core.Common.Interfaces
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
        void Command(string command, string workingDirectory = "");
    }
}