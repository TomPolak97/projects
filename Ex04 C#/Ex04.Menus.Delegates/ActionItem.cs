using System;

namespace Ex04.Menus.Delegates
{
    public class ActionItem : MenuItem
    {
        public event Action<ActionItem> ChosenAct;

        public ActionItem(MenuItem i_ParentItem, string i_Title) : base(i_ParentItem, i_Title) { }

        protected virtual void OnChosenAct()
        {
            if (this.ChosenAct != null)
            {
                ChosenAct.Invoke(this);
            }
        }

        protected override void MakeNextStepByItemType()
        {
            Console.Clear();
            (this as ActionItem).OnChosenAct();
            Console.WriteLine("the action you chose was done. please press enter to go back to menu");
            Console.ReadLine();
        }
    }
}
