using OAuthServer.Core.UnitOfWork;

namespace OAuthServer.Data.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public Task<int> CommitAsync() => _context.SaveChangesAsync();
}