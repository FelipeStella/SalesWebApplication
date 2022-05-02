using System;

namespace SalesWebApplication.Services.Exceptions
{
  public class NotFoundException : ApplicationException
  {
    public NotFoundException(string message) : base(message)
    {
    }
  }
}
