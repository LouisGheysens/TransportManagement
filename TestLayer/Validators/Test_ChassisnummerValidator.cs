namespace TestLayer.Validators
{
    public class Test_ChassisnummerValidator
    {
       [Fact]
        public void Test_Alles_Valid() {
            Assert.True(ChassisnummerValidator.isGeldig("4Y1SL65848Z411439"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Test_Leeg_InValid(string chassisnummer) {
            var ex = Assert.Throws<ChassisnummerException>(() => ChassisnummerValidator.isGeldig(chassisnummer));
            Assert.Equal("Chassisnummer mag niet leeg zijn!", ex.Message);
        }

        [Theory]
        [InlineData("4Y1SL65848Z41143")]
        [InlineData("4Y1SL65848Z4114399")]
        public void Test_Lengte_Invalid(string chassisnummer) {
            var ex = Assert.Throws<ChassisnummerException>(() => ChassisnummerValidator.isGeldig(chassisnummer));
            Assert.Equal("Chassisnummer moet 17 karakters lang zijn!", ex.Message);
        }

        [Theory]
        [InlineData("4Y1SL658Q8Z411439")] 
        [InlineData("4Y1SL658I8Z411439")] 
        [InlineData("4Y1SL658O8Z411439")] 
        public void Test_VerwarrendeLetter_InValid(string chassisnummer) {
            var ex = Assert.Throws<ChassisnummerException>(() => ChassisnummerValidator.isGeldig(chassisnummer));
            Assert.Equal("Chassisnummer kan geen verwarrende tekens bevatten!", ex.Message);
        }

        [Theory]
        [InlineData("4y1SL65808Z411439")]
        public void Test_Lowercase_Invalid(string chassisnummer) {
            var ex = Assert.Throws<ChassisnummerException>(() => ChassisnummerValidator.isGeldig(chassisnummer));
            Assert.Equal("Lowercase karakters zijn niet toegestaan!", ex.Message);
        }
    }
}
