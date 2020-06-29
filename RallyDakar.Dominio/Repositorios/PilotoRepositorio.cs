using RallyDakar.Dominio.DbContexto;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyDakar.Dominio.Repositorios
{
    public class PilotoRepositorio : IPilotoRepositorio
    {
        private readonly RallyDbContexto _rallyDbContexto;

        public PilotoRepositorio(RallyDbContexto rallyDbContexto)
        {
            _rallyDbContexto = rallyDbContexto;
        }

        public void Adicionar(Piloto piloto)
        {
            _rallyDbContexto.Pilotos.Add(piloto);
            _rallyDbContexto.SaveChanges();
        }

        public IEnumerable<Piloto> ObterTodos()
        {
            return _rallyDbContexto.Pilotos.ToList();
        }

        public IEnumerable<Piloto> ObterPilotosPorNome(string nome)
        {
            return _rallyDbContexto.Pilotos.Where(p => p.Nome.Contains(nome)).ToList();
        }

        public Piloto Obter(int id)
        {
            return _rallyDbContexto.Pilotos.FirstOrDefault(p => p.Id == id);
        }

        public bool Existe(int id)
        {
            return _rallyDbContexto.Pilotos.Any(p => p.Id == id);
        }

        public void Atualizar(Piloto piloto)
        {
            _rallyDbContexto.Attach(piloto);
            _rallyDbContexto.Entry(piloto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _rallyDbContexto.SaveChanges();
        }

        public void Deletar(Piloto piloto)
        {
            _rallyDbContexto.Pilotos.Remove(piloto);
            _rallyDbContexto.SaveChanges();
        }
    }
}
