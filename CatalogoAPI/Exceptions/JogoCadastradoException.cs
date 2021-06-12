using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoAPI.Exceptions
{
    public class JogoCadastradoException : Exception
    {
        public JogoCadastradoException():base("Este ja jogo cadastrado")
        { }
    }
}
