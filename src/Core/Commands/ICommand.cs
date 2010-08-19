namespace Core.Commands
{
    /// <summary>
    /// Encapsulates an action with a name.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets the name of the command which is used as an identifier, hence it must be unique.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();
    }
}