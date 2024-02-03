using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Users;
using System.Text;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class userinfo2 : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length != 2) return; string username = Params[1]; if (string.IsNullOrEmpty(username)) { Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("input.userparammissing", Session.Langue)); return; }
            GameClient clientByUsername = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(username);
            if (clientByUsername == null || clientByUsername.GetHabbo() == null)
            {
                Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("input.useroffline", Session.Langue));
                return;
            }
            string name_monedaoficial = (AkiledEnvironment.GetConfig().data["name_monedaoficial"]);
            Habbo Habbo = clientByUsername.GetHabbo();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Nome: " + Habbo.Username + "\r"); stringBuilder.Append("Id: " + Habbo.Id + "\r");
            stringBuilder.Append("Motto: " + Habbo.Motto + "\r");
            stringBuilder.Append("Diamanti" + name_monedaoficial + ": " + Habbo.AkiledPoints + "\r");
            stringBuilder.Append("Crediti: " + Habbo.Credits + "\r");
            stringBuilder.Append("Win-Win: " + Habbo.AchievementPoints + "\r");
            stringBuilder.Append("Premium: " + ((Habbo.Rank > 1) ? "Si" : "No") + "\r");
            stringBuilder.Append("Mazo Score: " + Habbo.MazoHighScore + "\r");
            stringBuilder.Append("Rispetti: " + Habbo.Respect + "\r");
            stringBuilder.Append("Si trova in stanza: " + ((Habbo.InRoom) ? "Si" : "No") + "\r");

            if (Habbo.CurrentRoom != null && !Habbo.SpectatorMode)
            {
                stringBuilder.Append("\r - Info della stanza  [" + Habbo.CurrentRoom.Id + "] - \r");
                stringBuilder.Append("Proprietario: " + Habbo.CurrentRoom.RoomData.OwnerName + "\r");
                stringBuilder.Append("Nome: " + Habbo.CurrentRoom.RoomData.Name + "\r");
                stringBuilder.Append("Utenti: " + Habbo.CurrentRoom.UserCount + "/" + Habbo.CurrentRoom.RoomData.UsersMax + "\r");
            }

            if (Session.GetHabbo().HasFuse("fuse_sysadmin"))
            {
                stringBuilder.Append("\r - Altre Info - \r");
                stringBuilder.Append("MachineId: " + clientByUsername.MachineId + "\r");
                stringBuilder.Append("IP Web: " + clientByUsername.GetHabbo().IP + "\r");
                stringBuilder.Append("IP Emu: " + clientByUsername.GetConnection().getIp() + "\r");
                stringBuilder.Append("Langue: " + clientByUsername.Langue.ToString() + "\r");


            }

            Session.SendNotification(stringBuilder.ToString());

        }
    }
}