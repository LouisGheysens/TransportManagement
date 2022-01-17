namespace TestLayer.Objects
{
    public class Test_Ongeval
    {
        private Vrachtwagen _vrachtwagen1 = new Vrachtwagen("3GNEC16TX1G242965", "Volkswagen", "polo", 20000, false, BussinesLayer.Enums.Brandstof.Benzine);
        private Chauffeur _chauffeur1 = new("1", "Louis", "Gheysens", new System.DateTime(2001, 06, 05), true);

        [Fact]
        public void Test_ZetPlaats_Valid()
        {
            Ongeval ongeval1 = new Ongeval(true, _vrachtwagen1, _chauffeur1, "Hoboken", new DateTime(2020, 12, 04), false, BussinesLayer.Enums.ErnstGraad.Frequentiegraad);
            ongeval1.ZetPlaats("Hoboken");
            Assert.Equal("Hoboken", ongeval1.Plaats);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Test_ZetPlaats_Invalid(string plaats)
        {
            var ex = Assert.Throws<OngevalException>(() => new Ongeval(true, _vrachtwagen1, _chauffeur1, plaats, new DateTime(2020, 12, 04), false, BussinesLayer.Enums.ErnstGraad.Frequentiegraad));
            Assert.Equal("Ongeval: Plaats is niet in correct formaat", ex.Message);

        }

        [Fact]
        public void Test_ZetDatum_Valid()
        {
            Ongeval ongeval1 = new Ongeval(true, _vrachtwagen1, _chauffeur1, "Hoboken", new DateTime(2020, 12, 04), false, BussinesLayer.Enums.ErnstGraad.Frequentiegraad);
            ongeval1.ZetDatum(new DateTime(2020, 12, 04));
            Assert.Equal(new DateTime(2020, 12, 04), ongeval1.Datum);
        }

        public void Test_ZetPertetotal_Valid()
        {
            Ongeval ongeval1 = new Ongeval(true, _vrachtwagen1, _chauffeur1, "Hoboken", new DateTime(2020, 12, 04), false, BussinesLayer.Enums.ErnstGraad.Frequentiegraad);
            ongeval1.ZetPerteTotal(false);
            Assert.False(ongeval1.PerteTotal);
        }

        public void Test_ZetEnum_Valid()
        {
            Ongeval ongeval1 = new Ongeval(true, _vrachtwagen1, _chauffeur1, "Hoboken", new DateTime(2020, 12, 04), false, BussinesLayer.Enums.ErnstGraad.Frequentiegraad);
            ongeval1.ZetEnum(BussinesLayer.Enums.ErnstGraad.Frequentiegraad);
            Assert.Equal(BussinesLayer.Enums.ErnstGraad.Frequentiegraad, ongeval1.Graad);
        }
    }
}
