namespace Api.Middleware.Handlers;
public interface IHappyValidator<T>
{
    public bool IsValid(T request, out IEnumerable<string> errors);
}