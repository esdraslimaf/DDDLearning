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
        private readonly MyContext _context;
        private readonly DbSet<T> _dataset;

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var Entity = await _dataset.SingleOrDefaultAsync(u => u.Id == id);
                if (Entity == null)
                {
                    return false;
                }
                _dataset.Remove(Entity);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception erro)
            {

                throw new Exception($"Um erro ocorreu. Detalhes: {erro}");
            }
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
                throw new Exception($"Um erro ocorreu. Detalhes: {erro}");
            }

            return item;

        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                var item = await _dataset.SingleOrDefaultAsync(u => u.Id == id);
                if (item != null)
                {
                    return item;
                }
                throw new Exception("Nenhum item encontrado com o ID fornecido.");
            }
            catch (Exception erro)
            {
                throw new Exception($"Um erro ocorreu. Detalhes: {erro}");
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception erro)
            {
                throw new Exception($"Um erro ocorreu. Detalhes: {erro}");
            }
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
                throw new Exception($"Um erro ocorreu. Detalhes: {erro}");
            }
            return item;
        }
    }
}
