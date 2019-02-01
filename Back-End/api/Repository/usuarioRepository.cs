using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class usuarioRepository : IusuarioRepository   
    {
        private readonly DataDbContext _context;
        public usuarioRepository(DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(Usuario usuario)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.usuario.Add(usuario);
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
        public Usuario Find(int id)
        {
            return _context.usuario.FirstOrDefault(u => u.id == id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.usuario.AsNoTracking().ToList();
        }

        public void Remove(int id)
        {
            var transaction = _context.Database.BeginTransaction();
            try {
                var usuario = _context.usuario.First(u => u.id == id);
                _context.usuario.Remove(usuario);
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

        public void Update(Usuario usuario)
        {
            var transaction = _context.Database.BeginTransaction();
            try 
            { 
                _context.usuario.Update(usuario);
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