using System;
using Android.App;
using AndroidClient.Fragments;
using Client.Fragments;
using Client.Fragments.Details;

namespace Client.Managers
{
    public class NavigationManager
    {
        private readonly Activity _activity;

        public NavigationManager(Activity activity)
        {
            _activity = activity;
        }

        public void GoToLogin()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Login());
            transaction.Commit();
        }

        public void GoToProducts()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Products());
            transaction.Commit();
        }

        public void GoToAddProduct()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new AddProduct());
            transaction.Commit();
        }

        public void GoToProductDetails()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Product());
            transaction.Commit();
        }

        public void GoToRoleDetails(Common.DTO.Role role)
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Role(role));
            transaction.Commit();
        }

        public void GoToUserDetails(Common.DTO.User user)
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new User(user));
            transaction.Commit();
        }

        public void GoToAddInvoice()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Fragments.Add.Invoice());
            transaction.Commit();
        }

        public void GoToUsers()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Users());
            transaction.Commit();
        }

        public void GoToAddCounterparty()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Fragments.Add.Counterparty());
            transaction.Commit();
        }

        public void GoToAddUser()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Fragments.Add.User());
            transaction.Commit();
        }

        public void GoToRoles()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Roles());
            transaction.Commit();
        }

        public void GoToAddRole()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Fragments.Add.Role());
            transaction.Commit();
        }

        public void GoToGoodsDispatchedNotes()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new GoodsDispatchedNotes());
            transaction.Commit();
        }

        public void GoToAddGoodsDispatchedNote()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Fragments.Add.GoodsDispatchedNote());
            transaction.Commit();
        }

        public void GoToGoodsReceivedNotes()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new GoodsReceivedNotes());
            transaction.Commit();
        }

        public void GoToAddGoodsReceivedNote()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Fragments.Add.GoodsReceivedNote());
            transaction.Commit();
        }

        public void GoToInvoices()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Invoices());
            transaction.Commit();
        }

        public void GoToCounterparties()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.Container, new Counterparties());
            transaction.Commit();
        }
    }
}