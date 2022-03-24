using AutoMapper;
using Core.ControllerBases;
using EventBus.Messages.Events;
using ServiceA.API.Dtos;
using ServiceA.API.Repositories.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ServiceAController : CustomBaseController
    {
        #region constructor
        private readonly IConfigRepository _configRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public ServiceAController(
                    IConfigRepository configRepository,
                    IMapper mapper,
                    IPublishEndpoint publishEndpoint)
        {
            _configRepository = configRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }
        #endregion

        [HttpGet("{key}")]
        public async Task<IActionResult> GetValue(string key)
        {
            var response = await _configRepository.GetValue(key);
            return CreateActionResultInstance(response);
        }
        [HttpPost()]
        public IActionResult SetValue(string key, string value)
        {
            var response =  _configRepository.SetValue(key, value);
            return CreateActionResultInstance(response);
        }

    }
}
