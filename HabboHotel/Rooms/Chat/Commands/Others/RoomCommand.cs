using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using System.Text;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class RoomCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            Room currentRoom = Session.GetHabbo().CurrentRoom;
            if (currentRoom == null)
                return;

            if (Params.Length == 1)
            {
                Session.SendWhisper("Oops, devi scegliere un'opzione da disattivare. esegui il comando ':room list'");
                return;
            }

            if (!Room.CheckRights(Session, true))
            {
                Session.SendWhisper("Oops, solo il proprietario della stanza può eseguire questo comando");
                return;
            }

            string Option = Params[1];
            switch (Option)
            {
                case "list":
                    {
                        StringBuilder List = new StringBuilder("");
                        List.AppendLine("Lista dei comandi della stanza");
                        List.AppendLine("-------------------------");
                        List.AppendLine(":Room pets      : " + (currentRoom.RoomData.PetMorphsAllowed == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room mascota   : " + (currentRoom.RoomData.PetMorphsAllowed == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room pull      : " + (currentRoom.RoomData.PullEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room push      : " + (currentRoom.RoomData.PushEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room colpisce    : " + (currentRoom.RoomData.GolpeEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room spull     : " + (currentRoom.RoomData.SPullEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room spush     : " + (currentRoom.RoomData.SPushEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room respect   : " + (currentRoom.RoomData.RespectNotificationsEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room enable    : " + (currentRoom.RoomData.EnablesEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room rubare     : " + (currentRoom.RoomData.RobarEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room bruciare    : " + (currentRoom.RoomData.BurnEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room bacio     : " + (currentRoom.RoomData.BesarEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room morto   : " + (currentRoom.RoomData.MatarEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room sex      : " + (currentRoom.RoomData.SexEnabled == true ? "Attivato" : "Disattivato"));
                        List.AppendLine(":Room fumare     : " + (currentRoom.RoomData.CrispyEnabled == true ? "Attivato" : "Disattivato"));



                        Session.SendNotification(List.ToString());
                        break;
                    }
                case "burn":
                case "bruciare":
                    {
                        currentRoom.RoomData.BurnEnabled = !currentRoom.RoomData.BurnEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `burn_enabled` = @BurnEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("BurnEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.BurnEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("L'abbrustolimento umano, in questa stanza è " + (currentRoom.RoomData.BurnEnabled == true ? "Attivato!" : "Disattivato!"));
                        break;
                    }
                case "sexo":
                case "sex":
                    {
                        currentRoom.RoomData.SexEnabled = !currentRoom.RoomData.SexEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `sex_enabled` = @SexEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("SexEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.SexEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("Il sesso, in questa stanza è " + (currentRoom.RoomData.SexEnabled == true ? "Attivato!" : "Disattivato!"));
                        break;
                    }
                case "atracos":
                case "rubare":
                    {
                        currentRoom.RoomData.RobarEnabled = !currentRoom.RoomData.RobarEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `robar_enabled` = @RobarEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("RobarEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.RobarEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("Le Rapine (furti), in questa stanza sono " + (currentRoom.RoomData.RobarEnabled == true ? "Attivate!" : "Disattivate!"));
                        break;
                    }
                case "besos":
                case "bacio":
                    {
                        currentRoom.RoomData.BesarEnabled = !currentRoom.RoomData.BesarEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `besar_enabled` = @BesarEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("BesarEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.BesarEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("I Baci (piccoli baci), in questa stanza sono " + (currentRoom.RoomData.BesarEnabled == true ? "Attivati!" : "Disattivati!"));
                        break;
                    }
                case "morto":
                case "matar":
                    {
                        currentRoom.RoomData.MatarEnabled = !currentRoom.RoomData.MatarEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `matar_enabled` = @MatarEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("MatarEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.MatarEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("Le Morti, in questa stanza sono " + (currentRoom.RoomData.MatarEnabled == true ? "Attivate!" : "Disattivate!"));
                        break;
                    }
                case "golpe":
                case "colpisce":
                    {
                        currentRoom.RoomData.GolpeEnabled = !currentRoom.RoomData.GolpeEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `golpe_enabled` = @GolpeEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("GolpeEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.GolpeEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("I colpi, in questa stanza sono " + (currentRoom.RoomData.GolpeEnabled == true ? "Attivati!" : "Disattivati!"));
                        break;
                    }
                case "crispy":
                case "criperos":
                case "fumare":
                case "smokee":
                    {
                        currentRoom.RoomData.CrispyEnabled = !currentRoom.RoomData.CrispyEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `crispy_enabled` = @CrispyEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("CrispyEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.CrispyEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("I Criperos (fumare crispy) in questa stanza sono " + (currentRoom.RoomData.CrispyEnabled == true ? "Attivati!" : "Disattivati!"));
                        break;
                    }


                case "push":
                    {
                        currentRoom.RoomData.PushEnabled = !currentRoom.RoomData.PushEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `push_enabled` = @PushEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("PushEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.PushEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("La modalità Push ora è " + (currentRoom.RoomData.PushEnabled == true ? "Attivata!" : "Disattivata!"));
                        break;
                    }

                case "spush":
                    {
                        currentRoom.RoomData.SPushEnabled = !currentRoom.RoomData.SPushEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `spush_enabled` = @PushEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("PushEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.SPushEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("La modalità Super Push ora è " + (currentRoom.RoomData.SPushEnabled == true ? "Attivata!" : "Disattivata!"));
                        break;
                    }

                case "spull":
                    {
                        currentRoom.RoomData.SPullEnabled = !currentRoom.RoomData.SPullEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `spull_enabled` = @PullEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("PullEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.SPullEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("La modalità Super Pull ora è " + (currentRoom.RoomData.SPullEnabled == true ? "Attivata!" : "Disattivata!"));
                        break;
                    }

                case "pull":
                    {
                        currentRoom.RoomData.PullEnabled = !currentRoom.RoomData.PullEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `pull_enabled` = @PullEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("PullEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.PullEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("La modalità Pull ora è " + (currentRoom.RoomData.PullEnabled == true ? "Attivata!" : "Disattivata!"));
                        break;
                    }

                case "enable":
                case "enables":
                    {
                        currentRoom.RoomData.EnablesEnabled = !currentRoom.RoomData.EnablesEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `enables_enabled` = @EnablesEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("EnablesEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.EnablesEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("Gli effetti, in questa stanza sono " + (currentRoom.RoomData.EnablesEnabled == true ? "Attivati!" : "Disattivati!"));
                        break;
                    }

                case "respect":
                case "respetos":
                    {
                        currentRoom.RoomData.RespectNotificationsEnabled = !currentRoom.RoomData.RespectNotificationsEnabled;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `respect_notifications_enabled` = @RespectNotificationsEnabled WHERE `id` = '" + currentRoom.RoomData.Id + "' LIMIT 1");
                            dbClient.AddParameter("RespectNotificationsEnabled", AkiledEnvironment.BoolToEnum(currentRoom.RoomData.RespectNotificationsEnabled));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("Le notifiche di Rispetto sono " + (currentRoom.RoomData.RespectNotificationsEnabled == true ? "Attivate!" : "Disattivate!"));
                        break;
                    }
                case "pets":
                case "morphs":
                case "mascota":
                    {
                        Room.PetMorphsAllowed = !Room.PetMorphsAllowed;
                        using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.SetQuery("UPDATE `rooms` SET `pet_morphs_allowed` = @PetMorphsAllowed WHERE `id` = '" + Room.Id + "' LIMIT 1");
                            dbClient.AddParameter("PetMorphsAllowed", AkiledEnvironment.BoolToEnum(Room.PetMorphsAllowed));
                            dbClient.RunQuery();
                        }

                        Session.SendWhisper("Trasformarsi in mascotte è " + (Room.PetMorphsAllowed == true ? "Attivato!" : "Disattivato!"));

                        if (!Room.PetMorphsAllowed)
                        {
                            foreach (RoomUser User in Room.GetRoomUserManager().GetRoomUsers())
                            {
                                if (User == null || User.GetClient() == null || User.GetClient().GetHabbo() == null)
                                    continue;

                                User.GetClient().SendWhisper("Il proprietario della stanza ha disabilitato l'opzione di trasformarsi in mascotte.");
                                if (User.GetClient().GetHabbo().PetId > 0)
                                {
                                    //Dillo all'utente cosa sta succedendo.
                                    User.GetClient().SendWhisper("Oops, il proprietario della stanza permette solo Utenti normali, no mascotte..");

                                    //Cambia l'Id Pet dell'utente.
                                    User.GetClient().GetHabbo().PetId = 0;

                                    //Rimuovi velocemente la vecchia istanza dell'utente.
                                    //Room.SendMessage(new UsersComposer(User));

                                    //Aggiungi la nuova, nemmeno se ne accorgeranno!!11 8-)
                                    //Room.SendMessage(new UsersComposer(User));
                                }
                            }
                        }
                        break;
                    }
            }
        }
    }
}

