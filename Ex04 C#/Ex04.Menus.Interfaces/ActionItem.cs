using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class ActionItem : MenuItem
    {
        private List<IChosenActionItemListener> m_ChosenActionItemListeners;

        public ActionItem(MenuItem i_Parent, string i_Title) : base(i_Parent, i_Title)
        {
            m_ChosenActionItemListeners = new List<IChosenActionItemListener>();
        }

        public List<IChosenActionItemListener> ChosenActionItemListeners
        {
            get
            {
                return m_ChosenActionItemListeners;
            }
        }

        public void AddListener(IChosenActionItemListener i_Listener)
        {
            ChosenActionItemListeners.Add(i_Listener);
        }

        public void NotifyChosenActionListeners()
        {
            foreach (IChosenActionItemListener listener in ChosenActionItemListeners)
            {
                listener.MakeAction(this);
            }
        }

        protected override void MakeNextStepByItemType()
        {
            Console.Clear();
            (this as ActionItem).NotifyChosenActionListeners();
            Console.WriteLine("the action you chose was done. please press enter to go back to menu");
            Console.ReadLine();
        }
    }
}