using Microsoft.EntityFrameworkCore;
using SRnetcore.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRnetcore.services
{
    public class BiddingService : IBiddingService
    {
        private readonly MainDbContext db;

        public BiddingService(MainDbContext db)
        {
            this.db = db;
        }

        public async Task<Bid> AddBid(Bid UserBid)
        {
            try
            {
                await db.Bids.AddAsync(UserBid);
                return (await db.SaveChangesAsync() > 0) ? UserBid : null;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<Bid> HighestBid()
        {
            var result = await db.Bids.OrderByDescending(t => t.Amount).FirstOrDefaultAsync();
            return result;
        }
    }
}
