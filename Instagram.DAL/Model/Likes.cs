using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class Likes
    {
        public Likes(int count, IEnumerable<User> liked)
        {
            this.Count = count;
            this.Liked = liked;

            if (liked != null)
            {
                foreach(var like in liked)
                {
                    Content += like.Username + " ";
                }
            }
        }

        public int Count { get; private set; }
        public IEnumerable<User> Liked { get; private set; }

        public string Content { get; private set; }

        public override string ToString()
        {
            return this.Count.ToString();
        }
    }
}
