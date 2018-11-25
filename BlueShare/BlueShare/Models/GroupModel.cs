using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueShare.Models
{
    [Table("Group")]
    public class GroupModel
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        [Unique] public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]  public List<UserModel> Users { get; set; }
    }
}