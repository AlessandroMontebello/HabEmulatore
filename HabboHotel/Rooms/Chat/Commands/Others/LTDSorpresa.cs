using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
using System.Text;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class LTDSorpresa : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            string Message = CommandManager.MergeParams(Params, 1);
            string name_hotel = (AkiledEnvironment.GetConfig().data["namehotel_text"]);
            string ltdsorpresa_alert = (AkiledEnvironment.GetConfig().data["ltdsorpresa_alert"]);
            AkiledEnvironment.GetGame().GetClientManager().SendMessage(new RoomNotificationComposer("LTD SORPRESA!",
              "Cari utenti di <font color=\"#ff871d\"><b>© " + name_hotel + " 2024</b></font> il catalogo è appena stato aggiornato!</b> Questa volta questa è una novità <font color=\"#fecb00\"><b>Rari LTD Surprise</b></font> Devi solo fare clic sul pulsante qui sotto per visualizzarlo.<br>", ltdsorpresa_alert, Encoding.GetEncoding("Windows-1252").GetString(Encoding.GetEncoding("UTF-8").GetBytes("Vai al negozio LTD")), "event:catalog/open/rares_ltd" + Message));

            Session.SendWhisper("Nuovo avviso settimanale LTD inviato con successo.", 34);
        }
    }
}
