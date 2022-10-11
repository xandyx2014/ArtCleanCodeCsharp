using Business.TestDouble.Testable;

namespace Business.Tests.TestDoubles
{
    public class LoggerDummy:ILogger
    {
        public void Info(string s)
        {
            //do nothing
        }
    }
}
