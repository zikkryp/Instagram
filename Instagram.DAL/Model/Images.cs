using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class Images
    {
        public Images(StandardResolution standardResolution, LowResolution lowResolution, Thumbnail thumbnail)
        {
            this.StandardResolution = standardResolution;
            this.LowResolution = LowResolution;
            this.Thumbnail = thumbnail;
        }

        public StandardResolution StandardResolution { get; private set; }
        public LowResolution LowResolution { get; private set; }
        public Thumbnail Thumbnail { get; private set; }

        public override string ToString()
        {
            return this.StandardResolution.Url;
        }
    }
}
