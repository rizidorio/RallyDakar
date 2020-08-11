using System;
using System.ComponentModel.DataAnnotations;

namespace RallyDakar.API.Modelo
{
    public class TelemetriaModelo
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Equipe não identifcada!")]
        public int EquipeID { get; set; }

        [Required(ErrorMessage = "Data da equipe não foi recebida.")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Hora da equipe não foi recebdida.")]
        public TimeSpan Hora { get; set; }

        public DateTime DataServidor { get; set; }
        public TimeSpan HoraServidor { get; set; }

        [Required(ErrorMessage = "Latitute não informada.")]
        public decimal Latitute { get; set; }

        [Required(ErrorMessage = "Longitute não informada.")]
        public decimal Longitude { get; set; }

        [Required(ErrorMessage = "Percentual de combustível não informado.")]
        public decimal PercentualCombustivel { get; set; }

        [Required(ErrorMessage = "Velocidade não informada.")]
        public double Velocidade { get; set; }

        [Required(ErrorMessage = "RPM não informado.")]
        public double RPM { get; set; }

        public int TemperaturaExterna { get; set; }
        public int TemperaturaMotor { get; set; }
        public double AltitudeNivekMar { get; set; }
        public bool PedalAcelerador { get; set; }
        public bool PedalFreio { get; set; }
    }
}
