using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ukol_A.Drawing
{
    static class Colors
    {
        public static Color Black
        {
            get
            {
                return Color.FromArgb(14,14,14);
            }
        }
        public static Color White
        {
            get
            {
                return Color.FromArgb(255,255,255);
            }
        }

        public static Color Brown
        {
            get
            {
                return Color.FromArgb(84, 55, 41);
            }
        }

        public static Color Green
        {
            get
            {
                return Color.FromArgb(118, 184, 82);
            }
        }

        public static Color Red
        {
            get
            {
                return Color.FromArgb(238, 79, 79);
            }
        }

        public static Color Yellow
        {
            get
            {
                return Color.FromArgb(254, 186, 2);
            }
        }

        public static Color Gray
        {
            get
            {
                return Color.FromArgb(180, 180, 180);
            }
        }

        public static Color Blue
        {
            get
            {
                return Color.FromArgb(0, 165, 220);
            }
        }
    }
}
