using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd

            if (Room.IsRoleplay)
                return;

            int Nombre = AkiledEnvironment.GetRandomNumber(1, 3);
            {
                    //SQL sauvegarde le score
                    UserRoom.SendWhisperChat(string.Format(AkiledEnvironment.GetLanguageManager().TryGetValue("cmd.mazo.newscore", Session.Langue), Habbo.Mazo));
            {
                //Mettre l'enable
                if (Habbo.Mazo > 0)

            using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                queryreactor.RunQuery("UPDATE users SET mazo = '" + Habbo.Mazo + "' WHERE id = " + Habbo.Id);