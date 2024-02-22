using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toxy.Proxy
{
    public class TokenizationService
    {

        private string detokenUrl = "";
        private string tokenUrl = "";

        public TokenizationService(string detokenUrl, string tokenUrl)
        {
            this.detokenUrl = detokenUrl;
            this.tokenUrl = tokenUrl;
        }
    }
}
