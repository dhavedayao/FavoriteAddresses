using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FavoriteAddresses.Models;

namespace FavoriteAddresses.Services
{
  public interface IGoogleMapsApiService
  {
    Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
    Task<GooglePlace> GetPlaceDetails(string placeId);
  }
}