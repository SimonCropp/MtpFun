using System.Diagnostics;
using System.Threading.Tasks;
using TUnit.Core;

public class Tests
{
    [Test]
    public Task Test()
    {
        Debug.WriteLine("Foo");
        return Task.CompletedTask;
    }
}
