using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAB
{
    public class UserConfig
    {
        public static void ReadSettings()
        {
            try
            {
                var stringRosterItems = "ad.roster.items=";
                string[] LastConnections = { "" };
                var pathToUserConfig = General.AnyDeskConfigFolder + General.AnyDeskConfigUserFile;
                if (File.Exists(pathToUserConfig))
                {
                    var allLinesFromFile = File.ReadAllLines(pathToUserConfig);
                    foreach (var search_roster_items in allLinesFromFile)
                    {
                        if (search_roster_items.Contains(stringRosterItems))
                        {
                            LastConnections = search_roster_items.Replace(stringRosterItems, "").Split(new char[] { ';' });
                            break;
                        }
                    }

                    foreach (var connection in LastConnections)
                    {
                        var currentRosterItem = new UserConfig.Roster_Item(connection);
                        //string DisplayName = "";
                        Roster_Items.Add(currentRosterItem);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static readonly List<Roster_Item> Roster_Items =  new List<Roster_Item>();

        public static Roster_Item GetItemFromRoster_ItemsByAnyData(string data)
        {
            Roster_Item retval = new Roster_Item("","","",false );
            foreach (var item in Roster_Items)
            {
                if (item.adAlias == data){retval = item; break;}
                if (item.Name == data) { retval = item; break;}
                if (item.ID == data) { retval = item; break;}
            }
            return retval;
        }
        /// <summary>
        /// Строка подключения в настройках 
        /// </summary>
        public class Roster_Item
        {
            public readonly string adAlias = "";
            public readonly string ID = "";
            public readonly string Name = "";
            public readonly string Favorite = "";

            /// <summary>
            /// Конструктор строки коннекта
            /// </summary>
            /// <param name="iD">ID удалённого компьютера</param>
            /// <param name="adAlias">Псевдоним формата </param>
            /// <param name="name">Понятное имя</param>
            /// <param name="isFavorite">В избранном  или нет</param>
            public Roster_Item(string iD, string adAlias = "", string name = "", bool isFavorite = false)
            {
                ID = iD ?? throw new ArgumentNullException(nameof(iD));
                this.adAlias = ID  ?? adAlias ; 
                Name = name ?? "";
                Favorite= isFavorite == true ? "fav" : "";
                return;
            }

            /// <summary>
            /// Конструктор строки коннекта
            /// </summary>
            /// <param name="iD">ID удалённого компьютера</param>
            /// <param name="adAlias">Псевдоним формата </param>
            /// <param name="name">Понятное имя</param>
            /// <param name="fav_flag">В избранном ("fav") или нет ("")</param>
            public Roster_Item(string iD, string adAlias = "", string name = "", string fav_flag = "")
            {
                ID = iD ?? throw new ArgumentNullException(nameof(iD));
                this.adAlias = ID ?? adAlias; 
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Favorite = fav_flag == "fav" ? "fav" : "";
                return;
            }

            public Roster_Item(string connectionString)
            {
                var splitString = connectionString.Split(',');
                try
                {
                    if (splitString.Count<string>() > 1)
                    {
                        ID = splitString[1];
                        adAlias = splitString[0] ?? splitString[1];
                        Name = splitString[2] ?? "";
                        Favorite = splitString[3] ?? "";
                    }
                    else
                    {
                        ID = splitString[0] ?? splitString[1];
                        adAlias = splitString[0] ?? splitString[1];
                        Name = "" ?? splitString[2];
                        Favorite = "" ?? splitString[3];
                    }
                }
                catch (Exception ex)
                {
                    //retval = new AdRosterItem(splitString[0],"","",false);
                    //throw ;
                }
                return;
            }

            /// <summary>
            /// Строка вида "adAlias,ID,Name,"fav""
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return this.adAlias + "," + this.ID + "," + this.Name + "," + this.Favorite;
            }
        }

        public static void AddRosterItem(Roster_Item oldItem, Roster_Item newItem)
        {

        }

        public static void ChangeRosterItem(Roster_Item item)
        { 
        
        }

        public static void DeleteRosterItem(Roster_Item item)
        {

        }


    }
}
