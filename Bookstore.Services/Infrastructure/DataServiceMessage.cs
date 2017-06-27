namespace Bookstore.Services.Infrastructure
{
    public class DataServiceMessage<TData> : ServiceMessage where TData : class
    {
        public TData Data { get; private set; }

        public DataServiceMessage(ActionState actionState, string message, TData data)
            : base(actionState, message)
        {
            this.Data = data;
        }
    }
}
