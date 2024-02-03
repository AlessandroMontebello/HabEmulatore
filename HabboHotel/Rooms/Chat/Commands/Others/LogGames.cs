using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using System;
using System.Data;
using System.Text;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class LogGames : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            DataTable GetLogs = null;
            StringBuilder HabboInfo = new StringBuilder();

            HabboInfo.Append("Queste sono le ultime partite aperte nelle ultime 24 ore.\n\n");

            using IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor();
            dbClient.SetQuery("SELECT `user_name`,`timestamp`,`extra_data` FROM `cmdlogs` WHERE `command` = 'alertjuego' and from_unixtime(timestamp) >= now() - INTERVAL 1 DAY ORDER BY `id` DESC LIMIT 25");
            GetLogs = dbClient.GetTable();

            if (GetLogs == null)
            {
                Session.SendMessage(new RoomCustomizedAlertComposer("Sfortunatamente non c'è traccia di partite aperte."));
            }

            else if (GetLogs != null)
            {
                int Number = 11;
                foreach (DataRow Log in GetLogs.Rows)
                {
                    Number -= 1;
                    HabboInfo.Append("<font size ='8' color='#B40404'><b>[" + Number + "]</b></font>" + " " + Convert.ToString(Log["user_name"]) + " <font size ='8' color='#B40404'> <b> " + Convert.ToString(Log["extra_data"]) + "</b></font>\r");
                }
            }

            Session.SendMessage(new RoomNotificationComposer("Ultime partite aperte", (HabboInfo.ToString()), "fig/" + Session.GetHabbo().Look + "", "", ""));
        }
    }
}


