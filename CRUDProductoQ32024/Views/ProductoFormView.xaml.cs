using CRUDProductoQ32024.Models;
using CRUDProductoQ32024.ViewModels;

namespace CRUDProductoQ32024.Views;

public partial class ProductoFormView : ContentPage
{
	private ProductoFormViewModel ViewModel;
	public ProductoFormView()
	{
		InitializeComponent();
		ViewModel = new ProductoFormViewModel();
		BindingContext = ViewModel;
	}

	public ProductoFormView(Producto Producto)
	{
		InitializeComponent();
		ViewModel = new ProductoFormViewModel(Producto);
		BindingContext = ViewModel;
	}
}