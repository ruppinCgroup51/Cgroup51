using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace HomeWork2.BL
{
    public class User
    {
        string firstName;
        string lastName;
        string email;
        string password;
        bool isActive;
        bool isAdmin;

        public User(string firstName, string lastName, string email, string password, bool isActive, bool isAdmin)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.isActive = isActive;
            this.isAdmin = isAdmin;
        }

        public User() { }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        public bool IsActive { get => isActive; set => isActive = value; }

        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }

        // insert user - registration
        public int Insert()
        {
            DBservices dbs = new DBservices();

            List<User> users = Read();

            foreach (var user in users)
            {
                if(user.email == this.email) { return -1; }
            }

            return dbs.InsertUser(this);
        }

        // get all users
        public List<User> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadUsers();
        }

        //update user

        public User Update()
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateUser(this);

        }

        // login user
        public User Login(string email, string password)
        {
            DBservices dbs = new DBservices();
            return dbs.LoginUser(email, password);
               
        }

        //delete user
        public int Delete(string email)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteUserByEmail(email);
        }
        
        //public ActionResult GetAveragePricePerNight(int month)
        //{
        //    try
        //    {
        //        // Use your DbServices to get the average price per night
        //        DBservices dbServices = new DBservices();
        //        List<object> result = dbServices.GetAveragePricePerNight(month);

        //        if (result.Count > 0)
        //        {
        //            // Return the result if data is available
        //            return Ok(result);
        //        }
        //        else
        //        {
        //            // Return 404 if no data is available for the selected month
        //            return NotFound("No data available for the selected month.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //    }
        
        //}
    }
}
