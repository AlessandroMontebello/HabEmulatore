using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class UnmuteCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Inserisci il nome utente!", 1);
                return;
            }
            GameClient TargetClient = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null || TargetClient.GetHabbo() == null)
            {
                Session.SendWhisper("Impossibile trovare il giocatore.", 1);
                return;
            }
            using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.RunQuery("UPDATE `users` SET `time_muted` = '0' WHERE `id` = '" + TargetClient.GetHabbo().Id + "' LIMIT 1");
            }
            TargetClient.GetHabbo().TimeMuted = 0.0;
            TargetClient.SendWhisper("Sei stato smutato da " + Session.GetHabbo().Username + " ringrazia!", 1);
            Session.SendWhisper("Hai smutato l'utente " + TargetClient.GetHabbo().Username + "!", 1);
        }
    }
}
