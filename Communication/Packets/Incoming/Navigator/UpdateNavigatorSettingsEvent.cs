using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.Rooms;




        public void Parse(GameClient Session, ClientPacket Packet)
            int RoomId = Packet.PopInt();



            Session.SendPacket(new UserHomeRoomComposer(RoomId, 0));
        }
    }
}


