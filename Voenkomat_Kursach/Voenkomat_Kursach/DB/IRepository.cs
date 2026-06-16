using System.Collections.Generic;

namespace Voenkomat_Kursach.DB;

public interface IRepository<T> where T : class
{
    public List<T> GetAll();
    public List<T> GetPage(int offset, int limit);
    public T GetById(int id);
    public void  Add(T t);
    public void Update(T t);
    public void Delete(T t);
}