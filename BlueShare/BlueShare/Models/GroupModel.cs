using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueShare.Models
{
    [Table("Group")]
    public class GroupModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }
    }
}