namespace BussinesLayer.Objects
{
    public class Chauffeur
    {
        public Chauffeur(string personeelsNummer, string voornaam, string achternaam, DateTime geboortedatum, bool internationaal, Vrachtwagen vrachtwagen) : this(personeelsNummer, voornaam, achternaam, geboortedatum, internationaal)
        {
            this.Vrachtwagen = vrachtwagen;
        }

        public Chauffeur(string personeelsNummer, string voornaam, string achternaam, DateTime geboortedatum, bool internationaal)
        {
            ZetPersoneelsNummer(personeelsNummer);
            ZetNaam(voornaam);
            ZetAchternaam(achternaam);
            ZetGeboortedatum(geboortedatum);
            ZetBoolean(internationaal);
        }

        public string PersoneelsNummer { get; private set; }
        public string Achternaam { get; set; }

        public string Voornaam { get; private set; }

        public DateTime Geboortedatum { get; private set; }

        public Boolean Internationaal { get; private set; }

        public Vrachtwagen Vrachtwagen { get; private set; }

        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam))
            {
                ChauffeurException ex = new ChauffeurException("Chauffeur: Voornaam is leeg!");
                ex.Data.Add("naam", naam);
                throw ex;
            }
            this.Voornaam = naam;
        }

        public void ZetAchternaam(string achternaam)
        {
            if (string.IsNullOrWhiteSpace(achternaam))
            {
                ChauffeurException ex = new ChauffeurException("Chauffeur: Achternaam is leeg!");
                ex.Data.Add("achternaam", achternaam);
                throw ex;
            }
            this.Achternaam = achternaam;
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

        public void ZetPersoneelsNummer(string personeelsnummer)
        {
            if (personeelsnummer == "0")
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
                Console.WriteLine($"{Voornaam} rijdt internationaal");
            }
            else
            {
                Console.WriteLine($"{Voornaam} rijdt niet internationaal");
            }
            return internationaal;
        }

        public override string ToString()
        {
            return $"Naam:{Voornaam}\nGeboortedatum: {Geboortedatum.ToShortDateString()}\nPersoneelsnummer: {PersoneelsNummer},\n{BoolToText(Internationaal)}\nVrachtwagen: {Vrachtwagen.ToString()}";
        }

    }
}
