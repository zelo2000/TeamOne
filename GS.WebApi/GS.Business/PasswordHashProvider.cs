using GS.Business.Infrastructure;
using GS.Domain.Models.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System;

namespace GS.Business
{
    public class PasswordHashProvider : IPasswordHashProvider
    {
        private readonly HashGenerationSettings _hashGenerationSetting;

        public PasswordHashProvider(IOptions<HashGenerationSettings> option)
        {
            _hashGenerationSetting = option.Value;
        }


        public string GetPasswordHash(string password)
        {
            var saltBytes = Convert.FromBase64String(_hashGenerationSetting.Salt);

            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                        password: password,
                        salt: saltBytes,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: _hashGenerationSetting.IterationCount,
                        numBytesRequested: _hashGenerationSetting.BytesNumber
                    )
                );

            return hashed;
        }
    }
}
