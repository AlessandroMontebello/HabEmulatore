namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class ViewInventory : IChatCommand
    {
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Room == null)
                return;

            if (Params.Length == 2)
            {
                string Username = Params[1];

                int UserId = AkiledEnvironment.GetGame().GetClientManager().GetUserIdByUsername(Username);
                if (UserId == 0)
                {
                    Session.SendWhisper("Il nome utente non esiste.");
                    return;
                }

                Session.GetHabbo().GetInventoryComponent().LoadUserInventory(UserId);

                Session.SendWhisper("L'inventario è stato modificato in quello di " + Username);
            }
            else
            {
                Session.GetHabbo().GetInventoryComponent().LoadUserInventory(0);

                Session.SendWhisper("Il tuo inventario è tornato alla normalità.", 34);
            }
        }
    }
}