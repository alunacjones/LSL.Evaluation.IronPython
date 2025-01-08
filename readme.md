[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-evaluation-ironpython.svg)](https://ci.appveyor.com/project/alunacjones/lsl-evaluation-ironpython)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.Evaluation.IronPython)](https://coveralls.io/github/alunacjones/LSL.Evaluation.IronPython)
[![NuGet](https://img.shields.io/nuget/v/LSL.Evaluation.IronPython.svg)](https://www.nuget.org/packages/LSL.Evaluation.IronPython/)

# LSL.Evaluation.IronPython

Provides an evaluator using the [IronPython](https://www.nuget.org/packages/IronPython) Python engine

## Quick Start

The following code provides a simple example of evaluating an expression using the `IronPythonEvaluatorFactory`

```csharp
using LSL.Evaluation.IronPython;
...
var sut = new IronPythonEvaluatorFactory()
    .Build(c =>
    {
        // Ensures we have a variable called value1 available for
        // evaluation
        // We can add functions and other code such as classes here too
        c.AddCode("value1 = 12");

        // This sets up IronPython specific options and engine settings
        c.Configure(s => s
            // Adds another variable via the IronPython engine
            .ConfigureEngine(e => e.Scope.SetVariable("value2", 24))
            // Sets up options for the IronPython engine
            .ConfigureOptions(o => o.Add("Debug", false))
        );
    });

var result = sut.Evaluate("12 + value1 + value2");
// Result will have a value of 48 

```
