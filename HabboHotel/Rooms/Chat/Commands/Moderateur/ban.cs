using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
using JNogueira.Discord.Webhook.Client;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
        {
         

            GameClient clientByUsername = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (clientByUsername == null || clientByUsername.GetHabbo() == null)
            {
                Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("input.usernotfound", Session.Langue));
                return;
            }
            {
                UserRoom.SendWhisperChat(AkiledEnvironment.GetLanguageManager().TryGetValue(string.Format("cmd.authorized.langue.user", clientByUsername.Langue), Session.Langue));
                return;
            }

            int num = 0;
            int.TryParse(Params[2], out num);
            if (num <= 600)
            {
                Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("ban.toolesstime", Session.Langue));
            }
            else
            {
                string Raison = CommandManager.MergeParams(Params, 3);
                if (string.IsNullOrEmpty(Raison))
                {
                    Raison = "Non è stato specificato alcun motivo.";
                }
                string Username = Habbo.Username;
                
                AkiledEnvironment.GetGame().GetClientManager().BanUserAsync(clientByUsername, Session.GetHabbo().Username, (double)num, Raison, false, false);
                AkiledEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble("baneo", "L'utente: " + Username + " è stato bannato, si prega di verificare il motivo del ban, per evitare malintesi"));
                Session.SendWhisper("Eccellente, hai bannato l'IP dell'utente '" + Username + "' per il motivo: '" + Raison + "'!");
                if (Session.Antipub(Raison, "<CMD>", Room.Id))
                    return;


            }

