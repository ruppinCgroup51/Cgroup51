﻿namespace HomeWork2.BL
{
    public class Flat
    {
        int id;
        string city;
        string address;
        double price;
        int numbers_of_rooms;

        //public static List<Flat> FlatsList = new List<Flat>();

        public Flat()
        {

        }
        public Flat(int id, string city, string address, int numbers_of_rooms, double price)
        {
            Id = id;
            City = city;
            Address = address;
            Numbers_of_rooms = numbers_of_rooms;
            Price = price;
        }

        public int Id { get => id; set => id = value; }
        public string City { get => city; set => city = value; }
        public string Address { get => address; set => address = value; }
        public int Numbers_of_rooms { get => numbers_of_rooms; set => numbers_of_rooms = value; }
        public double Price { get => price; set => price = Discount(value); }


        public double Discount(double p)
        {
            if (numbers_of_rooms > 1 && p > 100)
            {
                p *= 0.9;
                return p;
            }
            return p;

        }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertFlat(this);
        }



        public List<Flat> Read()  {
            DBservices dbs = new DBservices();
            return dbs.ReadFlats();
        }
    


        //public List<Flat> ReadByPriceAndCity(string city, double maxPrice)
        //{
        //    List<Flat> selectedList = new List<Flat>();

        //    foreach (var item in FlatsList)
        //    {
        //        if (item.city == city && item.price <= maxPrice) selectedList.Add(item);
        //    }
        //    return selectedList;
        //}
    }
}
