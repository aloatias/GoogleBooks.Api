﻿namespace GoogleBooks.Api.Dtos.Output.Exceptions
{
    public class ErrorBase
    {
        public string ErrorMessage { get; private set; }

        public ErrorBase(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
