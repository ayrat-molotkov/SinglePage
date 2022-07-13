using Microsoft.EntityFrameworkCore;
using SinglePage.Data;
using SinglePage.Data.Models;

namespace SinglePage.Service;

public class VirtualServerService
{
    public VirtualServerService(SinglePageContext context)
    {
        _context = context;
    }


    public async Task<List<VirtualServer>> GetAllServers()
    {
        var allServers = await _context.VirtualServers.ToListAsync();

        return allServers;
    }


    public async Task<VirtualServer> AddNewServer()
    {
        var newVirtualServer = new VirtualServer()
        {
            CreateDateTime = DateTime.Now
        };

        await _context.VirtualServers.AddAsync(newVirtualServer);

        await _context.SaveChangesAsync();

        return newVirtualServer;
    }

    public async Task<List<VirtualServer>> DeleteServers(List<int> ids)
    {
        foreach (int id in ids)
        {
            var existServer = await _context.VirtualServers.FindAsync(id);

            existServer.RemoveDateTime = DateTime.Now;

            _context.VirtualServers.Update(existServer);
        }

        await _context.SaveChangesAsync();

        var allServers = await _context.VirtualServers.ToListAsync();

        return allServers;
    }


    public async Task<TimeSpan?> CalculationWorkServers()
    {
        var allServers = await _context.VirtualServers.ToListAsync();        

        TimeSpan result = new TimeSpan();

        List<int> calculatedServerIds = new List<int>();

        foreach (var item in allServers.GroupBy(x => x.RemoveDateTime))
        {
            if (item.Select(x => x.VirtualServerId).Intersect(calculatedServerIds).Any())
            {
                continue;
            }

            if (item.Key != null)
            {
                var serverEndInKeyTime = allServers.Where(x => x.RemoveDateTime == item.Key).OrderBy(x => x.CreateDateTime).First();
                var otherWorkedServers = allServers.Where(x => x.CreateDateTime > serverEndInKeyTime.CreateDateTime && x.CreateDateTime < serverEndInKeyTime.RemoveDateTime.Value && !calculatedServerIds.Contains(x.VirtualServerId));
                if (otherWorkedServers.Any())
                {
                    result += otherWorkedServers.OrderByDescending(x => x.RemoveDateTime).First().RemoveDateTime.Value - item.OrderBy(x => x.CreateDateTime).First().CreateDateTime;
                    calculatedServerIds.AddRange(otherWorkedServers.Select(x => x.VirtualServerId));
                }                    
                else
                {
                    result += item.Key.Value - item.OrderBy(x => x.CreateDateTime).First().CreateDateTime;
                }                    
            }                
            else
                result += DateTime.Now - item.OrderBy(x => x.CreateDateTime).First().CreateDateTime;

            calculatedServerIds.AddRange(item.Select(x => x.VirtualServerId));
        }

        return result;
    }


    private readonly SinglePageContext _context;
}
