using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BlueShare.Models
{
    [Table("User")]
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
    }
}