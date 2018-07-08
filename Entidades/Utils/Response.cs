using System.Collections.Generic;

namespace Entidades.Utils
{
    public  class Response
    {
        public  bool IsSuccess { get; set; }
        public  string Message { get; set; }
        public object Resultado { get; set; }
    }
}
