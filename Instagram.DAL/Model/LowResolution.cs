using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class LowResolution
    {
        public LowResolution(string url, double width, double height)
        {
            this.Url = url;
            this.Width = width;
            this.Height = height;
        }

        public string Url { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }

        public override string ToString()
        {
            return this.Url;
        }
    }
}
