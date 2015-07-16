namespace EnergyTrading.Mdm.Client.IntegrationTests.SourceSystem.MdmEntityLocatorFactory
{
    using System.Configuration;
    using System.Linq;

    using EnergyTrading.Mdm.Client.Constants;
    using EnergyTrading.Mdm.Contracts;

    using NUnit.Framework;

    [TestFixture]
    public class NonCachedMdmEntityLocatorFactoryTests : MdmEntityLocatorFactoryIntegrationTestBase
    {
        private SourceSystem expected;

        protected override void OnSetup()
        {
            ConfigurationManager.AppSettings["MdmCaching"] = false.ToString();

            base.OnSetup();

            expected = SourceSystemData.PostBasicEntity();
        }

        [Test]
        public void ShouldSuccessfullyLocateEntity()
        {
            // given
            var nexusId = expected.Identifiers.First(id => id.SystemName == MdmConstants.MdmName);

            // when
            var candidate = MdmEntityLocatorService.Get<SourceSystem>(nexusId);

            // then
            this.Check(expected, candidate);
        }

        [Test]
        public void ShouldReturnDefaultWhenUnableToLocateEntity()
        {
            // given
            var nexusId = new MdmId { SystemName = MdmConstants.MdmName, Identifier = "0" };

            // when
            var candidate = MdmEntityLocatorService.Get<SourceSystem>(nexusId);

            // then
            Assert.AreEqual(default(SourceSystem), candidate);
        }
    }
}
