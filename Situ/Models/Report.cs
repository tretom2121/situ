using System;
using System.Collections.Generic;

namespace Situ.Models
{
    public class Report
    {
        public Department Department { get; set; }
        public DateTime Date { get; set; }

        public int Eintritte { get; set; }
        public int Termineinstellung { get; set; }
        public int Austritte { get; set; }
        public int MAImEinsatz { get; set; }
        public int KrankImEinsatz { get; set; }
        public int KrankOhneEinsatz { get; set; }
        public int KrankOhneLohnfortzahlung { get; set; }
        public int GleitzeitMitEinsatz { get; set; }
        public int GleitzeitOhneEinsatz { get; set; }
        public int UrlaubMitEinsatz { get; set; }
        public int UrlaubOhneEinsatz { get; set; }
        public int UrlaubUnbezahltMitEinsatz { get; set; }
        public int KurzarbeitKUG { get; set; }
        public int UnentschuldigtesFehlen { get; set; }
        public int Wartezeit { get; set; }
        public int Mutterschutz { get; set; }
        public int Elternzeit { get; set; }
        public int Gesamtmitarbeiter => MAImEinsatz + KrankImEinsatz + KrankOhneEinsatz +
                                        KrankOhneLohnfortzahlung + GleitzeitMitEinsatz +
                                        GleitzeitOhneEinsatz + UrlaubMitEinsatz +
                                        UrlaubOhneEinsatz + UrlaubUnbezahltMitEinsatz +
                                        KurzarbeitKUG + UnentschuldigtesFehlen + Wartezeit;

        public int AktiveKunden { get; set; }

        public double ProzKrankheit => Gesamtmitarbeiter > 0 ? (KrankImEinsatz + KrankOhneEinsatz) / (double)Gesamtmitarbeiter * 100 : 0d;
        public double ProzWartezeit => Gesamtmitarbeiter > 0 ? Wartezeit / (double)Gesamtmitarbeiter * 100 : 0d;
        public double ProzUrlaubUndGleitzeit => Gesamtmitarbeiter > 0 ? (GleitzeitMitEinsatz +
                                                                         GleitzeitOhneEinsatz +
                                                                         UrlaubMitEinsatz +
                                                                         UrlaubOhneEinsatz) / (double)Gesamtmitarbeiter * 100 : 0d;

        public double ProzUnbezahlteFehlzeit => Gesamtmitarbeiter > 0 ? (KrankOhneLohnfortzahlung +
                                                                         UrlaubUnbezahltMitEinsatz +
                                                                         KurzarbeitKUG +
                                                                         UnentschuldigtesFehlen) / (double)Gesamtmitarbeiter * 100 : 0d;

        //TODO what's tabelle7?
        public double ProzFehlzeitenquoteBezahlt => Gesamtmitarbeiter > 0 ? (KrankOhneEinsatz +
                                                                             GleitzeitMitEinsatz +
                                                                             GleitzeitOhneEinsatz +
                                                                             UrlaubMitEinsatz +
                                                                             UrlaubOhneEinsatz +
                                                                             Wartezeit) / (double)Gesamtmitarbeiter * 100 : 0d;

        public double ProzFehlzeitenquoteUnbezahlt => Gesamtmitarbeiter > 0 ? (KrankOhneEinsatz +
                                                                               UrlaubUnbezahltMitEinsatz +
                                                                               KurzarbeitKUG +
                                                                               UnentschuldigtesFehlen) / (double)Gesamtmitarbeiter * 100 : 0d;

        //TODO what's tabelle7?
        public double ProzGesamtFehlzeitenquote => Gesamtmitarbeiter > 0 ? (KrankImEinsatz +
                                                                            KrankOhneEinsatz +
                                                                            KrankOhneLohnfortzahlung +
                                                                            GleitzeitOhneEinsatz +
                                                                            UrlaubMitEinsatz +
                                                                            UrlaubOhneEinsatz +
                                                                            UrlaubUnbezahltMitEinsatz +
                                                                            KurzarbeitKUG +
                                                                            UnentschuldigtesFehlen +
                                                                            Wartezeit) / (double)Gesamtmitarbeiter * 100 : 0d;


        public List<EmploymentChance> EmploymentChances { get; set; } = new();
    }
}
