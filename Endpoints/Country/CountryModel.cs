using abc.Infrastructure.Models;
using abd.Geo;

namespace abc.Endpoints.Country;

public class CountryModel : GeoCountryModel, IModel{
    public GeoCityModel[]? Cities {get;set;}
}