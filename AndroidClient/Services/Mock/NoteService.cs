using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Common;
using Common.IUnitOfWork;
using WebApiServer.Controllers.Note.ViewModel;

namespace Client.Services.Mock
{
    public class NoteService : BaseService, INoteService
    {
        public NoteService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<HttpResult<bool>> AddGoodsDispatchedNote(GoodsDispatchedNote note, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new AddGoodsDispatchedNote()
            {
                DocumentId = note.DocumentId,
                InvoiceId = note.InvoiceId,
                IssueDate = note.IssueDate,
                DispatchDate = note.DispatchDate,
                NoteEntry = note
                    .NoteEntry
                    .Select(ne => new WebApiServer.Controllers.Note.ViewModel.NoteEntry
                    {
                        Location = new Common.DTO.Location
                        {
                            Id = ne.Location.Id,
                            Name = ne.Location.Name
                        },
                        Name = ne.Name
                    })
                    .ToList()
            };

            await _unitOfWork.AddGoodsDispatchedNote(model);

            return result;
        }

        public async Task<HttpResult<bool>> AddGoodsReceivedNote(GoodsReceivedNote note, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new WebApiServer.Controllers.Note.ViewModel.AddGoodsReceivedNote
            { 
                DocumentId = note.DocumentId,
                InvoiceId = note.InvoiceId,
                IssueDate = note.IssueDate,
                NoteEntry = note
                    .NoteEntry
                    .Select(ne => new WebApiServer.Controllers.Note.ViewModel.NoteEntry
                    {
                        Location = new Common.DTO.Location
                        {
                            Id = ne.Location.Id,
                            Name = ne.Location.Name
                        },
                        Name = ne.Name
                    })
                    .ToList(),
                ReceiveDate = note.ReceiveDate
            };

            await _unitOfWork.AddGoodsReceivedNote(model);

            return result;
        }

        public async Task<HttpResult<bool>> DeleteGoodsDispatchedNote(int invoiceId, CancellationToken token = default(CancellationToken))
        {
            var httpResult = new HttpResult<bool>();
            var result = await _unitOfWork.DeleteGoodsDispatchedNote(invoiceId);

            if (!string.IsNullOrEmpty(result))
            {
                httpResult.Error.Add("GoodsDispatchedNote", new string[] { result });
            }

            return httpResult;
        }

        public async Task<HttpResult<bool>> DeleteGoodsReceivedNote(int invoiceId, CancellationToken token = default(CancellationToken))
        {
            var httpResult = new HttpResult<bool>();
            var result = await _unitOfWork.DeleteGoodsReceivedNote(invoiceId);

            if (!string.IsNullOrEmpty(result))
            {
                httpResult.Error.Add("GoodsReceivedNote", new string[] { result });
            }

            return httpResult;
        }

        public async Task<HttpResult<List<GoodsDispatchedNote>>> GetGoodsDispatchedNotes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<GoodsDispatchedNote>>();

            result.Data = _unitOfWork
                .GetGoodsDispatchedNotes(criteria)
                .Select(gdn => new GoodsDispatchedNote
                {
                    DocumentId = gdn.DocumentId,
                    IssueDate = gdn.IssueDate,
                    DispatchDate = gdn.DispatchDate,
                    Invoice = new Invoice
                    {
                        Id = gdn.Invoice.Id,
                        DocumentId = gdn.Invoice.DocumentId
                    },
                    CreatedAt = gdn.CreatedAt,
                    CreatedBy = gdn.CreatedBy == null ? null : new User
                    {
                        Id = gdn.CreatedBy.Id,
                        Username = gdn.CreatedBy.Username
                    },
                    LastModifiedBy = gdn.LastModifiedBy == null ? null : new User
                    {
                        Id = gdn.LastModifiedBy.Id,
                        Username = gdn.LastModifiedBy.Username
                    },
                    LastModifiedAt = gdn.LastModifiedAt
                })
                .ToList();

            return result;
        }

        public async Task<HttpResult<List<GoodsReceivedNote>>> GetGoodsReceivedNotes(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<GoodsReceivedNote>>();

            result.Data = _unitOfWork
                .GetGoodsReceivedNotes(criteria)
                .Select(gdn => new GoodsReceivedNote
                {
                    DocumentId = gdn.DocumentId,
                    IssueDate = gdn.IssueDate,
                    ReceiveDate = gdn.ReceiveDate,
                    Invoice = new Invoice
                    {
                        Id = gdn.Invoice.Id,
                        DocumentId = gdn.Invoice.DocumentId
                    },
                    CreatedAt = gdn.CreatedAt,
                    CreatedBy = gdn.CreatedBy == null ? null : new User
                    {
                        Id = gdn.CreatedBy.Id,
                        Username = gdn.CreatedBy.Username
                    },
                    LastModifiedBy = gdn.LastModifiedBy == null ? null : new User
                    {
                        Id = gdn.LastModifiedBy.Id,
                        Username = gdn.LastModifiedBy.Username
                    },
                    LastModifiedAt = gdn.LastModifiedAt
                })
                .ToList();

            return result;
        }
    }
}