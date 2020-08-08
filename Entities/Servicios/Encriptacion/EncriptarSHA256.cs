using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Entities
{

 public   class EncriptarSHA256 : Enciptador 
    {
        public EncriptarSHA256(string _valorInicial)
        {
            valorInicial = _valorInicial;
        }

        public override int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string Hashear()
        {
            SHA512 SHA256 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(this.valorInicial);
            byte[] hash = SHA256.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= hash.Length - 1; i++)
            {
                stringBuilder.Append(hash[i].ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}
