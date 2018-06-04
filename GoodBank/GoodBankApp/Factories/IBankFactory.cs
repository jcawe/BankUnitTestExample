using GoodBankApp.Models;

namespace GoodBankApp.Factories
{
    public interface IBankFactory
    {
         Bank Create(string name, params Account[] accounts);
    }
}