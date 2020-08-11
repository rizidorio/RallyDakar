using RallyDakar.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Dominio.Intefaces
{
    public interface IEquipeRepositorio
    {
        void Adicionar(Equipe equipe);
        IEnumerable<Equipe> ObterTodos();
        Equipe Obter(int id);
        bool Existe(int id);
        void Atualizar(Equipe equipe);
        void Deletar(Equipe equipe);
    }
}
