using Core.ControllerBases;
using ServiceB.API.Repositories.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ServiceBController : CustomBaseController
    {
        #region constructor
        private readonly IConfigRepository _configRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ServiceBController(
                    IConfigRepository configRepository,
                    IPublishEndpoint publishEndpoint)
        {
            _configRepository = configRepository;
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
            var response = _configRepository.SetValue(key, value);
            return CreateActionResultInstance(response);
        }

    }
}
