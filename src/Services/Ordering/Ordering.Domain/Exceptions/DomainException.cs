﻿namespace Ordering.Domain.Exceptions
{
    public class DomainException : Exception
    {
      
        public DomainException(string message) : base($"Domain excpetion: \"{message}\" throws from Domain Layer")
        {
        }

      
    }
}