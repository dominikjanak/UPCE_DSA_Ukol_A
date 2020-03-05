
namespace GraphService.Tests
{
    class TestEdgeData : IEdgeData
    {
        private float _distance;
        private bool _blocked;

        public TestEdgeData(float distance)
        {
            _distance = distance;
            _blocked = false;
        }
        public TestEdgeData(float distance, bool blocked)
        {
            _distance = distance;
            _blocked = blocked;
        }

        public void Unblock()
        {
            _blocked = false;
        }

        public void Block()
        {
            _blocked = true;
        }

        public float GetDistance()
        {
            return _distance;
        }

        public bool IsBlocked()
        {
            return _blocked;
        }
    }
}
