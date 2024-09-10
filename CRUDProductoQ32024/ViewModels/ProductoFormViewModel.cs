
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUDProductoQ32024.Models;
using CRUDProductoQ32024.Services;
using CRUDProductoQ32024.Views;

namespace CRUDProductoQ32024.ViewModels
{
    public partial class ProductoFormViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string descripcion;

        [ObservableProperty]
        private string marca;

        [ObservableProperty]
        private double precio;

        private readonly ProductoService ProductoService;

        public ProductoFormViewModel()
        {
            ProductoService = new ProductoService();
        }

        public ProductoFormViewModel(Producto Producto)
        {
            ProductoService = new ProductoService();
            Id = Producto.Id;
            Nombre = Producto.Nombre;
            Descripcion = Producto.Descripcion;
            Marca = Producto.Marca;
            Precio = Producto.Precio;
        }

        /// <summary>
        /// Muestra un mensaje de alerta personalizado
        /// </summary>
        /// <param name="Titulo">Titulo de la alerta, por ejemplo, ERROR, ADVERTENCIA, etc</param>
        /// <param name="Mensaje">Mensaje a mostrar en la alerta</param>
        private void Alerta(string Titulo, string Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Titulo, Mensaje, "Aceptar"));
        }

        /// <summary>
        /// Verifica que los campos obligatorios esten ingresados
        /// </summary>
        /// <param name="Producto">Objeto a validar</param>
        /// <returns>Valor booleano que nos confirma si los campos obligatorios tienen datos</returns>
        private bool Validar(Producto Producto) {
            if (Producto.Nombre is null || Producto.Nombre == "")
            {
                Alerta("ADVERTENCIA", "Ingrese el nombre del producto");
                return false;
            } else if (Producto.Descripcion is null || Producto.Descripcion == "")
            {
                Alerta("ADVERTENCIA", "Ingrese la descripción del producto");
                return false;
            } else if (Producto.Marca is null || Producto.Marca == "")
            {
                Alerta("ADVERTENCIA", "INgrese la marca del producto");
                return false;
            } else
            {
                return true;
            }
        }

        /// <summary>
        /// Ingresa o actualiza un registro de producto
        /// </summary>
        /// <returns>Listado de productos con nuevo registro o registro actualizado</returns>
        [RelayCommand]
        private async Task AddUpdate()
        {
            try
            {
                Producto Producto = new Producto();
                Producto.Id = Id;
                Producto.Nombre = Nombre;
                Producto.Descripcion = Descripcion;
                Producto.Marca = Marca;
                Producto.Precio = Precio;

                if (Validar(Producto))
                {
                    if (Id == 0)
                    {
                        ProductoService.Insert(Producto);
                    } else
                    {
                        ProductoService.Update(Producto);
                    }
                    await App.Current!.MainPage!.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

    }
}
