﻿using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.Extensions.Runtime.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionStore : IIdentifiedDataItem
    {
        /// <summary>
        /// Adds the specified definition.
        /// </summary>
        /// <typeparam name="T">The BindOpen extension item definition class to consider.</typeparam>
        /// <param name="definition">The definition to add.</param>
        void Add<T>(T definition) where T : IBdoExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Dictionary<string, T> GetItemDefinitions<T>() where T : IBdoExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetItemDefinitionEnumerables<T>() where T : IBdoExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        T GetItemDefinitionWithUniqueId<T>(string uniqueId) where T : IBdoExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <param name="parentDefinition"></param>
        /// <returns></returns>
        IBdoScriptwordDefinition GetScriptwordDefinitionWithUniqueName(string uniqueId, IBdoScriptwordDefinition parentDefinition = null);
    }
}