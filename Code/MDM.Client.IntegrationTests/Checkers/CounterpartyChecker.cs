﻿// <autogenerated>
//   This file was generated by T4 code generator CreateIntegrationTestsScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace MDM.Client.IntegrationTests.Checkers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class CounterpartyChecker : Checker<Counterparty>
    {
        public CounterpartyChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
        }
    }
}
