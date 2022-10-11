using Business.TestDouble.Testable;

namespace Business.Tests.TestDoubles
{
    public class DbGatewayStub:IDbGateway
    {
        private WorkingStatistics _ws;

        public WorkingStatistics GetWorkingStatistics(int id)
        {
            return _ws;
        }

        public void SetWorkingStatistics(WorkingStatistics ws)
        {
            _ws = ws;
        }

        public bool Connected { get; }
    }
}
