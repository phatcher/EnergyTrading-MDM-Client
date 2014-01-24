﻿// <autogenerated>
//   This file was generated by T4 code generator CreateIntegrationTestsScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace MDM.Client.IntegrationTests.Party.MdmEntityLocatorFactory
{
	using System.Configuration;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class NonCachedMdmEntityLocatorFactoryTests : MdmEntityLocatorFactoryIntegrationTestBase
    {
        private Party expected;

        protected override void OnSetup()
        {
			ConfigurationManager.AppSettings["MdmCaching"] = false.ToString();

            base.OnSetup();

            expected = PartyData.PostBasicEntity();
        }

        [TestMethod]
        public void ShouldSuccessfullyLocateEntity()
        {
            // given
            var nexusId = expected.Identifiers.First(id => id.SystemName == "Nexus");

            // when
            var candidate = this.MdmEntityLocatorService.Get<Party>(nexusId);

            // then
            this.Check(expected, candidate);
        }

        [TestMethod]
        public void ShouldReturnDefaultWhenUnableToLocateEntity()
        {
            // given
            var nexusId = new NexusId { SystemName = "Nexus", Identifier = "0" };

            // when
            var candidate = this.MdmEntityLocatorService.Get<Party>(nexusId);

            // then
            Assert.AreEqual(default(Party), candidate);
        }
    }
}
