using AutoMapper;
using SmartInventory.Model.CrudDto;
using SmartInvetory.DAL;
using SmartInvetory.DAL.EF.Tables;
namespace SmartInventroy.BLL.Service;
public class CategoryService
{
    private readonly DataAccessFactory _f;
    private readonly IMapper _m;
    public CategoryService(DataAccessFactory f,IMapper m) {
        _f=f;
        _m=m;
    }
    public List<CategoryDto> GetAll()=>_m.Map<List<CategoryDto>>(_f.GetCategoryRepository().GetAll());
    public CategoryDto? Get(int id) {
        var x=_f.GetCategoryRepository().GetByID(id);
        return x==null?null:_m.Map<CategoryDto>(x);
    }
    public bool Save(CategoryDto d) {
        var r=_f.GetCategoryRepository();
        if(d.CategoryId==0) {
            var x=_m.Map<Category>(d);
            return r.Add(x);
        }
        var old=r.GetByID(d.CategoryId);
        if(old==null)return false;
        _m.Map(d,old);
        return r.Update(old);
    }
    public bool Delete(int id)=>_f.GetCategoryRepository().Delete(id);
}
public class SupplierService
{
    private readonly DataAccessFactory _f;
    private readonly IMapper _m;
    public SupplierService(DataAccessFactory f,IMapper m) {
        _f=f;
        _m=m;
    }
    public List<SupplierDto> GetAll()=>_m.Map<List<SupplierDto>>(_f.GetSupplierRepository().GetAll());
    public SupplierDto? Get(int id) {
        var x=_f.GetSupplierRepository().GetByID(id);
        return x==null?null:_m.Map<SupplierDto>(x);
    }
    public bool Save(SupplierDto d) {
        var r=_f.GetSupplierRepository();
        if(d.SupplierId==0) {
            var x=_m.Map<Supplier>(d);
            x.CreatedAt=DateTime.Now;
            return r.Add(x);
        }
        var old=r.GetByID(d.SupplierId);
        if(old==null)return false;
        _m.Map(d,old);
        return r.Update(old);
    }
    public bool Delete(int id)=>_f.GetSupplierRepository().Delete(id);
}
public class CustomerService
{
    private readonly DataAccessFactory _f;
    private readonly IMapper _m;
    public CustomerService(DataAccessFactory f,IMapper m) {
        _f=f;
        _m=m;
    }
    public List<CustomerDto> GetAll()=>_m.Map<List<CustomerDto>>(_f.GetCustomerRepository().GetAll());
    public CustomerDto? Get(int id) {
        var x=_f.GetCustomerRepository().GetByID(id);
        return x==null?null:_m.Map<CustomerDto>(x);
    }
    public bool Save(CustomerDto d) {
        var r=_f.GetCustomerRepository();
        if(d.CustomerId==0) {
            var x=_m.Map<Customer>(d);
            x.CreatedAt=DateTime.Now;
            return r.Add(x);
        }
        var old=r.GetByID(d.CustomerId);
        if(old==null)return false;
        _m.Map(d,old);
        return r.Update(old);
    }
    public bool Delete(int id)=>_f.GetCustomerRepository().Delete(id);
}
public class WarehouseService
{
    private readonly DataAccessFactory _f;
    private readonly IMapper _m;
    public WarehouseService(DataAccessFactory f,IMapper m) {
        _f=f;
        _m=m;
    }
    public List<WarehouseDto> GetAll()=>_m.Map<List<WarehouseDto>>(_f.GetWarehouseRepository().GetAll());
    public WarehouseDto? Get(int id) {
        var x=_f.GetWarehouseRepository().GetByID(id);
        return x==null?null:_m.Map<WarehouseDto>(x);
    }
    public bool Save(WarehouseDto d) {
        var r=_f.GetWarehouseRepository();
        if(d.WarehouseId==0) {
            var x=_m.Map<Warehouse>(d);
            return r.Add(x);
        }
        var old=r.GetByID(d.WarehouseId);
        if(old==null)return false;
        _m.Map(d,old);
        return r.Update(old);
    }
    public bool Delete(int id)=>_f.GetWarehouseRepository().Delete(id);
}
