using System;
using System.Collections.Generic;
using System.Text;
using Android.Database.Sqlite;
using Android.Util;
using BlueShare.Models;

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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
            }
        }
    }
}
