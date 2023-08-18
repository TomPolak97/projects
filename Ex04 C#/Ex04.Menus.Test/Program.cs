using System;

namespace Ex04.Menus.Test
{
    public class Program
     {
        public static void Main()
        {
            MenuDelegate menuDelegate = new MenuDelegate();
            menuDelegate.MainMenu.Show();
            MenuInterface menuInterface = new MenuInterface();
            menuInterface.MainMenu.Show();
        }
     }
}
