using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface Icontrole_acessoRepository
    {
        void Add(controle_acesso controle_acesso);
        IEnumerable<controle_acesso> GetAll();
        controle_acesso Find(int id);
        void Remove(int id);
        void Update(controle_acesso controle_acesso);
    }
}