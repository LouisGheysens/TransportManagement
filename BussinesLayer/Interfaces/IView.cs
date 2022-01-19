namespace BussinesLayer.Interfaces
{
    public interface IView
    {
        IViewModel ViewModel
        {
            get;
            set;
        }

        void Show();
    }
}
