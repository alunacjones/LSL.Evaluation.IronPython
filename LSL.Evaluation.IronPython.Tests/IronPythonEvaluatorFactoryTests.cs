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
                //c.AddCode("value2 = 24");
                c.Configure(s => s
                    .ConfigureEngine(e =>
                    {
                        e.Scope.SetVariable("by2", (int i) => i * 2);
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

            by2(12) + value1 + value2 + Math.Sqrt(144)
            """);

        // Assert
        result.Should().BeEquivalentTo(72);
    }
}