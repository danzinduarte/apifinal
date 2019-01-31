using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class acesso_siafRepository : Iacesso_siafRepository   
    {
        private readonly DataDbContext _context;
        public acesso_siafRepository(DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(acesso_siaf acesso_siaf)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.acesso_siaf.Add(acesso_siaf);
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
        public acesso_siaf Find(int id)
        {
            return _context.acesso_siaf.FirstOrDefault(u => u.id == id);
        }

        public IEnumerable<acesso_siaf> GetAll()
        {
            return _context.acesso_siaf.AsNoTracking().ToList();
        }

        public void Remove(int id)
        {
            var transaction = _context.Database.BeginTransaction();
            try {
                var acesso = _context.acesso_siaf.First(u => u.id == id);
                _context.acesso_siaf.Remove(acesso);
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

        public void Update(acesso_siaf acesso_siaf)
        {
            var transaction = _context.Database.BeginTransaction();
            try 
            { 
                _context.acesso_siaf.Update(acesso_siaf);
                _context.SaveChanges();
                transaction.Commit();
            } 
            catch (Exception e) 
            {
                Console.WriteLine("Erro");
                Console.WriteLine(e);
                transaction.Rollback();
                throw new System.Net.WebException (string.Format("Falha ao atualizar dados de acesso"));
            }
        }
    }
}