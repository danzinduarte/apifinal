using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class usuario_logRepository : Iusuario_logRepository   
    {
        private readonly DataDbContext _context;
        public usuario_logRepository(DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(usuario_log usuario_log)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.usuario_log.Add(usuario_log);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    return;
                }
            }
        }
        public usuario_log Find(int id)
        {
            return _context.usuario_log.FirstOrDefault(u => u.id == id);
        }

        public IEnumerable<usuario_log> GetAll()
        {
            return _context.usuario_log.AsNoTracking().ToList();
        }

        public void Remove(int id)
        {
            var transaction = _context.Database.BeginTransaction();
            try {
                var usuario_log = _context.usuario_log.First(u => u.id == id);
                _context.usuario_log.Remove(usuario_log);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e) 
            {
                Console.WriteLine("Erro");
                Console.WriteLine(e);
                transaction.Rollback();
                return;
            }
        }

        public void Update(usuario_log usuario_log)
        {
            var transaction = _context.Database.BeginTransaction();
            try 
            { 
                _context.usuario_log.Update(usuario_log);
                _context.SaveChanges();
                transaction.Commit();
            } 
            catch (Exception e) 
            {
                Console.WriteLine("Erro");
                Console.WriteLine(e);
                transaction.Rollback();
                throw new System.Net.WebException (string.Format("Falha ao atualizar dados do Usuario"));
            }
        }
    }
}