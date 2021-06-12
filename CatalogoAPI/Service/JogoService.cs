using CatalogoAPI.Entities;
using CatalogoAPI.Exceptions;
using CatalogoAPI.InputModel;
using CatalogoAPI.Repositories;
using CatalogoAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoAPI.Service
{
    public class JogoService :IjogoService
    {
        public readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quatidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quatidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();

        }
      
        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogos = await _jogoRepository.Obter(id);

            if(jogos == null)
            
                return null;


            return new JogoViewModel
            {
                Id = jogos.Id,
                Nome = jogos.Nome,
                Produtora = jogos.Produtora,
                Preco = jogos.Preco
            };



        }
      
        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if(entidadeJogo.Count() > 0)
            {
                throw new JogoCadastradoException();
            }

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome= jogo.Nome,
                Produtora=jogo.Produtora,
                Preco= jogo.Preco

            };

            await _jogoRepository.Inserir(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }
      
        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if (entidadeJogo == null)

               
                throw new JogoCadastradoException();
            
            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoRepository.Atualizar(entidadeJogo);
        }
       
        
        
        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if (entidadeJogo == null)

                throw new JogoCadastradoException();
            entidadeJogo.Preco = preco;

            await _jogoRepository.Atualizar(entidadeJogo);





        }
       public async Task Remover(Guid id)
        {
            var jogo = _jogoRepository.Obter(id);

            if (jogo == null)

                throw new JogoNaoCadastradoException();
            await _jogoRepository.Remover(id);

        }
        public void Dispose()
        {


            _jogoRepository?.Dispose();


        }

    }
}
