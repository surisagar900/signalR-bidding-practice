using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SRnetcore.Hubs;
using SRnetcore.Persistence;
using SRnetcore.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBiddingService biddingService;
        private readonly IHubContext<BidHub> bidHub;

        public BidController(IBiddingService biddingService, IHubContext<BidHub> bidHub)
        {
            this.biddingService = biddingService;
            this.bidHub = bidHub;
        }

        //[HttpPost]
        //[ProducesResponseType(404)]
        //public async Task<ActionResult<int>> AddNewBid(Bid UserBid)
        //{
        //    try
        //    {
        //        var result = await biddingService.AddBid(UserBid);
        //        if(result)
        //        {
        //            await bidHub.Clients.All.SendAsync("BidsRecieved", UserBid.Amount);
        //        }
        //        return Ok(UserBid.Amount);
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }    
        //}

        //[HttpGet]
        //public async Task<ActionResult<List<Bid>>> GetAllBids()
        //{
        //    try
        //    {
        //        bool results = false;
        //        //var results = await biddingService.AllBids();
        //        if(results)
        //        {
        //            await bidHub.Clients.All.SendAsync("AllBidsRecieved", results);
        //        }
        //        return Ok(results);
        //    }
        //    catch(Exception)
        //    {
        //        return BadRequest();
        //        //throw;
        //    }
        //}
    }
}
