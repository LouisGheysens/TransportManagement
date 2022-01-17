using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Objects
{
    public class Test_Vrachtwagen
    {
        [Fact]
        public void Test_ZetMerk_Valid()
        {
            Vrachtwagen vrachtwagen1 = new Vrachtwagen("3GNEC16TX1G242965", "Volkswagen", "polo", 20000, false, BussinesLayer.Enums.Brandstof.Benzine);
            vrachtwagen1.ZetMerk("Volkswagen");
            Assert.Equal("Volkswagen", vrachtwagen1.Merk);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Test_ZetMerk_Invalid(string merk)
        {
            var ex = Assert.Throws<VrachtwagenException>(() => new Vrachtwagen("3GNEC16TX1G242965", merk, "polo", 20000, false, BussinesLayer.Enums.Brandstof.Benzine));
            Assert.Equal("Vrachtwagen: Merk klopt niet!", ex.Message);
        }

        [Fact]
        public void Test_ZetModel_Valid()
        {
            Vrachtwagen vrachtwagen1 = new Vrachtwagen("3GNEC16TX1G242965", "Volkswagen", "polo", 20000, false, BussinesLayer.Enums.Brandstof.Benzine);
            vrachtwagen1.ZetModel("polo");
            Assert.Equal("polo", vrachtwagen1.Model);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Test_ZetModel_Invalid(string model)
        {
            var ex = Assert.Throws<VrachtwagenException>(() => new Vrachtwagen("3GNEC16TX1G242965", "Volkswagen", model, 20000, false, BussinesLayer.Enums.Brandstof.Benzine));
            Assert.Equal("Vrachtwagen: model is niet in correct formaat!", ex.Message);
        }

        [Fact]
        public void Test_ZetGewicht_Valid()
        {
            Vrachtwagen vrachtwagen1 = new Vrachtwagen("3GNEC16TX1G242965", "Volkswagen", "polo", 20000, false, BussinesLayer.Enums.Brandstof.Benzine);
            vrachtwagen1.ZetGewicht(20000);
            Assert.Equal(20000, vrachtwagen1.Gewicht);
        }

        [Fact]
        public void Test_ZetGewicht_Invalid()
        {
            var ex = Assert.Throws<VrachtwagenException>(() => new Vrachtwagen("3GNEC16TX1G242965", "Volkswagen", "polo", 1, false, BussinesLayer.Enums.Brandstof.Benzine));
            Assert.Equal("Vrachtwagen: gewicht klopt niet!", ex.Message);
        }

        [Fact]
        public void Test_ZetBoolean_Valid()
        {
            Vrachtwagen vrachtwagen1 = new Vrachtwagen("3GNEC16TX1G242965", "Volkswagen", "polo", 20000, false, BussinesLayer.Enums.Brandstof.Benzine);
            vrachtwagen1.ZetBoolean(false);
            Assert.False(vrachtwagen1.HeeftAanhangWagen);
        }

        [Fact]
        public void Test_ZetBrandstof_Valid()
        {
            Vrachtwagen vrachtwagen1 = new Vrachtwagen("3GNEC16TX1G242965", "Volkswagen", "polo", 20000, false, BussinesLayer.Enums.Brandstof.Benzine);
            vrachtwagen1.ZetEnum(BussinesLayer.Enums.Brandstof.Benzine);
            Assert.Equal(BussinesLayer.Enums.Brandstof.Benzine, vrachtwagen1.Brandstof);
        }
    }
}
