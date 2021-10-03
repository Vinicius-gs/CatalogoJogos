using CatalagoJogos.InputModel;
using CatalagoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalagoJogos.Services
{
    public interface IJogoService
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);

        Task<JogoViewModel> Obter(Guid id);

        Task<JogoViewModel> Inserir(JogoInputModel jogo);

        Task Atualizar(Guid id, JogoInputModel jogo);

        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
