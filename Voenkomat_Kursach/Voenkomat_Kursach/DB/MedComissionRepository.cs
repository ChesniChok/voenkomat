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
                    int recruitId = dr.GetInt32("Recruit_Id");
                    medComissions.Add(new MedComission(
                    
                        dr.GetInt32("Id"),
                        _recruitRepository.GetById(recruitId),
                        dr.GetDateOnly("StartDate"),
                        dr.GetDateOnly("EndDate"),
                        dr.GetString("Category"),
                        dr.GetString("Description")
                    ));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
        return medComissions;
    }
    
    public List<MedComission> GetPage(int offset, int limit, Recruit r)
    {
        List<MedComission> coms = new();
        try
        {
            OpenConnection();
            string sql = "select * from medcomissions where Recruit_Id = @rid and EndDate is null limit @limit offset @offset";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@limit", limit);
                mc.Parameters.AddWithValue("@offset", offset);
                mc.Parameters.AddWithValue("@rid", r.Id);
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var date = dr.GetOrdinal("EndDate");
                        var cat = dr.GetOrdinal("Category");
                        var desc = dr.GetOrdinal("Description");
                        coms.Add(new MedComission
                        (
                            dr.GetInt32("Id"),
                            _recruitRepository.GetById(dr.GetInt32("Recruit_Id")),
                            dr.GetDateOnly("StartDate"),
                            dr.IsDBNull(date) ? null : dr.GetDateOnly(date),
                            dr.IsDBNull(cat) ? null : dr.GetString("Category"),
                             dr.IsDBNull(desc) ? null : dr.GetString("Description")
                        ));
                    }
                }
            }
        }
        catch (Exception e)
        {
            CloseConnection();
            Console.WriteLine(e);
                    
        }
        return coms;
    }
    
    public MedComission GetRecsLast(Recruit r)
    {
        MedComission com = new();
        try
        {
            OpenConnection();
            string sql = "select * from medcomissions where Recruit_Id = @rid order by StartDate";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@rid", r.Id);
                using (var dr = mc.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var date = dr.GetOrdinal("EndDate");
                        var cat = dr.GetOrdinal("Category");
                        var desc = dr.GetOrdinal("Description");
                        com = new MedComission
                        (
                            dr.GetInt32("Id"),
                            _recruitRepository.GetById(dr.GetInt32("Recruit_Id")),
                            dr.GetDateOnly("StartDate"),
                            dr.IsDBNull(date) ? null : dr.GetDateOnly(date),
                            dr.IsDBNull(cat) ? null : dr.GetString("Category"),
                            dr.IsDBNull(desc) ? null : dr.GetString("Description")
                        );
                    }
                }
            }
        }
        catch (Exception e)
        {
            CloseConnection();
            Console.WriteLine(e);
                    
        }
        CloseConnection();
        return com;
    }

        public MedComission GetById(int id)
        {
            MedComission medcomission = new MedComission();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM medcomissions WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            var date = dr.GetOrdinal("EndDate");
                            var cat = dr.GetOrdinal("Category");
                            var desc = dr.GetOrdinal("Description");
                            medcomission = new MedComission
                            (
                                dr.GetInt32("Id"),
                                _recruitRepository.GetById(dr.GetInt32("Recruit_Id")),
                                dr.GetDateOnly("StartDate"),
                                dr.IsDBNull(date) ? null : dr.GetDateOnly(date),
                                dr.IsDBNull(cat) ? null : dr.GetString("Category"),
                                dr.IsDBNull(desc) ? null : dr.GetString("Description")
                            );
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
            finally
            {
                CloseConnection();
            }
            return medcomission;
        }
        
    public void Add(MedComission m)
    {
        try
        {
            OpenConnection();
            string sql = "insert into medcomissions values(0, @rid, @sdate, @edate, @categ, @desc);";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@rid", m.Recruit.Id);
                mc.Parameters.AddWithValue("@sdate", m.StartDate);
                mc.Parameters.AddWithValue("@edate", m.EndDate);
                mc.Parameters.AddWithValue("@categ", m.Category);
                mc.Parameters.AddWithValue("@desc", m.Description);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
    }

    public void Update(MedComission m)
    {
        try
        {
            OpenConnection();
            string sql = "update medcomissions set Recruit_Id = @rid, StartDate = @sdate, EndDate = @edate, Category = @categ, Description = @desc where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", m.Id);
                mc.Parameters.AddWithValue("@rid", m.Recruit.Id);
                mc.Parameters.AddWithValue("@sdate", m.StartDate);
                mc.Parameters.AddWithValue("@edate", m.EndDate);
                mc.Parameters.AddWithValue("@categ", m.Category);
                mc.Parameters.AddWithValue("@desc", m.Description);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
    }

    public void Delete(MedComission m)
    {
        try
        {
            OpenConnection();
            string sql = "delete from medcomissions where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", m.Id);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
    }

    public int Count()
    {
        int res = 0;
        try
        {
            OpenConnection();
            string sql = "select count(*) as counted from medcomissions";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                using (var r = mc.ExecuteReader())
                {
                    if  (r.Read()) res = r.GetInt32("counted");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
        return res;
    }
}
