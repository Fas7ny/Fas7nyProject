namespace Fas7ny.Application.CQrs.InterfaceCommandQuery
{
    public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
