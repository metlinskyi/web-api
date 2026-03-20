
using System.Text.Json.Serialization;

namespace Api.Middleware.Handlers;

public abstract class Happy : IHappyEndpoint
{
    public abstract Type RequestType { get; }
    public abstract Type ResponseType { get; }
    public abstract Type HandlerType { get; }
    public abstract Delegate HandlerDelegate { get; }
    public abstract JsonSerializerContext JsonSerializerContext { get; }
    public abstract class Endpoint<TRequest, TResponse> : Happy
    {
        public override Type RequestType => typeof(TRequest);
        public override Type ResponseType => typeof(TResponse);
        public abstract class WithHandler<THandler> : Endpoint<TRequest, TResponse>
            where THandler : IHappyHandler<TRequest, TResponse>
        {
            public override Type HandlerType => typeof(THandler);
            public Func<IHappyHandler<TRequest, TResponse>, TRequest, CancellationToken, Task<TResponse>> Handler  = (h, r, c) => h.Execute(r, c);
            public override Delegate HandlerDelegate => Handler;
            public abstract class WithValidation<TValidator> : WithHandler<THandler>
                where TValidator : IHappyValidator<TRequest>
            {
               
            }
        }
    }
}
