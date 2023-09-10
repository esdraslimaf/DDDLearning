using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity // Interface genérica onde T = BaseEntity(Qualquer que herde de BaseEntity)
    { //Métodos assincronos 
        Task<T> InsertAsync(T item); //Inserir
        Task<T> UpdateAsync(T item); //Atualizar
        Task<bool> DeleteAsync(Guid id); //Deletar
        Task<T> SelectAsync(Guid id); //Buscar por id
        Task<IEnumerable<T>> SelectAsync(); //Buscar todos
    }
}
