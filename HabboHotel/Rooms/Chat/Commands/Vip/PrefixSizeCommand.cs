
using Akiled.HabboHotel.GameClients;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    internal class PrefixSizeCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Spiacenti, devi scrivere un numero compreso tra 1 e 20!");
            }
            else
            {
                int result;
                if (int.TryParse(Params[1], out result))
                {
                    if (string.IsNullOrEmpty(Session.GetHabbo().Prefixnamecolor))
                        Session.GetHabbo().Prefixnamecolor = "000000;";
                    if (result == 12)
                    {
                        Session.GetHabbo().PrefixSize = "12;12";
                        Session.SendWhisper("La dimensione del tuo prefisso è tornata alla normalità");
                    }
                    else
                    {
                        bool flag = true;
                        if (result < 1)
                            flag = false;
                        if (result > 20 && Session.GetHabbo().Rank < 6)
                            flag = false;
                        if (flag)
                        {
                            string str = Session.GetHabbo().PrefixSize.Split(';')[1];
                            if (!string.IsNullOrEmpty(str))
                            {
                                Session.GetHabbo().PrefixSize = result.ToString() + ";" + str;
                                Session.SendWhisper("La dimensione è stata modificata in " + Convert.ToString(result));
                            }
                            else
                            {
                                Session.GetHabbo().PrefixSize = Convert.ToString(result) + ";";
                                Session.SendWhisper("La dimensione è stata modificata in " + Convert.ToString(result));
                            }
                        }
                        else
                            Session.SendWhisper("Dimensione non valida, deve essere il numero 1-20.");
                    }
                }
                else
                    Session.SendWhisper("Dimensione non valida, deve essere il numero 1-20.");
            }
        }
    }
}
