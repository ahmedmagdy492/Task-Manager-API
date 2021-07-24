using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Helpers
{
    public class Sha256HashService : ISha256HashService
    {
        private readonly SHA256 _sha256;

        public Sha256HashService()
        {
            _sha256 = SHA256.Create();
        }

        public string Hash(string txt)
        {
            return Converter.ToHexString(_sha256.ComputeHash(Encoding.UTF8.GetBytes(txt)));
        }
    }
}
