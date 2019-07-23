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
using FavoriteAddresses.Adapters;
using FavoriteAddresses.Models;
using FavoriteAddresses.Services;

namespace FavoriteAddresses
{
  [Activity(Label = "SelectAddressActivity")]
  public class SelectAddressActivity : Activity
  {
    IGoogleMapsApiService googleMapsApi = new GoogleMapsApiService();
    GooglePlaceAutoCompletePredictionAdapter Adapter;
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.SelectAddressActivity);

      var txtSearch = FindViewById<EditText>(Resource.Id.txtSearch);

      txtSearch.TextChanged += TxtSearch_TextChanged;
      var listSearch = FindViewById<ListView>(Resource.Id.listSearch);
      listSearch.ItemClick += async (o, ea) =>
      {
        var placeAutoSearch = Adapter[ea.Position];

        var googlePlace = await googleMapsApi.GetPlaceDetails(placeAutoSearch.PlaceId);

        Shell.AppDatabase.InsertGooglePlace(googlePlace);

        SetResult(Result.Ok);
        Finish();
      };
    }

    private async void TxtSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
    {
      var places = await googleMapsApi.GetPlaces(e.Text.ToString());

      var listSearch = FindViewById<ListView>(Resource.Id.listSearch);

      Adapter = new GooglePlaceAutoCompletePredictionAdapter(Application.Context, places.AutoCompletePlaces);

      listSearch.Adapter = Adapter;
    }
  }
}