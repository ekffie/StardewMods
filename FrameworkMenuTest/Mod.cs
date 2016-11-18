﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using StardewModdingAPI;
using StardewModdingAPI.Events;

using StardewValley;

using Entoarox.Framework.Events;
using Entoarox.Framework.Menus;

namespace Entoarox.FrameworkMenuTest
{
    public class FrameworkMenuTestMod : Mod
    {
        internal static ProgressbarComponent prog=new ProgressbarComponent(new Point(0, 20), 0, 40);
        public override void Entry(params object[] objects)
        {
            MoreEvents.WorldReady += MoreEvents_WorldReady;
        }
        internal static void MoreEvents_WorldReady(object s, EventArgs e)
        {
            GameEvents.HalfSecondTick += GameEvents_HalfSecondTick;
            FrameworkMenu menu = new FrameworkMenu(new Point(256, 168));
            TablistComponent tablist = new TablistComponent(new Rectangle(0, 0, menu.ZoomEventRegion.Width, menu.ZoomEventRegion.Height));
            Rectangle size = tablist.ZoomEventRegion;
            GenericCollectionComponent page1 = new GenericCollectionComponent(new Rectangle(0, 0, size.Width, size.Height));
            GenericCollectionComponent page2 = new GenericCollectionComponent(new Rectangle(0, 0, size.Width, size.Height));
            GenericCollectionComponent page3 = new ScrollableCollectionComponent(new Rectangle(0, 0, size.Width, size.Height));

            menu.AddComponent(tablist);

            tablist.AddTab(2, page1);
            tablist.AddTab(4, page2);
            tablist.AddTab(6, page3);

            page1.AddComponent(new LabelComponent(new Point(0, -38), "Form Components"));
            page1.AddComponent(new CheckboxFormComponent(new Point(0, 2), "Example Checkbox", CheckboxChanged));
            page1.AddComponent(new PlusMinusFormComponent(new Point(0, 12), 0, 100, PlusMinusChanged));
            page1.AddComponent(new SliderFormComponent(new Point(0, 22), 3, IntSliderChanged));
            page1.AddComponent(new SliderFormComponent(new Point(0, 32), 12, IntSliderChanged));
            page1.AddComponent(new SliderFormComponent(new Point(0, 42), 20, IntSliderChanged));
            page1.AddComponent(new DropdownFormComponent(new Point(0, 51), new List<string>() {"First","Second","Third","Fourth"}, DropdownChanged));
            page1.AddComponent(new DropdownFormComponent(new Point(0, 65), new List<string>() { "#1", "#2", "#3", "#4", "#5", "#6", "#7", "#8", "#9", "#10","#11","#12" }, DropdownChanged));
            page1.AddComponent(new ButtonFormComponent(new Point(0, 78), "Test Button", ButtonClicked));
            page1.AddComponent(new TextboxFormComponent(new Point(0, 90), TextboxChanged));
            page1.AddComponent(new TextboxFormComponent(new Point(0, 105), TextboxChanged));

            page2.AddComponent(new LabelComponent(new Point(0, -38), "Generic Components"));
            page2.AddComponent(new HeartsComponent(new Point(0, 0), 3, 10));
            page2.AddComponent(new ClickableHeartsComponent(new Point(0, 10), 8, 10, HeartsChanged));
            page2.AddComponent(prog);
            
            for(var c=0;c<41;c++)
                page3.AddComponent(new HeartsComponent(new Point(0, 10*c), c, 40));

            Game1.activeClickableMenu = menu;
        }
        private static int Skipped = 0;
        internal static void GameEvents_HalfSecondTick(object s, EventArgs e)
        {
            if (prog.Value == 40)
            {
                if (Skipped>5)
                    prog.Value = 0;
                Skipped++;
            }
            else if(Skipped>20)
            {
                Skipped = 0;
            }
            else
                prog.Value += 4;
            if (Skipped > 0)
                Skipped++;
        }
        internal static void CheckboxChanged(IInteractiveMenuComponent component, IComponentContainer collection, FrameworkMenu menu, bool value)
        {
            Console.WriteLine("CheckBoxChanged: " + (value ? "true" : "false"));
        }
        internal static void PlusMinusChanged(IInteractiveMenuComponent component, IComponentContainer collection, FrameworkMenu menu, int value)
        {
            Console.WriteLine("PlusMinusChanged: " + value.ToString());
        }
        internal static void IntSliderChanged(IInteractiveMenuComponent component, IComponentContainer collection, FrameworkMenu menu, int value)
        {
            Console.WriteLine("IntSliderChanged: " + value.ToString());
        }
        internal static void HeartsChanged(IInteractiveMenuComponent component, IComponentContainer collection, FrameworkMenu menu, int value)
        {
            Console.WriteLine("HeartsChanged: " + value.ToString());
        }
        internal static void DropdownChanged(IInteractiveMenuComponent component, IComponentContainer collection, FrameworkMenu menu, string value)
        {
            Console.WriteLine("DropdownChanged: " + value);
        }
        internal static void TextboxChanged(IInteractiveMenuComponent component, IComponentContainer collection, FrameworkMenu menu, string value)
        {
            Console.WriteLine("TextboxChanged: " + value);
        }
        internal static void ButtonClicked(IInteractiveMenuComponent component, IComponentContainer collection, FrameworkMenu menu, bool left)
        {
            Console.WriteLine("ButtonClicked");
        }
    }
}