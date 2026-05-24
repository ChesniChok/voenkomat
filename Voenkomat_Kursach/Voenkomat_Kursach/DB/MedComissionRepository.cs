using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class MedComissionRepository : BaseRepository<MedComission>
{
    private RecruitRepository _recruitRepository;
    public MedComissionRepository(string connectionString, RecruitRepository recruitRepository) : base(connectionString)
    {
        _recruitRepository = recruitRepository;
    }
    public List<MedComission> GetAll()
        {
            List<MedComission> medComissions = new List<MedComission>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM medComissions";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int recruitId = dr.GetInt32("recruit");
                        Recruit recruit = _recruitRepository.GetById(recruitId);
                        
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
            MedComission medcomission = new MedComission();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM medComissions WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int recruitId = dr.GetInt32("recruit");
                            Recruit recruit = _recruitRepository.GetById(recruitId);
                            
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
