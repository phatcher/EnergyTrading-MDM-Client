﻿// <autogenerated>
//   This file was generated by T4 code generator CreateIntegrationTestsScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace MDM.Client.IntegrationTests.Checkers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class ShapeDetailsChecker : Checker<ShapeDetails>
    {
        public ShapeDetailsChecker()
        {
			            Compare(x => x.Name);
			        }
    }
}
