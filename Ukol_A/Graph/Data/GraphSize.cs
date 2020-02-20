namespace Ukol_A
{
    public struct GraphSize
    {
        public float XMin { get; set; }
        public float XMax { get; set; }
        public float YMin { get; set; }
        public float YMax { get; set; }

        public GraphSize(float xMin = 0, float xMax = 0, float yMin = 0, float yMax = 0)
        {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
        }

        public override string ToString()
        {
            return "X: <" + XMin + "," +XMax+ ">\nY: <" + YMin + "," + YMax + ">";
        }
    }
}
