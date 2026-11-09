# SmartInventory implementation - milestone 1

## Included in this update
- Reused the existing synchronous generic repository interface.
- Made the generic repository and ProductRepository public for cross-project DI/use.
- Added Category, Supplier, Customer and Warehouse repositories and DataAccessFactory accessors.
- Added CRUD DTOs and AutoMapper mappings for those four entities.
- Added BLL CRUD services and MVC CRUD controllers/views for those entities.
- Connected Product Create/Edit category dropdowns to the database through BLL.
- Added Product search/category/status filtering and POST delete.
- Made DbContext OnConfiguring respect the DI-provided SQL Server connection.

## Database facts respected
- Product has CategoryId, but no SupplierId. Supplier selection belongs to Purchase Orders.
- Product has no InitialStock column. Initial stock must be recorded by a stock transaction and StockBalance update.
- PurchaseOrder and SalesOrder require CreatedBy user IDs. Their workflows need an authenticated/current user strategy before implementation.

## Configuration
Check `SmartInventroy.Web/appsettings.json` and ensure `ConnectionStrings:dbconn` points to your existing SmartInventoryDB. Do not commit real credentials.

## Important verification
The current environment did not have the .NET SDK available, so this update has not been compiled or runtime-tested. Run `dotnet restore` and `dotnet build` in Visual Studio/terminal, then resolve any package or compiler diagnostics before using it.

## Not yet implemented in this milestone
- Inventory adjustments and stock transaction posting.
- Purchase order creation/receiving.
- Sales order creation/stock deduction.
- Dashboard aggregation and reports.
- User authentication/roles/settings workflows.
These require transaction-safe workflows and the CreatedBy/user authorization design.
