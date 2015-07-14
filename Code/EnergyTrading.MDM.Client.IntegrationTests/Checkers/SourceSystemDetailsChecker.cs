namespace EnergyTrading.Mdm.Client.IntegrationTests.Checkers
{
    using EnergyTrading.Mdm.Contracts;

    public class SourceSystemDetailsChecker : NCheck.Checker<SourceSystemDetails>
    {
        public SourceSystemDetailsChecker()
        {
            Compare(x => x.Name);
        }
    }
}