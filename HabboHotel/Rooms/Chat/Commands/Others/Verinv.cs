using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class Verinv : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Room == null)
            {
                return;
            }
            if (Params.Length == 2)
            {
                string Username = Params[1];
                GameClient Client = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Username);
                if (Client != null)
                {
                    Session.SendWhisper("L'utente deve essere offline per poter visualizzare il proprio inventario!", 1);
                    return;
                }
                int UserId = AkiledEnvironment.GetGame().GetClientManager().GetUserIdByUsername(Username);
                if (UserId == 0)
                {
                    Session.SendWhisper("L'utente non esiste!", 1);
                    return;
                }
                Session.GetHabbo().GetInventoryComponent().LoadUserInventory(UserId);
                Session.SendWhisper("Nel tuo inventario ora vedrai tutti i mobili di'" + Username + "'", 1);
            }
            else
            {
                Session.GetHabbo().GetInventoryComponent().LoadUserInventory(0);
                Session.SendWhisper("Il tuo inventario è ora tornato alla normalità.", 1);
            }
            Session.SendWhisper("Inserisci :inventar per ripristinare il tuo inventario.", 1);
        }
    }
}
