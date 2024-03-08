namespace HomeWork2.BL
{
    public class Vacation
    {
        int id;
        string userId;
        int flatId;
        DateTime startDate;
        DateTime endDate;


        public Vacation() { }
        public Vacation(int id, string userId, int flatId, DateTime startDate, DateTime endDate)
        {

            Id = id;
            UserId = userId;
            FlatId = flatId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Id { get => id; set => id = value; }
        public string UserId { get => userId; set => userId = value; }
        public int FlatId
        {
            get => flatId;
            set
            {
                flatId = CheckFlat(value);

            }
        }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                TimeSpan duration = value - StartDate;

                if (value > StartDate && duration.TotalDays <= 20)
                {
                    endDate = value;
                }
                else endDate = DateTime.MinValue;

            }

        }

        public int CheckFlat(int value)
        {
            DBservices dbs = new DBservices();
            List<Flat> CheckFlats = dbs.ReadFlats();
            foreach (Flat item in CheckFlats)
            {
                if (item.Id == value)
                {
                    return value;
                }
            }
            return -1;
        }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            List<Vacation> vacationsList = dbs.ReadVacations();
            List<User> users = dbs.ReadUsers();

            if (this.flatId == -1 || this.endDate == DateTime.MinValue || IsRented(this))
            {
                
                return -1;
            }

            foreach (var item in vacationsList)
            {
                if (item.id == this.id)
                {
                    return -1;
                }
            }

            foreach(var user in users)
            {
                if(user.Email == this.UserId)
                {
                    return dbs.InsertVacation(this);
                }
            }

            return -1;

           // vacationsList.Add(this);
           
        }

        public bool IsRented(Vacation obj)
        {
            List<Vacation> vacationsList = Read();
            foreach (var item in vacationsList)
            {
                if (item.flatId == obj.flatId && !(item.endDate < obj.startDate || item.startDate > obj.endDate))
                {
                    return true;
                }
            }
            return false;
        }

        public List<Vacation> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadVacations();
        }

        public List<Vacation> ReadByDates(DateTime from, DateTime to)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadVacationsByDates(from,to);
        }

        public List<Vacation> ReadByEmail(string email)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadVacationsByEmail(email);
        }
    }
}

