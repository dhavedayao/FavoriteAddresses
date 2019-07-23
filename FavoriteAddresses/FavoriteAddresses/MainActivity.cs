using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;
using FavoriteAddresses.Services;
using Android.Gms.Maps;
using FavoriteAddresses.Adapters;
using Android.Gms.Maps.Model;

namespace FavoriteAddresses
{
  [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
  public class MainActivity : AppCompatActivity, IOnMapReadyCallback
  {
    static int PICK_ADDRESS_REQUEST = 1;
    bool isMapVisible = false;
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      Xamarin.Essentials.Platform.Init(this, savedInstanceState);
      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.MainActivity);
      SetMapAPIKey();

      Shell.AppDatabase = new Data.AppDatabase();

      var btnSelectAddress = FindViewById<Button>(Resource.Id.btnSelectAddress);
      var mapLayout = FindViewById<FrameLayout>(Resource.Id.mapLayout);
      var btnSwitchView = FindViewById<Button>(Resource.Id.btnSwitchView);

      btnSelectAddress.Click += BtnSelectAddress_Click;
      btnSwitchView.Click += BtnSwitchView_Click;
      mapLayout.Visibility = Android.Views.ViewStates.Gone;
      ReloadAddresses();
    }

    private void BtnSwitchView_Click(object sender, EventArgs e)
    {
      var listSelectedAddresses = FindViewById<ListView>(Resource.Id.listSelectedAddresses);
      listSelectedAddresses.Visibility = isMapVisible ? Android.Views.ViewStates.Visible : Android.Views.ViewStates.Gone;

      var mapLayout = FindViewById<FrameLayout>(Resource.Id.mapLayout);
      mapLayout.Visibility = isMapVisible ? Android.Views.ViewStates.Gone : Android.Views.ViewStates.Visible;

      var btn = sender as Button;

      btn.Text = isMapVisible ? GetString(Resource.String.switch_to_map_view) : GetString(Resource.String.switch_to_list_view);

      isMapVisible = !isMapVisible;
    }

    private void BtnSelectAddress_Click(object sender, EventArgs e)
    {
      StartActivityForResult(typeof(SelectAddressActivity), PICK_ADDRESS_REQUEST);
    }

    protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
    {
      base.OnActivityResult(requestCode, resultCode, data);

      if(requestCode == PICK_ADDRESS_REQUEST)
      {
        ReloadAddresses();
      }
    }

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    {
      Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

      base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }

    public void OnMapReady(GoogleMap googleMap)
    {
      var googlePlaces = Shell.AppDatabase.GetGooglePlaces();

      if(googlePlaces?.Count > 0)
      {
        foreach(var place in googlePlaces)
        {
          var markerOption = new MarkerOptions();
          markerOption.SetPosition(new LatLng(place.Latitude, place.Longitude));
          markerOption.SetTitle(place.Name);
          googleMap.AddMarker(markerOption);
        }
      }
    }

    #region Helper Methods

    private void SetMapAPIKey()
    {
      var bundle = PackageManager.GetApplicationInfo(PackageName, Android.Content.PM.PackageInfoFlags.MetaData).MetaData;
      foreach (var key in bundle.KeySet())
      {
        if (key == "com.google.android.geo.API_KEY")
        {
          GoogleMapsApiService.Initialize(bundle.GetString(key));
        }
      }
    }
    private void ReloadAddresses()
    {
      var listSelectedAddresses = FindViewById<ListView>(Resource.Id.listSelectedAddresses);
      var adapter = new GooglePlaceAdapter(Application.Context, Shell.AppDatabase.GetGooglePlaces());
      listSelectedAddresses.Adapter = adapter;

      var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
      mapFragment.GetMapAsync(this);
    }

    #endregion
  }
}