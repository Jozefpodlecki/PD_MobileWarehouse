using Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiServer.Controllers.Attribute.ViewModel;
using WebApiServer.Controllers.Counterparty.ViewModel;
using WebApiServer.Controllers.Invoice.ViewModel;
using WebApiServer.Controllers.Location.ViewModel;
using WebApiServer.Controllers.Product.ViewModel;
using WebApiServer.Controllers.Role.ViewModel;
using WebApiServer.Controllers.User.ViewModel;

namespace WebApiServer
{
    public interface IUnitOfWork
    {
        Task<IList<System.Security.Claims.Claim>> GetUserClaims(Data_Access_Layer.User user);
        Data_Access_Layer.User GetUser(string username);
        Task UpdateLastLogin(Data_Access_Layer.User user);
        Task AddUser(AddUser model);
        Task AddUsers(IEnumerable<AddUser> users);
        Task EditUser(EditUser model);
        Task EditProduct(EditProduct model);
        void BlockUser(Data_Access_Layer.User user);
        Task AddRoles(IEnumerable<AddRole> roles);
        Task AddRole(AddRole model);
        Task EditRole(EditRole model);
        Task DeleteUser(int id);
        Task DeleteAttribute(int id);
        Task DeleteLocation(int id);
        void DeleteRole(Data_Access_Layer.Role role);
        Task<bool> LocationExists(string name);
        bool ProductExists(string name);
        bool RoleExists(RoleExists model);
        bool UserExists(UserExists model);
        bool ExistsCounterparty(ExistsCounterparty model);
        Task AddCounterparty(AddCounterparty model);
        Task UpdateCounterparty(EditCounterparty model);
        List<KeyValue> GetPaymentMethods();
        List<KeyValue> GetInvoiceTypes();
        Task AddAttribute(AddAttribute model);
        Task EditAttribute(EditAttribute model);
        Task AddLocation(AddLocation model);
        Task EditLocation(EditLocation model);
        Task AddGoodsDispatchedNote(Controllers.Note.ViewModel.AddGoodsDispatchedNote model);
        Task AddGoodsReceivedNote(Controllers.Note.ViewModel.AddGoodsReceivedNote model);
        Task CreateInvoice(AddInvoice model);
        Task CreateInvoices(IEnumerable<AddInvoice> models);
        Task<List<Common.DTO.Attribute>> GetAttributes(FilterCriteria criteria);
        List<Common.DTO.GoodsDispatchedNote> GetGoodsDispatchedNotes(FilterCriteria criteria);
        List<Common.DTO.GoodsReceivedNote> GetGoodsReceivedNotes(FilterCriteria criteria);
        List<Common.DTO.Invoice> GetInvoices(InvoiceFilterCriteria criteria);
        List<Common.DTO.User> GetUsers(FilterCriteria criteria);
        List<Common.DTO.Claim> GetClaims();
        List<Common.DTO.Location> GetLocations(FilterCriteria criteria);
        List<Common.DTO.Role> GetRoles(FilterCriteria criteria);
        List<Common.DTO.Product> GetProducts(FilterCriteria criteria);
        List<Common.DTO.Counterparty> GetCounterparties(FilterCriteria criteria);
        List<Common.DTO.City> GetCities(FilterCriteria criteria);
        Task<Common.DTO.User> GetUser(int id);
        Task<Data_Access_Layer.Role> GetRole(int id);
        Common.DTO.Product GetProductByBarcode(string barcode);
        List<Common.DTO.Location> GetLocationsByProduct(string name);
        void Save();
    }
}
