﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Common;
using Common.IUnitOfWork;
using WebApiServer.Controllers.Role.ViewModel;

namespace Client.Services.Mock
{
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<HttpResult<bool>> AddRole(Role entity, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new AddRole
            {
                Name = entity.Name,
                Claims = entity.Claims
                    .Select(cl => new Common.DTO.Claim
                    {
                        Type = cl.Type,
                        Value = cl.Value
                    })
                    .ToList()
            };

            await _unitOfWork.AddRole(model);

            return result;
        }

        public async Task<HttpResult<bool>> DeleteRole(int id, CancellationToken token = default(CancellationToken))
        {
            var httpResult = new HttpResult<bool>();

            var role = await _unitOfWork.GetRole(id);

            var result = await _unitOfWork.DeleteRole(role);

            if (!string.IsNullOrEmpty(result))
            {
                httpResult.Error.Add("UserRoles", new string[] { result });
            }

            return httpResult;
        }

        public async Task<HttpResult<bool>> EditRole(Role entity, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new EditRole
            {
                Id = entity.Id,
                Name = entity.Name,
                Claims = entity.Claims
                    .Select(cl => new Common.DTO.Claim
                    {
                        Type = cl.Type,
                        Value = cl.Value
                    })
                    .ToList()
            };

            await _unitOfWork.EditRole(model);

            return result;
        }

        public async Task<HttpResult<List<Claim>>> GetClaims(CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Claim>>();

            result.Data = _unitOfWork
                .GetClaims()
                .Select(cl => new Claim
                {
                    Type = cl.Type,
                    Value = cl.Value
                })
                .ToList();

            return result;
        }

        public async Task<HttpResult<List<Role>>> GetRoles(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Role>>();

            result.Data = _unitOfWork
                .GetRoles(criteria)
                .Select(ro => new Models.Role
                {
                    Id = ro.Id,
                    Name = ro.Name,
                    Claims = ro.Claims
                        .Select(cl => new Claim
                        {
                            Type = cl.Type,
                            Value = cl.Value
                        })
                        .ToList(),
                    CreatedAt = ro.CreatedAt,
                    CreatedBy = ro.CreatedBy == null ? null : new User
                    {
                        Id = ro.CreatedBy.Id,
                        Username = ro.CreatedBy.Username
                    },
                    LastModifiedBy = ro.LastModifiedBy == null ? null : new User
                    {
                        Id = ro.LastModifiedBy.Id,
                        Username = ro.LastModifiedBy.Username
                    },
                    LastModifiedAt = ro.LastModifiedAt
                })
                .ToList();

            if (result.Data == null)
            {
                result.Data = new List<Role>();
            }

            return result;
        }

        public async Task<HttpResult<bool>> RoleExists(string name, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            result.Data = _unitOfWork
                .RoleExists(new WebApiServer.Controllers.Role.ViewModel.RoleExists { Name = name });

            return result;
        }

        public async Task<HttpResult<bool>> UpdateRole(Role entity, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new EditRole
            {
                Id = entity.Id,
                Name = entity.Name,
                Claims = entity
                    .Claims
                    .Select(cl => new Common.DTO.Claim
                    {
                        Type = cl.Type,
                        Value = cl.Value
                    })
                    .ToList()
            };

            await _unitOfWork.EditRole(model);

            return result;
        }
    }
}