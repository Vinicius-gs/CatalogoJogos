using System;
using System.Runtime.Serialization;

namespace CatalagoJogos.Exception
{

    public class JogoJaCadastradoExcepiton : SystemException
    {
        public JogoJaCadastradoExcepiton()
           : base("Este jogo já está cadastrado")
        { }
    }

}

