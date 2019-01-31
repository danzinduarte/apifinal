using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface Iusuario_logRepository
    {
        void Add(usuario_log usuario_log);
        IEnumerable<usuario_log> GetAll();
        usuario_log Find(int id);
        void Remove(int id);
        void Update(usuario_log usuario_log);
    }
}