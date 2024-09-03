using E_D_Project_1.Models;
using E_D_Project_1.Repository;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;

namespace E_D_Project_1.Service
{
    public class EdService : IEdService
    {
        private readonly IEdRepository _edRepository;
        private readonly IConfiguration _configuration;
        private readonly string _encryptionKey;

        public EdService(IEdRepository edRepository, IConfiguration configuration)
        {
            _edRepository = edRepository;
            _configuration = configuration;
            _encryptionKey = configuration.GetValue<string>("EncryptionKey"); 
        }

        public async Task<List<Ed1>> GetAllAsync()
        {
            return await _edRepository.GetAllUsersAsync();
          
        }

        public async Task<string> LoginUserAsync(Ed1 request)
        {
            var user = await _edRepository.GetUserByUsernameAsync(request.Username);
            if (user == null) // If it is Null then it will return "Invalid username or password":
            {
                return "Invalid username or password";
            }
            string decryptedPassword = Decrypt(user.Password);
            if (decryptedPassword == request.Password)
            {
                return "Login successful";
            }

            return "Invalid username or password !!!";
        }

            

        public async Task<Ed1> RegisterUserAsync(Ed1 request)
        {
            request.Password = Encrypt(request.Password); // This Encrypt the password
            var response = await _edRepository.AddUserAsync(request);

            if (response.StatusCode == 0)
            {
                return request; // Registration successful
            }
            else
            {
                throw new Exception("User registration failed.");
            }
        }




        private string Encrypt(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;              

            using (Aes aes = Aes.Create())  
            {
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }  
            return Convert.ToBase64String(array);
        }

        
  
        

        private string Decrypt(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
