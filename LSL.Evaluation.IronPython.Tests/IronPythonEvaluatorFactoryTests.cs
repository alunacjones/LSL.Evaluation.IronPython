using FluentAssertions;
using NUnit.Framework;

namespace LSL.Evaluation.IronPython.Tests;

public class IronPythonEvaluatorFactoryTests
{
    [Test]
    public void GivenABuiltEvaluator_ThenItShouldPerformAsExpected()
    {
        // Arrange
        var sut = new IronPythonEvaluatorFactory()
            .Build(c =>
            {
                c.AddCode("value1 = 12");
                c.SetValue("by2", (int i) => i * 2);
                
                c.Configure(s => s
                    .ConfigureEngine(e =>
                    {
                        e.Engine.Execute("value3 = 1", e.Scope);
                        e.Scope.SetVariable("value2", 24);
                    })
                    .ConfigureOptions(e =>
                    {
                        e.Add("Debug", false);
                    })
                );
            });

        // Act        
        var result = sut.Evaluate(
            """
            from System import Math

            by2(12) + value1 + value2 + value3 + Math.Sqrt(144)
            """);

        // Assert
        result.Should().BeEquivalentTo(73);
    }
}