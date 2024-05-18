using CMPBEMAssistant.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CMPBEMAssistant.Test
{
    [TestClass]
    public class UnitTestEmail
    {
        private KnownDataSource emailDataSource;

        public UnitTestEmail()
        {
            //var configuration = new ConfigurationBuilder()
            //    .AddUserSecrets<>()
            //    .Build();
            string? baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile(baseDir  + "/config.json", false, true);
            var configuration = builder.Build();

            emailDataSource = new KnownDataSource()
            {
                Address = configuration["Email.Address"],
                Name = configuration["Email.Name"],
                Password = configuration["Email.Password"],
            };
            Console.WriteLine(configuration.GetDebugView());
        }

        [TestMethod]
        public void TestReadEmail()
        {
            // test credentials
           // KnownDataSource ds = GetTestDataSourceProperties();

        }

        public void TestSendEmail()
        {

        }
    }
}