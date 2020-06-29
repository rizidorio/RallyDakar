using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyDakar.Dominio.Entidades;
using System.Linq;

namespace RallyDakar.Dominio.Testes.Temporadas
{
    [TestClass]
    public class AdicionarEquipeNaoAceitaNuloTeste
    {
        Temporada temporada;
        Equipe equipe1 = null;

        [TestInitialize]
        public void Initialize()
        {
            temporada = new Temporada
            {
                Id = 1,
                Nome = "Temporada2020"
            };

            temporada.AdicionarEquipe(equipe1);
        }

        [TestMethod]
        public void EquipeNuloNaoAdicionado()
        {
            Assert.IsTrue(temporada.Equipes.Count() == 0);
        }
    }
}
