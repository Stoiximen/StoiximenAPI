namespace Stoiximen.Contracts.Mediator
{
    public abstract class RequestBase<T> : IRequest<T>, IBaseRequest where T : ResponseBase
    {
    }
}
