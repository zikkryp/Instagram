using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class Counts
    {
        public Counts(int media, int follows, int followedBy)
        {
            this.Media = media;
            this.Follows = follows;
            this.FollowedBy = followedBy;
        }

        public int Media { get; private set; }
        public int Follows { get; private set; }
        public int FollowedBy { get; private set; }

        public override string ToString()
        {
            return string.Format("Media: {0} Follows: {1} Followed By: {2}", Media, Follows, FollowedBy);
        }
    }
}
