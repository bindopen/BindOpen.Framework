﻿using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    public interface IScriptwordIndexDto : ITAppExtensionItemIndexDto<ScriptwordDefinitionDto>
    {
        string DefinitionClass { get; set; }
    }
}