using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public class Lists
    {
       public List<Cities> getAllCities()
        {
            List<Cities> listOfCities = new List<Cities>();
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd5 = new SqlCommand
            {
                CommandText = "select * from Cities",
                Connection = con
            };
            SqlDataReader rd5 = cmd5.ExecuteReader();
            while (rd5.Read())
            {
                Cities city = new Cities();
                city.City_Name = rd5["City_Name"].ToString();
                city.City_Description = rd5["City_Description"].ToString();
                var id = rd5["CityID"].ToString();
                city.CityID = Int32.Parse(id);
                listOfCities.Add(city);
            }
            rd5.Close();
            return listOfCities;
        }
        public List<Attractions> getAllAttractions()
        {
            List<Attractions> listOfAttractions = new List<Attractions>();
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd8 = new SqlCommand
            {
                CommandText = "select * from Attraction",
                Connection = con
            };
            SqlDataReader rd8 = cmd8.ExecuteReader();
            while (rd8.Read())
            {
                Attractions attractions = new Attractions();
                var id = rd8[0].ToString();
                attractions.AttractionID = Int32.Parse(id);
                attractions.Attractions_TicketPrice = rd8[1].ToString();
                attractions.Attractions_Schedule = rd8[2].ToString();
                attractions.Attractions_Name = rd8[4].ToString();
                attractions.Attraction_Descriptions = rd8[5].ToString();
                var id2 = rd8[3].ToString();
                attractions.Attraction_CityID = Int32.Parse(id2);
                listOfAttractions.Add(attractions);
            }
            rd8.Close();
            return listOfAttractions;
        }
        public List<Attractions> random3Attractions(string searchID)
        {
            Details det = new Details();
            det.Cities.City_Name = searchID;
            List<Attractions> listOfRandomAtt = new List<Attractions>();
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd9 = new SqlCommand
            {
                CommandText = "SELECT top 3 * from Attraction inner join Cities on CityID=Attraction.Attraction_City where @city_Name=Cities.City_Name ORDER BY NEWID()",
                Connection = con
            };
            cmd9.Parameters.Add(new SqlParameter("@city_Name",det.Cities.City_Name));
            SqlDataReader rd9 = cmd9.ExecuteReader();
            while (rd9.Read())
            {
                Attractions attractions = new Attractions();
                var id = rd9[0].ToString();
                attractions.AttractionID = Int32.Parse(id);
                attractions.Attractions_TicketPrice = rd9[1].ToString();
                attractions.Attractions_Schedule = rd9[2].ToString();
                attractions.Attractions_Name = rd9[4].ToString();
                attractions.Attraction_Descriptions = rd9[5].ToString();
                var id2 = rd9[3].ToString();
                attractions.Attraction_CityID = Int32.Parse(id2);
                listOfRandomAtt.Add(attractions);
            }
            rd9.Close();
            return listOfRandomAtt;
        }
        public List<Cities> random3Cities(string searchID)
        {
            Details det = new Details();
            det.Cities.City_Name = searchID;
            List<Cities> listOfRandomCities = new List<Cities>();
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd11 = new SqlCommand
            {
                CommandText = "SELECT * from Cities inner join Countries on Cities.City_Country=Countries.CountryID",
                Connection = con
            };
            SqlDataReader rd11 = cmd11.ExecuteReader();
            while (rd11.Read())
            {
                if (rd11["City_Name"].ToString() == searchID)
                {
                    det.Country.Country_Name = rd11["Country_Name"].ToString();
                    break;
                }
            }
            rd11.Close();
            SqlCommand cmd10 = new SqlCommand
            {
                CommandText = "SELECT top 3 * from Cities inner join Countries on Cities.City_Country=Countries.CountryID where @Country_Name=Countries.Country_Name ORDER BY NEWID()",
                Connection = con
            };
            cmd10.Parameters.Add(new SqlParameter("@Country_Name", det.Country.Country_Name));
            SqlDataReader rd10 = cmd10.ExecuteReader();
            while (rd10.Read())
            {
                Cities cty = new Cities();
                var id = rd10[0].ToString();
                cty.CityID = Int32.Parse(id);
                cty.City_Name = rd10[1].ToString();
                cty.City_Description = rd10[3].ToString();
                var id2 = rd10[2].ToString();
                cty.City_Country = Int32.Parse(id2);
                listOfRandomCities.Add(cty);
            }
            rd10.Close();
            return listOfRandomCities;
        }
    }
}