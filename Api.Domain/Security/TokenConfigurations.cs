using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Security
{
    public class TokenConfigurations
    {
        public string Audience { get; set; } // Significa o Público
        public string Issuer { get; set; } // o Emissor
        public int Seconds { get; set; } // Tempo de validade do token
    }
}
