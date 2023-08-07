using SiaERP.Models;
using System.Collections.ObjectModel;

namespace SiaERP.Data
{
    static class DataManagement
    {
        //Get list of tax regimen by his id
        public static ObservableCollection<TaxRegime> ListTaxRegime()
        {
            ObservableCollection<TaxRegime> _ListTaxtRegimen = new ObservableCollection<TaxRegime>();

            for (int i = 1; i <= 19; i++)
            {
                _ListTaxtRegimen.Add(new TaxRegime(i));
            }

            return _ListTaxtRegimen;
        }

        //Get list of service status by his id
        public static ObservableCollection<ServiceStatus> ListServiceStatus()
        {
            ObservableCollection<ServiceStatus> _ListServiceStatus = new ObservableCollection<ServiceStatus>();

            for (int i = 1; i <= 3; i++)
            {
                _ListServiceStatus.Add(new ServiceStatus(i));
            }

            return _ListServiceStatus;
        }
    }
}
