using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class User
    {
        public User(string username, string profilePicture, string fullName, string id)
        {
            this.Username = username;
            this.ProfilePicture = profilePicture;
            this.FullName = fullName;
            this.Id = id;
        }

        public User(string username, string profilePicture, string fullName, string id, string website, string bio, Counts counts)
        {
            this.Username = username;
            this.ProfilePicture = profilePicture;
            this.FullName = fullName;
            this.Id = id;
            this.Website = website;
            this.Bio = bio;
            this.Counts = counts;
        }

        public string Username { get; private set; }
        public string ProfilePicture { get; private set; }
        public string FullName { get; private set; }
        public string Id { get; private set; }

        public string Website { get; private set; }
        public string Bio { get; private set; }
        public Counts Counts { get; private set; }

        public override string ToString()
        {
            return this.Username;
        }
    }
}
