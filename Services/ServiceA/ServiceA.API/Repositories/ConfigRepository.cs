﻿using AutoMapper;
using Core.ControllerBases.Dtos;
using ServiceA.API.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using DynamicConfigLibrary;
using System.Collections.Generic;
using DynamicConfigLibrary.Models;

namespace ServiceA.API.Repositories
{
    public class ConfigRepository : IConfigRepository
    {
        #region contructor

        private readonly ConfigurationReader _configReader;

        public ConfigRepository(ConfigurationReader configurationReader)
        {
            this._configReader = configurationReader;
        }

        #endregion

        public async Task<Response<string>> GetValue(string key)
        {
            var configDataEntity = await _configReader. GetValue<List<ConfigurationData>>(key);
            var response = configDataEntity.FirstOrDefault(x => x.IsActive)?.Value;
            return Response<string>.Success(response, 200);
        }
        public Response<bool> SetValue(string key,string value)
        {
            var configDataEntity = _configReader.SetValue(key, value);
            return Response<bool>.Success(configDataEntity,200);
        }

    }
}
