using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FavoriteAddresses.Models;
using SQLite;

namespace FavoriteAddresses.Data
{
  public class AppDatabase
  {
    public static readonly object Locker = new object();
    public SQLiteConnection Database;
    public AppDatabase()
    {
      Database = GetConnection();
      Database.CreateTable<GooglePlace>();
    }

    public void InsertGooglePlace(GooglePlace item)
    {
      lock (Locker)
      {
        if(GetGooglePlaceByPlaceId(item.PlaceId) == null)
          Database.Insert(item);
      }
    }

    public List<GooglePlace> GetGooglePlaces()
    {
      lock (Locker)
      {
        return Database.Table<GooglePlace>().ToList();
      }
    }

    public GooglePlace GetGooglePlaceByPlaceId(string placeId)
    {
      lock (Locker)
      {
        var place = Database.Table<GooglePlace>().FirstOrDefault(x => x.PlaceId == placeId);

        return place;
      }
    }

    public SQLiteConnection GetConnection()
    {
      var sqliteFilename = "favoriteaddress.Forms.db3";
      string databasePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
      var path = Path.Combine(databasePath, sqliteFilename);
      // Create the connection
      var conn = new SQLiteConnection(path);
      // Return the database connection
      return conn;
    }
  }
}