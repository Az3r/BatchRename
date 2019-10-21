using System;
using System.IO;

namespace BatchRename.Shared
{
    public class PathIsNotAbsoluteException : IOException
    {
        public PathIsNotAbsoluteException() : base() { }
        public PathIsNotAbsoluteException(string message) : base(message) { }
        public PathIsNotAbsoluteException(string message, Exception inner) : base(message, inner) { }
    }
}
