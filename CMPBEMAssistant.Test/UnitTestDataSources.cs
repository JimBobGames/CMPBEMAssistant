using CMPBEMAssistant.Core;

namespace CMPBEMAssistant.Test
{
    [TestClass]
    public class UnitTestDataSources
    {
        [TestMethod]
        public void TestDataSourceProperties()
        {
            // should be empty or have existing datasources
            List<KnownDataSource> dataSources =  DataProtectionHelper.LoadKnownDataSources();
            Assert.IsNotNull(dataSources, "Should be empty or contain data");

            // add a new data source
            dataSources.Clear();
            KnownDataSource ds = new KnownDataSource() { Address = "AAA", Name = "BBB", Password = "CCC" };
            dataSources.Add(ds);

            // save this to the file
            DataProtectionHelper.SaveKnownDataSources(dataSources);

            // read it back
            dataSources = DataProtectionHelper.LoadKnownDataSources();
            Assert.IsNotNull(dataSources, "Should be empty or contain data");
            Assert.AreEqual(dataSources.Count, 1, "Should be size 1");

            Assert.AreEqual(dataSources[0].Address, "AAA", "Should be size AAA");
            Assert.AreEqual(dataSources[0].Name, "BBB", "Should be size BBB");
            Assert.AreEqual(dataSources[0].Password, "CCC", "Should be size CCC");
        }
    }
}