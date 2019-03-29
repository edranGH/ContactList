using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using ContactList.Droid.Models;
using System.Collections.Generic;
using ContactList.Droid.Adapters;
using ContactList.Droid.Activities;
using Android.Content;
using Newtonsoft.Json;

namespace ContactList.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private List<Contact> contacts;

        private static int ADD_CONTACT_REQUEST_CODE = 200;
        public const string NewContactKey = "NewContactKey";

        private ContactListAdapter adapter = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button btAdd = FindViewById<Button>(Resource.Id.btAdd);
            btAdd.Click += BtAdd_Click;

            PopulateContacts();
            ListView lvContact = FindViewById<ListView>(Resource.Id.lvContact);
            adapter = new ContactListAdapter(contacts, this);
            lvContact.Adapter = adapter;
            lvContact.ItemLongClick += LvContact_ItemLongClick;
        }

        private void LvContact_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Contact contact = contacts[e.Position];
            var alert = new Android.App.AlertDialog.Builder(this).Create();
            alert.SetTitle("Kasowanie kontaktu");
            alert.SetMessage(string.Format("Usunąć {0}", contact.Name));
            alert.SetButton("Tak", delegate 
                {
                    contacts.Remove(contact);
                    adapter.NotifyDataSetChanged();
                });
            alert.SetButton2("Nie", delegate { });
            alert.Show();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == ADD_CONTACT_REQUEST_CODE && data != null)
            {
                Contact contact = JsonConvert.DeserializeObject<Contact>(data.GetStringExtra(NewContactKey));
                contacts.Add(contact);
                adapter.NotifyDataSetChanged();
            }
        }

        private void BtAdd_Click(object sender, System.EventArgs e)
        {
            StartActivityForResult(typeof(ContactEditActivity), ADD_CONTACT_REQUEST_CODE);
        }

        private void PopulateContacts()
        {
            contacts = new List<Contact>();
            for (int i = 0; i < 15; i++)
            {
                contacts.Add(new Contact("Kontakt " + i.ToString()) { Email = "email_" + i.ToString() + "@.onet.pl", PhoneNumber = "123 123 12" + i.ToString() });
            }
        }
    }
}