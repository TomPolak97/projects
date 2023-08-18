using System;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    public class MenuDelegate
    {
        private MainMenu m_MainMenuDelegate;

        public MenuDelegate()
        {
            m_MainMenuDelegate = this.buildWholeMenu();
        }

        public MainMenu MainMenu
        {
            get
            {
                return m_MainMenuDelegate; 
            }
        }
 
        private MainMenu buildWholeMenu()
        {
            MainMenu mainMenu = new MainMenu("**Delegates Main Menu**");
            mainMenu.FirstMenuTitle = "**Delegates Main Menu**";

            MenuItem showDateOrTime = new MenuItem(mainMenu, "Show Date/Time");
            MenuItem versionAndSpaces = new MenuItem(mainMenu, "Version and Spaces");

            ActionItem showTime = new ActionItem(showDateOrTime, "Show Time");
            ActionItem showDate = new ActionItem(showDateOrTime, "Show Date");
            ActionItem countSpaces = new ActionItem(versionAndSpaces, "Count Spaces");
            ActionItem showVersion = new ActionItem(versionAndSpaces, "Show Version");

            mainMenu.SubItems.Add(showDateOrTime);
            mainMenu.SubItems.Add(versionAndSpaces);
            showDateOrTime.SubItems.Add(showTime);
            showDateOrTime.SubItems.Add(showDate);  
            versionAndSpaces.SubItems.Add(countSpaces);
            versionAndSpaces.SubItems.Add(showVersion);

            showTime.ChosenAct += showTime_ChosenAct;
            showDate.ChosenAct += showDate_ChosenAct;
            countSpaces.ChosenAct += countSpaces_ChosenAct;
            showVersion.ChosenAct += showVersion_ChosenAct;

            return mainMenu;
        }

        private static void showTime_ChosenAct(ActionItem i_ChosenActionItem)
        {
            string time = DateTime.Now.ToShortTimeString();

            Console.WriteLine("The current time is {0}", time);
        }

        private static void showDate_ChosenAct(ActionItem i_ChosenActionItem)
        {
            string date = DateTime.Now.ToShortDateString();

            Console.WriteLine("The current date is {0}", date);
        }

        private static void countSpaces_ChosenAct(ActionItem i_ChosenActionItem)
        {
            int countSpaces = 0;

            Console.WriteLine("Please enter a sentence");
            string sentence = Console.ReadLine();
            foreach (char c in sentence)
            {
                if (c == ' ')
                {
                    countSpaces++;
                }
            }

            Console.WriteLine("There are {0} spaces in your sentence.", countSpaces);
            Console.WriteLine();
        }

        private static void showVersion_ChosenAct(ActionItem i_ChosenActionItem)
        {
            Console.WriteLine("Version: 22.2.4.8950");
            Console.WriteLine();
        }
    }
}