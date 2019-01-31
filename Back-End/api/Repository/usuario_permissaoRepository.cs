using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class usuario_permissaoRepository : Iusuario_permissaoRepository   
    {
        private readonly DataDbContext _context;
        public usuario_permissaoRepository(DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(usuario_permissao usuario_permissao)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.usuario_permissao.Add(usuario_permissao);
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
        public usuario_permissao Find(int id)
        {
            return _context.usuario_permissao.FirstOrDefault(u => u.id == id);
        }

        public IEnumerable<usuario_permissao> GetAll()
        {
            return _context.usuario_permissao.AsNoTracking().ToList();
        }

        public void Remove(int id)
        {
            var transaction = _context.Database.BeginTransaction();
            try {
                var usuario_permissao = _context.usuario_permissao.First(u => u.id == id);
                _context.usuario_permissao.Remove(usuario_permissao);
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

        public void Update(usuario_permissao usuario_permissao)
        {
            var transaction = _context.Database.BeginTransaction();
            try 
            { 
                _context.usuario_permissao.Update(usuario_permissao);
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