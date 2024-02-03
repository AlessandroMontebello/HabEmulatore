using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;using System;
using System.Data;
using System.Text;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd{    class UserInfo : IChatCommand    {        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)        {
            /*  if (Params.Length != 2)                return;            string username = Params[1];            if (string.IsNullOrEmpty(username))            {                Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("input.userparammissing", Session.Langue));                return;            }
    GameClient clientByUsername = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(username);
    if (clientByUsername == null || clientByUsername.GetHabbo() == null)
    {
        Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("input.useroffline", Session.Langue));
        return;
    }
    Habbo Habbo = clientByUsername.GetHabbo();
    StringBuilder stringBuilder = new StringBuilder();

     HabboInfo.Append("Nombre: " + Habbo.Username + "\r");             HabboInfo.Append("ID: " + Habbo.Id + "\r");
     HabboInfo.Append("Misi�n: " + Habbo.Motto + "\r");
     HabboInfo.Append("Diamantes: " + Habbo.AkiledPoints + "\r");
     HabboInfo.Append("Cr�ditos: " + Habbo.Credits + "\r");
     HabboInfo.Append("Recompensas: " + Habbo.AchievementPoints + "\r");
     HabboInfo.Append("VIP: " + ((Habbo.Rank > 1) ? "Si" : "No") + "\r");
     HabboInfo.Append("Mazo Score: " + Habbo.MazoHighScore + "\r");
     HabboInfo.Append("Respetos: " + Habbo.Respect + "\r");
     HabboInfo.Append("Sala: " + ((Habbo.InRoom) ? "Si" : "No") + "\r");

    if (Habbo.CurrentRoom != null && !Habbo.SpectatorMode)
    {
         HabboInfo.Append("\r - Informaci�n de la Sala [" + Habbo.CurrentRoom.Id + "] - \r");
         HabboInfo.Append("Due�o: " + Habbo.CurrentRoom.RoomData.OwnerName + "\r");
         HabboInfo.Append("Nomre: " + Habbo.CurrentRoom.RoomData.Name + "\r");
         HabboInfo.Append("Usuarios: " + Habbo.CurrentRoom.UserCount + "/" + Habbo.CurrentRoom.RoomData.UsersMax + "\r");
    }

    if (Session.GetHabbo().HasFuse("fuse_sysadmin"))
    {
         HabboInfo.Append("\r - Otra Informaci�n - \r");
         HabboInfo.Append("MAC: " + clientByUsername.MachineId + "\r");
         HabboInfo.Append("IP Web: " + clientByUsername.GetHabbo().IP + "\r");
         HabboInfo.Append("IP Emulador: " + clientByUsername.GetConnection().getIp() + "\r");

        WebClient ClientWeb = AkiledEnvironment.GetGame().GetClientWebManager().GetClientByUserID(Habbo.Id);
        if (ClientWeb != null)
        {
             HabboInfo.Append("WebSocket: En L�nea" + "\r");
            if (Session.GetHabbo().Rank > 12)  HabboInfo.Append("WebSocket Ip: " + ClientWeb.GetConnection().getIp() + "\r");
        }
        else
        {
             HabboInfo.Append("WebSocket: No conectado" + "\r");
        }
    }

    Session.SendNotification(stringBuilder.ToString());

}*/

            if (Params.Length == 1)
            {
                Session.SendWhisper("Inserisci il nome dell'utente che desideri vedere rivedere le sue informazioni.");
                return;
            }

            DataRow UserData = null;
            string Username = Params[1];

            using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT `id`,`username`,`mail`,`rank`,`motto`,'look',`credits`,`activity_points`,`vip_points`,`online`,`games_win` FROM users WHERE `username` = @Username LIMIT 1");
                dbClient.AddParameter("Username", Username);
                UserData = dbClient.GetRow();
            }

            if (UserData == null)
            {
                Session.SendNotification("Non esiste nessun utente con questo nome " + Username + ".");
                return;
            }


            GameClient TargetClient = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Username);


            DateTime valecrack = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            valecrack = valecrack.AddSeconds(Session.GetHabbo().LastOnline).ToLocalTime();

            string time = valecrack.ToString();
            string name_monedaoficial = (AkiledEnvironment.GetConfig().data["name_monedaoficial"]);
            StringBuilder HabboInfo = new StringBuilder();//
            HabboInfo.Append("<font  color='#0489B1'><b>Informazioni Generali:</b></font>\r");
            HabboInfo.Append("<font >ID: " + Convert.ToInt32(UserData["id"]) + "\r");
            HabboInfo.Append("Rango: " + Convert.ToInt32(UserData["rank"]) + "\r");
            HabboInfo.Append("Email: " + Convert.ToString(UserData["mail"]) + "\r");
            HabboInfo.Append("Online: " + (TargetClient != null ? "Si" : "No") + "\r");
            HabboInfo.Append("Ultima volta online: " + time + "</font>\r\r");

            HabboInfo.Append("<font  color='#0489B1'><b>Informazioni monetarie:</b></font>\r");
            HabboInfo.Append("<font><font color='#F7D358'><b>Crediti:</b></font> " + Convert.ToInt32(UserData["credits"]) + "\r");
            HabboInfo.Append("<font color='#00b1bb'><b>Diamanti:</b></font> " + Convert.ToInt32(UserData["activity_points"]) + "\r");
            HabboInfo.Append("<font color='#2E9AFE'><b>" + name_monedaoficial + ":</b></font> " + Convert.ToInt32(UserData["vip_points"]) + "\r");
            HabboInfo.Append("<font >Giochi vinti: " + Convert.ToInt32(UserData["games_win"]) + "\r\r");


            if (TargetClient != null)
            {
                HabboInfo.Append("<font size='15' color = '#0489B1'><b> Posizione nell'Hotel:</b></font>\r");
                if (!TargetClient.GetHabbo().InRoom)
                    HabboInfo.Append("Non trovato in nessuna stanza.\r");
                else
                {
                    HabboInfo.Append("<font >Stanza: " + TargetClient.GetHabbo().CurrentRoom.RoomData.Name + " (" + TargetClient.GetHabbo().CurrentRoom.Id + ")</font>\r");
                    HabboInfo.Append("<font >Proprietario: " + TargetClient.GetHabbo().CurrentRoom.RoomData.OwnerName + "</font>\r");
                    HabboInfo.Append("<font >Visitatori: " + TargetClient.GetHabbo().CurrentRoom.UserCount + " Di " + TargetClient.GetHabbo().CurrentRoom.RoomData.UsersMax + "</font>\r");
                }
                if (Session.GetHabbo().HasFuse("fuse_sysadmin"))
                {
                    HabboInfo.Append("\r - Altre Informazioni - \r");
                    HabboInfo.Append("MAC: " + TargetClient.GetHabbo().MachineId + "\r");
                    HabboInfo.Append("IP Web: " + TargetClient.GetHabbo().IP + "\r");
                    HabboInfo.Append("IP Emulatore: " + TargetClient.GetConnection().getIp() + "\r");


                }
                Session.SendPacket(new RoomNotificationComposer("Informazioni di " + Username + ":", (HabboInfo.ToString()), "fig/" + Session.GetHabbo().Look + "", "", ""));
            }
        }
        public class Meteorologia
        {
            public string ApiVersion { get; set; }
            public Data Data { get; set; }
        }

        public class Data
        {
            public string Location { get; set; }
            public string Temperature { get; set; }
            public string Skytext { get; set; }
            public string Humidity { get; set; }
            public string Wind { get; set; }
            public string Date { get; set; }
            public string Day { get; set; }
        }
    }
}