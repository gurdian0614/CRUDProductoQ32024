using CRUDProductoQ32024.Views;

namespace CRUDProductoQ32024
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ProductoMainView());
        }
    }
}
