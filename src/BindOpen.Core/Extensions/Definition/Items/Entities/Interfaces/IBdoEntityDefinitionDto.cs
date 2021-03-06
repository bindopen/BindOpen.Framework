﻿using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEntityDefinitionDto : IBdoExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        List<BdoFormatDefinitionDto> FormatDefinitions { get; }

        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoEntityKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DataSchema> PossibleMetaSchemas { get; }

        /// <summary>
        /// 
        /// </summary>
        string ViewerClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSpecSet DetailSpec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IBdoFormatDefinitionDto GetFormatWithId(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoFormatDefinitionDto GetFormatWithName(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueName"></param>
        /// <returns></returns>
        IBdoFormatDefinitionDto GetFormatWithUniqueName(string uniqueName);
    }
}