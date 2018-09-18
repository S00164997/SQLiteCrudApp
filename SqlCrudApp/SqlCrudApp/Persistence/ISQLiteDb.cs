using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlCrudApp.Persistence
{
    interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
