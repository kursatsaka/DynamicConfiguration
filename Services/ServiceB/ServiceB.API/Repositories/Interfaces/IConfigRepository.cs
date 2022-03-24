using Core.ControllerBases.Dtos;
using ServiceB.API.Dtos;
using System.Threading.Tasks;

namespace ServiceB.API.Repositories.Interfaces
{
    public interface IConfigRepository
    {
        public Task<Response<string>> GetValue(string key);
        public Response<bool> SetValue(string key, string value);
    }
}
