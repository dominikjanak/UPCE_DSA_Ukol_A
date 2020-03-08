﻿using RangeTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugConsole
{
    class RangeTreeData : IValue
    {
        private float _x;
        private float _y;

        public RangeTreeData(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public double X => _x;

        public double Y => _y;
    }
}