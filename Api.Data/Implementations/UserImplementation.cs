using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    //A classe UserImplementation está estendendo a classe BaseRepository<UserEntity> + A IUserRepository estende a IRepository
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        // variável privada para armazenar o contexto do banco de dados.
        private readonly MyContext _context; // Suponha que MyContext seja o contexto do banco de dados.

        // Construtor da classe que recebe o contexto do banco de dados por injeção de dependência.
        public UserImplementation(MyContext context) : base(context)
        {
            _context = context;
            /* Este construtor recebe um objeto 'context' do tipo 'MyContext' como parâmetro. 
   O construtor da classe base (BaseRepository) é chamado com 'context' como argumento. 
   Isso é feito através da sintaxe ': base(context)'. 

   A classe base (BaseRepository) usa esse 'context' para realizar operações no banco de dados.

   Após a chamada ao construtor da classe base, o controle retorna para este construtor. 
   Aqui, atribuímos o 'context' recebido ao campo '_context' desta classe. 

   É importante notar que o '_context' nesta classe (UserImplementation) e na classe base (BaseRepository) 
   referem-se ao mesmo objeto. Portanto, qualquer alteração no estado do '_context' em uma classe 
   será refletida na outra. Isso garante que ambas as classes estejam trabalhando com a mesma instância do contexto do banco de dados.*/

        }

        public async Task<UserEntity> FindByLogin(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            /* podemos também fazer: return await _context.Set<UserEntity>().FirstOrDefaultAsync(u=>u.Email==email);
            
            1. _context.Users.FirstOrDefaultAsync(u => u.Email == email); : Aqui, você está acessando diretamente a propriedade Users do contexto do banco de dados. Isso pressupõe que você tem uma propriedade Users definida em sua classe de contexto.

            2. _context.Set<UserEntity>().FirstOrDefaultAsync(u => u.Email == email); : Aqui, você está usando o método Set<T>() para obter o DbSet correspondente ao tipo UserEntity. Isso é útil quando você quer escrever código que possa lidar com diferentes tipos de entidades de maneira genérica. */
        }
    }
}
