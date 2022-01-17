namespace BussinesLayer.Objects
{
    public class Chauffeur
    {
        public Chauffeur(int personeelsNummer, string naam, DateTime geboortedatum, bool internationaal, Vrachtwagen? vrachtwagen)
        {
            ZetPersoneelsNummer(personeelsNummer);
            ZetNaam(naam);
            ZetGeboortedatum(geboortedatum);
            ZetBoolean(internationaal);
            this.Vrachtwagen = vrachtwagen;
        }

        public int PersoneelsNummer { get; private set; }

        public string Naam { get; private set; }

        public DateTime Geboortedatum { get; private set; }

        public Boolean Internationaal { get; private set; }

        public Vrachtwagen? Vrachtwagen { get; private set; }

        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam))
            {
                ChauffeurException ex = new ChauffeurException("Chauffeur: Naam is leeg!");
                ex.Data.Add("naam", naam);
                throw ex;
            }
            this.Naam = naam;
        }

        public void ZetGeboortedatum(DateTime geboortedatum)
        {
            if(geboortedatum.GetHashCode() == 0)
            {
                ChauffeurException ex = new ChauffeurException("Chauffeur: Geboortedatum is niet in correct formaat!");
                ex.Data.Add("geboortedatum", geboortedatum);
                throw ex;
            }
            this.Geboortedatum = geboortedatum;
        }

        public void ZetPersoneelsNummer(int personeelsnummer)
        {
            if (personeelsnummer <= 0)
            {
                ChauffeurException ex = new ChauffeurException("Chauffeur: Personeelsnummer klopt niet!");
                ex.Data.Add("personeelsummer", personeelsnummer);
                throw ex;
            }
            this.PersoneelsNummer = personeelsnummer;
        }

        public void ZetBoolean(bool waarde)
        {
            if(waarde == null)
            {
                ChauffeurException ex = new ChauffeurException("Chauffeur: Boolean is niet in correct formaat!");
                ex.Data.Add("waarde", waarde);
                throw ex;
            }
            this.Internationaal = waarde;
        }

        public bool BoolToText(bool internationaal)
        {
            if (internationaal)
            {
                Console.WriteLine($"{Naam} rijdt internationaal");
            }
            else
            {
                Console.WriteLine($"{Naam} rijdt niet internationaal");
            }
            return internationaal;
        }

        public override string ToString()
        {
            return $"Naam:{Naam}\nGeboortedatum: {Geboortedatum.ToShortDateString()}\nPersoneelsnummer: {PersoneelsNummer},\n{BoolToText(Internationaal)}\nVrachtwagen: {Vrachtwagen?.ToString()}";
        }

    }
}
