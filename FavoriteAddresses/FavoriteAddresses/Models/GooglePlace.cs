using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;

namespace FavoriteAddresses.Models
{
  public class GooglePlace
  {
    public string PlaceId { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Raw { get; set; }

    public GooglePlace()
    {

    }

    public GooglePlace(string placeId, JObject jsonObject)
    {
      PlaceId = placeId;
      Name = (string)jsonObject["result"]["name"];
      Latitude = (double)jsonObject["result"]["geometry"]["location"]["lat"];
      Longitude = (double)jsonObject["result"]["geometry"]["location"]["lng"];
      Raw = jsonObject.ToString();
    }
  }
}