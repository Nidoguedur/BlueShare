using System;
using System.Collections.Generic;
using System.Text;
using BlueShare.Models;

namespace BlueShare.DAO
{
    public class UserDAO : DataBase
    {
        public UserDAO() : base()
        {
            Connection.CreateTable<UserModel>();
        }

        public List<UserModel> Search()
        {
            return Connection.Table<UserModel>().ToList();
        }

        public List<UserModel> Select(string name)
        {
            return Connection.Table<UserModel>().Where(a => a.DeviceName.Contains(name)).ToList();
        }

        public UserModel GetUserById(string id)
        {
            return Connection.Table<UserModel>().Where(a => a.DeviceId == id).FirstOrDefault();
        }

        public void Insert(UserModel user)
        {
            Connection.Insert(user);
        }

        public void Update(UserModel user)
        {
            Connection.Update(user);
        }

        public void Exclusao(UserModel user)
        {
            Connection.Delete(user);
        }
    }
}
