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
using Newtonsoft.Json;

namespace FavoriteAddresses.Models
{
  public class GooglePlaceAutoCompletePrediction
  {
    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("place_id")]
    public string PlaceId { get; set; }

    [JsonProperty("reference")]
    public string Reference { get; set; }

    [JsonProperty("structured_formatting")]
    public StructuredFormatting StructuredFormatting { get; set; }

  }

  public class StructuredFormatting
  {
    [JsonProperty("main_text")]
    public string MainText { get; set; }

    [JsonProperty("secondary_text")]
    public string SecondaryText { get; set; }
  }

  public class GooglePlaceAutoCompleteResult
  {
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("predictions")]
    public List<GooglePlaceAutoCompletePrediction> AutoCompletePlaces { get; set; }
  }
}