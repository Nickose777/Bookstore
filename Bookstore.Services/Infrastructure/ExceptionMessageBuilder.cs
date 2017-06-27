using System;
using System.Text;

namespace Bookstore.Services.Infrastructure
{
    static class ExceptionMessageBuilder
    {
        public static string BuildMessage(Exception ex)
        {
            StringBuilder builder = new StringBuilder();

            do
            {
                builder.AppendFormat("{0}; ", ex.Message);
                ex = ex.InnerException;
            } while (ex != null);

            return builder.ToString();
        }
    }
}
