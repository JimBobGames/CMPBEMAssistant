using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents.Serialization;
using System.Configuration;

namespace CMPBEMAssistant.Core
{
    public class DataProtectionHelper
    {
        static byte[] s_additionalEntropy = { 11, 9, 4, 8 , 5, 9, 8, 7, 6, 5 };

        static string propfilename = "CMPBEMAssistant.properties";

        public static List<KnownDataSource> LoadKnownDataSources()
        {
            
            try
            {
                string? baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                byte[] rawData = File.ReadAllBytes(baseDir + "/" + propfilename);
                byte[] unprotectedData = Unprotect(rawData);


                List<KnownDataSource> dataSources = ByteArrayToObject<List<KnownDataSource>>(unprotectedData);

                return dataSources;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<KnownDataSource>();
            }
        }

        public static void SaveKnownDataSources(List<KnownDataSource> dataSources)
        {
            string? baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            byte[] rawData = ObjectToByteArray(dataSources);
            byte[] protectedData = Protect(rawData);
            File.WriteAllBytes(baseDir + "/" + propfilename, protectedData);
        }

        /// <summary>
        /// Convert an object to a Byte Array.
        /// </summary>
        public static byte[] ObjectToByteArray(object objData)
        {
            if (objData == null)
                return default;

            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(objData, GetJsonSerializerOptions()));
        }

        /// <summary>
        /// Convert a byte array to an Object of T.
        /// </summary>
        public static T ByteArrayToObject<T>(byte[] byteArray)
        {
            if (byteArray == null || !byteArray.Any())
                return default;

            return JsonSerializer.Deserialize<T>(byteArray, GetJsonSerializerOptions());
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }

        public static byte[] Protect(byte[] data)
        {
            try
            {
                // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted
                // only by the same current user.
                return ProtectedData.Protect(data, s_additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not encrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static byte[] Unprotect(byte[] data)
        {
            try
            {
                //Decrypt the data using DataProtectionScope.CurrentUser.
                return ProtectedData.Unprotect(data, s_additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not decrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        /*

        public static class ApiKeys
        {

            private static IConfiguration Config { get; set; }
            public static string PublicKey => Config.GetValue<string>("service-public-key");
            public static string SecretKey => Config.GetValue<string>("service-secret-key");


            static ApiKeys()
            {
                Config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                //.AddIniFile( "api-keys.ini", true ) // in this case you must add "api-keys.ini" to .gitignore file.
                .Build();
            }


        }
        */
    }
}
