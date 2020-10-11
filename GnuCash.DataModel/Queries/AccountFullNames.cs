using GnuCash.DataModel.DatabaseModel;
using GnuCash.DataModel.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.DataModel.Queries
{
    public static class AccountFullNames
    {
        public static async Task<List<AccountFullNameDto>> GetAccountFullNamesAsync(this DbSet<Account> dbSet)
        {
            var accounts = await dbSet.ToListAsync();

            var result = new List<AccountFullNameDto>();

            foreach (var account in accounts)
            {
                var checkedAccount = account;

                List<string> treeNames = new List<string>();

                while(checkedAccount.Parent != null)
                {
                    treeNames.Add(checkedAccount.name);
                    checkedAccount = checkedAccount.Parent;
                }
                treeNames.Reverse();

                result.Add(new AccountFullNameDto
                {
                    Code = account.code,
                    Name = account.name,
                    FullName = string.Join(":", treeNames),
                    Guid = account.guid
                });

            }

            return result;
        }
    }
}
