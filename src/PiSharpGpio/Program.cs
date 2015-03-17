//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Andrew Bradford">
//     Copyright (C) 2012 Andrew Bradford
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
//     associated documentation files (the "Software"), to deal in the Software without restriction, 
//     including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
//     and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject 
//     to the following conditions:
//     
//     The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//     
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//     WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
//     COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
//     ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

namespace PiSharpGpio
{
    using System;
    using System.Threading;
    
    using PiSharp.LibGpio;
    using PiSharp.LibGpio.Entities;

    /// <summary>
    /// Demo program for the LibGpio library 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program Entry point
        /// </summary>
        public static void Main()
        {
            Console.ReadKey();

            LibGpio.Gpio.TestMode = true;

            LibGpio.Gpio.SetupChannel(BroadcomPinNumber.Eighteen, Direction.Output);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Two, Direction.Output);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Three, Direction.Output);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Four, Direction.Output);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Five, Direction.Output);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Six, Direction.Output);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Seven, Direction.Output);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Zero, Direction.Output);

            Console.ReadKey();

            for (var i = 0; i < 5; ++i)
            {
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.Four, true);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.Seventeen, true);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.Eighteen, true);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.TwentyOne, true);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.TwentyTwo, true);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.TwentyThree, true);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.TwentyFour, true);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.TwentyFive, true);
                Thread.Sleep(200);

                LibGpio.Gpio.OutputValue(BroadcomPinNumber.Four, false);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.Seventeen, false);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.Eighteen, false);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.TwentyOne, false);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.TwentyTwo, false);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.TwentyThree, false);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.TwentyFour, false);
                Thread.Sleep(200);
                LibGpio.Gpio.OutputValue(BroadcomPinNumber.TwentyFive, false);
                Thread.Sleep(200);
            }

            Console.ReadKey();

            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.One, Direction.Input);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Two, Direction.Output);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Three, Direction.Input);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Four, Direction.Output);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Five, Direction.Input);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Six, Direction.Output);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Seven, Direction.Input);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Zero, Direction.Output);

            var oldInput = false;
            while (true)
            {
                var newInput = LibGpio.Gpio.ReadValue(RaspberryPinNumber.Seven);
                if (newInput != oldInput)
                {
                    LibGpio.Gpio.OutputValue(BroadcomPinNumber.Seventeen, newInput);
                    oldInput = newInput;
                }
            }
        }
    }
}
