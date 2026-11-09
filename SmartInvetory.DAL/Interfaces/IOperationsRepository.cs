using SmartInventory.Model.OperationsDto;
namespace SmartInvetory.DAL.Interfaces;
public interface IOperationsRepository
{
 List<SelectOptionDto> GetProducts(); List<SelectOptionDto> GetWarehouses(); List<SelectOptionDto> GetSuppliers(); List<SelectOptionDto> GetCustomers(); List<SelectOptionDto> GetRoles();
 List<StockRowDto> GetStock(); List<TransactionRowDto> GetTransactions(); List<OrderRowDto> GetPurchases(); List<OrderRowDto> GetSales();
 OrderDetailsDto? GetPurchase(int id); OrderDetailsDto? GetSale(int id); bool CreatePurchase(CreateOrderDto dto); bool CreateSale(CreateOrderDto dto); bool AdjustStock(StockAdjustmentDto dto);
 DashboardDto GetDashboard(); List<UserDto> GetUsers(); UserDto? GetUser(int id); bool SaveUser(UserDto dto); bool DeleteUser(int id);
}
