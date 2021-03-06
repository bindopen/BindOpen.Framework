﻿using BindOpen.Data.Elements.Schema;
using BindOpen.Data.References;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSchema : IDescribedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        DataReferenceDto MetaSchemreference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        SchemaZoneElement RootZone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentMetobject1"></param>
        /// <returns></returns>
        SchemaElement GetElementWithId(string id, SchemaElement parentMetobject1 = null);
    }
}