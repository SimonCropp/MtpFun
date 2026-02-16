using System.Diagnostics;
using System.Threading.Tasks;

public class Tests
{
    [Test]
    public Task Test()
    {
        Debug.WriteLine("Foo");
        return Task.CompletedTask;
    }
}
