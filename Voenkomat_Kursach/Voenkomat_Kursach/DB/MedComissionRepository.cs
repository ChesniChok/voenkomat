using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class MedComissionRepository : BaseRepository<MedComission>
{
    public MedComissionRepository(string connectionString) : base(connectionString)
    {
    }
    public List<MedComission> GetAll()
        {
            List<MedComission> medComissions = new List<MedComission>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM MedComission";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        medComissions.Add(new MedComission
                        {
                            Id = dr.GetInt32("Id"),
                            Ter= dr.GetBoolean("Ter"),
                            Otor = dr.GetBoolean("Otor"),
                            Psih = dr.GetBoolean("Psih"),
                            Nevr= dr.GetBoolean("Nevr"),
                            Hir = dr.GetBoolean("Hir"),
                            Stom = dr.GetBoolean("Stom"),
                            Okul = dr.GetBoolean("Okul"),
                            StartDate = dr.GetDateTimeOffset("StartDate"),
                            EndDate = dr.GetDateTimeOffset("EndDate"),
                            Category = dr.GetString("Category"),
                            Description = dr.GetString("Category")
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
            return medComissions;
        }

        public MedComission GetById(int id)
        {
            MedComission medcomission = null;
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM MedComission WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            medcomission = new MedComission
                            {
                                Id = dr.GetInt32("Id"),
                                Ter= dr.GetBoolean("Ter"),
                                Otor = dr.GetBoolean("Otor"),
                                Psih = dr.GetBoolean("Psih"),
                                Nevr= dr.GetBoolean("Nevr"),
                                Hir = dr.GetBoolean("Hir"),
                                Stom = dr.GetBoolean("Stom"),
                                Okul = dr.GetBoolean("Okul"),
                                StartDate = dr.GetDateTimeOffset("StartDate"),
                                EndDate = dr.GetDateTimeOffset("EndDate"),
                                Category = dr.GetString("Category"),
                                Description = dr.GetString("Category")
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
            return medcomission;
        }
}
