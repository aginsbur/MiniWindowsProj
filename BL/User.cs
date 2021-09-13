using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        string username;
        string password;
        bool admin;
        #region***properties***
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public bool Admin { get => admin; set => admin = value; }
        #endregion
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
