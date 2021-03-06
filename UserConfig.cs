﻿using System;
using System.Collections.Generic;
using System.IO;
using static ADAB.Logic;

namespace ADAB
{
    public class UserConfig
    {
        public static string ConfigUserFile { get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AnyDesk\\user.conf"; }
        public static string ConfigSystemFile { get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AnyDesk\\system.conf"; }

        public static List<Connect_Item> GetLastConnections()
        {
            var returnValue = new List<Connect_Item>();

            try
            {
                var stringRosterItems = "ad.roster.items=";
                string[] LastConnections = { "" };
                if (File.Exists(ConfigUserFile))
                {
                    var allLinesFromFile = File.ReadAllLines(ConfigUserFile);
                    foreach (var search_roster_items in allLinesFromFile)
                    {
                        if (search_roster_items.Contains(stringRosterItems))
                        {
                            LastConnections = search_roster_items.Replace(stringRosterItems, "").Split(new char[] { ';' });

                            break;
                        }
                    }
                    if (LastConnections[0] != "")
                    {
                        foreach (var connection in LastConnections)
                        {
                            var currentRosterItem = new Logic.Connect_Item(connection);
                            returnValue.Add(currentRosterItem);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return returnValue;
        }
        public static Connect_Item GetThisID()
        {
            var returnValue = zeroConnectItem;

            try
            {
                var stringid = "ad.anynet.id=";
                if (File.Exists(ConfigUserFile))
                {
                    var allLinesFromFile = File.ReadAllLines(ConfigSystemFile);
                    foreach (var search_roster_items in allLinesFromFile)
                    {
                        if (search_roster_items.Contains(stringid))
                        {
                            returnValue = new Connect_Item(search_roster_items.Replace(stringid, ""));
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
#if DEBUG
                throw;
#endif
            }

            return returnValue;
        }
    }
}
