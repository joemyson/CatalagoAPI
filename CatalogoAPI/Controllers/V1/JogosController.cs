using CatalogoAPI.InputModel;
using CatalogoAPI.Service;
using CatalogoAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CatalogoAPI.Exceptions;


/// <summary>
/// criado a controle com uso do CRUD PARA INTERAÇÃO ,COM USO DE METODOS ASSICRONOS
/// CRIAÇÃO DE CAMADA DE VERSOES
/// USO DO STRUCT "GUID"PARA PREENCHIMENTO
/// </summary>
namespace CatalogoAPI.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
              
    {

        private readonly IjogoService _jogoService;

        public JogosController(IjogoService jogoService)
        {
         
         
            _jogoService = jogoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<IEnumerable<JogoViewModel>>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1,50)] int quantidade =5)// metodo assicrono retorna lista //saida de dados uso de JogoViewModel
        {

            var jogos = await _jogoService.Obter( pagina, quantidade);
            if(jogos.Count() == 0)
            {

                return NoContent(); 

            }
            return Ok(jogos);
        }

       
        [HttpGet("{idJogo: guid}")]// uso de "guid" preeenchimento aleatorio
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)// retorna
        {
            var jogo = await _jogoService.Obter(idJogo);

            if(jogo == null)
            {

                return NoContent();
            }


            return Ok(jogo);
        }


        [HttpPost]
        public async Task<ActionResult<List<JogoViewModel>>> Inserir([FromBody] JogoInputModel jogoInputModel)//recebe     
        { 
        try{
                var jogo = await _jogoService.Inserir(jogoInputModel);

                return Ok(jogo);
        }
            catch(JogoCadastradoException e)
           
            {


                return UnprocessableEntity("ja existe um jogo com nome para esta Produtora");
        
        
        }
        
        
        return Ok();
        }
        
        
        [HttpPut("{idJogo: guid}")]
        public async Task<ActionResult> Atualizar([FromRoute] Guid idJogo , [FromBody] JogoInputModel jogo) // entrada de dados 
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogo);

                return Ok();

            }
            catch(JogoCadastradoException ex)
           
            {

                return NotFound("Não Existe este jogo");
            }

          
        }
       
        
        [HttpPatch("{idJogo: guid}/preco/{preco:double")]
        public async Task<ActionResult> Atualizar([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, preco);

                return Ok();

            }
            catch(JogoCadastradoException ex)
          
            {

                return NotFound("Não Existe este jogo");
            }

            return Ok();
        }
        
        
        
        [HttpPut("{idJogo: guid}")]
        public async Task<ActionResult> Remover(Guid idJogo)
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();

            }
            catch(JogoNaoCadastradoException ex)
           
            {
                return NotFound("Não existe este jogo");
            }
        }
    }
}
