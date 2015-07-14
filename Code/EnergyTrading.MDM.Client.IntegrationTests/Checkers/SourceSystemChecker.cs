namespace EnergyTrading.Mdm.Client.IntegrationTests.Checkers
{
    using EnergyTrading.Mdm.Contracts;

    public class SourceSystemChecker : NCheck.Checker<SourceSystem>
    {
        public SourceSystemChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
        }
    }
}
