using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RallyDakar.Dominio.Entidades;
using System;

namespace RallyDakar.Dominio.DbContexto
{
    public class BaseDados
    {
        public static void CargaInicial(IServiceProvider serviceProvider)
        {
            using(var context = new RallyDbContexto(serviceProvider.GetRequiredService<DbContextOptions<RallyDbContexto>>()))
            {
                var temporada = new Temporada
                {
                    Id = 1,
                    Nome = "Temporada2020",
                    DataInicio = DateTime.Now,
                };

                var equipe = new Equipe
                {
                    Id = 1,
                    Nome = "Equipe Azul",
                    Identificador = "AZL",
                };

                var pilotoPedro = new Piloto
                {
                    Id = 1,
                    Nome = "Pedro",
                };

                var pilotoLuiz = new Piloto
                {
                    Id = 2,
                    Nome = "Luiz",
                };

                equipe.AdicionarPiloto(pilotoPedro);
                equipe.AdicionarPiloto(pilotoLuiz);

                temporada.AdicionarEquipe(equipe);

                context.Temporadas.Add(temporada);
                context.SaveChanges();
            }
        }
    }
}
