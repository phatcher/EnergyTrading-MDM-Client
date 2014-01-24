﻿// <autogenerated>
//   This file was generated by T4 code generator CreateIntegrationTestsScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace MDM.Client.IntegrationTests.BrokerCommodity
{
    using EnergyTrading.MDM.Client.Services;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    public class BrokerCommodityData
    {
        public static BrokerCommodity PostBasicEntity()
        {
            var mdmService = ServiceLocator.Current.GetInstance<IMdmEntityService<BrokerCommodity>>();

            var entity = ObjectMother.Create<BrokerCommodity>();
            var mappings = entity.Identifiers;
            entity.Identifiers = new NexusIdList();
            SetAdditionalData(entity);
            var response = mdmService.Create(entity);
            
                        Assert.IsTrue(response.IsValid, "###Error : " + response.Code + " : " + (response.Fault == null ? string.Empty : response.Fault.Message + " : " + response.Fault.Reason));

            var createdEntity = response.Message;

            foreach (var identifier in mappings)
            {
                var mappingResponse = mdmService.CreateMapping(createdEntity.ToNexusKey(), identifier);
                Assert.IsTrue(mappingResponse.IsValid);
                createdEntity.Identifiers.Add(identifier);
            }

            return createdEntity;
        }

        protected static void SetAdditionalData(BrokerCommodity entity)
        {
			
            var broker = IntegrationTests.Broker.BrokerData.PostBasicEntity();
            entity.Details.Broker = broker.ToNexusId().ToEntityId();
            var commodity = IntegrationTests.Commodity.CommodityData.PostBasicEntity();
            entity.Details.Commodity = commodity.ToNexusId().ToEntityId();
	
        }
    }
}
