using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class UsersInPhoto
    {
        public UsersInPhoto(IEnumerable<UserInPhoto> users)
        {
            this.Users = users;
        }

        public IEnumerable<UserInPhoto> Users { get; private set; }

        private string users;
        public override string ToString()
        {
            if (Users != null)
            {
                foreach(var user in Users)
                {
                    users += user.User.Username + " ";
                }
            }

            return string.Empty;
        }
    }
}
