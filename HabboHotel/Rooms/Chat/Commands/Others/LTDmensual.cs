using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
using System.Text;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class LTDmensual : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            string Message = CommandManager.MergeParams(Params, 1);
            string name_hotel = (AkiledEnvironment.GetConfig().data["namehotel_text"]);
            string ltdmensual_alert = (AkiledEnvironment.GetConfig().data["ltdmensual_alert"]);
            AkiledEnvironment.GetGame().GetClientManager().SendMessage(new RoomNotificationComposer("NUOVO LTD MENSILE DISPONIBILE!",
              "Cari utenti di <font color=\"#d100fe\"><b>© " + name_hotel + "</b></font> <b>Il catalogo è appena stato aggiornato!</b> Questa volta è un nuovo <font color=\"#fecb00\"><b>Rari LTD MENSILE </b></font> Basta fare clic sul pulsante in basso per visualizzarlo.<br>", ltdmensual_alert, Encoding.GetEncoding("Windows-1252").GetString(Encoding.GetEncoding("UTF-8").GetBytes("Vai al negozio LTD")), "event:catalog/open/rares_ltd" + Message));

            Session.SendWhisper("Nuovo avviso mensile LTD inviato correttamente.", 34);
        }
    }
}
