namespace Fitness.BusinessLogic.Controller
{
    public interface IDataSaver
    {
        void Save(string fileName, object item);
        T Load<T>(string fileName);
    }
}
