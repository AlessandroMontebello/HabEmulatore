using Akiled.Communication.Packets.Outgoing;
            int userId = Packet.PopInt();
            Session.SendPacket(onGuideSessionAttached);