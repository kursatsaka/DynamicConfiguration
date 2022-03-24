using System;
using System.Collections.Generic;

namespace ServiceA.API.Dtos
{
    public class ConfigDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public string ApplicationName { get; set; }

        public bool IsActive { get; set; }
    }
}
