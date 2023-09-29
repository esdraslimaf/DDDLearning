using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            // Define a string de conexão para o banco de dados MySQL. Isso é usado para criar as migrações em tempo de desenvolvimento.
            var connectionString = "Server=localhost;Port=3306;Database=dbApiDDD;Uid=root;Pwd=admin";

            // Cria uma instância do construtor de opções do DbContext com a string de conexão definida.
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new MyContext(optionsBuilder.Options);
            // Retorna uma nova instância do contexto do banco de dados com as opções definidas. 
            // Este ContextFactory é usado pelo Entity Framework para criar as migrações do banco de dados.
            // Note que isso não tem relação com a injeção de dependência usada para operações do repositório, essa configuração deve ser feita separadamente.
        }
    }
}
