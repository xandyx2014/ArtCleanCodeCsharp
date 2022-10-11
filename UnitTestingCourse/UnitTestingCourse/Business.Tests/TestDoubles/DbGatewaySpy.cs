using Business.TestDouble.Testable;

namespace Business.Tests.TestDoubles
{
    public class DbGatewaySpy:IDbGateway
    {
        private WorkingStatistics _ws;
        public int Id { get; private set; }

        public WorkingStatistics GetWorkingStatistics(int id)
        {
            Id = id;
            return _ws;
        }

        public void SetWorkingStatistics(WorkingStatistics ws)
        {
            _ws = ws;
        }

        public bool Connected { get; }
    }
}
