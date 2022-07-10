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

    public async Task<bool> DeleteServers(List<int> ids)
    {
        foreach (int id in ids)
        {
            var existServer = await _context.VirtualServers.FindAsync(id);

            if (existServer != null)
                _context.Remove(existServer);
        }

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        return true;
    }


    public async Task<TimeSpan?> CalculationWorkServers()
    {
        var allServers = await _context.VirtualServers.ToListAsync();

        if (allServers.Any(x => x.RemoveDateTime == null))
        {
            var longerWorkedServer = allServers.Where(x => x.RemoveDateTime == null).OrderBy(x => x.CreateDateTime).First();

            return (DateTime.Now - longerWorkedServer.CreateDateTime).Duration();
        }

        return null;
    }


    private readonly SinglePageContext _context;
}
