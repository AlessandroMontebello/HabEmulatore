using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class info : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            TimeSpan Uptime = DateTime.Now - AkiledEnvironment.ServerStarted;

            int OnlineUsers = AkiledEnvironment.GetGame().GetClientManager().Count;
            string about_image = (AkiledEnvironment.GetConfig().data["about_image"]);
            string name_hotel = (AkiledEnvironment.GetConfig().data["namehotel_text"]);
            int OnlineWeb = AkiledEnvironment.GetGame().GetClientWebManager().Count;
            int RoomCount = AkiledEnvironment.GetGame().GetRoomManager().Count;



            Session.SendMessage(new RoomNotificationComposer("StocazzoEmu per " + name_hotel + "",
             "<b><font color='#9200cc'>StocazzoEMu</font></b>\n<font color='#363636'>StocazzoEmu es un proyecto de varios fanaticos de habbo para tener todas las Funciones de Habbo.\n\n</font><font color='#9200cc'><b>Creditos para</b>:</font>\n" +
             "Hanami (Dev)\n" +
             "<b><font color='#9200cc'>Informazioni:</font></b>\n" +
                        "<font color='#363636'><b>Utenti On:</b> " + OnlineUsers + "\n" +
                        "<b>Stanze:</b> " + RoomCount + "\n" +
                        "<b>Tempo Online:</b> " + Uptime.Days + " day(s), " + Uptime.Hours + " hours and " + Uptime.Minutes + " minutes.</font>\n\n", about_image, ""));

        }
    }
}