using ShowTime.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShowTime.DataAccess.Repositories.Implementations
{
    public class GenericImplement<T> : IRepo<T> where T : class
    {
        private readonly ShowTimeDBContext _context;
        public GenericImplement(ShowTimeDBContext context) {
            _context = context; 
        }

        public virtual async Task Add(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
                }

                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Validation error in Add: {ex.Message}");
                throw; 
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error in Add: {ex.Message}");
                throw new InvalidOperationException("Failed to add entity to database", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in Add: {ex.Message}");
                throw new InvalidOperationException("An unexpected error occurred while adding the entity", ex);
            }
        }

        public virtual async Task Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
                }

                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Validation error in Delete: {ex.Message}");
                throw; 
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error in Delete: {ex.Message}");
                throw new InvalidOperationException("Failed to delete entity from database", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in Delete: {ex.Message}");
                throw new InvalidOperationException("An unexpected error occurred while deleting the entity", ex);
            }
        }


        public virtual async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Database operation error in GetAll: {ex.Message}");
                throw new InvalidOperationException("Failed to retrieve entities from database", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in GetAll: {ex.Message}");
                throw new InvalidOperationException("An unexpected error occurred while retrieving entities", ex);
            }

        }

        public virtual async Task<T?> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
                }

                return await _context.Set<T>().FindAsync(id);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Validation error in GetById: {ex.Message}");
                throw; 
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Database operation error in GetById: {ex.Message}");
                throw new InvalidOperationException($"Failed to retrieve entity with ID {id}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in GetById: {ex.Message}");
                throw new InvalidOperationException($"An unexpected error occurred while retrieving entity with ID {id}", ex);
            }
        }

        public virtual async Task Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
                }

                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Validation error in Update: {ex.Message}");
                throw; 
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Concurrency error in Update: {ex.Message}");
                throw new InvalidOperationException("The entity was modified by another process. Please refresh and try again.", ex);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error in Update: {ex.Message}");
                throw new InvalidOperationException("Failed to update entity in database", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in Update: {ex.Message}");
                throw new InvalidOperationException("An unexpected error occurred while updating the entity", ex);
            }
        }
    }
}
