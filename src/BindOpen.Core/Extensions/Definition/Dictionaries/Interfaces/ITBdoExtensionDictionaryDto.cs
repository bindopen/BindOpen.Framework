﻿using BindOpen.Extensions.Definition;
using BindOpen.Extensions.Runtime;
using System.Collections.Generic;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoExtensionDictionaryDto<T> : IBdoExtensionDictionaryDto where T : BdoExtensionItemDefinitionDto
    {
        /// <summary>
        /// The definitions.
        /// </summary>
        List<T> Definitions { get; set; }

        /// <summary>
        /// The groups.
        /// </summary>
        List<BdoExtensionItemGroup> Groups { get; }

        /// <summary>
        /// ID of library.
        /// </summary>
        string LibraryId { get; set; }

        /// <summary>
        /// Name of library.
        /// </summary>
        string LibraryName { get; set; }
    }
}