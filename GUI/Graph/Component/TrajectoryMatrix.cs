using System;
using System.Collections.Generic;
using System.Linq;

namespace GUI.Graph.Component
{
    class TrajectoryMatrix
    {
        private Dictionary<string, int> _cols; // Vertex keys of indexes into matrix
        private Dictionary<string,int> _rows; // Vertex keys of indexes into matrix
        private string[,] _matrix; // matrix with titles (c+1)(r+1)

        List<string> _restAreas = new List<string>();
        List<string> _stops = new List<string>();

        public int ColumnsCount()
        {
            return _cols.Count;
        }

        public int RowsCount()
        {
            return _rows.Count;
        }

        public TrajectoryMatrix(List<(string key, VertexData data)> vertices)
        {
            InitMatrix(vertices);
        }

        public string GetColumnKey(int index)
        {
            if(index < 0 || index >= _cols.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _matrix[index + 1, 0];
        }

        public string GetRowKey(int index)
        {
            if (index < 0 || index >= _rows.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _matrix[0, index + 1];
        }

        public List<string> GetAllRestAreas()
        {
            return _restAreas;
        }

        public List<string> GetAllStops()
        {
            return _stops;
        }

        public string GetValue(string col, string row)
        {
            int colIdx, rowIdx;
            FindIndexes(col, row, out colIdx, out rowIdx);

            return _matrix[colIdx, rowIdx];
        }

        public bool SetValue(string col, string row, string value)
        {
            int colIdx, rowIdx;
            FindIndexes(col, row, out colIdx, out rowIdx);

            if(_matrix[colIdx, rowIdx] != "-")
            {
                return false;
            }

            _matrix[colIdx, rowIdx] = value;
            return true;
        }

        public string this[string col, string row]
        {
            get => GetValue(col, row);
            set => SetValue(col, row, value);
        }

        public string GetValue(int colIdx, int rowIdx)
        {
            if (colIdx <= 0 || rowIdx <= 0 || colIdx > ColumnsCount() || rowIdx > RowsCount())
            {
                throw new ArgumentOutOfRangeException();
            }

            return _matrix[colIdx, rowIdx];
        }

        public bool SetValue(int colIdx, int rowIdx, string value)
        {
            if (colIdx <= 0 || rowIdx <= 0 || colIdx > ColumnsCount() || rowIdx > RowsCount())
            {
                throw new ArgumentOutOfRangeException();
            }

            if (_matrix[colIdx, rowIdx] != "-")
            {
                return false;
            }

            _matrix[colIdx, rowIdx] = value;
            return true;
        }

        public string this[int col, int row]
        {
            get => GetValue(col+1, row+1);
            set => SetValue(col+1, row+1, value);
        }

        private void InitMatrix(List<(string key, VertexData data)> vertices)
        {
            List<string> junctions = new List<string>();

            // Categorize vertex types
            foreach (var vertex in vertices)
            {
                switch (vertex.data.VertexType)
                {
                    case VertexType.Junction:
                        junctions.Add(vertex.key);
                        break;
                    case VertexType.RestArea:
                        _restAreas.Add(vertex.key);
                        break;
                    case VertexType.Stop:
                        _stops.Add(vertex.key);
                        break;
                }
            }

            _cols = new Dictionary<string, int>();
            _rows = new Dictionary<string, int>();
            _matrix = new string[_stops.Count+1, vertices.Count+1]; // With column and row name
            _matrix[0, 0] = "#"; // Empty cell

            // Prepare index and matrix header for columns
            for (int i = 1; i <= _stops.Count; i++)
            {
                _cols.Add(_stops[i-1], i);
                _matrix[i, 0] = _stops[i - 1];
            }

            int idx = 1; // index of line (0 - header)

            // Prepare rest areas index and matrix header for rows
            for (int i = 0; i < _restAreas.Count; i++)
            {
                _rows.Add(_restAreas[i], idx);
                _matrix[0, idx] = _restAreas[i];
                idx++;
            }

            // Prepare junctions area index and matrix header for rows
            for (int i = 0; i < junctions.Count; i++)
            {
                _rows.Add(junctions[i], idx);
                _matrix[0, idx] = junctions[i];
                idx++;
            }

            // Prepare stops index and matrix header for rows
            for (int i = 0; i < _stops.Count; i++)
            {
                _rows.Add(_stops[i], idx);
                _matrix[0, idx] = _stops[i];
                idx++;
            }

            // Fill all cells with empty val "-"
            for (int c = 1; c <= _cols.Count; c++)
            {
                for (int r = 1; r <= _rows.Count; r++)
                {
                    _matrix[c, r] = "-";
                }
            }

        }

        private void FindIndexes(string col, string row, out int colIdx, out int rowIdx)
        {
            if (!_cols.TryGetValue(col, out colIdx) || !_rows.TryGetValue(row, out rowIdx))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

    }
}
