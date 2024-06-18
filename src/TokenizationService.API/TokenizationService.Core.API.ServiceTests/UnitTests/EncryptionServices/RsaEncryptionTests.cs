using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TokenizationService.Core.API.Services.EncryptionServices;

namespace TokenizationService.Core.API.Tests.UnitTests.EncryptionServices
{
    public class RsaEncryptionTests
    {
        private const string KEY = "<RSAKeyValue><Modulus>wX/tSxYs2ic7kFhshFRAgDa5ILFmI4x5Wl52eO1nPVEM4KTX+AD+WpCachdSWO8biyPyDLyKtXk8IUydZKOrhm1NVbuOX1QM7NdYEaUyWnZb/gTSNyyu1lBHQtpegqnHH5SEI9SNOfQ8nybrgrAO4w3ZuME9u/oPBsHUjT2Oqi1pb+iPeGzFeAW3tscYI6MwaufvFAIGOjlZeANF6PWyFXSOonCr9SB0nvQzmB38RrE3Jt+TB7cfC5qwnG76q2QF3rNDPDmv7SaVoKi9ukdkUILlCGhNVBVlEXzPrUZd+kDk1VDtkpRzt8sGmUCshvHn49FQxfViAJQ48Wq4meaJBQ==</Modulus><Exponent>AQAB</Exponent><P>3Dw7s2TXw1ExtFLLM+rZVtjolpnS1bIBZIMk+lhdwNNKCJoqvxJ74aLVWcHoFDa7te2N3BPCnXXnBpD7uV1i+WQ5pcKRbKGciu5NYrWKZ1gXf8u7LP4ZU8x8xZnmVKB7+5I9VugVHj1L0JBqGSCQx+Odm5WhZqOXY09GGVShZ98=</P><Q>4Ow5khOpSIRohaZBby6M8k7xsbS7/rCJulrOPx8+JKIWXMtO4Z1isjIy6ayv6bMNEatiTKzHNMF+deJV3EEmgwwdjPlq6rHFmjFtgYB8wlrGOHnEwaJpHJ0ljaGRUoq9sF1OtilknqExxgOhUm6v8JWL4Tf8eaoURbamOvNL+5s=</Q><DP>EHg8qrS+4e9ffTG32oEUkccU1bZZKvvt2Ltp94LGOBZU8gG9cp6KPxxzQgwbM+/H59oFAtFDe40GotoSyRDvxuC04u2C/JG1aDpb9SbIW+QdlcQBkmLKOBcRuaFBtVBvFSghiC9A7Qr+cXFrTce+rAQclDcDY+6V8qpJ/rjyfRc=</DP><DQ>GUezuM1o63OmY88p75sl0F47ICQWNSwWbtGh+MpEK83kAY4AUa9k1m559nWoV/X99Ax1QfTxduaAljNYPyc7cuM7twqZj8NnWEtR6YpNe3LEcLiO+syMT+EBxYdxU/uwElCKdkL0wjeP5pel3CQAtmO4kxm90Nbi7Gr5CAsscR0=</DQ><InverseQ>s21Te8Rq8JJ1tLvRHikXElomZt6MaWdfnOtWOp/Rn6wdcbfSsIQvwIucDFehXbmnAtgJQIYcfER2Q6ys0CqAzN8K4MQd7hkhn1nunnhB7yXTgWjx+tw1jyuxzDrupCeZ0uIspuE2c8/jFDIhghA9qqUgj1aVTyOPoVu5wbDSICg=</InverseQ><D>HeHsuoDVMmeGqlcNgy4ju+k3k9XxqSHDh/I4p5o6LFz8+SBb/f/hSAk24NG7NOLQRR+q/M6NOVNctuPHFsiz75GntMq/RPeM+3KSNj9l1FSdBlCmSkELSyugpbFlkRY5GyZrdcEYgk+2oPl4u3kHA4ebDQ5Cw2ntsiRy99D+3EbcVzh1wCEeTPf+tjM6afdX26IbFDkjBGTyxeIemc69bCdQ6wpX4j03Qte+AhugWabHu4qyxgU37Tm+MW9JsL97I+Dx52wR58VPfom84tCsfLYKgJs0LNVQFTAHseezkatS3SEXbri6bjqoRxWfT+E8KmN6t8qDzCZgO4UMZyBr7Q==</D></RSAKeyValue>";
        private const string SALT = "SaltCity";
        private const string CLEAR = "NowYouSeeMe";
        private const string ENCRYPTED = "pBkV5dEUBgLDiXkg6BN188LsPxCnk02DDwq5+cKhY5rXyS6eqPkHF5cITwmq8IXOUK452xM+mC1wuT3UnEC66umvlWzF7N9FndYcK4so6fTsvX5xEXhMCW7kmh8yU5WaBL6yYUOgGQr4/byN6pj03JC62jsXpKbW5hB4QFgxr8+eJDHsTgB3SeQlMyWkIZf0JFzfmbOW7WJxg2/sRqc2B0+6Lw4jemtlnKR0HEcPEFuI2qKNn5qlqkZRkQDNCvbKO+RNUqm1Q4TaUolWHS3Eb7lbyzoH6qQvzQgxwiMq9+U/gS8oqBgeHF6j8zfc/WeL9reMcVJlMMKKBcxSlLgysw==";

        [Test]
        public void BasicEncryption()
        {
            var service = new RsaEncryptionService();

            var encryption = service.EncryptString(CLEAR, KEY, SALT);

            Assert.That(encryption, Is.Not.Null);
            Assert.That(ENCRYPTED, Is.EqualTo(ENCRYPTED));
        }

        [Test]
        public void BasicDecryption()
        {
            var service = new RsaEncryptionService();

            var clear = service.DecryptString(ENCRYPTED, KEY, SALT);

            Assert.That(clear, Is.Not.Null);
            Assert.That(clear, Is.EqualTo(CLEAR));
        }
    }
}
