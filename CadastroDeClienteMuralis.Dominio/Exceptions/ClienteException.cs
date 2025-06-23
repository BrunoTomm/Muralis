﻿using System;

namespace CadastroDeClienteMuralis.Dominio.Exceptions
{
    public class ClienteException : Exception
    {
        public ClienteException() { }

        public ClienteException(string message)
            : base(message) { }

        public ClienteException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
