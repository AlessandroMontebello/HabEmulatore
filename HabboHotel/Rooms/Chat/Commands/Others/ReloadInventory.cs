namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class ReloadInventory : IChatCommand
    {
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Room == null)
                return;

            Session.GetHabbo().GetInventoryComponent().LoadUserInventory(Session.GetHabbo().Id);
            Session.GetHabbo().GetInventoryComponent().UpdateItems(true);
            Session.SendWhisper("Il tuo inventario è stato aggiornato, riaprilo", 3);
            return;
        }
    }
}