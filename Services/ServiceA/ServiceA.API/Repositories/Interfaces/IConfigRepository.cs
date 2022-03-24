using Core.ControllerBases.Dtos;
using Core.Models;
using ServiceA.API.Dtos;
using System.Threading.Tasks;

namespace ServiceA.API.Repositories.Interfaces
{
    public interface IConfigRepository
    {
        public Task<Response<string>> GetValue(string key);
        public Response<bool> SetValue(string key,string value);
    }
}
