using System;
using System.Collections.Generic;
using System.Text;
using Android.Database.Sqlite;
using Android.Util;
using BlueShare.Models;

namespace BlueShare.DAO
{
    public class GroupDAO : DataBase
    {
        public GroupDAO() : base()
        {
            try
            {
                Connection.CreateTable<GroupModel>();
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public List<GroupModel> Search()
        {
            try
            {
                return Connection.Table<GroupModel>().ToList();
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public List<GroupModel> Select(string name)
        {
            try
            {
                return Connection.Table<GroupModel>().Where(a => a.Name.Contains(name)).ToList();
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public GroupModel GetGroupById(int id)
        {
            try
            {
                return Connection.Table<GroupModel>().Where(a => a.Id == id).FirstOrDefault();
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public void Insert(GroupModel group)
        {
            try
            {
                Connection.Insert(group);
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public void Update(GroupModel group)
        {
            try
            {
                Connection.Update(group);
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public void Delete(GroupModel group)
        {
            try
            {
                Connection.Delete(group);
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }
    }
}
