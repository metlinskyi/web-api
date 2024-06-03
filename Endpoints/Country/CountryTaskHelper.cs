using abd.Geo;

namespace abc.Endpoints.Country;
internal static class CountryTaskHelper{
    public static async Task SendGeoRequestAsync(this HttpClient client, GeoRequestMessage request, CountryModel model)
    {   
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            //var data = await response.Content.ReadFromJsonAsync(request.ResponseType);
            var body = await response.Content.ReadAsStringAsync();
        }
    }
}