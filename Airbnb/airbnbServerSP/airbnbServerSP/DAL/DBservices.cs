using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HomeWork2.BL;
using System.Net; 



/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //-------------------------------------------------------------------------------------------------
    // This method get flats 
    //-------------------------------------------------------------------------------------------------

    public List<Flat> ReadFlats()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Flat> flats = new List<Flat>();

        cmd = buildReadStoredProcedureCommand(con, "SP_Avia_GetFlats");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            Flat f = new Flat();
            f.Id = Convert.ToInt32(dataReader["Id"]);
            f.City = dataReader["City"].ToString();
            f.Address = dataReader["Address"].ToString();
            f.Price = Convert.ToDouble(dataReader["Price"]);
            f.Numbers_of_rooms = Convert.ToInt32(dataReader["Numbers_of_rooms"]);
            flats.Add(f);

        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }

        return flats;


    }


    //-------------------------------------------------------------------------------------------------
    // This method get vacations 
    //-------------------------------------------------------------------------------------------------

    public List<Vacation> ReadVacations()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Vacation> vacations = new List<Vacation>();

        cmd = buildReadStoredProcedureCommand(con, "SP_Avia_GetVacations");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            Vacation v = new Vacation();
            v.Id = Convert.ToInt32(dataReader["Id"]);
            v.UserId = dataReader["UserId"].ToString();
            v.FlatId = Convert.ToInt32(dataReader["FlatId"]);
            v.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
            v.EndDate = Convert.ToDateTime(dataReader["EndDate"]);

            vacations.Add(v);
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }

        return vacations;


    }

    //-------------------------------------------------------------------------------------------------
    // This method get vacations by dates
    //-------------------------------------------------------------------------------------------------

    public List<Vacation> ReadVacationsByDates(DateTime from, DateTime to)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Vacation> vacations = new List<Vacation>();

        cmd = buildReadStoredProcedureCommandVacationDates(con, "SP_Avia_GetVacationsByDates", from, to);

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            Vacation v = new Vacation();
            v.Id = Convert.ToInt32(dataReader["Id"]);
            v.UserId = dataReader["UserId"].ToString();
            v.FlatId = Convert.ToInt32(dataReader["FlatId"]);
            v.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
            v.EndDate = Convert.ToDateTime(dataReader["EndDate"]);

            vacations.Add(v);
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }

        return vacations;


    }




    public List<Vacation> ReadVacationsByEmail(string email)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Vacation> vacations = new List<Vacation>();

        cmd = buildReadStoredProcedureCommandVacationEmail(con, "SP_Avia_GetVacationByEmail", email);

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            Vacation v = new Vacation();
            v.Id = Convert.ToInt32(dataReader["Id"]);
            v.UserId = dataReader["UserId"].ToString();
            v.FlatId = Convert.ToInt32(dataReader["FlatId"]);
            v.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
            v.EndDate = Convert.ToDateTime(dataReader["EndDate"]);

            vacations.Add(v);
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }

        return vacations;


    }


    private SqlCommand buildReadStoredProcedureCommandVacationEmail(SqlConnection con, String spName, string email)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", email);

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // Create the read vacation by dates SqlCommand using a stored procedure
    //--------------------------------------------------------------------------------------------------

    private SqlCommand buildReadStoredProcedureCommandVacationDates(SqlConnection con, String spName, DateTime from, DateTime to)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@startDate", from);
        cmd.Parameters.AddWithValue("@endDate", to);

        return cmd;
    }
    
    //-------------------------------------------------------------------------------------------------
    // This method get users 
    //-------------------------------------------------------------------------------------------------

    public List<User> ReadUsers()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<User> users = new List<User>();

        cmd = buildReadStoredProcedureCommand(con, "SP_Avia_GetUsers");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            User u = new User();
            u.FirstName = dataReader["FirstName"].ToString();
            u.LastName = dataReader["LastName"].ToString();
            u.Email = dataReader["Email"].ToString();
            u.Password = dataReader["Password"].ToString();
            u.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
            u.IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]);

            users.Add(u);
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }

        return users;


    }

    //-------------------------------------------------------------------------------------------------
    // This method insert flat 
    //-------------------------------------------------------------------------------------------------

    public int InsertFlat(Flat flat)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertFlatsWithStoredProcedure("SP_Avia_InsertFlat", con, flat);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------------------------------------
    // Create the insert flat SqlCommand using a stored procedure
    //--------------------------------------------------------------------------------------------------
    private SqlCommand CreateInsertFlatsWithStoredProcedure(String spName, SqlConnection con, Flat flat)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text


        cmd.Parameters.AddWithValue("@city", flat.City);

        cmd.Parameters.AddWithValue("@address", flat.Address);

        cmd.Parameters.AddWithValue("@rooms", flat.Numbers_of_rooms);

        cmd.Parameters.AddWithValue("@price", flat.Price);


        return cmd;
    }

    //-------------------------------------------------------------------------------------------------
    // This method insert vacation 
    //-------------------------------------------------------------------------------------------------

    public int InsertVacation(Vacation vacation)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertVacationWithStoredProcedure("SP_Avia_InsertVacation", con, vacation);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------------------------------------
    // Create the insert vacation SqlCommand using a stored procedure
    //--------------------------------------------------------------------------------------------------

    private SqlCommand CreateInsertVacationWithStoredProcedure(String spName, SqlConnection con, Vacation vacation)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@userId", vacation.UserId);

        cmd.Parameters.AddWithValue("@flatId", vacation.FlatId);

        cmd.Parameters.AddWithValue("@startDate", vacation.StartDate);

        cmd.Parameters.AddWithValue("@endDate", vacation.EndDate);


        return cmd;
    }

    //-------------------------------------------------------------------------------------------------
    // This method insert user 
    //-------------------------------------------------------------------------------------------------

    public int InsertUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertUserWithStoredProcedure("SP_Avia_InsertUser", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------------------------------------
    // Create the insert user SqlCommand using a stored procedure
    //--------------------------------------------------------------------------------------------------
    private SqlCommand CreateInsertUserWithStoredProcedure(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);

        cmd.Parameters.AddWithValue("@lastName", user.LastName);

        cmd.Parameters.AddWithValue("@email", user.Email);

        cmd.Parameters.AddWithValue("@password", user.Password);


        return cmd;
    }

    //-------------------------------------------------------------------------------------------------
    // This method update user
    //-------------------------------------------------------------------------------------------------

     public User UpdateUser(User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateUpdateUserCommandWithStoredProcedure("SP_Avia_UserUpdate", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery();
            if (numEffected == 1)
            {
                return user;
            } return null;
        }


        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        }
    
    //--------------------------------------------------------------------------------------------------
    // Create the update user SqlCommand using a stored procedure
    //--------------------------------------------------------------------------------------------------
    private SqlCommand CreateUpdateUserCommandWithStoredProcedure(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);

        cmd.Parameters.AddWithValue("@lastName", user.LastName);

        cmd.Parameters.AddWithValue("@email", user.Email);

        cmd.Parameters.AddWithValue("@password", user.Password);

        cmd.Parameters.AddWithValue("@isActive", user.IsActive);


        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // Create the read SqlCommand using a stored procedure
    //--------------------------------------------------------------------------------------------------

    public User LoginUser(string email, string password)
    {

        SqlConnection con;
        SqlCommand cmd;
        User authenticatedUser = null;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = buildReadStoredProcedureCommandLoginUser(con, "SP_Avia_UserLogin", email, password);

        try
        {


            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dataReader.Read())
            {
                authenticatedUser = new User
                {

                    FirstName = dataReader["FirstName"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    Email = dataReader["Email"].ToString(),
                    Password = dataReader["Password"].ToString(),
                    IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                    IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"])
            };



            }
            return authenticatedUser;
        }
        catch(Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
        {
            // close the db connection
            con.Close();
        }
        }


    }

    private SqlCommand buildReadStoredProcedureCommandLoginUser(SqlConnection con, String spName, string email, string password)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@password", password);

        return cmd;
    }

    private SqlCommand buildReadStoredProcedureCommand(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;

    }

    //-------------------------------------------------------------------------------------------------
    // This method delete user
    //-------------------------------------------------------------------------------------------------
    public int DeleteUserByEmail(string email)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateDeleteUserByEmailWithStoredProcedure("SP_Avia_DeleteUser", con, email); // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //-------------------------------------------------------------------------------------------------
    // create the delete user command
    //-------------------------------------------------------------------------------------------------
    private SqlCommand CreateDeleteUserByEmailWithStoredProcedure(string spName, SqlConnection con, string email)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // stored procedure name

        cmd.CommandTimeout = 10;           // Time to wait for the execution, the default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", email);

        return cmd;
    }

    public List<object> GetAveragePricePerNight(int month)
    {
        List<object> result = new List<object>();

        // Use your connect method to open a connection
        using (SqlConnection connection = connect("myProjDB"))
        {
            try
            {
                // Create and configure a SqlCommand for the stored procedure
                using (SqlCommand command = new SqlCommand("SP_GetAveragePricePerNight", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@Month", month);

                    // Execute the command and read the data
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Log the values being read
                            Console.WriteLine($"City: {reader["city"]}, AveragePricePerNight: {reader["AveragePricePerNight"]}");

                            // Create an ad hoc object with properties "City" and "AveragePrice"
                            var cityAverage = new
                            {
                                City = reader["city"].ToString(),
                                AveragePrice = Convert.ToDouble(reader["AveragePricePerNight"])
                            };

                            result.Add(cityAverage);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw ex;
            }
        }

        return result;
    }


}





