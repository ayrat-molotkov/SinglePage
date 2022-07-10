using SinglePage.Data.Models;

namespace SinglePage.Models;

public class VirtualServerViewModel
{
    public VirtualServerViewModel(List<VirtualServer> servers)
    {
        VirtualServers = servers;
    }
    public List<VirtualServer> VirtualServers { get; set; }
}
