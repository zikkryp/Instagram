using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Instagram.DAL.Storage.Model
{
    public class Account
    {
        public Account()
        {

        }

        public Account(int id, string token)
        {
            this.Id = id;
            this.Token = token;
            this.IsActive = true;
        }

        [PrimaryKey]
        public int Id { get; private set; }
        [MaxLength(100)]
        public string Token { get; private set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
