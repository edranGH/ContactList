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
using ContactList.Droid.Models;
using Newtonsoft.Json;

namespace ContactList.Droid.Activities
{
    [Activity(Label = "@string/contact_edit_name")]
    public class ContactEditActivity : Activity
    {
        EditText nameEdit = null;
        EditText emailEdit = null;
        EditText phoneEdit = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ContactEdit);

            Button addButton = FindViewById<Button>(Resource.Id.btAddContact);
            addButton.Click += AddButton_Click;

            nameEdit = FindViewById<EditText>(Resource.Id.etName);
            emailEdit = FindViewById<EditText>(Resource.Id.etEmail);
            phoneEdit = FindViewById<EditText>(Resource.Id.etPhone);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Contact contact = new Contact(nameEdit.Text) { Email = emailEdit.Text, PhoneNumber = phoneEdit.Text };

            Intent intent = new Intent();
            intent.PutExtra(MainActivity.NewContactKey, JsonConvert.SerializeObject(contact));

            SetResult(Result.Ok, intent);
            Finish();
        }
    }
}