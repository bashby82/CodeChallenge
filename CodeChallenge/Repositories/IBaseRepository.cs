using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface IBaseRepository<T>
    {
        T GetById(string id);
        T Add(T item);
        T Remove(T item);
        Task SaveAsync();
    }
}