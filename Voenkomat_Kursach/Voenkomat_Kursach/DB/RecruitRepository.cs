using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class RecruitRepository : BaseRepository<Recruit>
{
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
                        recruits.Add(new Recruit(
                            dr.GetInt32("Id"),
                            dr.GetString("FamilyName"),
                            dr.GetString("Name"),
                            dr.GetString("FatherName"),
                            dr.GetDateOnly("DateOfBirth"),
                            dr.GetString("PhoneNumber"),
                            dr.GetString("Adress"),
                            dr.GetString("Passport"),
                            dr.GetString("SNILS"),
                            dr.GetString("INN")
                        ));
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
                CloseConnection();
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
                            recruit = new Recruit(
                            
                                dr.GetInt32("Id"),
                                dr.GetString("FamilyName"),
                                dr.GetString("Name"),
                                dr.GetString("FatherName"),
                                dr.GetDateOnly("DateOfBirth"),
                                dr.GetString("PhoneNumber"),
                                dr.GetString("Adress"),
                                dr.GetString("Passport"),
                                dr.GetString("SNILS"),
                                dr.GetString("INN")
                            );
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
                CloseConnection();
            }
            return recruit;
        }
}
