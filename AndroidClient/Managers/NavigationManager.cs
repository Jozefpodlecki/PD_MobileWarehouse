﻿using Android.App;
using Android.OS;
using AndroidClient.Fragments;
using Client.Fragments;

namespace Client.Managers
{
    public class NavigationManager
    {
        private readonly Activity _activity;
        private readonly FragmentManager _fragmentManager;
        private FragmentTransaction _transaction;

        public NavigationManager(Activity activity)
        {
            _activity = activity;
            _fragmentManager = _activity.FragmentManager;
        }
        
        private void GoTo<T>() where T : BaseFragment, new()
        {
            var fragment = _fragmentManager
                .FindFragmentByTag<T>(nameof(T));

            _transaction = _fragmentManager.BeginTransaction();

            if (fragment == null)
            {
                fragment = new T();
            }

            _transaction
                .Replace(Resource.Id.Container, fragment)
                .AddToBackStack(nameof(T));

            _transaction.Commit();
        }

        private void GoTo<T>(IParcelable data) where T : BaseFragment, new()
        {
            var fragment = _fragmentManager
                .FindFragmentByTag<T>(nameof(T));

            _transaction = _fragmentManager.BeginTransaction();

            if (fragment == null)
            {
                fragment = new T();
            }

            var bundle = new Bundle();
            bundle.PutParcelable("data",data);
            fragment.Arguments = bundle;

            _transaction
                .Replace(Resource.Id.Container, fragment)
                .AddToBackStack(nameof(T));

            _transaction.Commit();
        }

        public void GoToLogin()
        {
            GoTo<AndroidClient.Fragments.Login>();
        }

        public void GoToProducts()
        {
            GoTo<Products>();
        }

        public void GoToAddProduct()
        {
            GoTo<AddProduct>();
        }

        public void GoToProductDetails(Models.Product product)
        {
            GoTo<Fragments.Details.Product>(product);
        }

        public void GoToRoleDetails(Models.Role role)
        {
            GoTo<Fragments.Details.Role>();
        }

        public void GoToUserDetails(Models.User user)
        {
            GoTo<Fragments.Details.User>();
        }

        public void GoToAddInvoice()
        {
            GoTo<Fragments.Add.Invoice>();
        }

        public void GoToUsers()
        {
            GoTo<Users>();
        }

        public void GoToAddCounterparty()
        {
            GoTo<Fragments.Add.Counterparty>();
        }

        public void GoToAddUser()
        {
            GoTo<Fragments.Add.User>();
        }

        public void GoToRoles()
        {
            GoTo<Roles>();
        }

        public void GoToAddRole()
        {
            GoTo<Fragments.Add.Role>();
        }

        public void GoToGoodsDispatchedNotes()
        {
            GoTo<GoodsDispatchedNotes>();
        }

        public void GoToAddGoodsDispatchedNote()
        {
            GoTo<Fragments.Add.GoodsDispatchedNote>();
        }

        public void GoToCounterpartyDetails(Models.Counterparty item)
        {
            GoTo<Fragments.Details.Counterparty>(item);
        }

        public void GoToGoodsReceivedNotes()
        {
            GoTo<GoodsReceivedNotes>();
        }

        internal void GoToCounterpartyEdit(Models.Counterparty item)
        {
            GoTo<Fragments.Edit.Counterparty>(item);
        }

        public void GoToAddGoodsReceivedNote()
        {
            GoTo<Fragments.Add.GoodsReceivedNote>();
        }

        public void GoToInvoices()
        {
            GoTo<Invoices>();
        }

        public void GoToCounterparties()
        {
            GoTo<Counterparties>();
        }

        public void GoToLocations()
        {
            GoTo<Locations>();
        }

        public void GoToAddLocation()
        {
            GoTo<Fragments.Add.Location>();
        }

        public void GoToAccount()
        {
            GoTo<Account>();
        }

        public void GoToEditDetails()
        {
            GoTo<Fragments.Edit.Details>();
        }

        public void GoToSettings()
        {
            
        }

        public void GoToLanguages()
        {
            GoTo<Fragments.Language>();
        }
    }
}