using GnuCash.DataModel.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.DataModel.Queries
{
    public static class GetAccountByCode
    {
        public static async Task<Account> GetAccountByCodeAsync(this DbSet<Account> dbSet, string code)
        {
            return await dbSet.GetAccountByCodeQuery(code).SingleOrDefaultAsync();
        }

        public static IQueryable<Account> GetAccountByCodeQuery(this DbSet<Account> dbSet, string code)
        {
            return dbSet.Where(q => q.code == code);
        }
    }
}
