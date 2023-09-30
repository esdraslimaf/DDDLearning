using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Api.Domain.Security
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; set; } //Esta linha define uma propriedade chamada Key que armazena a chave de segurança usada para assinar o token.
        public SigningCredentials SigningCredentials { get; set; } // Esta linha define uma propriedade chamada SigningCredentials que armazena as credenciais usadas para assinar o token.

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048)) // Esta linha cria um novo provedor de serviços criptográficos RSA com uma chave de 2048 bits. O RSA é um algoritmo de criptografia assimétrica, o que significa que usa duas chaves: uma chave pública para criptografar os dados e uma chave privada para descriptografá-los.
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true)); //Esta linha cria uma nova chave de segurança RSA a partir dos parâmetros do provedor de serviços criptográficos RSA e a atribui à propriedade Key.
            }
            SigningCredentials = new SigningCredentials(Key,SecurityAlgorithms.RsaSha256Signature); //Esta linha cria novas credenciais de assinatura usando a chave RSA e o algoritmo RSA SHA256, e atribui à propriedade SigningCredentials.

        }
    }
}


/* Toda essa classe é necessária para configurar as credenciais e a chave usadas na assinatura de tokens de segurança. A chave é gerada a partir de um provedor de criptografia RSA e é usada junto com o algoritmo RSA com SHA-256 para criar credenciais de assinatura que garantem a segurança dos tokens gerados pelo sistema. Esses tokens são frequentemente usados em sistemas de autenticação e autorização para verificar a identidade dos usuários. */
