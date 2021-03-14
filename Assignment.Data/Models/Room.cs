using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Data.Models
{
    public class Room : ModelBase
    {
        public int RoomNum { get; set; }
        public string RoomName { get; set; }
        public string UserNameOne { get; set; }
        public string UserNameTwo { get; set; }
    }
    public class RoomsList
    {
        public List<Room> Rooms { get; set; }
    }
}
