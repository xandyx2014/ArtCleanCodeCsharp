using System.Diagnostics;
using NUnit.Framework;

[SetUpFixture]
class AssemblySetupTeardown
{
    [OneTimeSetUp]
    public void AssemblySetup()
    {
        Trace.WriteLine("Assembly setup.");
    }

    [OneTimeTearDown]
    public void AssemblyTeardown()
    {
        Trace.WriteLine("Assembly teardown.");
    }
}

namespace Business.Tests
{
    [SetUpFixture]
    class NamespaceSetupTeardown
    {
        [OneTimeSetUp]
        public void NamespaceSetup()
        {
            Trace.WriteLine("Namespace setup.");
        }

        [OneTimeTearDown]
        public void NamespaceTeardown()
        {
            Trace.WriteLine("Namespace teardown.");
        }
    }
}