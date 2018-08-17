using DemoBlazorCodingNight.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlazorCodingNight.Server.Services
{
    public interface IContatoService
    {
        Contato Obter(long id);
        IEnumerable<Contato> ListarTodos();
        IEnumerable<Contato> Buscar(string texto);
        Contato Incluir(Contato contato);
        void Atualizar(Contato contato);
        void Excluir(long id);
    }
}
