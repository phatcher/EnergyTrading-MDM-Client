﻿// <autogenerated>
//   This file was generated by T4 code generator CreateIntegrationTestsScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace MDM.Client.IntegrationTests.SettlementContact.MdmEntityLocatorFactory
{
	using System.Configuration;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class CachedMdmEntityLocatorFactoryTests : NonCachedMdmEntityLocatorFactoryTests
	{
		protected override void OnSetup()
		{
			ConfigurationManager.AppSettings["MdmCaching"] = true.ToString();

			base.OnSetup();
		}
	}
}
