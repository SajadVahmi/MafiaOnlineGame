using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Administration.Api.Test.Integration
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddTestConfiguration(this IConfigurationBuilder builder,string configurationFileName)
        {
            if (!File.Exists(configurationFileName))
                throw new Exception("There is'nt exist any {configurationFileName} .");
 
           builder.AddJsonFile(configurationFileName);

            return builder;
        }
    }
}
