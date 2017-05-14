using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MoneyKeeper.Mobile.Android.DataAccess;

namespace MoneyKeeper.Mobile.Android.Adapters
{
    public class FinOperationsAdapter : ArrayAdapter<FinOperation>
    {
        private readonly IList<FinOperation> list;
        private readonly LayoutInflater inflater;

        public FinOperationsAdapter(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public FinOperationsAdapter(Context context, int textViewResourceId) : base(context, textViewResourceId)
        {
        }

        public FinOperationsAdapter(Context context, int resource, int textViewResourceId) : base(context, resource, textViewResourceId)
        {
        }

        public FinOperationsAdapter(Context context, int textViewResourceId, FinOperation[] objects) : base(context, textViewResourceId, objects)
        {
        }

        public FinOperationsAdapter(Context context, int resource, int textViewResourceId, FinOperation[] objects) : base(context, resource, textViewResourceId, objects)
        {
        }

        public FinOperationsAdapter(Context context, int textViewResourceId, IList<FinOperation> objects) : base(context, textViewResourceId, objects)
        {
        }

        public FinOperationsAdapter(Context context, int resource, int textViewResourceId, IList<FinOperation> objects) : base(context, resource, textViewResourceId, objects)
        {
        }

        public FinOperationsAdapter(Context context, LayoutInflater inflater, IList<FinOperation> list) : base(context, 0, list)
        {
            this.inflater = inflater;
            this.list = list;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = this.inflater.Inflate(Resource.Layout.FinOperationListItem, parent, false);

                var finOperation = this.list[position];

                var holder = new FinOperationHolder(
                    view.FindViewById<TextView>(Resource.Id.descriptionTextView),
                    view.FindViewById<TextView>(Resource.Id.valueTextView));

                holder.FillData(finOperation);

                view.SetTag(Resource.Integer.view_holder_tag, holder);
            }
            else
            {
                var holder = (FinOperationHolder)view.GetTag(Resource.Integer.view_holder_tag);

                holder.FillData(this.list[position]);
            }

            return view;
        }
    }

    public class FinOperationHolder : Java.Lang.Object
    {
        public TextView DescriptionTextView { get; set; }
        public TextView ValueTextView { get; set; }

        public FinOperationHolder(TextView descriptionTextView, TextView valueTextView)
        {
            DescriptionTextView = descriptionTextView;
            ValueTextView = valueTextView;
        }

        public void FillData(FinOperation finOperation)
        {
            this.DescriptionTextView.SetText(finOperation.Description, TextView.BufferType.Normal);
            this.ValueTextView.SetText(finOperation.Value.ToString(), TextView.BufferType.Normal);
        }
    }
}