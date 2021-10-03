using System;
using System.Runtime.Serialization;

namespace CatalagoJogos.Exception
{
    public class JogonaCadastradoExcepiton : SystemException
    {
        public JogonaCadastradoExcepiton()
            : base("Este jogo não está cadastrado")
        { }

    }
}
