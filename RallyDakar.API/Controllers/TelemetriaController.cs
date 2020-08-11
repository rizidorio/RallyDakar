using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using RallyDakar.API.Modelo;
using RallyDakar.Dominio.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RallyDakar.API.Controllers
{
    [ApiController]
    [Route("api/equipes/{equipeId}/telemetria")]
    public class TelemetriaController : ControllerBase
    {
        private readonly ITelemetriaRepositorio _telemetriaRepositorio;
        private readonly IEquipeRepositorio _equipeRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<TelemetriaController> _logger;

        public TelemetriaController(ITelemetriaRepositorio telemetriaRepositorio, IMapper mapper, 
            ILogger<TelemetriaController> logger, IEquipeRepositorio equipeRepositorio)
        {
            _telemetriaRepositorio = telemetriaRepositorio;
            _mapper = mapper;
            _logger = logger;
            _equipeRepositorio = equipeRepositorio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TelemetriaModelo>> Obter(int equipeId)
        {
            try
            {
                _logger.LogInformation($"Verificando se a Equipe: {equipeId} existe na base.");

                if(!_equipeRepositorio.Existe(equipeId))
                {
                    _logger.LogWarning($"Equipe id não identificada - EquipeId: {equipeId}.");
                    return NotFound();
                }

                _logger.LogInformation($"Obtendo os dados da teletria para equipe: {equipeId}.");

                var dadosTelemetria = _telemetriaRepositorio.ObterTodosPorEquipe(equipeId);

                if (!dadosTelemetria.Any())
                {
                    _logger.LogInformation($"Não foram encontrados dados de telemetria para equipe informada: {equipeId}.");
                    return NotFound("Não foram encontrados dados de telemetria para equipe informada.");
                }

                var dadosTelemetriaModelo = _mapper.Map<IEnumerable<TelemetriaModelo>>(dadosTelemetria);

                return Ok(dadosTelemetria);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Entre em contato com o suporte.");
            }
        }

    }
}
