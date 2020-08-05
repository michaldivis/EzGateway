namespace EzGateway.Interfaces
{
    public interface IEzGateway
    {
        void SendMessage(string recipient, string messageText);
        void SendMessage(string recipient, string messageText, string messageTitle);
    }
}
