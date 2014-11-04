using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class Pagination
    {
        public Pagination()
        {
            this._hasMorePages = false;
        }

        public Pagination(string nextUrl, string nextMaxId)
        {
            this.NextUrl = nextUrl;
            this.NextMaxId = nextMaxId;
            this._hasMorePages = true;
        }

        public string NextUrl { get; set; }
        public string NextMaxId { get; set; }

        private bool _hasMorePages = false;

        public bool HasMorePages
        {
            get
            {
                return _hasMorePages;
            }
            private set
            {
                _hasMorePages = value;
            }
        }

        public override string ToString()
        {
            return this.NextUrl;
        }
    }
}
