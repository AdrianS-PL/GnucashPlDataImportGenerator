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
    public static class LastPriceDates
    {
        public static async Task<List<LastPriceDateDto>> GetLastCommodityPricesDatesAsync(this DbSet<Price> dbSet)
        {
            return await dbSet.GetLastCommodityPricesDatesQuery().ToListAsync();
        }

        public static IQueryable<LastPriceDateDto> GetLastCommodityPricesDatesQuery(this DbSet<Price> dbSet)
        {
            return dbSet.GroupBy(q => new { q.CommodityInstance.mnemonic, q.CommodityInstance.Namespace },
            (r,t) => new LastPriceDateDto() {  Date = t.Max(u => u.date).Date, Mnemonic = r.mnemonic, Namespace = r.Namespace });
        }
    }
}
