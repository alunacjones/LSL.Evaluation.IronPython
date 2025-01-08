using System;
using System.Collections.Generic;

namespace LSL.Evaluation.IronPython;

/// <summary>
/// Settings for the IronPython engine
/// </summary>
public class IronPythonSettings
{
    internal List<Action<IronPythonEngineContainer>> EngineConfigurators = [];
    internal List<Action<IDictionary<string, object>>> OptionsConfigurators = [];
    
    /// <summary>
    /// Add a configurator to setup an IronPython options dictionary
    /// </summary>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public IronPythonSettings ConfigureOptions(Action<IDictionary<string, object>> configurator)
    {
        OptionsConfigurators.Add(configurator);
        return this;
    }

    /// <summary>
    /// Add a configurator to setup an IronPython Engine and Scope instance
    /// </summary>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public IronPythonSettings ConfigureEngine(Action<IronPythonEngineContainer> configurator)
    {
        EngineConfigurators.Add(configurator);
        return this;
    }    
}
