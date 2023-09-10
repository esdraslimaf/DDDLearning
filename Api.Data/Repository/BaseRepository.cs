using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context;
        private DbSet<T> _dataset; //Pra não ter que ficar fazendo por exemplo: _context.Set<T>().ToList(); toda vez que necessário(.ToList também  é apenas exemplo da operação, pdoe ser delete, etc).

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }

                item.CreateAt = DateTime.UtcNow;
                _dataset.Add(item);

                await _context.SaveChangesAsync();
                
            }
            catch (Exception erro)
            {
                throw erro;
            }
            
            return item;

        }

        public Task<T> SelectAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> SelectAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var ExisteT = await _dataset.SingleOrDefaultAsync(u => u.Id == item.Id);
                if (ExisteT == null)
                {
                    return null;
                }

                item.UpdateAt = DateTime.UtcNow;
                item.CreateAt = ExisteT.CreateAt;
                _context.Entry(ExisteT).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();

            }
            catch (Exception erro)
            {

                throw erro;
            }
            return item;
        }


    }
}
