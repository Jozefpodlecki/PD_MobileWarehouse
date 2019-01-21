using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Interfaces;
using Common;
using Common.IUnitOfWork;
using WebApiServer.Controllers.User.ViewModel;

namespace Client.Services.Mock
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<HttpResult<bool>> AddUser(User user, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var model = new AddUser
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = new Common.DTO.Role
                {
                    Id = user.Role.Id,
                    Name = user.Role.Name
                }
            };

            await _unitOfWork.AddUser(model);

            return result;
        }

        public async Task<HttpResult<bool>> DeleteUser(int id, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            return result;
        }

        public async Task<HttpResult<User>> GetUser(int id, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<User>();

            var entity = await _unitOfWork.GetUser(id);
            result.Data = new User
            {
                Id = entity.Id,
                Username = entity.Username,
                Avatar = entity.Avatar,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Role = new Role
                {
                    Id = entity.Role.Id,
                    Name = entity.Role.Name
                }
            };

            return result;
        }

        public async Task<HttpResult<List<User>>> GetUsers(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<User>>();

            result.Data = _unitOfWork
            .GetUsers(criteria)
            .Select(us => new Models.User
            {
                Id = us.Id,
                Username = us.Username,
                FirstName = us.FirstName,
                LastName = us.LastName,
                Email = us.Email,
                Avatar = us.Avatar,
                Role = new Role
                {
                    Id = us.Role.Id,
                    Name = us.Role.Name
                },
                Claims = us.Claims
                    .Select(cl => new Claim
                    {
                        Type = cl.Type,
                        Value = cl.Value
                    })
                    .ToList(),
                CreatedAt = us.CreatedAt,
                CreatedBy = us.CreatedBy == null ? null : new User
                {
                    Id = us.CreatedBy.Id,
                    Username = us.CreatedBy.Username
                },
                LastModifiedBy = us.LastModifiedBy == null ? null : new User
                {
                    Id = us.LastModifiedBy.Id,
                    Username = us.LastModifiedBy.Username
                },
                LastModifiedAt = us.LastModifiedAt
            })
            .ToList();   

            if(result.Data == null)
            {
                result.Data = new List<User>();
            }

            return result;
        }

        public async Task<HttpResult<bool>> UpdateUser(User model, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();

            var user = new EditUser
            {
                Id = model.Id,
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Role = new Common.DTO.Role
                {
                    Id = model.Role.Id,
                    Name = model.Role.Name
                }
            };

            await _unitOfWork.EditUser(user);

            return result;
        }
    }
}