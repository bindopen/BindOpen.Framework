﻿using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This interface represents a globally described DTO.
    /// </summary>
    public interface IGloballyDescribed
    {
        /// <summary>
        /// The global description of this instance.
        /// </summary>
        DictionaryDataItem Description
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        void AddDescription(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        void AddDescription(string key, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variantName"></param>
        /// <param name="defaultVariantName"></param>
        /// <returns></returns>
        string GetDescription(string variantName = "*", string defaultVariantName = "*");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        void SetDescription(string key = "*", string text = "*");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        void SetDescription(string text);
    }
}