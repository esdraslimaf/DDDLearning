using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// Com CrossCutting nossa camada de Aplicação não tem referência alguma a Data, apenas a CrossCutting, além de ficar tudo separada com suas devidas funções, embora o D.I pudesse ser configurado no próprio program.cs
namespace Api.CrossCutting.DependencyInjection
{

    public static class ConfigureRepository //Vamos colocar esse método abaixo no program.cs
    {
        public static void ConfiguracaoDependenciaRepositorio(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>)); //Scoped pq uma conexão com banco tem que ser scoped
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();   //A utilização de typeof é necessária porque estamos trabalhando com tipos genéricos e desejamos registrar uma dependência genérica no contêiner de injeção de dependência (DI).

            serviceCollection.AddDbContext<MyContext>(options =>
 {
     options.UseMySql("Server=localhost;Port=3306;Database=dbApiDDD;Uid=root;Pwd=admin", ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=dbApiDDD;Uid=root;Pwd=admin"));
 });

        }


    }

}

