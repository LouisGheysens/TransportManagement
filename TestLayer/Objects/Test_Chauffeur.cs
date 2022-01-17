namespace TestLayer.Objects
{
    public class Test_Chauffeur
    {

        [Fact]
        public void Test_ZetPersoneelsnummer_Valid()
        {
            Chauffeur chauffeur1 = new("1", "Louis","Gheysens", new System.DateTime(2001, 06, 05), true);
            chauffeur1.ZetPersoneelsNummer("1");
            Assert.Equal("1", chauffeur1.PersoneelsNummer);
        }

        [Theory]
        [InlineData("0")]
        public void Test_ZetPersoneelsnummer_Invalid(string personeelsnummer)
        {
            var ex = Assert.Throws<ChauffeurException>(() => new Chauffeur(personeelsnummer, "Louis", "Gheysens", new System.DateTime(2001, 06, 05), true));
            Assert.Equal("Chauffeur: Personeelsnummer klopt niet!", ex.Message);
        }

        [Fact]
        public void Test_ZetNaam_Valid()
        {
            Chauffeur chauffeur1 = new("1", "Louis", "Gheysens", new System.DateTime(2001, 06, 05), true);
            chauffeur1.ZetNaam("Louis");
            Assert.Equal("Louis", chauffeur1.Voornaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Test_ZetNaam_Invalid(string naam)
        {
            var ex = Assert.Throws<ChauffeurException>(() => new Chauffeur("1", naam, "Gheysens", new System.DateTime(2001, 06, 05), true));
            Assert.Equal("Chauffeur: Voornaam is leeg!", ex.Message);
        }

        [Fact]
        public void Test_ZetAchternaamNaam_Valid()
        {
            Chauffeur chauffeur1 = new("1", "Louis", "Gheysens", new System.DateTime(2001, 06, 05), true);
            chauffeur1.ZetNaam("Gheysens");
            Assert.Equal("Gheysens", chauffeur1.Achternaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Test_ZetAchternaamNaam_Invalid(string achternaam)
        {
            var ex = Assert.Throws<ChauffeurException>(() => new Chauffeur("1", "Louis", achternaam, new System.DateTime(2001, 06, 05), true));
            Assert.Equal("Chauffeur: Achternaam is leeg!", ex.Message);
        }


        [Fact]
        public void Test_ZetGeboortedatum_Valid()
        {
            Chauffeur chauffeur1 = new("1", "Louis", "Gheysens", new System.DateTime(2001, 06, 05), true);
            chauffeur1.ZetGeboortedatum(new System.DateTime(2001, 06, 05));
            Assert.Equal(new System.DateTime(2001, 06, 05), chauffeur1.Geboortedatum);
        }

        [Fact]
        public void Test_ZetBoolean_valid()
        {
            Chauffeur chauffeur1 = new("1", "Louis", "Gheysens", new System.DateTime(2001, 06, 05), true);
            chauffeur1.ZetBoolean(true);
            Assert.True(chauffeur1.Internationaal);
        }


    }
}
