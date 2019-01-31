using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class controle_acessoRepository : Icontrole_acessoRepository   
    {
        private readonly DataDbContext _context;
        public controle_acessoRepository(DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(controle_acesso controle_acesso)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.controle_acesso.Add(controle_acesso);
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
        public controle_acesso Find(int id)
        {
            return _context.controle_acesso.FirstOrDefault(u => u.id == id);
        }

        public IEnumerable<controle_acesso> GetAll()
        {
            return _context.controle_acesso.AsNoTracking().ToList();
        }

        public void Remove(int id)
        {
            var transaction = _context.Database.BeginTransaction();
            try {
                var controle_acesso = _context.controle_acesso.First(u => u.id == id);
                _context.controle_acesso.Remove(controle_acesso);
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

        public void Update(controle_acesso controle_acesso)
        {
            var transaction = _context.Database.BeginTransaction();
            try 
            { 
                _context.controle_acesso.Update(controle_acesso);
                _context.SaveChanges();
                transaction.Commit();
            } 
            catch (Exception e) 
            {
                Console.WriteLine("Erro");
                Console.WriteLine(e);
                transaction.Rollback();
                throw new System.Net.WebException (string.Format("Falha ao atualizar dados"));
            }
        }
    }
}