using System;

namespace Situ.Models
{
    public class Statistic
    {
        public DateTime Date { get; set; }
        public string DateString => Date.ToString("dd.MM.yyyy");

        public double ProzKrankheit { get; set; }
        public double ProzWartezeit { get; set; }
        public double ProzUrlaubUndGleitzeit { get; set; }
        public double ProzUnbezahlteFehlzeit { get; set; }
        public double ProzFehlzeitenquoteBezahlt { get; set; }
        public double ProzFehlzeitenquoteUnbezahlt { get; set; }
        public double ProzGesamtFehlzeitenquote { get; set; }
    }
}
