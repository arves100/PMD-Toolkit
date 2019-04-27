﻿/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMDToolkit.Logic.Gameplay {
    public class SpellMenu : SingleStripMenu {

        
        public SpellMenu() {
            string[] moves = new string[Processor.MAX_MOVE_SLOTS];
            for (int i = 0; i < Processor.MAX_MOVE_SLOTS; i++) {
                if (Processor.Moves[i].MoveNum >= 0) {
                    moves[i] = "(" + Data.GameData.MoveDex[Processor.Moves[i].MoveNum].PP + "PP) " + Data.GameData.MoveDex[Processor.Moves[i].MoveNum].Name;
                } else {
                    moves[i] = "Empty";
                }
            }

            Initialize(new Maps.Loc2D(100, 100), 256, moves, 0);
        
        }

        public override void Process(Input input, ActiveChar character, ref bool moveMade) {
            if (input[Input.InputType.W] && !Processor.InputState[(int)Processor.InputType.W]) {
                MenuManager.Menus.RemoveAt(0);
            } else {
                base.Process(input, character, ref moveMade);
            }
        }

        protected override void Choose(ActiveChar character, ref bool moveMade) {
            MenuManager.Menus.Clear();
            Processor.ProcessDecision(new Command(Command.CommandType.Spell, CurrentChoice), character, ref moveMade);
        }

    }
}
