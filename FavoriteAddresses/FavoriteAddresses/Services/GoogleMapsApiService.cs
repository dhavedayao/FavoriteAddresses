﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FavoriteAddresses.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FavoriteAddresses.Services
{
  public class GoogleMapsApiService : IGoogleMapsApiService
  {
    static string _googleMapsKey;

    private const string ApiBaseAddress = "https://maps.googleapis.com/maps/";
    private HttpClient CreateClient()
    {
      var httpClient = new HttpClient
      {
        BaseAddress = new Uri(ApiBaseAddress)
      };

      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      return httpClient;
    }
    public static void Initialize(string googleMapsKey)
    {
      _googleMapsKey = googleMapsKey;
    }


    public async Task<GooglePlaceAutoCompleteResult> GetPlaces(string text)
    {
      GooglePlaceAutoCompleteResult results = null;

      using (var httpClient = CreateClient())
      {
        var response = await httpClient.GetAsync($"api/place/autocomplete/json?input={Uri.EscapeUriString(text)}&key={_googleMapsKey}").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
          var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
          {
            results = await Task.Run(() =>
               JsonConvert.DeserializeObject<GooglePlaceAutoCompleteResult>(json)
            ).ConfigureAwait(false);
          }
        }
      }

      return results;
    }

    public async Task<GooglePlace> GetPlaceDetails(string placeId)
    {
      GooglePlace result = null;
      using (var httpClient = CreateClient())
      {
        var response = await httpClient.GetAsync($"api/place/details/json?placeid={Uri.EscapeUriString(placeId)}&key={_googleMapsKey}").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
          var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
          {
            result = new GooglePlace(placeId, JObject.Parse(json));
          }
        }
      }

      return result;
    }
  }
}