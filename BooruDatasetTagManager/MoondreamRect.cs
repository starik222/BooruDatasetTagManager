using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class MoondreamRect
    {
        [JsonProperty("x min")]
        public float x_min { get; set; }
        [JsonProperty("y min")]
        public float y_min { get; set; }
        [JsonProperty("x max")]
        public float x_max { get; set; }
        [JsonProperty("y max")]
        public float y_max { get; set; }


        public float Area => (x_max - x_min) * (y_max - y_min);

        public MoondreamRect() { }

        public MoondreamRect(float Xmin, float Ymin, float Xmax, float Ymax)
        {
            x_min = Xmin;
            y_min = Ymin;
            x_max = Xmax;
            y_max = Ymax;
        }

        public void Add(MoondreamRect rect)
        {
            x_min = Math.Min(x_min, rect.x_min);
            y_min = Math.Min(y_min, rect.y_min);
            x_max = Math.Max(x_max, rect.x_max);
            y_max = Math.Max(y_max, rect.y_max);
        }

        public Rectangle ToRealRect(int imgWidth, int imgHeight)
        {
            int x = (int)Math.Round(x_min * (float)imgWidth);
            int y = (int)Math.Round(y_min * (float)imgHeight);
            int w = (int)Math.Round(x_max * (float)imgWidth) - x;
            int h = (int)Math.Round(y_max * (float)imgHeight) - y;
            return new Rectangle(x, y, w, h);
        }

        public Rectangle ToRealRect()
        {
            return new Rectangle((int)x_min, (int)y_min, (int)(x_max - x_min), (int)(y_max - y_min));
        }

        public MoondreamRect ToRealCoordinates(int imgWidth, int imgHeight)
        {
            return new MoondreamRect()
            {
                x_min = (int)Math.Round(x_min * (float)imgWidth),
                y_min = (int)Math.Round(y_min * (float)imgHeight),
                x_max = (int)Math.Round(x_max * (float)imgWidth),
                y_max = (int)Math.Round(y_max * (float)imgHeight)
            };
        }

        public override string ToString()
        {
            return $"{x_min}, {y_min}, {x_max}, {y_max}";
        }

        public MoondreamRect Clone()
        {
            return new MoondreamRect(x_min, y_min, x_max, y_max);
        }
    }
}
