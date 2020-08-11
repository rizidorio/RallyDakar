using RallyDakar.Dominio.DbContexto;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Intefaces;
using System.Collections.Generic;
using System.Linq;

namespace RallyDakar.Dominio.Repositorios
{
    public class EquipeRepositorio : IEquipeRepositorio
    {
        private readonly RallyDbContexto _rallyDbContexto;

        public EquipeRepositorio(RallyDbContexto rallyDbContexto)
        {
            _rallyDbContexto = rallyDbContexto;
        }

        public void Adicionar(Equipe equipe)
        {
            _rallyDbContexto.Equipes.Add(equipe);
            _rallyDbContexto.SaveChanges();
        }

        public IEnumerable<Equipe> ObterTodos()
        {
            return _rallyDbContexto.Equipes.ToList();
        }

        public IEnumerable<Equipe> ObterEquipesPorNome(string nome)
        {
            return _rallyDbContexto.Equipes.Where(p => p.Nome.Contains(nome)).ToList();
        }

        public Equipe Obter(int id)
        {
            return _rallyDbContexto.Equipes.FirstOrDefault(p => p.Id == id);
        }

        public bool Existe(int id)
        {
            return _rallyDbContexto.Equipes.Any(p => p.Id == id);
        }

        public void Atualizar(Equipe equipe)
        {
            if (_rallyDbContexto.Entry(equipe).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _rallyDbContexto.Attach(equipe);
                _rallyDbContexto.Entry(equipe).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                _rallyDbContexto.Update(equipe);
            }


            _rallyDbContexto.SaveChanges();
        }

        public void Deletar(Equipe equipe)
        {
            _rallyDbContexto.Equipes.Remove(equipe);
            _rallyDbContexto.SaveChanges();
        }
    }
}
