using CustomersOrderOtomation.Data.DBOperations;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CustomersOrderOtomation.Data.Repository.Concrete
{
    public class ShopListRepository : GenericRepository<ShopList>, IShopListRepository
    {
        string connectingString = "";
        private readonly Microsoft.AspNetCore.SignalR.IHubContext<NotifyHub> hubContext;

        public ShopListRepository(DatabaseContext context, IConfiguration configuration, IHubContext<NotifyHub> hubContext) : base(context)
        {
            connectingString = configuration.GetConnectionString("SqlServerConnectionString");
            this.hubContext = hubContext;
        }

        public async Task<ShopList> GetByIdAsyncWithProductsAsync(int id, int userId)
        {
            return await _context.ShopLists.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ShopList>> GetAllWithProductsForAdminAsync()
        {
            return await _context.ShopLists.ToListAsync();
        }

        public List<ShopList> GetAllShopListWithSıgnalR()
        {
            var shopLists = new List<ShopList>();
            using(SqlConnection connection = new SqlConnection(connectingString))
            {
                connection.Open();
                SqlDependency.Start(connectingString);

                string commandText = "select [OrderCustomerName],[OrderTableNumber],[ShopListProductsData] from dbo.ShopLists";

                SqlCommand cmd = new SqlCommand(commandText, connection);

                SqlDependency dependency = new SqlDependency(cmd);

                dependency.OnChange += new OnChangeEventHandler(dbChangeNotification);

                var reader = cmd.ExecuteReader();

                while(reader.Read()) {
                    var shopList = new ShopList
                    {
                        OrderCustomerName = reader["OrderCustomerName"].ToString(),
                        OrderTableNumber = reader["OrderTableNumber"].ToString(),
                        ShopListProductsData = reader["ShopListProductsData"].ToString(),
                    };

                    shopLists.Add(shopList);    
                }

                connection.Close();

                return shopLists;
            }  
        }

        private void dbChangeNotification(object sender, SqlNotificationEventArgs e) 
        {
            //var notifications = e.ToString();

            hubContext.Clients.All.SendAsync("refreshShoplists");
        }
    }
}
