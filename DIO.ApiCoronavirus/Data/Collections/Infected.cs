using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;
using System;

namespace DIO.ApiCoronavirus.Data.Collections
{
    public class Infected
    {
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public ObjectId Id { get; set; }
        public GeoJson2DGeographicCoordinates Localization { get; set; }

        public Infected(DateTime birthDate, string sex, double lat , double lon)
        {
            BirthDate = birthDate;
            Sex = sex;
            Localization = new GeoJson2DGeographicCoordinates(lon, lat);
        }   
        public Infected(string id,DateTime birthDate, string sex, double lat , double lon)
        {
            Id =  ObjectId.Parse( id);
            BirthDate = birthDate;
            Sex = sex;
            Localization = new GeoJson2DGeographicCoordinates(lon, lat);
        }
    }
}
