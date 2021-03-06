﻿using BindOpen.Extensions.Runtime;
using System.Collections.Generic;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptwordDefinition : ITBdoExtensionItemDefinition<IBdoScriptwordDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoScriptwordDefinition Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, IBdoScriptwordDefinition> Children { get; }

        /// <summary>
        /// 
        /// </summary>
        BdoScriptwordScopedDelegate RuntimeScopedFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoScriptwordBasicDelegare RuntimeBasicFunction { get; set; }
    }
}