using System.Threading.Tasks;
using WebSocketManager;

namespace ChatAppV1.Core
{
    public class ChatHandler : WebSocketHandler
    {
        public ChatHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager) {}

        public async Task TriggerClients()
        {
            await InvokeClientMethodToAllAsync("clientCallback");   
        }
    }
}
