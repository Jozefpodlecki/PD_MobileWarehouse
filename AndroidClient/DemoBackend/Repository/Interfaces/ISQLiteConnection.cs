using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Repository.Interfaces
{
    public interface ISQLiteConnection
    {
        int? UserId { get; set; }
        List<T> GetAllWithChildren<T>(Expression<Func<T, bool>> filter = null, bool recursive = false) where T : new();
        T GetWithChildren<T>(object pk, bool recursive = false) where T : new();
        CreateTableResult CreateTable<T>(CreateFlags createFlags = CreateFlags.None);
        TableQuery<T> Table<T>() where T : new();
        int DropTable<T>();
        int Update(object obj);
        int Insert(object obj);
        int Delete(object objectToDelete);
        int InsertAll(IEnumerable objects, bool runInTransaction = true);
        T Get<T>(object pk) where T : new();
        T Find<T>(object pk) where T : new();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}