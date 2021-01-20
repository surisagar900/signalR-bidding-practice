using Microsoft.AspNetCore.SignalR;
using SRnetcore.Persistence;
using SRnetcore.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRnetcore.Hubs
{
    public class BidHub : Hub
    {
        private readonly string[] groups = { "Auction approved", "Auction delete", "SignalR Users" };
        private readonly string[] notificationEnabledUser = { "sagar", "suri", "surisagar" };
        static long increment;

        private readonly IBiddingService biddingService;

        public BidHub(IBiddingService biddingService)
        {
            this.biddingService = biddingService;
        }

        public override async Task OnConnectedAsync()
        {
            if((increment % 3) == 0)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "SEND");
                Console.WriteLine("==== Congrats ====   {0}   ====", Context.ConnectionId);
            }
            else
            {
                Console.WriteLine("==== OOPs ====   {0}   ====", Context.ConnectionId);
            }
            increment++;
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SEND");
            Console.WriteLine("==== BYE ====   {0}   ====", Context.ConnectionId);
            increment++;
            await base.OnDisconnectedAsync(exception);
        }

        public async Task BidRecieve(Bid results)
        {
            Bid result;
            Bid currentHighest = await biddingService.HighestBid();
            if(currentHighest == null)
            {
                result = await biddingService.AddBid(results);
            }
            else
            {
                if(results.Amount > currentHighest.Amount)
                {
                    result = await biddingService.AddBid(results);
                }
                else
                {
                    result = currentHighest;
                }
            }
            results = result;
            //await Clients.All.SendAsync("HighestBidSend", results);
            await Clients.Group("SEND").SendAsync("HighestBidSend", results);
        }

        public async Task<Bid> initialBidStatus()
        {
            return await biddingService.HighestBid();  
        }
    }
}
