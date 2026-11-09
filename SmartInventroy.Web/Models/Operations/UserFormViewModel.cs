using Microsoft.AspNetCore.Mvc.Rendering;using SmartInventory.Model.OperationsDto;
namespace SmartInventroy.Web.Models.Operations;public class UserFormViewModel{public UserDto User{get;set;}=new();public List<SelectListItem> Roles{get;set;}=new();}
