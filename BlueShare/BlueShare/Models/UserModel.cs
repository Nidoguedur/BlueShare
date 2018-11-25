using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BlueShare.Models
{
    [Table("User")]
    public class UserModel
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }

        [Unique] public string DeviceId { get; set; }

        public string DeviceName { get; set; }

        [ForeignKey(typeof(GroupModel))] public int IdGroup { get; set; }
    }
}