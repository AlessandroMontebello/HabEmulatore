using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;

            Habbo Habbo = AkiledEnvironment.GetHabboByUsername(Params[1]);
            {
                return;
            }
            {
                UserRoom.SendWhisperChat(AkiledEnvironment.GetLanguageManager().TryGetValue(string.Format("cmd.authorized.langue.user", clientByUsername.Langue), Session.Langue));
                return;
            }
                if (Params.Length == 3)
                    int.TryParse(Params[2], out num);
                    if (Params.Length > 2)
                    {
                        for (int i = 2; i < Params.Length; i++)
                        {
                            Raison += Params[i] + " ";
                        }
                    }
                    else
                    {

                        Raison = "Non è stato specificato alcun motivo.";
                    }

                    string Username = Habbo.Username;
                     AkiledEnvironment.GetGame().GetClientManager().BanUserAsync(clientByUsername, Session.GetHabbo().Username, (double)num, Raison, false, false).ConfigureAwait(false);
                    AkiledEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble("baneo", "L'utente: " + Username + " è stato bannato, si prega di verificare il motivo del ban, per evitare malintesi"));
                    Session.SendWhisper("Eccellente, hai bannato l'IP dell'utente '" + Username + "' per il motivo: '" + Raison + "'!");
                {
                     AkiledEnvironment.GetGame().GetClientManager().BanUserAsync(Session, "Robot", (double)788922000, "Il tuo account è stato bannato per motivi di sicurezza", false, false).ConfigureAwait(false);
                }