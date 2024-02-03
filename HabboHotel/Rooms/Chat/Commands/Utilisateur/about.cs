using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class About : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            TimeSpan Uptime = DateTime.Now - AkiledEnvironment.ServerStarted;

            int OnlineUsers = AkiledEnvironment.GetGame().GetClientManager().Count;
            string about_image = (AkiledEnvironment.GetConfig().data["about_image"]);
            string name_hotel = (AkiledEnvironment.GetConfig().data["namehotel_text"]);
            int OnlineWeb = AkiledEnvironment.GetGame().GetClientWebManager().Count;
            int RoomCount = AkiledEnvironment.GetGame().GetRoomManager().Count;



            Session.SendMessage(new RoomNotificationComposer("Stocazzo Emulatore per " + name_hotel + "",
                        "<b><font color='#9200cc'>StocazzoEmu</font></b>\n<font color='#363636'>StocazzoEmu È un progetto di diversi fan di Habbo avere tutte le funzionalità di Habbo.\n\n</font><font color='#9200cc'><b>Crediti per</b>:</font>\n" +
                        "Hanami il fenomeno (Program)\n" +
                        "<b><font color='#9200cc'>Informazioni:</font></b>\n" +
                        "<font color='#363636'><b>Utenti On:</b> " + OnlineUsers + "\n" +
                        "<b>Stanze:</b> " + RoomCount + "\n" +
                        "<b>Tempo Online:</b> " + Uptime.Days + " day(s), " + Uptime.Hours + " hours and " + Uptime.Minutes + " minutes.</font>\n\n", about_image, ""));

        }
    }
}