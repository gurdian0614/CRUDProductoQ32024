using CRUDProductoQ32024.ViewModels;

namespace CRUDProductoQ32024.Views;

public partial class ProductoMainView : ContentPage
{
	private ProductoMainViewModel ViewModel;

	public ProductoMainView()
	{
		InitializeComponent();
		ViewModel = new ProductoMainViewModel();
		this.BindingContext = ViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		ViewModel.GetAll();
    }

}