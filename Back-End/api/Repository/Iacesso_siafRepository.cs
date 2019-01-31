using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface Iacesso_siafRepository
    {
        void Add(acesso_siaf acesso_siaf);
        IEnumerable<acesso_siaf> GetAll();
        acesso_siaf Find(int id);
        void Remove(int id);
        void Update(acesso_siaf acesso_siaf);
    }
}