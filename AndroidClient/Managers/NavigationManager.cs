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

        public void GoToLogin()
        {
            GoTo<Login>();
        }

        public void GoToProducts()
        {
            GoTo<Products>();
        }

        public void GoToAddProduct()
        {
            GoTo<AddProduct>();
        }

        public void GoToProductDetails()
        {
            GoTo<Product>();
        }

        public void GoToRoleDetails(Common.DTO.Role role)
        {
            GoTo<Role>();
            //transaction.Replace(Resource.Id.Container, new Role(role));
        }

        public void GoToUserDetails(Common.DTO.User user)
        {
            GoTo<User>();
            //transaction.Replace(Resource.Id.Container, new User(user));
        }

        public void GoToAddInvoice()
        {
            //var transaction = _activity.FragmentManager.BeginTransaction();
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

        public void GoToGoodsReceivedNotes()
        {
            GoTo<GoodsReceivedNotes>();
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
    }
}