using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class RecruitRepository : BaseRepository<Recruit>
{
    private List<Recruit> _recruits;
    public RecruitRepository(string connectionString) : base(connectionString)
    {
        
    }
    public List<Recruit> GetAll()
        {
            List<Recruit> recruits = new List<Recruit>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM recruits";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        recruits.Add(new Recruit
                        {
                            Id = dr.GetInt32("Id"),
                            FamilyName = dr.GetString("FamilyName"),
                            Name = dr.GetString("Name"),
                            FatherName = dr.GetString("FatherName"),
                            DateOfBirth = dr.GetDateOnly("DateOfBirth"),
                            PhoneNumber = dr.GetString("PhoneNumber"),
                            Adress = dr.GetString("Adress"),
                            Passport = dr.GetString("Passport"),
                            SNILS = dr.GetString("SNILS"),
                            INN = dr.GetString("INN")
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return recruits;
        }

        public Recruit GetById(int id)
        {
            Recruit recruit = new Recruit();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM recruits WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            recruit = new Recruit
                            {
                                Id = dr.GetInt32("Id"),
                                FamilyName = dr.GetString("FamilyName"),
                                Name = dr.GetString("Name"),
                                FatherName = dr.GetString("FatherName"),
                                DateOfBirth = dr.GetDateOnly("DateOfBirth"),
                                PhoneNumber = dr.GetString("PhoneNumber"),
                                Adress = dr.GetString("Adress"),
                                Passport = dr.GetString("Passport"),
                                SNILS = dr.GetString("SNILS"),
                                INN = dr.GetString("INN")
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return recruit;
        }
}
