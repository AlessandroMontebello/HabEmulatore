using Akiled.HabboHotel.GameClients;
using System.Collections.Generic;
using System.Linq;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class CasinoCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Ops, devi specificare se vuoi attivare la modalità casinò o dare pl! Digita :casino start o :casino pl", 34);
                return;
            }
            string query = Params[1];

            RoomUser roomUser = Room.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
            if (roomUser == null)
            {
                return;
            }

            List<Items.Item> userBooth = Room.GetRoomItemHandler().GetFloor.Where(x => x != null && Gamemap.TilesTouching(
                x.Coordinate, roomUser.Coordinate) && x.Data.InteractionType == Items.InteractionType.dice).ToList();

            if (userBooth.Count != 5)
            {
                Session.SendWhisper("Devi avere 5 dadi per attivare il contatore", 34);
                return;
            }

            if (query == "pl" || query == "PL")
            {
                UserRoom.SendWhisperChat("L'utente " + Session.GetHabbo().Username + " tiro " + Session.GetHabbo().casinoCount + " sui dadi (PL Automatico)");
                Session.GetHabbo().casinoEnabled = false;
                Session.GetHabbo().casinoCount = 0;
            }
            else if (query == "start" || query == "START")
            {
                Session.SendWhisper("Inizi la modalità casinò. Il contatore dei dadi è attivo", 34);
                Session.GetHabbo().casinoEnabled = true;

            }
            else
            {
                Session.SendWhisper("Ops, devi specificare se vuoi attivare la modalità casinò o dare pl! Digita :casino start o :casino pl", 34);
            }

        }
    }
}
