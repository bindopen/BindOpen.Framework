﻿using BindOpen.Data.Common;
using BindOpen.Data.Conditions;
using BindOpen.Data.Items;
using System;
using System.Collections.Generic;

namespace BindOpen.Application.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptionSpecSet : ITDataItemSet<IOptionSpec>, INamed, IIndexed, IGloballyDescribed
    {
        /// <summary>
        /// 
        /// </summary>
        Condition Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IOptionSpecSet> SubSets { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataValueType"></param>
        /// <param name="nameKind"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        IOptionSpecSet AddOption(DataValueTypes dataValueType, OptionNameKind nameKind, params string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataValueType"></param>
        /// <param name="requirementLevel"></param>
        /// <param name="nameKind"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        IOptionSpecSet AddOption(DataValueTypes dataValueType, RequirementLevels requirementLevel, OptionNameKind nameKind, params string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameKind"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        IOptionSpecSet AddOption(OptionNameKind nameKind, params string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliases"></param>
        /// <returns></returns>
        IOptionSpecSet AddOption(params string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requirementLevel"></param>
        /// <param name="nameKind"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        IOptionSpecSet AddOption(RequirementLevels requirementLevel, OptionNameKind nameKind, params string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requirementLevel"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        IOptionSpecSet AddOption(RequirementLevels requirementLevel, params string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="requirementLevel"></param>
        /// <param name="nameKind"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        IOptionSpecSet AddOption(Type type, RequirementLevels requirementLevel, OptionNameKind nameKind, params string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="optionSpecifications"></param>
        /// <returns></returns>
        IOptionSpecSet AddSubSet(ICondition condition, params IOptionSpec[] optionSpecifications);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subSet"></param>
        /// <returns></returns>
        IOptionSpecSet AddSubSet(IOptionSpecSet subSet);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionSpecifications"></param>
        /// <returns></returns>
        IOptionSpecSet AddSubSet(params IOptionSpec[] optionSpecifications);

        /// <summary>
        /// 
        /// </summary>
        void ClearOptions();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiCulture"></param>
        /// <returns></returns>
        string GetHelpText(string uiCulture = "*");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool HasOption(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IOptionSpecSet RemoveOption(string name);
    }
}