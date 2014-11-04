using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class Feed
    {
        public Feed()
        {
            Items = new ObservableCollection<FeedItem>();
            Pagination = new Pagination();
        }

        public ObservableCollection<FeedItem> Items { get; set; }
        public Pagination Pagination { get; set; }
        public Meta Meta { get; set; }

        public void AppendItems(ObservableCollection<FeedItem> items)
        {
            foreach(var item in items)
            {
                this.Items.Add(item);
            }
        }

        public override string ToString()
        {
            return this.Items.Count.ToString();
        }
    }
}
