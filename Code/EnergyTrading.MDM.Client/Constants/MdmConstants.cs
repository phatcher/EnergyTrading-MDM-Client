using System;
using System.Configuration;

namespace EnergyTrading.Mdm.Client.Constants
{
    public static class MdmConstants
    {
        public static readonly string MdmRequestHeaderName = "X-MDMREQ-HDR";
        public static readonly string MdmRequestSourceSystemName = "Mdm:SourceSystem";

        public static readonly bool LogResponse = Convert.ToBoolean(ConfigurationManager.AppSettings["LogResponse"] ?? "false");

        public static string MdmName
        {
            get { return ConfigurationManager.AppSettings["Mdm.InternalName"] ?? "Nexus"; }
        }
    }
}