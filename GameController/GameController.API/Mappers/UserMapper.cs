using AutoMapper;
using GameController.API.Models.User;
using GameController.Services.Models.User;
using System.Security.Cryptography;

namespace GameController.API.Mappers
{
    /// <summary>
    /// Mappings for User models.
    /// </summary>
    public class UserMapper : Profile
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;
        public UserMapper()
        {
            // Controller models -> Service models
            CreateMap<CreateUserModel, CreateUserDto>()
                .ForMember(x => x.PasswordHash, opt => opt.MapFrom(x => HashPassword(x.Password,5)));
            CreateMap<UpdateUserModel, UpdateUserDto>();

            // Service models -> Controller models
            CreateMap<UserDto, UserModel>();
        }

        private string HashPassword(string password, int iterations = 5) 
        {  
                // Create salt
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

                // Create hash
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
                var hash = pbkdf2.GetBytes(HashSize);

                // Combine salt and hash
                var hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                // Convert to base64
                var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
        }
    }
}
