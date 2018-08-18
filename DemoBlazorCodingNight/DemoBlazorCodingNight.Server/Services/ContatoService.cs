using System;
using System.Collections.Generic;
using System.Linq;
using DemoBlazorCodingNight.Server.Data;
using DemoBlazorCodingNight.Server.Exceptions;
using DemoBlazorCodingNight.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoBlazorCodingNight.Server.Services
{
    public class ContatoService : IContatoService
    {
        private CodingNightContext _context;

        public ContatoService(CodingNightContext context)
        {
            _context = context;
        }

        public void Atualizar(Contato contato)
        {
            ValidarContato(contato);

            var dbContato = _context.Contatos.FirstOrDefault(x => x.Id == contato.Id);

            if (dbContato == null)
                throw new NaoEncontradoException($"Contato com ID {contato.Id} não encontrado");

            dbContato.Nome = contato.Nome;
            dbContato.Email = contato.Email;
            dbContato.Observacoes = contato.Observacoes;
            dbContato.TelefoneCel = contato.TelefoneCel;
            dbContato.TelefoneRes = contato.TelefoneRes;

            _context.Entry(dbContato).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<Contato> Buscar(string texto)
        {
            return _context.Contatos.Where(x => x.Nome.Contains(texto, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Excluir(long id)
        {
            var dbContato = _context.Contatos.FirstOrDefault(x => x.Id == id);

            if (dbContato == null)
                throw new NaoEncontradoException($"Contato com ID {id} não encontrado");

            _context.Contatos.Remove(dbContato);
            _context.SaveChanges();
        }

        public Contato Incluir(Contato contato)
        {
            ValidarContato(contato);

            _context.Contatos.Add(contato);
            _context.SaveChanges();

            return contato;
        }

        public IEnumerable<Contato> ListarTodos()
        {
            return _context.Contatos.ToList();
        }

        public Contato Obter(long id)
        {
            return _context.Contatos.FirstOrDefault(x => x.Id == id);
        }

        private void ValidarContato(Contato contato)
        {
            List<string> erros = new List<string>();

            if (String.IsNullOrWhiteSpace(contato.Nome))
            {
                erros.Add("Nome do contato não pode ser nulo ou em branco");
            }

            if (contato.Nome != null && contato.Nome.Length < 3)
            {
                erros.Add("Nome do contato deve ter ao menos 3 caracteres");
            }

            if (contato.Nome != null && contato.Nome.Length > 50)
            {
                erros.Add("Nome do contato deve ter no máximo 50 caracteres");
            }

            if (erros.Count > 0)
            {
                throw new FalhaDeValidacaoException("Ocorreram erros de validação", erros);
            }
        }
    }
}
