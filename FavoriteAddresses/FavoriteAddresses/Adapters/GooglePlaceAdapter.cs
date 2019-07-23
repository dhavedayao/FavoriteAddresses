﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FavoriteAddresses.Models;

namespace FavoriteAddresses.Adapters
{
  public class GooglePlaceAdapter : BaseAdapter<GooglePlace>
  {
    public List<GooglePlace> placeList;
    private Context placeContext;

    public GooglePlaceAdapter(Context context, List<GooglePlace> list)
    {
      placeList = list;
      placeContext = context;
    }

    public override GooglePlace this[int position]
    {
      get
      {
        return placeList[position];
      }
    }

    public override int Count
    {
      get
      {
        return placeList.Count;
      }
    }

    public override long GetItemId(int position)
    {
      return position;
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      View row = convertView;
      try
      {
        if (row == null)
        {
          row = LayoutInflater.From(placeContext).Inflate(Resource.Layout.list_item, null, false);
        }
        TextView txtName = row.FindViewById<TextView>(Resource.Id.listItemLabel);
        txtName.Text = placeList[position].Name;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
      }
      finally { }
      return row;
    }
  }
}