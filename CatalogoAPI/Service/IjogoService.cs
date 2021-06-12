using CatalogoAPI.InputModel;
using CatalogoAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoAPI.Service
{
    public interface IjogoService // contrato
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quatidade);
        Task<JogoViewModel> Obter(Guid id);
        Task<JogoViewModel> Inserir(JogoInputModel jogoInputModel);
        Task Atualizar(Guid id, JogoInputModel jogo);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);

    }
}
