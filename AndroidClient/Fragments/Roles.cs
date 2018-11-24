﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Common;
using Common.DTO;
using Java.Lang;

namespace Client.Fragments
{
    public class Roles : BaseFragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public FloatingActionButton AddRoleFloatActionButton { get; set; }
        public AutoCompleteTextView SearchRoles { get; set; }
        public RecyclerView RolesList { get; set; }
        private RoleService _service;
        private RoleAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Roles, container, false);

            AddRoleFloatActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddRoleFloatActionButton);
            SearchRoles = view.FindViewById<AutoCompleteTextView>(Resource.Id.SearchRoles);
            RolesList = view.FindViewById<RecyclerView>(Resource.Id.RolesList);

            var linearLayoutManager = new LinearLayoutManager(Activity);
            linearLayoutManager.Orientation = LinearLayoutManager.Vertical;
            RolesList.SetLayoutManager(linearLayoutManager);

            _service = new RoleService(Activity);
            _adapter = new RoleAdapter(Context, _service, Resource.Layout.RoleRowItem);

            RolesList.SetAdapter(_adapter);

            GetRoles();

            return view;
        }

        public void OnClick(View view)
        {
            NavigationManager.GoToAddRole();
        }

        public void GetRoles()
        {
            HttpResult<List<Role>> result = null;

            var task = Task.Run(async () =>
            {
                result = await _service.GetRoles(Criteria);
            });

            task.Wait();

            if(result.Error != null)
            {
                Toast.MakeText(Context, "An error occurred", ToastLength.Short);

                return;
            }

            _adapter.UpdateList(result.Data);

            if (result.Data.Any())
            {
                RolesList.Visibility = ViewStates.Visible;

                return;
            }

            RolesList.Visibility = ViewStates.Invisible;

        }

        public void AfterTextChanged(IEditable text)
        {
            Criteria.Name = text.ToString();


        }
        
    }
}