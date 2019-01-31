using System;
using api.Models;
using api.Repository;
using api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace api.test
{
    [TestClass]
    public class TestMotorista
    {
        private readonly DataDbContext context;
        private readonly  MotoristaController controller;
        
        public TestMotorista()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-B6D4O8M; Database= Sistema-Viagem; User Id=sa; Password=1234;");

            context = new DataDbContext(optionsBuilder.Options);
            var motoristaRepo = new MotoristaRepository(context);
            controller = new MotoristaController(motoristaRepo);
            
        }
        [TestCategory("Controle de Testes Motorista")]
        [TestMethod]
        public void deve_salvar_motorista_perfeito()
        {
            var motorista = new Motorista()
            {
                Nome = "Motorista"
            };
            controller.Create(motorista);
            Assert.AreNotEqual(0, motorista.Id);
        }
         [TestMethod]
        public void nome_motorista_menor_que_3_caracteres()
        {
            var motorista = new Motorista()
            {
                Nome = "Mo"
            };
            controller.Create(motorista);
            Assert.AreEqual(0, motorista.Id);
        }
        [TestMethod]
        public void nao_salvar_objeto_nulo()
        {
            var motorista = new Motorista();
    
            controller.Create(motorista);
            Assert.AreEqual(0, motorista.Id);
        }

        [TestMethod]
        public void nao_deve_atualizar_caracteres_menor_que_3()
        {
            var motorista = new Motorista()
            {
                Nome = "Joao"
            };

            var motorista2 = new Motorista()
            {
                Nome = "mo"
            };

            controller.Create(motorista);
            controller.Update(motorista.Id, motorista2);
            Assert.AreNotEqual(motorista.Nome, motorista2.Nome);
        }
         [TestMethod]
        public void deve_atualizar_o_nome()
        {
            var motorista = new Motorista()
            {
                Nome = "Joao"
            };

            var motorista2 = new Motorista()
            {
                Nome = "zezin"
            };

            controller.Create(motorista);
            controller.Update(motorista.Id, motorista2);
            Assert.AreEqual(motorista.Nome, motorista2.Nome);
        }
    }
}
