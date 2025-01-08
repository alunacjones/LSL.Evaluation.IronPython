using Microsoft.Scripting.Hosting;

namespace LSL.Evaluation.IronPython;

/// <summary>
/// Contains the IronPython <c>Engine</c> and <c>Scope</c> used by the evaluator
/// </summary>
public class IronPythonEngineContainer
{
    /// <summary>
    /// The IronPython <c>ScriptEngine</c> instance used by the evaluator
    /// </summary>
    /// <value></value>
    public ScriptEngine Engine { get; internal set; }

    /// <summary>
    /// The IronPython <c>ScriptScope</c> instance used by the evaluator
    /// </summary>
    /// <value></value>    
    public ScriptScope Scope { get; internal set; } 
}