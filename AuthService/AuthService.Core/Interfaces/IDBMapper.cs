namespace AuthService.Core.Interfaces
{
    public interface IDBMapper<CoreType, DBType>
    {
        DBType MapToDB(CoreType entity);
        CoreType MapFromDB(DBType entity);
    }
}
