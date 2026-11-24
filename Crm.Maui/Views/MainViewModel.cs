using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crm.Maui.Models;
using Crm.Maui.Persistence;
using System.Collections.ObjectModel;

namespace Crm.Maui.Views
{
    public partial class MainViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Customer> _customers; //Variable de instancia configurada para que pueda reflejarse los cambios.
        
        [ObservableProperty]
        private bool _refreshing;

        [RelayCommand]
        private async Task LoadCustomersAsync()
        {
            await Task.Run( () => { using CrmDbContext dbContext = new CrmDbContext();
                Customers = new ObservableCollection<Customer>(dbContext.Customers);
            });
            Refreshing = false;

        }
        [RelayCommand]
        private void Showing()
        {
            Refreshing = true;  
        }

        [RelayCommand]
        private async Task ShowNewCustomerFormAsync()
        {
            await Shell.Current.GoToAsync(nameof(CustomerEditPage),parameters: new Dictionary<string,object>
            {
                {"ParameterRefreshAction",(Func<Customer,Task>) RefreshAddedAsync},
                {"Item", new Customer() }
            });
        }

        //este metodo actualiza el nuevo cliente agregandolo a la colección
        public Task RefreshAddedAsync (Customer addedCustomer)
        {
            Customers.Add(addedCustomer);
            return Task.CompletedTask;
        }

        [RelayCommand]
        public void DeleteCustomer(Customer customer)
        {
            using var dbContext = new CrmDbContext();
            //elimina del modelo de la base de datos
            dbContext.Customers.Remove(customer);
            //elimina de la base de datos
            dbContext.SaveChanges();

            Customers.Remove(customer);
        }
    }
}
