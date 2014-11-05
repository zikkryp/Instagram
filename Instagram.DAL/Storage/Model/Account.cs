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

        private string _username = string.Empty;
        private string _profilePicture = string.Empty;
        private string _fullName = string.Empty;

        [PrimaryKey]
        public int Id { get; private set; }
        public string Token { get; private set; }
        public bool IsActive { get; set; }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string FullName
        {
            get
            {
                if (_fullName == string.Empty)
                {
                    return this._username;
                }
                return _fullName;
            }
            set { _fullName = value; }
        }

        public string ProfilePicture
        {
            get { return _profilePicture; }
            set { _profilePicture = value; }
        }

        public bool IsAutoSignIn { get; set; }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
