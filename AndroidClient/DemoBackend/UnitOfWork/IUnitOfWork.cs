using Common.Repository.Interfaces;
using Data_Access_Layer;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiServer.Controllers.Attribute.ViewModel;
using WebApiServer.Controllers.Counterparty.ViewModel;
using WebApiServer.Controllers.Invoice.ViewModel;
using WebApiServer.Controllers.Location.ViewModel;
using WebApiServer.Controllers.Product.ViewModel;
using WebApiServer.Controllers.Role.ViewModel;
using WebApiServer.Controllers.User.ViewModel;

namespace Common.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        INameRepository<Data_Access_Layer.Attribute> AttributeRepository { get; }
        IProductAttributeRepository ProductAttributeRepository { get; }
        IUserRepository UserRepository { get; }
        INameRepository<Data_Access_Layer.Role> RoleRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }
        IRoleClaimRepository RoleClaimRepository { get; }
        IUserClaimRepository UserClaimRepository { get; }
        INameRepository<Data_Access_Layer.Counterparty> CounterpartyRepository { get; }
        INameRepository<Data_Access_Layer.City> CityRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IEntryRepository EntryRepository { get; }
        IGoodsDispatchedNoteRepository GoodsDispatchedNoteRepository { get; }
        IGoodsReceivedNoteRepository GoodsReceivedNoteRepository { get; }
        IProductDetailsRepository ProductDetailsRepository { get; }
        ILocationRepository LocationRepository { get; }
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
        Task<string> DeleteAttribute(int id);
        Task<string> DeleteLocation(int id);
        Task<string> DeleteRole(Data_Access_Layer.Role role);
        Task<bool> LocationExists(string name);
        bool ProductExists(string name);
        bool RoleExists(RoleExists model);
        bool UserExists(UserExists model);
        bool ExistsCounterparty(ExistsCounterparty model);
        Task AddCounterparty(AddCounterparty model);
        Task UpdateCounterparty(EditCounterparty model);
        List<KeyValue> GetPaymentMethods();
        List<KeyValue> GetInvoiceTypes();
        Task<string> DeleteGoodsDispatchedNote(int invoiceId);
        Task AddAttribute(AddAttribute model);
        Task EditAttribute(EditAttribute model);
        Task AddLocation(AddLocation model);
        Task UpdateAttribute(int id, string name);
        Task<string> DeleteGoodsReceivedNote(int invoiceId);
        Task EditLocation(EditLocation model);
        Task AddGoodsDispatchedNote(WebApiServer.Controllers.Note.ViewModel.AddGoodsDispatchedNote model);
        Task AddGoodsReceivedNote(WebApiServer.Controllers.Note.ViewModel.AddGoodsReceivedNote model);
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
    }
}
