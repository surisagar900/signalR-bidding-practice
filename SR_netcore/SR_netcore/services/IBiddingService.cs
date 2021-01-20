using SRnetcore.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRnetcore.services
{
    public interface IBiddingService
    {
        Task<Bid> AddBid(Bid UserBid);
        Task<Bid> HighestBid();
    }
}
