using System;
using System.Collections.Generic;
using System.Text;
using BlueShare.Models;

namespace BlueShare.DAO
{
    public class GroupDAO : DataBase
    {
        public GroupDAO() : base()
        {
            Connection.CreateTable<GroupModel>();
        }

        public List<GroupModel> Search()
        {
            return Connection.Table<GroupModel>().ToList();
        }

        public List<GroupModel> Select(string name)
        {
            return Connection.Table<GroupModel>().Where(a => a.Name.Contains(name)).ToList();
        }

        public GroupModel GetUserById(int id)
        {
            return Connection.Table<GroupModel>().Where(a => a.Id == id).FirstOrDefault();
        }

        public void Insert(GroupModel user)
        {
            Connection.Insert(user);
        }

        public void Update(GroupModel user)
        {
            Connection.Update(user);
        }

        public void Exclusao(GroupModel user)
        {
            Connection.Delete(user);
        }
    }
}
