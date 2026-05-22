using System.Collections.Generic;

namespace Voenkomat_Kursach.DB;

public interface IRepository<T> where T : class
{
    public List<T> GetAll();
    public T GetById(int id);
}