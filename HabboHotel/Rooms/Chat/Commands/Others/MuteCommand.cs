using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Users;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class MuteCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Inserisci nome utente e tempo in secondi! (Massimo 600 secondi)", 1);
                return;
            }
            Habbo Habbo = AkiledEnvironment.GetHabboByUsername(Params[1]);
            double Time;
            if (Habbo == null)
            {
                Session.SendWhisper("Utente non trovato.", 1);
            }
            else if (!Session.GetHabbo().HasFuse("fuse_mod"))
            {
                Session.SendWhisper("Non disponi dell'autorizzazione necessaria per utilizzare questo comando.", 1);
            }
            else if (double.TryParse(Params[2], out Time))
            {
                if (Time > 600.0 && !Session.GetHabbo().HasFuse("mod_mute_limit_override"))
                {
                    Time = 600.0;
                }
                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.RunQuery("UPDATE `users` SET `time_muted` = '" + Time + "' WHERE `id` = '" + Habbo.Id + "' LIMIT 1");
                }
                if (Habbo.GetCliente() != null)
                {
                    Habbo.TimeMuted = Time;
                    Habbo.GetCliente().SendWhisper("L'utente è stato mutato per " + Time + " Secondi!", 1);
                }
                Session.SendWhisper("L'utente " + Habbo.Username + " è stato mutato per " + Time + " Secondi!", 1);
            }
            else
            {
                Session.SendWhisper("È necessario inserire l'ora in secondi.", 1);
            }
        }
    }
}
