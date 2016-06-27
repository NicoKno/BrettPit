using System;

namespace BrettPit.Models
{
    public class MatchModel
    {
        public int Id { get; set; }
        public string Player { get; set; }
        public string Opponent { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public string Game { get; set; }
        public DateTime Date { get; set; }
    }
}