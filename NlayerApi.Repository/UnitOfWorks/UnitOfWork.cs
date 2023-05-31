using NlayerApi.Core.UnitOfWork;
using NlayerApi.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlayerApi.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbcontext;

        public UnitOfWork(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Commit()
        {
            _dbcontext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }
    }
}
