using SiaERP.Models;
using SiaERP.Models.Secondary;
using System.Collections.ObjectModel;

namespace SiaERP.Data
{
    static class DataManagement
    {
        //Get list of tax regimen by his id
        public static ObservableCollection<TaxRegime> ListTaxRegime()
        {
            ObservableCollection<TaxRegime> ListTaxtRegimen = new ObservableCollection<TaxRegime>();

            for (int i = 1; i <= 19; i++)
            {
                ListTaxtRegimen.Add(new TaxRegime(i));
            }

            return ListTaxtRegimen;
        }

        //Get list of service status by his id
        public static ObservableCollection<ServiceStatus> ListServiceStatus()
        {
            ObservableCollection<ServiceStatus> ListServiceStatus = new ObservableCollection<ServiceStatus>();

            for (int i = 1; i <= 3; i++)
            {
                ListServiceStatus.Add(new ServiceStatus(i));
            }

            return ListServiceStatus;
        }

        //Get list of service status by his id
        public static ObservableCollection<PaymentMethod> ListPaymentMethod()
        {
            ObservableCollection<PaymentMethod> ListPaymentMethod = new ObservableCollection<PaymentMethod>();

            for (int i = 1; i <= 4; i++)
            {
                ListPaymentMethod.Add(new PaymentMethod(i));
            }

            return ListPaymentMethod;
        }
    }
}
