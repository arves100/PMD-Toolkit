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

using System.Collections.Generic;
using PMDToolkit.Maps;
using OpenTK;
using PMDToolkit.Graphics;

namespace PMDToolkit.Logic.Gameplay {
    public abstract class MenuBase {

        public Loc2D Start { get; set; }
        public Loc2D End { get; set; }

        public bool Visible { get; set; }

        public List<MenuText> NonChoices { get; set; }
        public List<MenuText> Choices { get; set; }

        public abstract Loc2D PickerPos { get; }

        public MenuBase() {
            NonChoices = new List<MenuText>();
            Choices = new List<MenuText>();
            Visible = true;
        }

        public abstract void Process(Input input, ActiveChar character, ref bool moveMade);

        public void Draw() {
            if (!Visible) return;

            //draw background
            //top-left
            TextureManager.TextureProgram.PushModelView();
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateTranslation(Start.X, Start.Y, 0));
            TextureManager.TextureProgram.UpdateModelView();
            TextureManager.MenuBack.RenderTile(0, 0);
            TextureManager.TextureProgram.PopModelView();
            //top-right
            TextureManager.TextureProgram.PushModelView();
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateTranslation(End.X - Graphics.TextureManager.MenuBack.TileWidth, Start.Y, 0));
            TextureManager.TextureProgram.UpdateModelView();
            TextureManager.MenuBack.RenderTile(2, 0);
            TextureManager.TextureProgram.PopModelView();
            //bottom-right
            TextureManager.TextureProgram.PushModelView();
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateTranslation(End.X - Graphics.TextureManager.MenuBack.TileWidth, End.Y - Graphics.TextureManager.MenuBack.TileHeight, 0));
            TextureManager.TextureProgram.UpdateModelView();
            TextureManager.MenuBack.RenderTile(2, 2);
            TextureManager.TextureProgram.PopModelView();
            //bottom-left
            TextureManager.TextureProgram.PushModelView();
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateTranslation(Start.X, End.Y - Graphics.TextureManager.MenuBack.TileHeight, 0));
            TextureManager.TextureProgram.UpdateModelView();
            TextureManager.MenuBack.RenderTile(0, 2);
            TextureManager.TextureProgram.PopModelView();

            //top
            TextureManager.TextureProgram.PushModelView();
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateTranslation(Start.X + Graphics.TextureManager.MenuBack.TileWidth, Start.Y, 0));
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateScale((float)(End.X - Start.X - 2 * Graphics.TextureManager.MenuBack.TileWidth) / Graphics.TextureManager.MenuBack.TileWidth, 1, 1));
            TextureManager.TextureProgram.UpdateModelView();
            TextureManager.MenuBack.RenderTile(1, 0);
            TextureManager.TextureProgram.PopModelView();

            //right
            TextureManager.TextureProgram.PushModelView();
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateTranslation(End.X - Graphics.TextureManager.MenuBack.TileWidth, Start.Y + Graphics.TextureManager.MenuBack.TileHeight, 0));
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateScale(1, (float)(End.Y - Start.Y - 2 * Graphics.TextureManager.MenuBack.TileHeight) / Graphics.TextureManager.MenuBack.TileHeight, 1));
            TextureManager.TextureProgram.UpdateModelView();
            TextureManager.MenuBack.RenderTile(2, 1);
            TextureManager.TextureProgram.PopModelView();

            //bottom
            TextureManager.TextureProgram.PushModelView();
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateTranslation(Start.X + Graphics.TextureManager.MenuBack.TileWidth, End.Y - Graphics.TextureManager.MenuBack.TileHeight, 0));
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateScale((float)(End.X - Start.X - 2 * Graphics.TextureManager.MenuBack.TileWidth) / Graphics.TextureManager.MenuBack.TileWidth, 1, 1));
            TextureManager.TextureProgram.UpdateModelView();
            TextureManager.MenuBack.RenderTile(1, 2);
            TextureManager.TextureProgram.PopModelView();

            //left
            TextureManager.TextureProgram.PushModelView();
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateTranslation(Start.X, Start.Y + Graphics.TextureManager.MenuBack.TileHeight, 0));
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateScale(1, (float)(End.Y - Start.Y - 2 * Graphics.TextureManager.MenuBack.TileHeight) / Graphics.TextureManager.MenuBack.TileHeight, 1));
            TextureManager.TextureProgram.UpdateModelView();
            TextureManager.MenuBack.RenderTile(0, 1);
            TextureManager.TextureProgram.PopModelView();

            //center
            TextureManager.TextureProgram.PushModelView();
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateTranslation(Start.X + Graphics.TextureManager.MenuBack.TileWidth, Start.Y + Graphics.TextureManager.MenuBack.TileHeight, 0));
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateScale((float)(End.X - Start.X - 2 * Graphics.TextureManager.MenuBack.TileWidth) / Graphics.TextureManager.MenuBack.TileWidth,
                (float)(End.Y - Start.Y - 2 * Graphics.TextureManager.MenuBack.TileHeight) / Graphics.TextureManager.MenuBack.TileHeight, 1));
            TextureManager.TextureProgram.UpdateModelView();
            TextureManager.MenuBack.RenderTile(1, 1);
            TextureManager.TextureProgram.PopModelView();

            //draw choices
            for (int i = 0; i < Choices.Count; i++) {
                TextureManager.SingleFont.RenderText(Choices[i].Loc.X, Choices[i].Loc.Y, Choices[i].Text, null, AtlasSheet.SpriteVOrigin.Top, AtlasSheet.SpriteHOrigin.Left, 0, Choices[i].Color);
            }

            //draw picker
            TextureManager.TextureProgram.PushModelView();
            TextureManager.TextureProgram.LeftMultModelView(Matrix4.CreateTranslation(PickerPos.X, PickerPos.Y, 0));
            TextureManager.TextureProgram.UpdateModelView();
            TextureManager.Picker.Render(null);
            TextureManager.TextureProgram.PopModelView();

        }
    }
}
