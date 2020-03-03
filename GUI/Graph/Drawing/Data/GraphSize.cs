namespace GUI.Graph.Drawing
{
    public class GraphSize<T>
    {
        public T XMin { get; set; }
        public T XMax { get; set; }
        public T YMin { get; set; }
        public T YMax { get; set; }

        public GraphSize(T xMin, T xMax, T yMin, T yMax)
        {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
        }        
    }
}
