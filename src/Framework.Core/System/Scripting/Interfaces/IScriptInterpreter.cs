﻿using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.System.Scripting
{
    public interface IScriptInterpreter : IDataItem
    {
        object Evaluate(IDataExpression dataExpression, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        object Evaluate(IDataExpression dataExpression, out string resultScript, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        object Evaluate(string script, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        object Evaluate(string script, out string resultScript, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        IScriptword FindNextScriptword(ref string script, IScriptword parentScriptword, ref int index, int offsetIndex, IScriptVariableSet scriptVariableSet = null, bool isSimulationModeOn = false, ILog log = null);

        string Interprete(IDataExpression dataExpression, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        string Interprete(string script, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        bool IsWordMatching(IScriptword scriptWord, IScriptwordDefinition scriptWordDefinition);

        List<IScriptwordDefinition> GetDefinitionsWithApproximativeName(string name, IScriptwordDefinition parentDefinition = null);

        List<IScriptwordDefinition> GetDefinitionsWithExactName(string name, IScriptwordDefinition parentDefinition = null);
        List<IScriptwordDefinition> GetDefinitions();
    }
}