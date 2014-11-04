using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class Videos
    {
        public Videos(LowBandwidth lowBandwidth, LowResolution lowResolution, StandardResolution standardResolution)
        {
            this.LowBandwidth = lowBandwidth;
            this.LowResolution = lowResolution;
            this.StandardResolution = standardResolution;
        }

        public StandardResolution StandardResolution { get; private set; }
        public LowResolution LowResolution { get; private set; }
        public LowBandwidth LowBandwidth { get; private set; }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
