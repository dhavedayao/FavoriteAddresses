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
using FavoriteAddresses.Data;

namespace FavoriteAddresses
{
  public class Shell
  {
    public static AppDatabase AppDatabase { get; set; }
  }
}