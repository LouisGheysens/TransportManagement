namespace BussinesLayer.Objects
{
    public class Ongeval
    {
        public Ongeval(bool arbeidsongeval, Vrachtwagen vrachtwagen, Chauffeur chaffeur, string plaats, DateTime datum, bool perteTotal, ErnstGraad graad)
        {
            ZetAanhangWagen(arbeidsongeval);
            this.Vrachtwagen = vrachtwagen;
            this.Chaffeur = chaffeur;
            ZetPlaats(plaats);
            ZetDatum(datum);
            ZetPerteTotal(perteTotal);
            ZetEnum(graad);
        }

        public int Id { get; set; }
        public Boolean Arbeidsongeval { get; private set; }

        public Vrachtwagen Vrachtwagen { get; private set; }

        public Chauffeur Chaffeur { get; private set; }

        public string Plaats { get; private set; }

        public DateTime Datum { get; private set; }

        public Boolean PerteTotal { get; private set; }

        public ErnstGraad Graad { get; private set; }

        public void ZetAanhangWagen(bool aanhangwagen)
        {
            if (aanhangwagen == null)
            {
                OngevalException ex = new OngevalException("Ongeval: Boolean is niet in correct formaat!");
                ex.Data.Add("waarde", aanhangwagen);
                throw ex;
            }
            this.Arbeidsongeval = aanhangwagen;
        }

        public void ZetPerteTotal(bool pertetotal)
        {
            if (pertetotal == null)
            {
                OngevalException ex = new OngevalException("Ongeval: Boolean is niet in correct formaat!");
                ex.Data.Add("waarde", pertetotal);
                throw ex;
            }
            this.PerteTotal = pertetotal;
        }

        public void ZetEnum(ErnstGraad graad)
        {
            if (!Enum.IsDefined(graad))
            {
                OngevalException ex = new OngevalException("Ongeval: Ernstgraad was niet in correct formaat");
                ex.Data.Add("graad", graad);
                throw ex;
            }
            this.Graad = graad;
        }

        public void ZetPlaats(string plaats)
        {
            if (string.IsNullOrEmpty(plaats))
            {
                OngevalException ex = new OngevalException("Ongeval: Plaats is niet in correct formaat");
                ex.Data.Add("plaats", plaats);
                throw ex;
            }
            this.Plaats = plaats;
        }

        public void ZetDatum(DateTime datum)
        {
            if (datum.GetHashCode() == 0)
            {
                OngevalException ex = new OngevalException("Ongeval: Datum is niet in correct formaat!");
                ex.Data.Add("datum", datum);
                throw ex;
            }
            this.Datum = datum;
        }

        public bool ArbeidsOngevalToText(bool aanhangwagen)
        {
            if (aanhangwagen)
            {
                Console.WriteLine("Met aanhangwagen");
            }
            else
            {
                Console.WriteLine("Geen aanhangwagen");
            }
            return aanhangwagen;
        }

        public bool PerteTotalToText(bool pertetotal)
        {
            if (pertetotal)
            {
                Console.WriteLine("Perte total");
            }
            else
            {
                Console.WriteLine("Niet perte total");
            }
            return pertetotal;
        }

        public override string ToString()
        {
            return $"Ongeval" +
                $"  ........ " +
                $"  {Chaffeur.ToString()}" +
                $"  {Vrachtwagen.ToString()}" +
                $"  {Plaats}" +
                $"  {Datum.ToShortDateString()}" +
                $"  {ArbeidsOngevalToText(Arbeidsongeval)}" +
                $"  {PerteTotalToText(PerteTotal)}" +
                $"  {Graad.ToString()}";
        }
    }

    }
