using abd.Geo;
namespace abc.Endpoints.Country;
internal class CountryTask : ApiTask<CountryModel>
{
    private GeoRequestMessage[] requests;
    public CountryTask(string countryId)
    {
        requests = new GeoRequestMessage[]{
            new GeoRequestMessage<GeoCountryModel>
            {
                RequestUri = new Uri($"https://wft-geo-db.p.rapidapi.com/v1/geo/countries/{countryId}"),
            },
            new GeoRequestMessage<List<GeoCityModel>>
            {
                RequestUri = new Uri($"https://wft-geo-db.p.rapidapi.com/v1/geo/cities?types=CITY&countryIds={countryId}"),
            }
        };
    }

    protected override void Action()
    {
        Result = new CountryModel();

        var client = new HttpClient();
        var tasks = new List<Task>(requests.Count());
        foreach(var request in requests)
        {   
            var task = client.SendGeoRequestAsync(request, Result);
            tasks.Add(task);
            task.Wait();
        }

        // var tasks = requests
        //     .Select( async request => await client.SendGeoRequestAsync(request, Result))
        //     .ToArray();    
        // Task.WaitAll(tasks);

        var exceptions =  tasks.Where(x=> x.Exception != null).Select(x=> x.Exception).ToArray();
        if( exceptions.Count() > 0 )
            throw new AggregateException(exceptions.Select(x=>x.GetBaseException()));
    }
}