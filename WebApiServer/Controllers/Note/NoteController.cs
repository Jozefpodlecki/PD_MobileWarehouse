using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Data_Access_Layer;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiServer.Controllers.Note.ViewModel;

namespace WebApiServer.Controllers.Note
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        public readonly IRepository<Counterparty> _counterpartyRepository;
        public readonly INameRepository<Data_Access_Layer.City> _cityRepository;
        public readonly INameRepository<Data_Access_Layer.Product> _productRepository;
        public readonly IRepository<GoodsDispatchedNote> _goodsDispatchedNoteRepository;
        public readonly IRepository<GoodsReceivedNote> _goodsReceivedNoteRepository;
        public readonly IRepository<Data_Access_Layer.Invoice> _invoiceRepository;
        public readonly IRepository<Data_Access_Layer.Entry> _entryRepository;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly UserManager<Data_Access_Layer.User> UserManager;

        public Data_Access_Layer.User User;

        public NoteController(
            IRepository<Counterparty> counterpartyRepository,
            INameRepository<Data_Access_Layer.City> cityRepository,
            IRepository<GoodsDispatchedNote> goodsDispatchedNoteRepository,
            IRepository<GoodsReceivedNote> goodsReceivedNoteRepository,
            IRepository<Data_Access_Layer.Invoice> invoiceRepository,
            IRepository<Data_Access_Layer.Entry> entryRepository,
            IHttpContextAccessor httpContextAccessor,
            UserManager<Data_Access_Layer.User> userManager,
            INameRepository<Data_Access_Layer.Product> productRepository
            )
        {
            _counterpartyRepository = counterpartyRepository;
            _cityRepository = cityRepository;
            _goodsDispatchedNoteRepository = goodsDispatchedNoteRepository;
            _goodsReceivedNoteRepository = goodsReceivedNoteRepository;
            _invoiceRepository = invoiceRepository;
            _entryRepository = entryRepository;

            _httpContextAccessor = httpContextAccessor;
            UserManager = userManager;
            _productRepository = productRepository;
        }

        [HttpGet("counterparties")]
        public IActionResult GetCounterparties()
        {
            var result = _counterpartyRepository
                .Entities;

            return new ObjectResult(result);
        }

        [HttpPost("counterparties/search")]
        public IActionResult GetCounterparties(FilterCriteria criteria)
        {
            var result = _counterpartyRepository
                .Entities;

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                result = result
                    .Where(cr => cr.Name.Contains(criteria.Name)
                        || cr.NIP.Contains(criteria.Name));
            }

            var entities = result
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage)
                .ToList();

            return new ObjectResult(entities);
        }

        [HttpPost("counterparty")]
        public async Task<IActionResult> UpdateCounterparty([FromBody] EditCounterparty model)
        {
            var city = await GetOrAddCity(model.City);

            var entity = new Counterparty
            {
                Id = model.Id,
                Name = model.Name,
                Street = model.Street,
                NIP = model.NIP,
                CityId = city.Id,
                PostalCode = model.PostalCode,
                PhoneNumber = model.PhoneNumber
            };

            _counterpartyRepository.Update(entity);
            await _counterpartyRepository.Save();

            return Ok();
        }

        private async Task<Data_Access_Layer.City> GetOrAddCity(ViewModel.City model)
        {
            Data_Access_Layer.City city = null;

            if (model.Id == 0)
            {
                city = new Data_Access_Layer.City()
                {
                    Name = model.Name
                };

                await _cityRepository.Add(city);
                await _cityRepository.Save();
            }
            else
            {
                city = await _cityRepository.Get(model.Id);
            }

            return city;
        }

        [HttpPut("counterparty")]
        public async Task<IActionResult> AddCounterparty([FromBody] AddCounterparty model)
        {
            var city = await GetOrAddCity(model.City);

            var entity = new Counterparty
            {
                Name = model.Name,
                Street = model.Street,
                NIP = model.NIP,
                CityId = city.Id,
                PostalCode = model.PostalCode,
                PhoneNumber = model.PhoneNumber
            };

            await _counterpartyRepository.Add(entity);
            await _counterpartyRepository.Save();

            return Ok();
        }

        private async Task<Data_Access_Layer.Invoice> GetOrCreateInvoice([FromBody] ViewModel.Invoice model)
        {
            Data_Access_Layer.Invoice invoice = null;
            Data_Access_Layer.City city = null;

            if (model.City != null)
            {
                city = await GetOrAddCity(model.City);
            }

            if (model.Id == 0)
            {
                var entries = new List<Data_Access_Layer.Entry>();
                var totalVAT = 0m;
                var total = 0m;

                if(model.IssueDate == DateTime.MinValue)
                {
                    model.IssueDate = DateTime.Now;
                }

                if (model.CompletionDate == DateTime.MinValue)
                {
                    model.CompletionDate = DateTime.Now;
                }

                invoice = new Data_Access_Layer.Invoice
                {
                    CompletionDate = model.CompletionDate,
                    IssueDate = model.IssueDate,
                    DocumentId = model.DocumentId,
                    Total = total,
                    VAT = totalVAT,
                    PaymentMethod = model.PaymentMethod,
                    CanEdit = model.CanEdit,
                    AuthorId = User.Id
                };

                if (city != null)
                {
                    invoice.CityId = city.Id;
                }

                await _invoiceRepository.Add(invoice);
                await _invoiceRepository.Save();

                foreach (var product in model.Products)
                {
                    var entry = new Data_Access_Layer.Entry
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Count = product.Count,
                        VAT = product.VAT,
                        InvoiceId = invoice.Id
                    };

                    var amount = entry.Count * entry.Price;
                    totalVAT += amount * entry.VAT;
                    total += amount;
                    entries.Add(entry);
                }

                await _entryRepository.AddRange(entries);
                await _entryRepository.Save();

                invoice.Total = total;
                invoice.VAT = totalVAT;

                _invoiceRepository.Update(invoice);
                await _invoiceRepository.Save();
            }
            else
            {
                invoice = await _invoiceRepository.Get(model.Id);
            }

            return invoice;
        }

        private async Task<Data_Access_Layer.Invoice> GetOrCreateInvoiceForDispatchNote([FromBody] ViewModel.Invoice model)
        {
            Data_Access_Layer.Invoice invoice = null;
            Data_Access_Layer.City city = null;

            if (model.City != null)
            {
                city = await GetOrAddCity(model.City);
            }

            if (model.Id == 0)
            {
                var entries = new List<Data_Access_Layer.Entry>();
                var totalVAT = 0m;
                var total = 0m;

                if (model.IssueDate == DateTime.MinValue)
                {
                    model.IssueDate = DateTime.Now;
                }

                if (model.CompletionDate == DateTime.MinValue)
                {
                    model.CompletionDate = DateTime.Now;
                }

                invoice = new Data_Access_Layer.Invoice
                {
                    CompletionDate = model.CompletionDate,
                    IssueDate = model.IssueDate,
                    DocumentId = model.DocumentId,
                    Total = total,
                    VAT = totalVAT,
                    PaymentMethod = model.PaymentMethod,
                    CanEdit = model.CanEdit,
                    AuthorId = User.Id
                };

                if (city != null)
                {
                    invoice.CityId = city.Id;
                }

                await _invoiceRepository.Add(invoice);
                await _invoiceRepository.Save();

                foreach (var product in model.Products)
                {
                    var entry = new Data_Access_Layer.Entry
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Count = product.Count,
                        VAT = product.VAT,
                        InvoiceId = invoice.Id,
                        ProductId = product.ProductId
                    };

                    var amount = entry.Count * entry.Price;
                    totalVAT += amount * entry.VAT;
                    total += amount;
                    entries.Add(entry);
                }

                await _entryRepository.AddRange(entries);
                await _entryRepository.Save();

                invoice.Total = total;
                invoice.VAT = totalVAT;

                _invoiceRepository.Update(invoice);
                await _invoiceRepository.Save();
            }
            else
            {
                invoice = await _invoiceRepository.Get(model.Id);
            }

            return invoice;
        }

        [HttpPut("goodsReceivedNote")]
        public async Task<IActionResult> AddGoodsReceivedNote([FromBody] AddGoodsReceivedNote model)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;
            User = await UserManager.GetUserAsync(claimsPrincipal);

            var invoice = await GetOrCreateInvoice(model.Invoice);

            var note = new GoodsReceivedNote
            {
                IssueDate = model.IssueDate,
                ReceiveDate = model.ReceiveDate,
                InvoiceId = invoice.Id,
                AuthorId = User.Id
            };

            await _goodsReceivedNoteRepository.Add(note);
            await _goodsReceivedNoteRepository.Save();

            return Ok();
        }

        [HttpPut("goodsDispatchedNote")]
        public async Task<IActionResult> AddGoodsDispatchedNote([FromBody] AddGoodsDispatchedNote model)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;
            User = await UserManager.GetUserAsync(claimsPrincipal);

            var invoice = await GetOrCreateInvoiceForDispatchNote(model.Invoice);

            var note = new GoodsDispatchedNote
            {
                IssueDate = model.IssueDate,
                DispatchDate = model.DispatchDate,
                Invoice = invoice
            };

            await _goodsDispatchedNoteRepository.Add(note);
            await _goodsDispatchedNoteRepository.Save();




            return Ok();
        }

        [HttpPut("invoice")]
        public async Task<IActionResult> AddInvoice([FromBody] ViewModel.Invoice model)
        {
            var invoice = await GetOrCreateInvoice(model);

            return Ok();
        }

        [HttpPost("goodsReceivedNotes/search")]
        public async Task<IActionResult> GetGoodsReceivedNotes([FromBody] FilterCriteria criteria)
        {
            var result = _goodsReceivedNoteRepository
                .Entities;


            if (string.IsNullOrEmpty(criteria.Name))
            {
                result = result.Where(en => en.DocumentId.Contains(criteria.Name));
            }

            var entities = await result
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage)
                .ToListAsync();

            return new ObjectResult(entities);
        }

        [HttpPost("goodsDispatchedNotes/search")]
        public async Task<IActionResult> GetGoodsDispatchedNotes([FromBody] FilterCriteria criteria)
        {
            var result = _goodsDispatchedNoteRepository
                .Entities;


            if (string.IsNullOrEmpty(criteria.Name))
            {
                result = result.Where(en => en.DocumentId.Contains(criteria.Name));
            }

            var entities = await result
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage)
                .ToListAsync();

            return new ObjectResult(entities);
        }

        [HttpPost("invoices/search")]
        public async Task<IActionResult> GetInvoices([FromBody] FilterCriteria criteria)
        {
            var result = _goodsReceivedNoteRepository
                .Entities;


            if (string.IsNullOrEmpty(criteria.Name))
            {
                result = result.Where(en => en.DocumentId.Contains(criteria.Name));
            }

            var entities = await result
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage)
                .ToListAsync();

            return new ObjectResult(entities);
        }

        [HttpPost("invoice")]
        public async Task<IActionResult> EditInvoice([FromBody] ViewModel.Invoice model)
        {
            if (!model.CanEdit)
            {
                return BadRequest();
            }

            var invoice = await _invoiceRepository.Get(model.Id);
            invoice.CanEdit = false;

            _invoiceRepository.Update(invoice);

            await _invoiceRepository.Save();

            return Ok();
        }
    }
}
