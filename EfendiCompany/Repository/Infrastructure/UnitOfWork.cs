using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Hosting;
using Core.Constants;

namespace Repository.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    SqliteTransaction BeginTransaction();
    SqliteConnection GetConnection();
    SqliteTransaction GetTransaction();
    void SaveChanges();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly string _connectionString;
    private bool disposed = false;

    private SqliteTransaction sqliteTransaction;
    private SqliteConnection sqliteConnection;
    public UnitOfWork(string connStr = "")
    {
        if (!string.IsNullOrWhiteSpace(connStr))
        {
            _connectionString = connStr;
        }
        else
        {
            _connectionString = Statics.ConnStr;
        }
        sqliteConnection = new SqliteConnection(_connectionString);
    }

    public SqliteTransaction BeginTransaction()
    {
        if (sqliteConnection.State != System.Data.ConnectionState.Open)
        {
            sqliteConnection.Open();
            sqliteTransaction = sqliteConnection.BeginTransaction();
        }

        return sqliteTransaction;
    }

    public SqliteConnection GetConnection()
    {
        return sqliteConnection;
    }

    public SqliteTransaction GetTransaction()
    {
        return sqliteTransaction;
    }

    public void Dispose()
    {
        Dispose(true);
        //GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                sqliteTransaction = null;
            }

            // Release unmanaged resources.
            if (sqliteConnection.State == System.Data.ConnectionState.Open)
            {
                sqliteConnection.Close();
            }
            disposed = true;
        }
    }

    public void SaveChanges()
    {
        sqliteTransaction.Commit();
        sqliteConnection.Close();
        sqliteTransaction = null;
    }

    ~UnitOfWork() { Dispose(false); }
}

