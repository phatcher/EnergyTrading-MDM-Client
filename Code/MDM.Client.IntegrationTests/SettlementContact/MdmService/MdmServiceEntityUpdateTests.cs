﻿// <autogenerated>
//   This file was generated by T4 code generator CreateIntegrationTestsScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace MDM.Client.IntegrationTests.SettlementContact.MdmService
{
	using System.Configuration;
    using System.Linq;
    using System.Net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class MdmServiceEntityUpdateIntegrationTests : MdmServiceIntegrationTestBase
    {
        private SettlementContact settlementcontact;

        protected override void OnSetup()
        {
			ConfigurationManager.AppSettings["MdmCaching"] = false.ToString();

            base.OnSetup();

            settlementcontact = SettlementContactData.PostBasicEntity();
        }
	
        [TestMethod]
        public void ShouldSucceedUpdateWhenETagMatches()
        {
            // given
            var id = int.Parse(settlementcontact.ToNexusId().Identifier);

            // when
            var response = MdmService.Get<SettlementContact>(id);
            var entity = response.Message;
            entity.Identifiers = new NexusIdList() {entity.Identifiers.SystemId()};

            // then
            response = MdmService.Update(id, entity, response.Tag);
            Assert.IsTrue(response.IsValid, "###Error : " + response.Code + " : " + (response.Fault == null ? string.Empty : response.Fault.Message + " : " + response.Fault.Reason));
        }

        [TestMethod]
        public void ShouldFailUpdateWhenETagDiffers()
        {
            // given
            var id = int.Parse(settlementcontact.ToNexusId().Identifier);

            // when
            var response = MdmService.Get<SettlementContact>(id);

            // then
            response = MdmService.Update(id, response.Message, "\"999888777666555\"");
            Assert.IsFalse(response.IsValid);
            Assert.AreEqual(HttpStatusCode.PreconditionFailed, response.Code);
            Assert.AreEqual("Exception of type 'EnergyTrading.MDM.Services.VersionConflictException' was thrown.", response.Fault.Message);
        }
    }
}
