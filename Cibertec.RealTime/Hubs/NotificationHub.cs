using Microsoft.AspNet.SignalR;

namespace Cibertec.RealTime.Hubs

{
    public class NotificationHub:Hub
    {
        public void UpdateProduct(int id)
        {

            Clients.All.updateProduct(id);
        }
    }
}