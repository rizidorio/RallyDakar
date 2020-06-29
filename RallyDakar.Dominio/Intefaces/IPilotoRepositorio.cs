using RallyDakar.Dominio.Entidades;
using System.Collections.Generic;

namespace RallyDakar.Dominio.Intefaces
{
    public interface IPilotoRepositorio
    {
        void Adicionar(Piloto piloto);
        IEnumerable<Piloto> ObterTodos();
        Piloto Obter(int id);
        bool Existe(int id);
        void Atualizar(Piloto piloto);
        void Deletar(Piloto piloto);
    }
}
