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

namespace ContactList.Droid.Models
{
    public class ContactViewHolder : Java.Lang.Object
    {
        public TextView NameTextView { get; set; }
        public TextView PhoneTextView { get; set; }
        public ImageView EmailImageView { get; set; }
        public ImageView ContactImageView { get; set; }
    }
}