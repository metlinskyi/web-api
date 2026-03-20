using Data.Access;

public interface IData
{
    /// <summary>
    /// Gets the data context for accessing repositories.
    /// </summary>
    DataContext Context { get; }
}