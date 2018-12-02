using System;
using System.Collections.Generic;
using System.Text;
using BlueShare.Models;
using SQLite;
using Xamarin.Forms.Internals;

namespace BlueShare.DAO
{
    public class UserDAO : DataBase
    {
        public UserDAO() : base()
        {
            try
            {
                Connection.CreateTable<UserModel>();
            }
            catch (SQLiteException ex)
            {
                Log.Warning("SQLiteEx", ex.Message);
            }
        }

        public List<UserModel> Search()
        {
            try
            {
                return Connection.Table<UserModel>().ToList();
            }
            catch (SQLiteException ex)
            {
                Log.Warning("SQLiteEx", ex.Message);
                return null;
            }
        }

        public List<UserModel> Select(string name)
        {
            try
            {
                return Connection.Table<UserModel>().Where(a => a.DeviceName.Contains(name)).ToList();
            }
            catch (SQLiteException ex)
            {
                Log.Warning("SQLiteEx", ex.Message);
                return null;
            }
        }

        public UserModel GetUserById(string id)
        {
            try
            {
                return Connection.Table<UserModel>().Where(a => a.DeviceId == id).FirstOrDefault();
            }
            catch (SQLiteException ex)
            {
                Log.Warning("SQLiteEx", ex.Message);
                return null;
            }
        }

        public List<UserModel> GetUsersByGroupId(int idGroup)
        {
            try
            {
                return Connection.Table<UserModel>().Where(a => a.IdGroup == idGroup).ToList();
            }
            catch (SQLiteException ex)
            {
                Log.Warning("SQLiteEx", ex.Message);
                return null;
            }
        }

        public void Insert(UserModel user)
        {
            try
            {
                Connection.Insert(user);
            }
            catch (SQLiteException ex)
            {
                Log.Warning("SQLiteEx", ex.Message);
            }
        }

        public void Update(UserModel user)
        {
            try
            {
                Connection.Update(user);
            }
            catch (SQLiteException ex)
            {
                Log.Warning("SQLiteEx", ex.Message);
            }
        }

        public void Delete(UserModel user)
        {
            try
            {
                Connection.Delete(user);
            }
            catch (SQLiteException ex)
            {
                Log.Warning("SQLiteEx", ex.Message);
            }
        }
    }
}
