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
using OpenTK.Input;
using PMDToolkit.Maps;

namespace PMDToolkit.Logic.Gameplay {
    public class Input : IEquatable<Input>
    {

        public enum InputType {
            Z,
            X,
            C,
            A,
            S,
            D,
            Q,
            W,
            Enter
        }

        private bool[] inputStates = new bool[9];

        public bool this[InputType i] {
            get {
                return inputStates[(int)i];
            }
        }

        public int TotalInputs { get { return inputStates.Length; } }

        public Direction8 Direction { get; protected set;  } = Direction8.None;

        public bool LeftMouse { get; set; }
        public bool RightMouse { get; set; }
        public Loc2D MouseLoc { get; set; }
        public int MouseWheel { get; set; }

        public bool Shift { get; set; }

        public bool ShowDebug { get; set; }
        public bool SpeedDown { get; set; }
        public bool SpeedUp { get; set; }
        public bool Intangible { get; set; }
        public bool Print { get; set; }
        public bool Restart { get; set; }

        public Input() {
            Direction = Direction8.None;
        /*}

        public Input(KeyboardDevice keyboard, MouseDevice mouse) {
            Loc2D dirLoc = new Loc2D();
            
            if (keyboard[Key.Down]) {
                Operations.MoveInDirection8(ref dirLoc, Direction8.Down, 1);
            }
            if (keyboard[Key.Left]) {
                Operations.MoveInDirection8(ref dirLoc, Direction8.Left, 1);
            }
            if (keyboard[Key.Up]) {
                Operations.MoveInDirection8(ref dirLoc, Direction8.Up, 1);
            }
            if (keyboard[Key.Right]) {
                Operations.MoveInDirection8(ref dirLoc, Direction8.Right, 1);
            }

            Direction = Operations.GetDirection8(new Loc2D(), dirLoc);

            inputStates[(int)InputType.X] = keyboard[Key.X];
            inputStates[(int)InputType.Z] = keyboard[Key.Z];
            inputStates[(int)InputType.C] = keyboard[Key.C];
            inputStates[(int)InputType.A] = keyboard[Key.A];
            inputStates[(int)InputType.S] = keyboard[Key.S];
            inputStates[(int)InputType.D] = keyboard[Key.D];

            inputStates[(int)InputType.Q] = keyboard[Key.Q];
            inputStates[(int)InputType.W] = keyboard[Key.W];

            inputStates[(int)InputType.Enter] = (keyboard[Key.Enter]);

            LeftMouse = mouse[MouseButton.Left];
            RightMouse = mouse[MouseButton.Right];

            MouseWheel = mouse.Wheel;

            MouseLoc = new Loc2D(mouse.X, mouse.Y);

            Shift = keyboard[Key.ShiftLeft] || keyboard[Key.ShiftRight];

            ShowDebug = keyboard[Key.F1];
            SpeedDown = keyboard[Key.F2];
            SpeedUp = keyboard[Key.F3];
#if GAME_MODE
            Intangible = keyboard[Key.F4];
            Print = keyboard[Key.F5];
            Restart = keyboard[Key.F12];
#endif*/
        }

        public static bool operator ==(Input input1, Input input2) {
            return input1.Equals(input2);
        }

        public static bool operator !=(Input input1, Input input2) {
            return !input1.Equals(input2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Input);
        }

        public bool Equals(Input other)
        {
            if (Direction != other.Direction) return false;

            for (int i = 0; i < 9; i++)
            {
                if (this[(InputType)i] != other[(InputType)i]) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 1660314596;
            hashCode = hashCode * -1521134295 + EqualityComparer<bool[]>.Default.GetHashCode(inputStates);
            hashCode = hashCode * -1521134295 + Direction.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalInputs.GetHashCode();
            hashCode = hashCode * -1521134295 + Direction.GetHashCode();
            hashCode = hashCode * -1521134295 + LeftMouse.GetHashCode();
            hashCode = hashCode * -1521134295 + RightMouse.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Loc2D>.Default.GetHashCode(MouseLoc);
            hashCode = hashCode * -1521134295 + MouseWheel.GetHashCode();
            hashCode = hashCode * -1521134295 + Shift.GetHashCode();
            hashCode = hashCode * -1521134295 + ShowDebug.GetHashCode();
            hashCode = hashCode * -1521134295 + SpeedDown.GetHashCode();
            hashCode = hashCode * -1521134295 + SpeedUp.GetHashCode();
            hashCode = hashCode * -1521134295 + Intangible.GetHashCode();
            hashCode = hashCode * -1521134295 + Print.GetHashCode();
            hashCode = hashCode * -1521134295 + Restart.GetHashCode();
            return hashCode;
        }

        public void HandleKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            Loc2D dirLoc = new Loc2D();

            switch (e.Key)
            {
                case Key.Down:
                    Operations.MoveInDirection8(ref dirLoc, Direction8.Down, 1);
                    break;
                case Key.Up:
                    Operations.MoveInDirection8(ref dirLoc, Direction8.Up, 1);
                    break;
                case Key.Left:
                    Operations.MoveInDirection8(ref dirLoc, Direction8.Left, 1);
                    break;
                case Key.Right:
                    Operations.MoveInDirection8(ref dirLoc, Direction8.Right, 1);
                    break;
                case Key.X:
                    inputStates[(int)InputType.X] = true;
                    break;
                case Key.Z:
                    inputStates[(int)InputType.Z] = true;
                    break;
                case Key.C:
                    inputStates[(int)InputType.C] = true;
                    break;
                case Key.A:
                    inputStates[(int)InputType.A] = true;
                    break;
                case Key.S:
                    inputStates[(int)InputType.S] = true;
                    break;
                case Key.D:
                    inputStates[(int)InputType.D] = true;
                    break;
                case Key.Q:
                    inputStates[(int)InputType.Q] = true;
                    break;
                case Key.W:
                    inputStates[(int)InputType.W] = true;
                    break;
                case Key.Enter:
                    inputStates[(int)InputType.Enter] = true;
                    break;
                case Key.ShiftLeft:
                case Key.ShiftRight:
                    Shift = true;
                    break;
                case Key.F1:
                    ShowDebug = true;
                    break;
                case Key.F3:
                    SpeedUp = true;
                    break;
                case Key.F2:
                    SpeedDown = true;
                    break;
#if GAME_MODE
                case Key.F4:
                    Intangible = true;
                    break;
                case Key.F5:
                    Print = true;
                    break;
                case Key.F12:
                    Restart = true;
                    break;
#endif


            }

            Direction = Operations.GetDirection8(new Loc2D(), dirLoc);

        }

        public void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseWheel = e.Value;
        }

        public void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
                LeftMouse = true;
            else if (e.Button == MouseButton.Right)
                RightMouse = true;

            MouseLoc = new Loc2D(e.X, e.Y);
        }

        public void HandleMouseMove(object sender, MouseMoveEventArgs e)
        {
            MouseLoc = new Loc2D(e.X, e.Y);
        }

        public bool AreHandleBinded = false;

    }
}
