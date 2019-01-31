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
    public class TestViagem
    {
        private readonly DataDbContext context;
        private readonly ViagemController controller;
        public TestViagem()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-B6D4O8M; Database= Sistema-Viagem; User Id=sa; Password=1234;");

            context = new DataDbContext(optionsBuilder.Options);
            var viagemRepo = new ViagemRepository(context);
            controller = new ViagemController(viagemRepo);
        }
        [TestCategory("Controle de Testes Viagem")]
        
         [TestMethod]
        public void deve_salvar_objeto_perfeito_com_despesa()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
                despesas = new List<ViagemDespesa>()
           };
           var despesas = new ViagemDespesa()
           {
               DataLancamento = new DateTime(2019,3,20),
               Historico = "alimentação",
               Valor = 500,
               Tipo = 1
           };
            viagem.despesas.Add(despesas);
            controller.Create(viagem);
            Assert.IsTrue(viagem.Id >0);
        }      
        [TestMethod]
         public void deve_salvar_TotalBruto_resultado_PrecoUnitario_mais_ToneladaCarga()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
           };
            controller.Create(viagem);
            Assert.IsTrue(viagem.ValorTotalBruto == (viagem.ToneladaPrecoUnitario * viagem.ToneladaCarga));
        }
        [TestMethod]
        public void nao_deve_salvar_dataChegada_inferior_DataSaida()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,3),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
           };
            controller.Create(viagem);
            Assert.AreEqual(0, viagem.Id);
        }
        [TestMethod]
        public void nao_deve_salvar_cidadeOrigem_igual_cidadeDestino()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 75,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
           };
            controller.Create(viagem);
            Assert.AreEqual(0, viagem.Id);
        }
        [TestMethod]
        public void nao_deve_salvar_ToneladaPrecoUnitario_menor_que_1()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 900,
                ToneladaPrecoUnitario = 0,
                ToneladaCarga = 90,
           };
            controller.Create(viagem);
            Assert.AreEqual(0, viagem.Id);
        }
        [TestMethod]
        public void nao_deve_salvar_ToneladaCarga_menor_que_1()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,3),
                OrigemCidadeId = 75,
                DestinoCidadeId = 900,
                ToneladaPrecoUnitario = 90,
                ToneladaCarga = 0,
           };
            controller.Create(viagem);
            Assert.AreEqual(0, viagem.Id);
        }
        [TestMethod]
        public void nao_deve_salvar_VeiculoId_faltando()
        {
            var viagem = new Viagem()
           {
                
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
           };
            controller.Create(viagem);
            Assert.IsFalse( viagem.Id > 0);
        }
        [TestMethod]
        public void nao_deve_salvar_MotoristaId_faltando()
        {
            var viagem = new Viagem()
           {   
                VeiculoId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
           };
            controller.Create(viagem);
            Assert.IsFalse(viagem.Id > 0);
        }
         [TestMethod]
         public void nao_deve_salvar_dataSaida_faltando()
        {
            var viagem = new Viagem()
           {
                MotoristaId = 8, 
                VeiculoId = 8,
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
           };
            controller.Create(viagem);
            Assert.AreEqual(0, viagem.Id);
        }
        [TestMethod]
         public void nao_deve_salvar_dataChegada_faltando()
        {
            var viagem = new Viagem()
           {
                MotoristaId = 8, 
                VeiculoId = 8,
                DataSaida = new DateTime(2019,3,4),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
           };
            controller.Create(viagem);
            Assert.AreEqual(0, viagem.Id);
        }
        [TestMethod]
         public void nao_deve_salvar_CidadeOrigem_faltando()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90
           };
            controller.Create(viagem);
            Assert.IsFalse(viagem.Id >0);
        }
        [TestMethod]
        public void nao_deve_salvar_CidadeDestino_faltando()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90
           };
            controller.Create(viagem);
            Assert.IsFalse(viagem.Id >0);
        }
        [TestMethod]
        public void nao_deve_salvar_só_salvar_quando_a_despesa_estiver_correta()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
                despesas = new List<ViagemDespesa>()
           };
           var despesas = new ViagemDespesa()
           {
               DataLancamento = new DateTime(2019,3,4),
               Historico = "Gasolina",
               Valor = 0,
               Tipo = 1
           };
           viagem.despesas.Add(despesas);
            controller.Create(viagem);
            Assert.IsFalse(viagem.Id >0);
        }
        [TestMethod]
        public void nao_deve_salvar_dataLancamento_posterior_a_DataChegada()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
                despesas = new List<ViagemDespesa>()
           };
           var despesas = new ViagemDespesa()
           {
               DataLancamento = new DateTime(2019,3,21),
               Historico = "Gasolina",
               Valor = 500,
               Tipo = 1
           };
           viagem.despesas.Add(despesas);
            controller.Create(viagem);
            Assert.IsFalse(viagem.Id >0);
        }
        [TestMethod]
        public void nao_deve_salvar_historico_menor_que_5_caracteres()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
                despesas = new List<ViagemDespesa>()
           };
           var despesas = new ViagemDespesa()
           {
               DataLancamento = new DateTime(2019,3,21),
               Historico = "baba",
               Valor = 500,
               Tipo = 1
           };
           viagem.despesas.Add(despesas);
            controller.Create(viagem);
            Assert.IsFalse(viagem.Id >0);
        }
        [TestMethod]
        public void nao_deve_salvar_valor_da_despesa_deve_Ser_maior_que_zero()
        {
            var viagem = new Viagem()
           {
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
                despesas = new List<ViagemDespesa>()
           };
           var despesas = new ViagemDespesa()
           {
               DataLancamento = new DateTime(2019,3,21),
               Historico = "Gasolina",
               Valor = 0,
               Tipo = 1
           };
           viagem.despesas.Add(despesas);
            controller.Create(viagem);
            Assert.IsFalse(viagem.Id >0);
        }
        [TestMethod]
        public void deveSalvar_verificar_preco_bruto()
        {


            var viagem = new Viagem()
            {   
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2019,3,4),
                DataChegada = new DateTime(2019,3,20),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
                despesas = new List<ViagemDespesa>()
            };

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 3, 16),
                Historico       = "Lazer",
                Valor           = 150,
                Tipo            = 1
            };
            
            viagem.despesas.Add(despesa);
          
            controller.Create(viagem);

            Assert.IsTrue(viagem.ValorTotalBruto == (viagem.ToneladaPrecoUnitario * viagem.ToneladaCarga));
        }
        [TestMethod]
        public void deve_salvar_objeto_perfeito_com_combustivel()
        {
            var viagem = new Viagem()
            {   
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2018,12,21),
                DataChegada = new DateTime(2019,1,1),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
                despesas = new List<ViagemDespesa>(),
                combustivel = new List<CombustivelDTO>()
            };


            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 150,
                Tipo            = 1
            };
            var ipiranga = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Ipiranga",
                Valor           = 300,
                Tipo            = 2
            };
            var powerShell = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "PowerShell",
                Valor           = 500,
                Tipo            = 2
            };

            viagem.despesas.Add(despesa);
            viagem.combustivel.Add(ipiranga);
            viagem.combustivel.Add(powerShell);

            controller.Create(viagem);
            
            double total = 0;

            viagem.combustivel.ForEach(item => {
                total += item.Valor;
            });

            Assert.IsTrue(viagem.Id > 0);
        }
        [TestMethod]
        public void nao_deve_salvar_historico_combustivel_menor_que_5_caracteres()
        {
            var viagem = new Viagem()
            {   
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2018,12,21),
                DataChegada = new DateTime(2019,1,1),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
                despesas = new List<ViagemDespesa>(),
                combustivel = new List<CombustivelDTO>()
            };


            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 150,
                Tipo            = 1
            };
            var ipiranga = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "ipi",
                Valor           = 300,
                Tipo            = 2
            };
            var powerShell = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "PowerShell",
                Valor           = 500,
                Tipo            = 2
            };

            viagem.despesas.Add(despesa);
            viagem.combustivel.Add(ipiranga);
            viagem.combustivel.Add(powerShell);

            controller.Create(viagem);
            
            double total = 0;

            viagem.combustivel.ForEach(item => {
                total += item.Valor;
            });

            Assert.IsFalse(viagem.Id > 0);
        }
        [TestMethod]
        public void nao_deve_salvar_combustivel_0()
        {
            var viagem = new Viagem()
            {   
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2018,12,21),
                DataChegada = new DateTime(2019,1,1),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
                despesas = new List<ViagemDespesa>(),
                combustivel = new List<CombustivelDTO>()
            };


            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 150,
                Tipo            = 1
            };
            var ipiranga = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Ipiranga",
                Valor           = 300,
                Tipo            = 2
            };
            var powerShell = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "PowerShell",
                Valor           = 0,
                Tipo            = 2
            };

            viagem.despesas.Add(despesa);
            viagem.combustivel.Add(ipiranga);
            viagem.combustivel.Add(powerShell);

            controller.Create(viagem);
            
            double total = 0;

            viagem.combustivel.ForEach(item => {
                total += item.Valor;
            });

            Assert.IsFalse(viagem.Id > 0);
        }
        [TestMethod]
        public void deve_salvar_verificar_valor_combustivel()
        {
            var viagem = new Viagem()
            {   
                VeiculoId = 8,
                MotoristaId = 8,
                DataSaida = new DateTime(2018,12,21),
                DataChegada = new DateTime(2019,1,1),
                OrigemCidadeId = 75,
                DestinoCidadeId = 244,
                ToneladaPrecoUnitario = 20,
                ToneladaCarga = 90,
                despesas = new List<ViagemDespesa>(),
                combustivel = new List<CombustivelDTO>()
            };


            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 150,
                Tipo            = 1
            };
            var ipiranga = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Ipiranga",
                Valor           = 300,
                Tipo            = 2
            };
            var powerShell = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "PowerShell",
                Valor           = 500,
                Tipo            = 2
            };

            viagem.despesas.Add(despesa);
            viagem.combustivel.Add(ipiranga);
            viagem.combustivel.Add(powerShell);

            controller.Create(viagem);
            
            double total = 0;

            viagem.combustivel.ForEach(item => {
                total += item.Valor;
            });

            Assert.IsTrue(viagem.ValorTotalCombustivel == total);
        }
    }
}