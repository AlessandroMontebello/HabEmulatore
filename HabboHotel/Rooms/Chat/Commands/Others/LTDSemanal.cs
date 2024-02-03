using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class LTDSemanal : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            string Message = CommandManager.MergeParams(Params, 1);
            string name_hotel = (AkiledEnvironment.GetConfig().data["namehotel_text"]);
            string ltdsemanal_alert = (AkiledEnvironment.GetConfig().data["ltdsemanal_alert"]);
            AkiledEnvironment.GetGame().GetClientManager().SendMessage(new RoomNotificationComposer("NUOVO LTD SETTIMANALE DISPONIBILE!",
                "Cari utenti di <font color=\"#d100fe\"><b>© " + name_hotel + " 2024</b></font> il catalogo è appena stato aggiornato!</b> Questa volta questa è una novità <font color=\"#fecb00\"><b>Rari LTD Settimanali</b></font> Devi solo fare clic sul pulsante in basso per visualizzarlo.<br>", ltdsemanal_alert, "Vai al negozio LTD", "event:catalog/open/ltdsemanal" + Message));

            Session.SendWhisper("Nuovo avviso settimanale LTD inviato con successo.", 34);
        }
    }
}
