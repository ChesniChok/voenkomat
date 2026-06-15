using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class MedComissionRepository : BaseRepository<MedComission>
{
    private RecruitRepository _recruitRepository;
    public MedComissionRepository(AppSettings appSettings, RecruitRepository recruitRepository) : base(appSettings)
    {
        _recruitRepository = recruitRepository;
    }
    public List<MedComission> GetAll()
    {
        List<MedComission> medComissions = new List<MedComission>();
        try
        {
            _connection.Open();
            string sql = "SELECT * FROM medcomissions";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    int recruitId = dr.GetInt32("RecruitId");
                    medComissions.Add(new MedComission(
                    
                        dr.GetInt32("Id"),
                        _recruitRepository.GetById(recruitId),
                        dr.GetDateTimeOffset("StartDate"),
                        dr.GetBoolean("Ter"),
                        dr.GetBoolean("Otor"),
                        dr.GetBoolean("Psih"),
                        dr.GetBoolean("Nevr"),
                        dr.GetBoolean("Hir"),
                        dr.GetBoolean("Stom"),
                        dr.GetBoolean("Okul"),
                        dr.GetDateTimeOffset("EndDate"),
                        dr.GetString("Category"),
                        dr.GetString("Description")
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
        return medComissions;
    }

        public MedComission GetById(int id)
        {
            MedComission medcomission = new MedComission();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM medcomissions WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int recruitId = dr.GetInt32("Recruit_Id");
                            medcomission = new MedComission(
                            
                                dr.GetInt32("Id"),
                                _recruitRepository.GetById(recruitId),
                                dr.GetDateTimeOffset("StartDate"),
                                dr.GetBoolean("Ter"),
                                dr.GetBoolean("Otor"),
                                dr.GetBoolean("Psih"),
                                dr.GetBoolean("Nevr"),
                                dr.GetBoolean("Hir"),
                                dr.GetBoolean("Stom"),
                                dr.GetBoolean("Okul"),
                                dr.GetDateTimeOffset("EndDate"),
                                dr.GetString("Category"),
                                dr.GetString("Description")
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
            return medcomission;
        }
}
