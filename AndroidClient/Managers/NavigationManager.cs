using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Android.App;
using Android.Gms.Vision.Barcodes;
using Android.OS;
using Client.Fragments;
using Client.Models;

namespace Client.Managers
{
    public class NavigationManager
    {
        private readonly Activity _activity;
        private readonly FragmentManager _fragmentManager;
        private FragmentTransaction _transaction;
        public BaseFragment CurrentFragment { get; set; }
        public BaseFragment LastFragment { get; set; }

        public NavigationManager(Activity activity)
        {
            _activity = activity;
            _fragmentManager = _activity.FragmentManager;
        }

        public delegate T ObjectActivator<T>(params object[] args);

        public static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
        {
            var type = ctor.DeclaringType;
            var paramsInfo = ctor.GetParameters();
            var param = Expression.Parameter(typeof(object[]), "args");
            var argsExp = new Expression[paramsInfo.Length];
            
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                var index = Expression.Constant(i);
                var paramType = paramsInfo[i].ParameterType;

                var paramAccessorExp = Expression.ArrayIndex(param, index);

                var paramCastExp = Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            var newExp = Expression.New(ctor, argsExp);

            var lambda =
                Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            var compiled = (ObjectActivator<T>)lambda.Compile();

            return compiled;
        }

        private BaseFragment FindOrCreateFragment<T>(string name) 
            where T : BaseFragment, new()
        {
            var fragment = _fragmentManager
                .FindFragmentByTag<T>(name);

            if (fragment == null)
            {
                fragment = new T();
            }

            LastFragment = CurrentFragment;
            CurrentFragment = fragment;

            return fragment;
        }

        private void GoTo<T>() where T : BaseFragment, new()
        {
            var fullName = typeof(T).FullName;

            var fragment = FindOrCreateFragment<T>(fullName);

            ReplaceFragment(fullName, fragment);
        }

        private void ReplaceFragment(string name, BaseFragment fragment)
        {
            _transaction = _fragmentManager.BeginTransaction();

            _transaction
                .SetCustomAnimations(Android.Resource.Animator.FadeIn, Android.Resource.Animator.FadeOut);

            _transaction
                .Replace(Resource.Id.Container, fragment)
                .AddToBackStack(name);

            _transaction.Commit();
        }

        private void GoTo<T>(IParcelable data) where T : BaseFragment, new()
        {
            var fullName = typeof(T).FullName;

            var fragment = FindOrCreateFragment<T>(fullName);

            var bundle = data as Bundle;
            if (bundle == null)
            {
                bundle = new Bundle();
                bundle.PutParcelable(Constants.Entity, data);
            }
            fragment.Arguments = bundle;

            ReplaceFragment(fullName, fragment);
        }

        public void GoToGoodsReceivedNoteDetails(GoodsReceivedNote model)
        {
            GoTo<Fragments.Details.GoodsReceivedNote>(model);
        }

        public void GoToGoodsDispatchedNoteDetails(GoodsDispatchedNote model)
        {
            GoTo<Fragments.Details.GoodsDispatchedNote>(model);
        }

        public void GoToAttributes()
        {
            GoTo<Fragments.Attributes>();
        }

        public void GoToAddAttribute()
        {
            GoTo<Fragments.Add.Attribute>();
        }

        public void GoToAttributeEdit(Models.Attribute item)
        {
            GoTo<Fragments.Edit.Attribute>(item);
        }

        public void GoToPrevious()
        {
            _fragmentManager.PopBackStack();
        }

        public void InvoiceDetails(Invoice item)
        {
            GoTo<Fragments.Details.Invoice>(item);
        }

        public void GoToLogin()
        {
            GoTo<Fragments.Login>();
        }

        public void GoToEditRole(Role item)
        {
            GoTo<Fragments.Edit.Role>(item);
        }

        public void GoToProducts()
        {
            GoTo<Products>();
        }

        public void GoToProductEdit(Product product)
        {
            GoTo<Fragments.Edit.Product>(product);
        }

        public void GoToProductDetails(Models.Product product)
        {
            GoTo<Fragments.Details.Product>(product);
        }

        public void GoToRoleDetails(Models.Role role)
        {
            GoTo<Fragments.Details.Role>(role);
        }

        public void GoToUserDetails(Models.User user)
        {
            GoTo<Fragments.Details.User>(user);
        }

        public void GoToAddInvoice()
        {
            GoTo<Fragments.Add.Invoice>();
        }

        public void GoToUsers()
        {
            GoTo<Users>();
        }

        public void GoToEditUser(User item)
        {
            GoTo<Fragments.Edit.User>(item);
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

        internal void GoToEditLocation(Location item)
        {
            GoTo<Fragments.Edit.Location>(item);
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

        public void GoToEditUserProfile(User user)
        {
            GoTo<Fragments.Edit.UserProfile>(user);
        }

        public void GoToSettings()
        {
            
        }

        public void GoToLanguages()
        {
            GoTo<Fragments.Edit.Language>();
        }

        public void GoToQRScanner(bool goToDetailsWhenFound = true)
        {
            var data = new Bundle();
            var barcodeFormats = new BarcodeFormat[]
            {
                BarcodeFormat.QrCode
            }.Cast<int>().ToArray();

            data.PutIntArray(Constants.BarcodeFormats, barcodeFormats);
            data.PutBoolean(Constants.Callback, goToDetailsWhenFound);

            GoTo<BarcodeQRScanner>(data);
        }

        public void GoToBarcodeScanner(bool callback = true)
        {
            var data = new Bundle();
            var barcodeFormats = new BarcodeFormat[]
            {
                BarcodeFormat.Code128,
                BarcodeFormat.Code39,
                BarcodeFormat.Code93
            }.Cast<int>().ToArray();

            data.PutIntArray(Constants.BarcodeFormats, barcodeFormats);
            data.PutBoolean(Constants.Callback, callback);

            GoTo<BarcodeQRScanner>(data);
        }
    }
}