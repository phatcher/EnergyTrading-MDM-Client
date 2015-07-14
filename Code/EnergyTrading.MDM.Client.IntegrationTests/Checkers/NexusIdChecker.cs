using EnergyTrading.Mdm.Contracts;

namespace EnergyTrading.Mdm.Client.IntegrationTests.Checkers
{
    public class MdmIdChecker : NCheck.Checker<MdmId>
    {
        public MdmIdChecker()
        {
            Compare(x => x.SystemId);
            Compare(x => x.SystemName);
            Compare(x => x.Identifier);
        }
    }
}