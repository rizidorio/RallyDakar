using System;

namespace RallyDakar.Dominio.Entidades
{
    public class Telemetria
    {
        public int Id { get; set; }
        public int EquipeId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Latitute { get; set; }
        public decimal Longitude { get; set; }
        public decimal PercentualCombustivel { get; set; }
        public double Velocidade { get; set; }
        public double RPM { get; set; }
        public int TemperaturaExterna { get; set; }
        public int TemperaturaMotor { get; set; }
        public double AltitudeNivekMar { get; set; }
        public bool PedalAcelerador { get; set; }
        public bool PedalFreio { get; set; }
    }
}
