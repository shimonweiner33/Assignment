using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Data.Models
{
    public class Room : ModelBase
    {
        public int RoomNum { get; set; }
        public string RoomName { get; set; }
        public List<ExtendMember> Users { get; set; }
        public List<Rooms_UserConnectinons> UserConnectinons { get; set; }
    }
    public class RoomsList
    {
        public List<Room> Rooms { get; set; }
    }
    public class Rooms_UserConnectinons
    {
        public string UserConnectinonId { get; set; }
        public int RoomNum { get; set; }
    }
}
