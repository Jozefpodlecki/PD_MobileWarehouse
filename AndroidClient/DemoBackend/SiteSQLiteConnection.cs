using Common.Repository.Interfaces;
using Data_Access_Layer;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Client.DemoBackend
{
    public class SiteSQLiteConnection : ISQLiteConnection
    {
        private readonly SQLiteConnection _connection;

        public SiteSQLiteConnection(string databasePath)
        {
            _connection = new SQLiteConnection(databasePath);
        }

        public int? UserId { get; set; }

        public object Instance => _connection;

        public void BeginTransaction()
        {
            _connection.BeginTransaction();
        }

        public void Commit()
        {
            _connection.Commit();
        }

        public CreateTableResult CreateTable<T>(CreateFlags createFlags = CreateFlags.None)
        {
            return _connection.CreateTable<T>(createFlags);
        }

        public int Delete(object objectToDelete)
        {
            return _connection.Delete(objectToDelete);
        }

        public int DropTable<T>()
        {
            return _connection.DropTable<T>();
        }

        public int Execute(string query, params object[] args)
        {
            return _connection.Execute(query, args);
        }

        public T Find<T>(object pk) where T : new()
        {
            return _connection.Find<T>(pk);
        }

        public T Get<T>(object pk) where T : new()
        {
            return _connection.Get<T>(pk);
        }

        public List<T> GetAllWithChildren<T>(Expression<Func<T, bool>> filter = null, bool recursive = false) where T : new()
        {
            return _connection.GetAllWithChildren(filter, recursive);
        }

        public T GetWithChildren<T>(object pk, bool recursive = false) where T : new()
        {
            return _connection.GetWithChildren<T>(pk, recursive);
        }

        public int Insert(object obj)
        {
            var baseEntity = obj as BaseEntity;

            if (baseEntity != null)
            {
                var currentDate = DateTime.UtcNow;

                baseEntity.CreatedAt = currentDate;
                baseEntity.CreatedById = UserId;
                baseEntity.LastModifiedAt = currentDate;
                baseEntity.LastModifiedById = UserId;
            }

            return _connection.Insert(obj);
        }

        public int InsertAll(IEnumerable objects, bool runInTransaction = true)
        {
            var baseEntities = objects.OfType<BaseEntity>();

            if (baseEntities.Any())
            {
                var currentDate = DateTime.UtcNow;

                foreach (var baseEntity in baseEntities)
                {
                    baseEntity.CreatedAt = currentDate;
                    baseEntity.CreatedById = UserId;
                    baseEntity.LastModifiedAt = currentDate;
                    baseEntity.LastModifiedById = UserId;
                }
            }

            return _connection.InsertAll(objects, runInTransaction);
        }

        public void Rollback()
        {
            _connection.Rollback();
        }

        public TableQuery<T> Table<T>() where T : new()
        {
            return _connection.Table<T>();
        }

        public int Update(object obj)
        {
            var baseEntity = obj as BaseEntity;

            if (baseEntity != null)
            {
                var currentDate = DateTime.UtcNow;

                baseEntity.LastModifiedAt = currentDate;
                baseEntity.LastModifiedById = UserId;
            }

            return _connection.Update(obj);
        }

    }
}