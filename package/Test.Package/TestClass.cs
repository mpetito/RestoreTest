using System;

namespace Test.Package
{
    public static class TestClass
    {
        public static string GetVersion()
        {
            return typeof(TestClass).Assembly.GetName().Version.ToString();
        }
    }
}
