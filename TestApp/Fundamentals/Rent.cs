using System;

namespace TestApp
{
    public class Rent
    {
        public User Rentee { get; set; }

        public bool CanReturn(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            return user.IsAdmin || Rentee == user;
                
        }

    }


    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; private set; }

        public void MakeAdmin()
        {
            IsAdmin = true;
        }
    }

}
