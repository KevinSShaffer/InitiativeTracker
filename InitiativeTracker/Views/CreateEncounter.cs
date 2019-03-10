using InitiativeTracker.Components;
using InitiativeTracker.Helpers.Interfaces;
using InitiativeTracker.Models;
using InitiativeTracker.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeTracker.Views
{
    public class CreateEncounter : Component
    {
        private int activeComponentIndex = 0;
        private int ActiveComponentIndex
        {
            get
            {
                return activeComponentIndex;
            }
            set
            {
                activeComponent.Unfocus();

                if (value >= components.Count)
                    activeComponentIndex = 0;
                else if (value < 0)
                    activeComponentIndex = components.Count - 1;
                else
                    activeComponentIndex = value;

                activeComponent.Focus();
            }
        }
        private Component activeComponent => components[activeComponentIndex];
        private IEnumerable<string> monsterNames => encounter.MonsterGroups.Select(group => listBoxLine(group));
        private readonly Encounter encounter = new Encounter();
        private const string quantityLabel = "Quantity: ";
        private readonly List<Component> components = new List<Component>();
        private readonly IRenderer renderer;
        private readonly SearchBox searchBox;
        private readonly TextBox textBox;
        private readonly ListBox listBox;
        private readonly Button okButton;

        public CreateEncounter(IRenderer renderer, IGuesser<string> guesser)
        {
            this.renderer = renderer;
            searchBox =  new SearchBox(renderer, guesser, new Point(2, 1), 30, 16);
            textBox = new TextBox(renderer, new Point(quantityLabel.Length + 3, 18), 2);
            listBox = new ListBox(renderer, new Point(2, 20), 30, 8, monsterNames);
            okButton = new Button(renderer, new Point(23, 29)) { Text = "Done" };

            components.Add(searchBox);
            components.Add(textBox);
            components.Add(listBox);
            components.Add(okButton);

            okButton.EnterPressed += OkButton_EnterPressed;
            EnterPressed += CreateEncounter_EnterPressed;
            TabPressed += (o, e) => ActiveComponentIndex++;
            ShiftTabPressed += (o, e) => ActiveComponentIndex--;

            renderer.SetWindowSize(32, 32);
        }

        public override void KeyPressed(ConsoleKeyInfo keyInfo)
        {
            activeComponent.KeyPressed(keyInfo);

            base.KeyPressed(keyInfo);
        }

        private void OkButton_EnterPressed(object sender, KeyPressedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void CreateEncounter_EnterPressed(object sender, KeyPressedEventArgs e)
        {
            if (textBox == activeComponent &&
                tryCreateMonsterGroup(searchBox.Text, textBox.Text, out MonsterGroup monsterGroup))
            {
                encounter.MonsterGroups.Add(monsterGroup);
                searchBox.Clear();
                textBox.Clear();
                ActiveComponentIndex = 0;
            }
            else
                ActiveComponentIndex++;
        }

        private string listBoxLine(MonsterGroup monsterGroup)
        {
            string name = monsterGroup.Monster.Name, quantity = monsterGroup.Quantity.ToString();
            return $"{name.PadRight(26 - quantity.Length, ' ')}{quantity}";
        }

        private bool tryCreateMonsterGroup(string name, string quantity, out MonsterGroup monsterGroup)
        {
            if (!string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(quantity) &&
                int.TryParse(quantity, out int i))
            {
                monsterGroup = new MonsterGroup(new Monster(name), i);
                return true;
            }

            monsterGroup = null;
            return false;
        }

        public override void Draw()
        {
            ConsoleColor textColor = activeComponent == textBox ? ConsoleColor.White : ConsoleColor.DarkGray;

            foreach (var component in components)
                component.Draw();
            
            renderer.With(textColor).DrawText(new Point(3, 18), quantityLabel);
        }

        public override void Focus()
        {
            activeComponent.Focus();
        }

        public override void Unfocus()
        {
            activeComponent.Unfocus();
        }
    }
}
