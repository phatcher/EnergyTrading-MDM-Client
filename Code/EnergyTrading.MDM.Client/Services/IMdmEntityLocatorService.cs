﻿namespace EnergyTrading.Mdm.Client.Services
{
    using EnergyTrading.Mdm.Contracts;

    /// <summary>
    /// Finds MDM contracts based on an identifier
    /// </summary>
    public interface IMdmEntityLocatorService
    {
        /// <summary>
        /// Retrieve an MDM contract based on a key.
        /// </summary>
        /// <typeparam name="TContract">Type of contract to find</typeparam>
        /// <param name="id">Identifier to use</param>
        /// <param name="version">contract version default = 0</param>
        /// <returns>MDM contract if found, null otherwise</returns>
        TContract Get<TContract>(MdmId id, uint version = 0) 
            where TContract : IMdmEntity;
    }
}