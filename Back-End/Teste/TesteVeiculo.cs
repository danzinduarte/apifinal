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
    public class TestVeiculo
    {
        private readonly DataDbContext context;
        private readonly  VeiculoController controller;
        
        public TestVeiculo()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-B6D4O8M; Database= Sistema-Viagem; User Id=sa; Password=1234;");

            context = new DataDbContext(optionsBuilder.Options);
            var veiculoRepo = new VeiculoRepository(context);
            controller = new VeiculoController(veiculoRepo);
            
        }
        [TestCategory("Controle de Testes Veiculo")]
        [TestMethod]
        public void nao_deve_salvar_objeto_null()
        {
            var veiculo = new Veiculo();

            controller.Create(veiculo);

            Assert.AreEqual(0, veiculo.Id);
        }
        [TestMethod]

        public void deve_salvar_veiculo_perfeito()
        {
            var veiculo = new Veiculo()
            {
                Fabricante = "Fiat",
                Modelo = "Toro",
                AnoFabricacao = 2016,
                AnoModelo = 2017,
                Desativado = 0
            };

            controller.Create(veiculo);
            Assert.AreNotEqual(0, veiculo.Id);
        }
        [TestMethod]
        public void nao_deve_salvar_faltando_fabricante()
        {
            var veiculo = new Veiculo()
            {
                Modelo = "Toro",
                AnoFabricacao = 2016,
                AnoModelo = 2017,
                Desativado = 0
            };
            controller.Create(veiculo);
            Assert.AreEqual(0, veiculo.Id);
        }
        [TestMethod]
        public void nao_deve_salvar_faltando_AnoFabricacao()
        {
            var veiculo = new Veiculo()
            {
                Fabricante = "Fiat",
                Modelo = "Toro",
                AnoModelo = 2017,
                Desativado = 0
            };
            controller.Create(veiculo);
            Assert.AreEqual(0, veiculo.Id);
        }
        [TestMethod]
        public void nao_deve_salvar_faltando_AnoModelo()
        {
            var veiculo = new Veiculo()
            {
                Fabricante = "Fiat",
                Modelo = "Toro",
                AnoFabricacao = 2016,
                Desativado = 0
            };
            controller.Create(veiculo);
            Assert.AreEqual(0, veiculo.Id);
        }
        [TestMethod]
        public void nao_deve_salvar_anoFabricacao_menor_que_2000()
        {
            var veiculo = new Veiculo()
            {
                Fabricante = "Fiat",
                Modelo = "Toro",
                AnoFabricacao = 1999,
                AnoModelo = 2000,
                Desativado = 0
            };

            controller.Create(veiculo);
            Assert.AreEqual(0, veiculo.Id);
        }
         [TestMethod]
        public void nao_deve_salvar_anoFabricacao_maior_que_anoModelo()
        {
            var veiculo = new Veiculo()
            {
                Fabricante = "Fiat",
                Modelo = "Toro",
                AnoFabricacao = 2018,
                AnoModelo = 2017,
                Desativado = 0
            };

            controller.Create(veiculo);
            Assert.AreEqual(0, veiculo.Id);
        }
    }
}
