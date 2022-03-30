using System.Collections.Generic;

namespace QuizProjectFE.Services
{
    public interface IApiRequest<T>
    {

        List<T> GetAll(string controllername);
        
        T GetSingle(string controllername, int id);

        T Create(string controllername, T entity);

        T Edit(string controllername, int id, T entity);

        void Delete(string controllername, int id);

        List<T> GetChildren(string controllername, string enpoint, int id);

    }
}
