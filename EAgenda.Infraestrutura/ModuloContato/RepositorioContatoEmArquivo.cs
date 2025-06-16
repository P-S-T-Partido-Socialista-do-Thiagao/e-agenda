﻿using ControleDeBar.Infraestrura.Arquivos.Compartilhado;
using EAgenda.Dominio.ModuloContato;
using EAgenda.Infraestrutura.Compartilhado;

namespace EAgenda.Infraestrutura.ModuloContato
{
    public class RepositorioContatoEmArquivo : RepositorioBaseEmArquivo<Contato>, IRepositorioContato
    {
        public RepositorioContatoEmArquivo(ContextoDados contextoDados) : base(contextoDados)
        {
        }
        protected override List<Contato> ObterRegistros()
        {
            throw new NotImplementedException();
        }
    }
}
