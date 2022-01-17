namespace BussinesLayer.Validators
{
    public static class ChassisnummerValidator
    {
        public static bool isGeldig(string chassisnummer)
        {
            List<char> verbodenTekens = new List<char>() { 'I', 'O', 'Q' };
            if (string.IsNullOrWhiteSpace(chassisnummer)) throw new ChassisnummerException("Chassisnummer mag niet leeg zijn!");
            if (chassisnummer.Length != 17) throw new ChassisnummerException("Chassisnummer moet 17 karakters lang zijn!");
            if (verbodenTekens.Any(c => chassisnummer.Contains(c))) throw new ChassisnummerException("Chassisnummer kan geen verwarrende tekens bevatten!");
            if (chassisnummer.Any(c => char.IsLower(c))) throw new ChassisnummerException("Lowercase karakters zijn niet toegestaan!");
            return true;
        }
    }
}
