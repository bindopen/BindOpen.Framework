﻿using BindOpen.Framework.Extensions.Runtime;
using System.Collections.Generic;

namespace BindOpen.Framework.Extensions.Definition
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
        BdoScriptwordFunction RuntimeFunction { get; set; }
    }
}