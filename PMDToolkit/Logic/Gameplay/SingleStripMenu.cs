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
using PMDToolkit.Maps;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using PMDToolkit.Graphics;

namespace PMDToolkit.Logic.Gameplay {
    public abstract class SingleStripMenu : MenuBase {

        private int currentChoice;
        public int CurrentChoice { get { return currentChoice; } }

        public override Loc2D PickerPos {
            get { return new Loc2D(Start.X + TextureManager.MenuBack.TileWidth*2 - TextureManager.Picker.ImageWidth,
                Start.Y + TextureManager.MenuBack.TileHeight + currentChoice * 10); }
        }

        protected void Initialize(Loc2D start, int width, string[] choices, int defaultChoice) {
            Start = start;
            int horizSpace = width;
            int vertSpace = 10;
            for(int i = 0; i < choices.Length; i++) {
                Choices.Add(new MenuText(choices[i], start + new Loc2D(TextureManager.MenuBack.TileWidth * 2, TextureManager.MenuBack.TileHeight + vertSpace * i)));
            }
            End = Start + new Loc2D(horizSpace, choices.Length * vertSpace + TextureManager.MenuBack.TileHeight * 2);
            currentChoice = defaultChoice;
        }

        public override void Process(Input input, ActiveChar character, ref bool moveMade) {
            if (input[Input.InputType.Enter] && !Processor.InputState[(int)Processor.InputType.Enter]) {
                Choose(character, ref moveMade);
            } else if (input[Input.InputType.X] && !Processor.InputState[(int)Processor.InputType.X]) {
                Choose(character, ref moveMade);
            } else if (input[Input.InputType.Z] && !Processor.InputState[(int)Processor.InputType.Z]) {
                MenuManager.Menus.RemoveAt(0);
            } else {
                bool chooseDown = (input.Direction == Direction8.Down || input.Direction == Direction8.DownLeft || input.Direction == Direction8.DownRight);
                bool prevDown = (Processor.oldDirection == Direction8.Down || Processor.oldDirection == Direction8.DownLeft || Processor.oldDirection == Direction8.DownRight);
                bool chooseUp = (input.Direction == Direction8.Up || input.Direction == Direction8.UpLeft || input.Direction == Direction8.UpRight);
                bool prevUp = (Processor.oldDirection == Direction8.Up || Processor.oldDirection == Direction8.UpLeft || Processor.oldDirection == Direction8.UpRight);
                if (chooseDown && (!prevDown || Processor.InputTime >= RenderTime.FromMillisecs(40)))
                {
                    currentChoice = (currentChoice + 1) % Choices.Count;
                } else if (chooseUp && (!prevUp || Processor.InputTime >= RenderTime.FromMillisecs(40))) {
                    currentChoice = (currentChoice + Choices.Count - 1) % Choices.Count;
                }
            }
        }

        protected abstract void Choose(ActiveChar character, ref bool moveMade);

    }
}
