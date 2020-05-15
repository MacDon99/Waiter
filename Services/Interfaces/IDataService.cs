using System.Collections.Generic;
using Waiter.Models;
using Waiter.ViewModels;

namespace Waiter.Services.Interfaces
{
    public interface IDataService
    {
        List<Table> GetAllTables();
        TableViewModel GetTableInformation(TableViewModel model);
        void CreateTablesAndDishes();
    }
}