namespace BussinesLayer.Objects
{
    public class Vrachtwagen
    {
        public Vrachtwagen(string chassisNummer, string merk, string model, float gewicht, bool heeftAanhangWagen, Brandstof brandstof, Chauffeur? chauffeur) : this(chassisNummer, merk, model, gewicht, heeftAanhangWagen, brandstof)
        {
            this.Chauffeur = chauffeur;
        }

        public Vrachtwagen(string chassisNummer, string merk, string model, float gewicht, bool heeftAanhangWagen, Brandstof brandstof)
        {
            ZetChassisnummer(chassisNummer);
            ZetMerk(merk);
            ZetModel(model);
            ZetGewicht(gewicht);
            ZetBoolean(heeftAanhangWagen);
            ZetEnum(brandstof);
        }

        public string ChassisNummer { get; private set; }

        public string Merk { get; private set; }

        public string Model { get; private set; }

        public float Gewicht { get; private set; }

        public Boolean HeeftAanhangWagen { get; private set; }

        public Brandstof Brandstof { get; private set; }

        public Chauffeur? Chauffeur { get; private set; }

        public void ZetChassisnummer(string chassisnummer)
        {
            if (!ChassisnummerValidator.isGeldig(chassisnummer))
            {
                VrachtwagenException ex = new VrachtwagenException("Vrachtwagen: chasisnummer klopt niet!");
                ex.Data.Add("chassisnummer", chassisnummer);
                throw ex;
            }
            this.ChassisNummer = chassisnummer;
        }

        public void ZetMerk(string merk)
        {
            if (string.IsNullOrWhiteSpace(merk))
            {
                VrachtwagenException ex = new VrachtwagenException("Vrachtwagen: Merk klopt niet!");
                ex.Data.Add("merk", merk);
                throw ex;
            }
            this.Merk = merk;
        }

        public void ZetModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                VrachtwagenException ex = new VrachtwagenException("Vrachtwagen: model is niet in correct formaat!");
                ex.Data.Add("model", model);
                throw ex;
            }
            this.Model = model;
        }

        public void ZetGewicht(float gewicht)
        {
            if(gewicht < 18000F || gewicht > 50000F)
            {
                VrachtwagenException ex = new VrachtwagenException("Vrachtwagen: gewicht klopt niet!");
                ex.Data.Add("gewicht", gewicht);
                throw ex;
            }
            this.Gewicht = gewicht;
        }

        public void ZetBoolean(bool waarde)
        {
            if (waarde == null)
            {
                ChauffeurException ex = new ChauffeurException("Chauffeur: Boolean is niet in correct formaat!");
                ex.Data.Add("waarde", waarde);
                throw ex;
            }
            this.HeeftAanhangWagen = waarde;
        }

        public bool BoolToText(bool aanhanwagen)
        {
            if (aanhanwagen)
            {
                Console.WriteLine($"{Merk} met model {Model} heeft een aanhangwagen");
            }
            else
            {
                Console.WriteLine($"{Merk} met model {Model} heeft geen een aanhangwagen");
            }
            return aanhanwagen;
        }

        public void ZetEnum(Brandstof brandstof)
        {
            if (!Enum.IsDefined(brandstof))
            {
                VrachtwagenException ex = new VrachtwagenException("Vrachtwagen: brandstof is niet in correct formaat!");
                ex.Data.Add("brandstof", brandstof);
                throw ex;
            }
            this.Brandstof = brandstof;
        }
        public override string ToString()
        {
            return $"Chassisnummer: {ChassisNummer}\nMerk: {Merk}\nModel: {Model}\nGewicht: {Gewicht}\nAanhangwagen: {BoolToText(HeeftAanhangWagen)}\nBrandstof: {Brandstof.ToString()}\n{Chauffeur?.ToString()}";
        }

    }
}
