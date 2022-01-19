namespace BussinesLayer.Objects
{
    public class AnonymousIdentity: CustomIdentity
    {
        public AnonymousIdentity()
            : base(string.Empty, string.Empty, new string[] { })
        { }
    }
}
