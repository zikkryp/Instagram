using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class Tags
    {
        public Tags(IEnumerable<Tag> tags)
        {
            this.ItemTags = tags;

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    Content += "#" + tag + " ";
                }
            }
        }

        public IEnumerable<Tag> ItemTags { get; private set; }

        public string Content { get; private set; }
        public override string ToString()
        {
            return this.Content;
        }
    }
}
