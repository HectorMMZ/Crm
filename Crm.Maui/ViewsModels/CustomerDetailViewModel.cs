using CommunityToolkit.Mvvm.ComponentModel;
using Crm.Maui.Models;

namespace Crm.Maui.ViewsModels
{
    public partial class CustomerDetailViewModel 
        : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private Customer _customer;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}
