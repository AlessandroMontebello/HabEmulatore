using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using System;
using System.Data;
using System.Text;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class LastCMDCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Inserisci il nome dell'utente che desideri vedere rivedere le sue informazioni.");
                return;
            }

            DataRow UserData = null;
            string Username = Params[1];

            using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT `username` FROM users WHERE `username` = @Username LIMIT 1");
                dbClient.AddParameter("Username", Username);
                UserData = dbClient.GetRow();
            }

            if (UserData == null)
            {
                Session.SendMessage(new RoomCustomizedAlertComposer("Non esiste nessun utente con questo nome " + Username + "."));
                return;
            }

            GameClient TargetClient = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Username);

            StringBuilder HabboInfo = new StringBuilder();

            HabboInfo.Append("Questi sono gli ultimi comandi utilizzati dall'utente, ricordati di rivedere sempre questi casi prima di procedere al ban a meno che non si tratti di un evidente caso di furto o abuso.\n\n");

            using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT `extra_data`,`command` FROM `cmdlogs` WHERE `user_id` = '" + TargetClient.GetHabbo().Id + "' ORDER BY `id` DESC LIMIT 15");
                DataTable GetLogs = dbClient.GetTable();

                if (GetLogs == null)
                {
                    Session.SendMessage(new RoomCustomizedAlertComposer("Purtroppo l'utente che hai richiesto non ha messaggi nel registro."));
                }

                else if (GetLogs != null)
                {
                    int Number = 11;
                    foreach (DataRow Log in GetLogs.Rows)
                    {
                        Number -= 1;
                        HabboInfo.Append("<font size ='8' color='#B40404'><b>[" + Number + "]</b></font>" + " " + Convert.ToString(Log["extra_data"]) + " <font size ='8' color='#B40404'><b> " + Convert.ToString(Log["command"]) + "</b></font>\r");
                    }
                }

                Session.SendMessage(new RoomNotificationComposer("Ultimi messaggi da " + Username + ":", (HabboInfo.ToString()), "fig/" + Session.GetHabbo().Look + "", "", ""));

            }
        }
    }
}


