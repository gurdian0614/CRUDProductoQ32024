
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUDProductoQ32024.Models;
using CRUDProductoQ32024.Services;
using System.Collections.ObjectModel;
using CRUDProductoQ32024.Views;

namespace CRUDProductoQ32024.ViewModels
{
    public partial class ProductoMainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> productoCollection = new ObservableCollection<Producto>();

        private readonly ProductoService ProductoService;

        public ProductoMainViewModel()
        {
            ProductoService = new ProductoService();
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
        /// Muestra el listado de productos
        /// </summary>
        public void GetAll()
        {
            var GetAll = ProductoService.GetAll();

            if (GetAll.Count > 0)
            {
                ProductoCollection.Clear();
                foreach (var producto in GetAll)
                {
                    ProductoCollection.Add(producto);
                }
            }
        }

        /// <summary>
        /// Redirecciona a la vista de agregar / editar producto
        /// </summary>
        /// <returns>Muestra la vista de agregar / editar producto</returns>
        [RelayCommand]
        private async Task GoToAddProductoForm()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new ProductoFormView());
        }

        /// <summary>
        /// Muestra las opciones para editar o eliminar un prudcto al seleccionarlo
        /// </summary>
        /// <param name="Producto">Objeto seleccionado para actualizar o eliminar el registro</param>
        /// <returns>Proceso de opciones al seleccionar el registro del producto</returns>
        [RelayCommand]
        private async Task SelectProducto(Producto Producto)
        {
            try
            {
                const string ACTUALIZAR = "1. Actualizar";
                const string ELIMINAR = "2. Eliminar";

                string res = await App.Current!.MainPage!.DisplayActionSheet("OPCIONES", "Cancelar", null, ACTUALIZAR, ELIMINAR);

                if (res == ACTUALIZAR)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ProductoFormView(Producto));
                }
                else if (res == ELIMINAR)
                {
                    bool respuesta = await App.Current!.MainPage!.DisplayAlert("ELIMINAR PRODUCTO", "¿Desea eliminar el producto?", "Si", "No");

                    if (respuesta)
                    {
                        int del = ProductoService.Delete(Producto);

                        if (del > 0)
                        {
                            ProductoCollection.Remove(Producto);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

    }
}
