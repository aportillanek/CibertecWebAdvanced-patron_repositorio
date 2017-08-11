using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

namespace Cibertec.RealTime.Hubs

{
    public class NotificationHub:Hub
    {
      

        public void AddProductId(List<int> productListIds,int id)
        {
            productListIds.Add(id);
            Clients.All.addProductId(productListIds);
        }

        public void RemoveProductId(List<int> productListIds,int id)
        {
            productListIds.Remove(id);
            Clients.All.removeProductId(productListIds);
        }
    }
}