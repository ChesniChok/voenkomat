using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class DocumentRepository : BaseRepository<Document>
{
    public DocumentRepository(string connectionString) : base(connectionString)
    {
    }
    public List<Document> GetAll()
        {
            List<Document> documents = new List<Document>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM Document";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        documents.Add(new Document
                        {
                            Id = dr.GetInt32("Id"),
                            Type = dr.GetString("Type"),
                            Number = dr.GetString("Number"),
                            FileName = dr.GetString("FileName"),
                            Description = dr.GetString("Description")
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
            return documents;
        }

        public Document GetById(int id)
        {
            Document document = null;
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM Document WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            document = new Document
                            {
                                Id = dr.GetInt32("Id"),
                                Type = dr.GetString("Type"),
                                Number = dr.GetString("Number"),
                                FileName = dr.GetString("FileName"),
                                Description = dr.GetString("Description")
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
            return document;
        }
}
