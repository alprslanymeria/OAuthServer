using OAuthServer.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Data.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
