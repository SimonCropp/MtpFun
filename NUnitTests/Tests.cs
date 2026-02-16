using System.Diagnostics;
using NUnit.Framework;

[TestFixture]
public class Tests
{
    [Test]
    public void Test() =>
        Debug.WriteLine("Foo");
}
