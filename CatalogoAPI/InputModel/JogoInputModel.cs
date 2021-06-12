using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CatalogoAPI.InputModel

    // recebe dataAnnotation a forma de saida

{
    public class JogoInputModel ///entrada de dados 
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O NOME DO JOGO DEVE CONTER ENTRE 3 E 100 CARACTERES")]
        public string Nome { get; set; }

         [Required]
         [StringLength(100, MinimumLength = 1, ErrorMessage = "O NOME DA PRODUTORA DEVE CONTER ENTRE 3 E 100 CARACTERES")]
            public string Produtora { get; set; }
         
        [Required]
        [Range(1,100,ErrorMessage ="O preço minimo deve ser de no monimo 1 rel e no maximo 10000")]
        public double Preco { get; set; }


    }
    }
