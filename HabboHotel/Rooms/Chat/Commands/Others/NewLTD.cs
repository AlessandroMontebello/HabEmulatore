using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
using System.Text;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class NewLTD : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            string Message = CommandManager.MergeParams(Params, 1);
            string name_hotel = (AkiledEnvironment.GetConfig().data["namehotel_text"]);
            AkiledEnvironment.GetGame().GetClientManager().SendMessage(new RoomNotificationComposer("NUOVO LTD SETTIMANALE DISPONIBILE!",
              "Cari utenti di <font color=\"#ff871d\"><b>© " + name_hotel + "</b></font> <b>Il catalogo è appena stato aggiornato!</b> Abbiamo aggiunto i nuovi <font color=\"#fecb00\"><b>Rari LTD</b></font> Devi solo fare clic sul pulsante in basso per visualizzarli.<br>", "newltd", Encoding.GetEncoding("Windows-1252").GetString(Encoding.GetEncoding("UTF-8").GetBytes("Vai al negozio LTD")), "event:catalog/open/rares_ltd" + Message));

            Session.SendWhisper("Alerta De Nuevo LTD Mensual Enviado Correctamente.", 34);
        }
    }
}
