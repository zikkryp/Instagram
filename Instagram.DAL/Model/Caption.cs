using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class Caption
    {
        public Caption(string createdTime, string text, User from, string id)
        {
            this.CreatedTime = createdTime;
            this.Text = text;
            this.From = from;
            this.Id = id;
        }

        public string CreatedTime{ get; private set; }
        public string Text { get; private set; }
        public User From { get; private set; }
        public string Id { get; set; }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
