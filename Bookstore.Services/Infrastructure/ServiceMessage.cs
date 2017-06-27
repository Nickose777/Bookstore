namespace Bookstore.Services.Infrastructure
{
    public class ServiceMessage
    {
        public ActionState ActionState { get; private set; }

        public string Message { get; private set; }

        public ServiceMessage(ActionState actionState, string message)
        {
            this.ActionState = actionState;
            this.Message = message;
        }
    }
}
