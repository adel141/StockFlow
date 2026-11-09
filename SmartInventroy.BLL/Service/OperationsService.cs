using System.Security.Cryptography;
using SmartInventory.Model.OperationsDto;
using SmartInvetory.DAL.Interfaces;
namespace SmartInventroy.BLL.Service;
public class OperationsService
{
    private readonly IOperationsRepository _repo;
    public OperationsService(IOperationsRepository repo) {
        _repo=repo;
    }
    public bool NeedsFirstAdminSetup()=>!_repo.HasAnyUsers();
    public bool CreateFirstAdmin(string fullName,string username,string email,string password) {
        if(!_repo.HasAnyUsers()) {
            var dto=new LoginUserDto {
                FullName=fullName, Username=username, Email=email, PasswordHash=HashPassword(password), IsActive=true
            };
            return _repo.CreateFirstAdmin(dto);
        }
        return false;
    }
    public LoginUserDto? ValidateLogin(string usernameOrEmail,string password) {
        var u=_repo.FindUserForLogin(usernameOrEmail.Trim());
        if(u==null||!u.IsActive||!VerifyPassword(password,u.PasswordHash))return null;
        u.PasswordHash="";
        return u;
    }
    public List<StockRowDto> LowStockNotifications()=>_repo.GetLowStockNotifications();
    private static bool VerifyPassword(string password,string stored) {
        try {
            var parts=stored.Split('$');
            if(parts.Length!=4||parts[0]!="PBKDF2-SHA256"||!int.TryParse(parts[1],out var iterations))return false;
            var salt=Convert.FromBase64String(parts[2]);
            var expected=Convert.FromBase64String(parts[3]);
            var actual=Rfc2898DeriveBytes.Pbkdf2(password,salt,iterations,HashAlgorithmName.SHA256,expected.Length);
            return CryptographicOperations.FixedTimeEquals(actual,expected);
        }
        catch {
            return false;
        }
    }
    public List<SelectOptionDto> Products()=>_repo.GetProducts();
    public List<SelectOptionDto> Warehouses()=>_repo.GetWarehouses();
    public List<SelectOptionDto> Suppliers()=>_repo.GetSuppliers();
    public List<SelectOptionDto> Customers()=>_repo.GetCustomers();
    public List<SelectOptionDto> Roles()=>_repo.GetRoles();
    public List<StockRowDto> Stock()=>_repo.GetStock();
    public List<TransactionRowDto> Transactions()=>_repo.GetTransactions();
    public List<OrderRowDto> Purchases()=>_repo.GetPurchases();
    public List<OrderRowDto> Sales()=>_repo.GetSales();
    public OrderDetailsDto? Purchase(int id)=>_repo.GetPurchase(id);
    public OrderDetailsDto? Sale(int id)=>_repo.GetSale(id);
    public bool CreatePurchase(CreateOrderDto d)=>_repo.CreatePurchase(d);
    public bool CreateSale(CreateOrderDto d)=>_repo.CreateSale(d);
    public bool Adjust(StockAdjustmentDto d)=>_repo.AdjustStock(d);
    public DashboardDto Dashboard()=>_repo.GetDashboard();
    public List<UserDto> Users()=>_repo.GetUsers();
    public UserDto? User(int id)=>_repo.GetUser(id);
    public bool SaveUser(UserDto d) {
        if(!string.IsNullOrWhiteSpace(d.Password))d.Password=HashPassword(d.Password);
        return _repo.SaveUser(d);
    }
    public bool DeleteUser(int id)=>_repo.DeleteUser(id);
    private static string HashPassword(string password) {
        var salt=RandomNumberGenerator.GetBytes(16);
        var hash=Rfc2898DeriveBytes.Pbkdf2(password,salt,210000,HashAlgorithmName.SHA256,32);
        return $"PBKDF2-SHA256$210000${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }
}
