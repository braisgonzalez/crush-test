using MediatR;
using Users.Domain;

namespace Users.Application;

public static class Users
{
    public sealed record Dto(Guid Id, string Email, string Name, DateTime CreatedAt);

    public sealed record CreateUserCommand(string Email, string Name) : IRequest<Dto>;
    public sealed class CreateUserHandler(IUserRepository repo) : IRequestHandler<CreateUserCommand, Dto>
    {
        public async Task<Dto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await repo.GetByEmailAsync(request.Email, cancellationToken);
            if (existing is not null) throw new InvalidOperationException("Email already in use");
            var user = User.Create(request.Email, request.Name);
            await repo.AddAsync(user, cancellationToken);
            return new Dto(user.Id, user.Email, user.Name, user.CreatedAt);
        }
    }

    public sealed record RenameUserCommand(Guid Id, string Name) : IRequest<Dto>;
    public sealed class RenameUserHandler(IUserRepository repo) : IRequestHandler<RenameUserCommand, Dto>
    {
        public async Task<Dto> Handle(RenameUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repo.GetByIdAsync(request.Id, cancellationToken) ?? throw new KeyNotFoundException("User not found");
            user.Rename(request.Name);
            await repo.UpdateAsync(user, cancellationToken);
            return new Dto(user.Id, user.Email, user.Name, user.CreatedAt);
        }
    }

    public sealed record GetUserByIdQuery(Guid Id) : IRequest<Dto?>;
    public sealed class GetUserByIdHandler(IUserRepository repo) : IRequestHandler<GetUserByIdQuery, Dto?>
    {
        public async Task<Dto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await repo.GetByIdAsync(request.Id, cancellationToken);
            return user is null ? null : new Dto(user.Id, user.Email, user.Name, user.CreatedAt);
        }
    }

    public sealed record ListUsersQuery() : IRequest<IReadOnlyList<Dto>>;
    public sealed class ListUsersHandler(IUserRepository repo) : IRequestHandler<ListUsersQuery, IReadOnlyList<Dto>>
    {
        public async Task<IReadOnlyList<Dto>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await repo.ListAsync(cancellationToken);
            return list.Select(u => new Dto(u.Id, u.Email, u.Name, u.CreatedAt)).ToList();
        }
    }
}