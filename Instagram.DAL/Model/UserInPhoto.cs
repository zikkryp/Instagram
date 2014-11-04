using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class UserInPhoto
    {
        public UserInPhoto(Position position, User user)
        {
            this.Position = position;
            this.User = user;
        }

        public Position Position { get; set; }
        public User User { get; private set; }

        public override string ToString()
        {
            return this.Position.ToString();
        }
    }
}
