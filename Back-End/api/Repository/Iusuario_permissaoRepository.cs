using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface Iusuario_permissaoRepository
    {
        void Add(usuario_permissao usuario_permissao);
        IEnumerable<usuario_permissao> GetAll();
        usuario_permissao Find(int id);
        void Remove(int id);
        void Update(usuario_permissao usuario_permissao);
    }
}