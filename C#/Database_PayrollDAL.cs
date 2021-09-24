using System;
using System.Data.Common;
using System.Data.Entity;

public class Database
{
    public static DbConnection GetDbCtxConnection(string ConnString)
    {
        var db = new DbContext(ConnString);
        return db.Database.Connection;
    }
}
