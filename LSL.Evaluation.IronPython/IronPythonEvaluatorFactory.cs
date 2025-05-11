using System;
using System.Collections.Generic;
using System.Linq;
using IronPython.Hosting;
using LSL.Evaluation.Core;
using Microsoft.Scripting.Hosting;

namespace LSL.Evaluation.IronPython;

/// <summary>
/// The factory to create an IronPython based evaluator
/// </summary>
public class IronPythonEvaluatorFactory : IEvaluatorFactoryWithSettings<IronPythonSettings>
{
    /// <inheritdoc/>
    public IEvaluator Build(Action<IEvaluatorFactoryConfigurationWithSettings<IronPythonSettings>> configurator)
    {
        static void Configure<T>(IEnumerable<Action<T>> configurators, T toConfigure) => 
            configurators.ForEach(i => i.Invoke(toConfigure));
            

        var config = new EvaluatorFactoryConfigurationWithSettings<IronPythonSettings>();
        var ironPythonEngineSettings = new Dictionary<string, object>();
        var ironPythonSettings = new IronPythonSettings();

        configurator.Invoke(config);

        Configure(config.SettingsConfigurators, ironPythonSettings);
        Configure(ironPythonSettings.OptionsConfigurators, ironPythonEngineSettings);

        var engine = Python.CreateEngine(ironPythonEngineSettings);
        var scope = engine.CreateScope();

        Configure(ironPythonSettings.EngineConfigurators, new IronPythonEngineContainer { Engine = engine, Scope = scope });
        Configure(config.CodeToAdd.Select(c => (Action<ScriptEngine>)(e => e.Execute(c, scope))), engine);
        Configure(config.ValuesToSet.Select(c => (Action<ScriptScope>)(e => e.SetVariable(c.Name, c.Value))), scope);
        
        return new IronPythonEvaluator(engine, scope);
    }

    private class IronPythonEvaluator(ScriptEngine engine, ScriptScope scope) : IEvaluator
    {
        public object Evaluate(string expression) => engine.Execute(expression, scope);
    }
}