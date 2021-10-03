using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalagoJogos.InputModel;
using CatalagoJogos.Repository;
using CatalagoJogos.ViewModel;
using CatalagoJogos.Exception;
using CatalagoJogos.Entity;

namespace CatalagoJogos.Services
{

    public class JogoService : IJogoService
    {
        public readonly IJogoRepository _JogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _JogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _JogoRepository.Obter(pagina, quantidade);

            return jogos.Select( jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _JogoRepository.Obter(id);
            if (jogo == null)
                return null;

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await _JogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
                throw new JogoJaCadastradoExcepiton();
            

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _JogoRepository.Inserir(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogoInsert.Nome,
                Produtora = jogoInsert.Produtora,
                Preco = jogoInsert.Preco
            };

        }

        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await _JogoRepository.Obter(id);

            if(entidadeJogo == null)
                throw new JogoJaCadastradoExcepiton();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _JogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _JogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoJaCadastradoExcepiton();

            entidadeJogo.Preco = preco;

            await _JogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Remover(Guid id)
        {
            var jogo = await _JogoRepository.Obter(id);

            if (jogo == null)
                throw new JogoJaCadastradoExcepiton();

            await _JogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _JogoRepository?.Dispose();
        }

    }
}
