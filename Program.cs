using ChargeBee.Api;
using ChargeBee.Models;
using Newtonsoft.Json;



class Program
{
    static async Task Main(string[] args)
    {
        ApiConfig.Configure("test-site", "test_api_key");
        await Pagination();
    }
    static async Task<List<Event>> Pagination()
    {
        var events = new List<Event>();

        const int pageSize = 100;
        string offset = null;

        do
        {
            var result = await Event
                .List()
                .Offset(offset)
                .Limit(pageSize)
                .RequestAsync();

            offset = result.NextOffset;

            events.AddRange(result.List.Select(x => x.Event));
        } while (offset != null);

        return events;
    }
}