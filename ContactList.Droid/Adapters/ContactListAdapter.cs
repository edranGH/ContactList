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

namespace ContactList.Droid.Adapters
{
    public class ContactListAdapter : BaseAdapter<Contact>
    {
        private List<Contact> contacts;

        private Activity parent;

        public ContactListAdapter(List<Contact> contacts, Activity parent)
        {
            this.contacts = contacts;
            this.parent = parent;
        }

        public override Contact this[int position]
        {
            get => contacts[position];
        }

        public override int Count
        {
            get => contacts.Count;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //tego nie musi być
            ContactViewHolder viewHolder = null;

            if (convertView == null)
            {
                convertView = this.parent.LayoutInflater.Inflate(Resource.Layout.view_contact, null);

                viewHolder = new ContactViewHolder();
                viewHolder.NameTextView = convertView.FindViewById<TextView>(Resource.Id.tvName);
                viewHolder.PhoneTextView = convertView.FindViewById<TextView>(Resource.Id.tvPhone);
                viewHolder.EmailImageView = convertView.FindViewById<ImageView>(Resource.Id.ivEmail);
                viewHolder.ContactImageView = convertView.FindViewById<ImageView>(Resource.Id.ivPhone);

                viewHolder.EmailImageView.Click += EmailImageView_Click;
                viewHolder.ContactImageView.Click += ContactImageView_Click;

                convertView.Tag = viewHolder;
            }

            if (viewHolder == null)
            {
                viewHolder = convertView.Tag as ContactViewHolder;
            }

            Contact contact = contacts[position];

            viewHolder.NameTextView.Text = contact.Name;
            viewHolder.PhoneTextView.Text = contact.PhoneNumber;
            viewHolder.EmailImageView.Tag = position;
            viewHolder.ContactImageView.Tag = position;

            /*
            //Jak by nie było viewHoldera
            TextView name = convertView.FindViewById<TextView>(Resource.Id.tvName);
            name.Text = contact.Name;

            TextView phone = convertView.FindViewById<TextView>(Resource.Id.tvPhone);
            phone.Text = contact.PhoneNumber;
            */

            return convertView;
        }

        private void ContactImageView_Click(object sender, EventArgs e)
        {
            var contact = contacts[(int)(sender as ImageView).Tag];
            Intent intent = new Intent(Intent.ActionDial);
            intent.SetData(Android.Net.Uri.Parse(String.Format("tel:{0}", contact.PhoneNumber)));
            parent.StartActivity(intent);
        }

        private void EmailImageView_Click(object sender, EventArgs e)
        {
            var contact = contacts[(int)(sender as ImageView).Tag];
            Intent intent = new Intent(Intent.ActionSend);
            intent.SetType("plain/text");
            intent.PutExtra(Intent.ExtraEmail, contact.Email);
            parent.StartActivity(intent);
        }
    }
}