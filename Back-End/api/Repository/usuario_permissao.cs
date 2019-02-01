using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            var teste =  _context.usuario_permissao
            .Include(u => u.usuario)
            .FirstOrDefault(u => u.id == id);
            
           if (teste == null)
            return teste;

            var permissao = new usuario_permissao();
            permissao.id = teste.id;
            permissao.rotina = teste.rotina;
            permissao.usuario = teste.usuario;
            permissao.usuario_id = teste.usuario_id;
            permissao.usuario.nome = teste.usuario.nome;

            return permissao;

        }

        public IEnumerable<usuario_permissao> GetAll()
        {
            var teste = _context.usuario_permissao
            .Include(u => u.usuario)
            .ToList();
            return teste;
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