using Microsoft.AspNetCore.Mvc;
using HomeWork2.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeWork2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            User user = new User(); 
            return user.Read();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //POST 

        [HttpPost]
        public int Post([FromBody] User user)
        {

            return user.Insert();
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("login")]
        public ActionResult Login(User user)
        {

            User authenticatedUser = user.Login(user.Email, user.Password);

            if (authenticatedUser != null)
            {
                // Return the authenticated user
                return Ok(authenticatedUser);
            }
            else
            {
                // Return 404 if user does not exist or credentials are invalid
                return NotFound("User not found or invalid credentials");
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("update")]
        public User Put([FromBody] User user)
        {
            return user.Update();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("delete")]
        public int Delete(string email)
        {
            User user = new User();
            return user.Delete(email);
        }

        [HttpGet("averagePricePerNight/{month}")]
        public List<object> GetAveragePricePerNight(int month)
        {
            try
            {
                // Use your DBservices to get the average price per night
                DBservices dbServices = new DBservices();
                List<object> result = dbServices.GetAveragePricePerNight(month);

                if (result.Count > 0)
                {
                    // Return the result if data is available
                    return result;
                }
                else
                {
                    // Return an empty list if no data is available for the selected month
                    return new List<object>();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception: {ex.Message}");
                // Return an empty list or handle the error as appropriate for your application
                return new List<object>();
            }
        }

    }
}
