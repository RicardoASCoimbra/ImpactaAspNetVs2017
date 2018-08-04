using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oficina.Repositorios.SistemaDeArquivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oficina.Repositorios.SistemaArquivos.Tests
{
    [TestClass()]
    public class MarcaRepositorioTests
    {
        MarcaRepositorio _repositorio = new MarcaRepositorio();

        [TestMethod()]
        public void SelecionarTodosTest()
        {
            var marcas = _repositorio.Selecionar();

            Assert.IsTrue(marcas.Count > 0);
            Assert.IsTrue(marcas[0].Id ==1);
            Assert.IsTrue(marcas[0].Nome == "Fiat");
            Assert.IsTrue(marcas[0].Nome.Equals("Fiat"));

        }

        [TestMethod()]
        [DataRow(1)]
        [DataRow(-1)]
        public void SelecionarPorIdTest(int marcaId)
        {
            var marca = _repositorio.Selecionar(marcaId);

            if (marcaId > 0)
            {
                Assert.AreEqual(marca.Nome, "Fiat");
            }
            else
            {
                Assert.IsNull(marca);
            }
        }
    }
}