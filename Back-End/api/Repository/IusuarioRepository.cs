using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface IusuarioRepository
    {
        void Add(Usuario usuario);
        IEnumerable<Usuario> GetAll();
        Usuario Find(int id);
        void Remove(int id);
        void Update(Usuario usuario);
    }
}