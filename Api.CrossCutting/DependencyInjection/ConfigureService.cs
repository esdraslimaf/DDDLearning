using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureService //Vamos colocar esse método abaixo no program.cs
    {
       public static void ConfiguracaoDependenciaService(IServiceCollection serviceCollection){
        serviceCollection.AddTransient<IUserService, UserService>(); //Ou poderia ser Scoped também
       }
    }
}
