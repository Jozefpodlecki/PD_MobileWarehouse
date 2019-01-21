using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Common;
using Common.IUnitOfWork;
using WebApiServer.Controllers.Invoice.ViewModel;

namespace Client.Services.Mock
{
    public class InvoiceService : BaseService, IInvoiceService
    {
        public InvoiceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<HttpResult<bool>> AddInvoice(Invoice invoice, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new AddInvoice
            {
                DocumentId = invoice.DocumentId,
                CompletionDate = invoice.CompletionDate,
                Counterparty = new Common.DTO.Counterparty
                {
                    Id = invoice.Counterparty.Id,
                    Name = invoice.Counterparty.Name,
                    NIP = invoice.Counterparty.NIP,
                },
                City = new Common.DTO.City
                {
                    Id = invoice.City.Id,
                    Name = invoice.City.Name
                },
                InvoiceType = invoice.InvoiceType,
                PaymentMethod = invoice.PaymentMethod,
                IssueDate = invoice.IssueDate,
                Products = invoice.Products.Select(inv => new Data_Access_Layer.Entry
                {
                    Name = inv.Name,
                    Price = inv.Price,
                    Count = inv.Count,
                    VAT = inv.VAT
                })
                .ToList()
            };

            await _unitOfWork.CreateInvoice(model);

            return result;
        }

        public async Task<HttpResult<bool>> DeleteInvoice(int id, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            return result;
        }

        public async Task<HttpResult<List<Invoice>>> GetInvoices(InvoiceFilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Invoice>>();

            result.Data = _unitOfWork
                .GetInvoices(criteria)
                .Select(inv => new Invoice
                {
                    DocumentId = inv.DocumentId,
                    IssueDate = inv.IssueDate,
                    Products = inv.Products
                        .Select(pr => new Entry
                        {
                            Id = pr.Id,
                            Name = pr.Name,
                            Price = pr.Price,
                            VAT = pr.VAT,
                            Count = pr.Count
                        })
                        .ToList(),
                    CompletionDate = inv.CompletionDate,
                    CreatedAt = inv.CreatedAt,
                    LastModifiedAt = inv.LastModifiedAt
                })
                .ToList();

            return result;
        }

        public async Task<HttpResult<List<Models.KeyValue>>> GetInvoiceTypes(CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Models.KeyValue>>();

            result.Data = _unitOfWork
                .GetInvoiceTypes()
                .Select(pm => new Models.KeyValue
                {
                    Id = pm.Id,
                    Name = pm.Name
                })
                .ToList();

            return result;
        }

        public async Task<HttpResult<List<Models.KeyValue>>> GetPaymentMethods(CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Models.KeyValue>>();

            result.Data = _unitOfWork
                .GetPaymentMethods()
                .Select(pm => new Models.KeyValue
                {
                    Id = pm.Id,
                    Name = pm.Name
                })
                .ToList();

            return result;
        }
    }
}