using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RallyDakar.API.Modelo;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Controllers
{
    [ApiController]
    [Route("api/pilotos")]
    public class PilotoController : ControllerBase
    {
        private readonly IPilotoRepositorio _pilotoRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<PilotoController> _logger;

        public PilotoController(IPilotoRepositorio pilotoRepositorio, IMapper mapper, ILogger<PilotoController> logger)
        {
            _pilotoRepositorio = pilotoRepositorio;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "Obter")]
        public IActionResult Obter(int id)
        {
            try
            {
                _logger.LogInformation($"Obtendo dados do Piloto na base: {id}");
                var piloto = _pilotoRepositorio.Obter(id);

                if (piloto == null)
                {
                    _logger.LogWarning($"Piloto com Id: {id}, não encontrado");
                    return NotFound();
                }

                var pilotoModelo = _mapper.Map<PilotoModelo>(piloto);

                _logger.LogInformation($"Retornando Piloto modelo");
                return Ok(pilotoModelo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor contate o suporte.");
            }
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] PilotoModelo pilotoModelo)
        {
            try
            {
                _logger.LogInformation("Mapeando piloto modelo");
                var piloto = _mapper.Map<Piloto>(pilotoModelo);

                _logger.LogInformation($"Verificando se existe piloto com o id informado {piloto.Id}");
                if (_pilotoRepositorio.Existe(piloto.Id))
                {
                    _logger.LogWarning($"Já existe piloto com a mesma identificação {piloto.Id}");
                    return StatusCode(409, "Já existe piloto com a mesma identificação.");
                }

                _logger.LogInformation("Adicionando piloto");
                _logger.LogInformation($"Nome do piloto: {piloto.Nome}");
                _logger.LogInformation($"Sobrenome do piloto: {piloto.Sobrenome}");
                _pilotoRepositorio.Adicionar(piloto);
                _logger.LogInformation("Operação adicionar Piloto ocorreu sem erros");

                _logger.LogInformation("Mapeamento de retorno");
                var pilotoModeloRetorno = _mapper.Map<PilotoModelo>(piloto);

                _logger.LogInformation("Chamando rotar Obter");
                return CreatedAtRoute("Obter", new { id = piloto.Id }, pilotoModeloRetorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor contate o suporte.");
            }
        }

        [HttpPut]
        public IActionResult Atulizar([FromBody] PilotoModelo pilotoModelo)
        {
            try
            {
                _logger.LogInformation($"Verificando se o piloto {pilotoModelo.Id} existe na base");
                if (!_pilotoRepositorio.Existe(pilotoModelo.Id))
                {
                    _logger.LogWarning($"{pilotoModelo.Id} não foi encontrado");
                    return NotFound();
                }
                    
                var piloto = _mapper.Map<Piloto>(pilotoModelo);

                _logger.LogInformation($"Atualizando a base de dados com o pilotoid: {piloto.Id}");
                _pilotoRepositorio.Atualizar(piloto);

                _logger.LogInformation("Finalizada a operação");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor contate o suporte.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarParcial(int id, [FromBody] JsonPatchDocument<PilotoModelo> patchPilotoModelo)
        {
            try
            {
                _logger.LogInformation($"Executando a atualização em patch do pilotoid: {id}");
                _logger.LogInformation($"Verificando se o pilotoid {id} existe na base");
                if (!_pilotoRepositorio.Existe(id))
                {
                    _logger.LogWarning($"Pilotoid {id} não foi encontrado");
                    return NotFound();
                }

                _logger.LogInformation($"Obetendo instancia com EFCore {id}");
                var piloto = _pilotoRepositorio.Obter(id);

                _logger.LogInformation($"Mapeando para modelo");
                var pilotoModelo = _mapper.Map<PilotoModelo>(piloto);

                _logger.LogInformation($"Aplicando patch");
                patchPilotoModelo.ApplyTo(pilotoModelo);

                piloto = _mapper.Map(pilotoModelo, piloto);

                _logger.LogInformation($"Atualiazndo o pilotoid {id}");
                _pilotoRepositorio.Atualizar(piloto);

                _logger.LogInformation($"Finalizada a operação");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor contate o suporte.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Obtendo pilotoid {id} na base");
                var piloto = _pilotoRepositorio.Obter(id);

                if (piloto == null)
                {
                    _logger.LogInformation($"Pilotoid {id} não foi encontrado na base");
                    return NotFound();
                }

                _logger.LogInformation($"Deletando o pilotoid {id} da base");
                _pilotoRepositorio.Deletar(piloto);

                _logger.LogInformation($"Finalizando a operação");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor contate o suporte.");
            }
        }

        [HttpOptions]
        public IActionResult ListarOperacoes()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, PATCH, DELETE, OPTIONS");
            return Ok();
        }
    }
}
