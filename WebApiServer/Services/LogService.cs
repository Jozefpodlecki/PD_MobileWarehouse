using Data_Access_Layer;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiServer.Services
{
    public enum Events
    {
        LOG_IN,
        LOG_OUT,
        NEW_PRODUCT,
        UPDATE_PRODUCT,
        DELETE_PRODUCT,
        ADD_ATTRIBUTE,
        UPDATE_ATTRIBUTE,
        REMOVE_ATTRIBUTE,
        ADD_USER,
        UPDATE_USER,
        REMOVE_USER,
        ADD_ROLE,
        UPDATE_ROLE,
        REMOVE_ROLE
    }

    public class LogService
    {
        private readonly IRepository<History> _historyRepository;
        private readonly IRepository<Event> _eventRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly Dictionary<Events, Event> _events;

        public LogService(
            IHttpContextAccessor httpContextAccessor,
            IRepository<History> historyRepository,
            IRepository<Event> eventRepository,
            UserManager<User> userManager
            )
        {
            _historyRepository = historyRepository;
            _eventRepository = eventRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

            _events = _eventRepository
                .Entities
                .ToDictionary(er => Enum.Parse<Events>(er.Name), er => er);
        }

        public async Task Log(Events ev)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var history = new History
            {
                Event = _events[ev],
                User = user,
            };

            await _historyRepository.Add(history);

            await _historyRepository.Save();

            return;
        }
    }
}
