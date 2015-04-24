//-----------------------------------------------------------------------
// <copyright file="LibGpio.cs" company="Andrew Bradford">
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

namespace PiSharp.LibGpio
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    using PiSharp.LibGpio.Entities;

    /// <summary>
    /// Library for interfacing with the Raspberry Pi GPIO ports. 
    /// This class is implemented as a singleton - no attempt should be made to instantiate it
    /// </summary>
    public partial class LibGpio
    {
        /// <summary>
        /// Stores the singleton instance of the class
        /// </summary>
        private static LibGpio instance;

        /// <summary>
        /// Stores the configured directions of the GPIO ports
        /// </summary>
        private Dictionary<BroadcomPinNumber, Direction> directions = new Dictionary<BroadcomPinNumber, Direction>();

        /// <summary>
        ///  Prevents a default instance of the LibGpio class from being created.
        /// </summary>
        private LibGpio()
        {
            var gpioPath = this.GetGpioPath();
            if (!Directory.Exists(gpioPath))
            {
                Directory.CreateDirectory(gpioPath);
            }
        }

        /// <summary>
        /// Gets the class instance 
        /// </summary>
        public static LibGpio Gpio
        {
            get
            {
                if (instance == null)
                {
                    instance = new LibGpio();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the library should be used in test mode.
        /// Note: If running under Windows or Mac OSx, test mode is assumed.
        /// </summary>
        public bool TestMode
        {
            get;
            set;
        }

        /// <summary>
        /// Configures a GPIO channel for use
        /// </summary>
        /// <param name="pinNumber">The physical pin number to configure</param>
        /// <param name="direction">The direction to configure the pin for</param>
        public void SetupChannel(PhysicalPinNumber pinNumber, Direction direction)
        {
            this.SetupChannel(ConvertToBroadcom(pinNumber), direction);
        }

        /// <summary>
        /// Configures a GPIO channel for use
        /// </summary>
        /// <param name="pinNumber">The Broadcom pin number to configure</param>
        /// <param name="direction">The direction to configure the pin for</param>
        public void SetupChannel(BroadcomPinNumber pinNumber, Direction direction)
        {
            if (pinNumber == BroadcomPinNumber.Undefined)
            {
                throw new InvalidOperationException("Attempt to perform action on an undefined pin");
            }

            var outputName = string.Format("gpio{0}", (int)pinNumber);
            var gpioPath = Path.Combine(this.GetGpioPath(), outputName);

            // If already exported, unexport it before continuing
            if (Directory.Exists(gpioPath))
            {
                this.UnExport(pinNumber);
            }

            // Now export the channel
            this.Export(pinNumber);

            // Set the IO direction
            this.SetDirection(pinNumber, direction);

            Debug.WriteLine(string.Format("[PiSharp.LibGpio] Broadcom GPIO number '{0}', configured for use", pinNumber));
        }

        /// <summary>
        /// Outputs a value to a GPIO pin
        /// </summary>
        /// <param name="pinNumber">The pin number to use</param>
        /// <param name="value">The value to output</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when an attempt to use an incorrectly configured channel is made
        /// </exception>
        public void OutputValue(PhysicalPinNumber pinNumber, bool value)
        {
            this.OutputValue(ConvertToBroadcom(pinNumber), value);
        }

        /// <summary>
        /// Outputs a value to a GPIO pin
        /// </summary>
        /// <param name="pinNumber">The pin number to use</param>
        /// <param name="value">The value to output</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when an attempt to use an incorrectly configured channel is made
        /// </exception>
        public void OutputValue(BroadcomPinNumber pinNumber, bool value)
        {
            if (pinNumber == BroadcomPinNumber.Undefined)
            {
                throw new InvalidOperationException("Attempt to perform action on an undefined pin");
            }

            // Check that the output is configured
            if (!this.directions.ContainsKey(pinNumber))
            {
                throw new InvalidOperationException("Attempt to output value on un-configured pin");
            }

            // Check that the channel is not being used incorrectly
            if (this.directions[pinNumber] == Direction.Input)
            {
                throw new InvalidOperationException("Attempt to output value on pin configured for input");
            }

            var gpioId = string.Format("gpio{0}", (int)pinNumber);
            var filePath = Path.Combine(gpioId, "value");
            using (var streamWriter = new StreamWriter(Path.Combine(this.GetGpioPath(), filePath), false))
            {
                if (value)
                {
                    streamWriter.Write("1");
                }
                else
                {
                    streamWriter.Write("0");
                }
            }

            Debug.WriteLine(string.Format("[PiSharp.LibGpio] Broadcom GPIO number '{0}', outputting value '{1}'", pinNumber, value));
        }

        /// <summary>
        /// Reads a value form a GPIO pin
        /// </summary>
        /// <param name="pinNumber">The pin number to read</param>
        /// <returns>The value at that pin</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when an attempt to use an incorrectly configured channel is made
        /// </exception>
        public bool ReadValue(PhysicalPinNumber pinNumber)
        {
            return this.ReadValue(ConvertToBroadcom(pinNumber));
        }

        /// <summary>
        /// Reads a value form a GPIO pin
        /// </summary>
        /// <param name="pinNumber">The pin number to read</param>
        /// <returns>The value at that pin</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when an attempt to use an incorrectly configured channel is made
        /// </exception>
        public bool ReadValue(BroadcomPinNumber pinNumber)
        {
            if (pinNumber == BroadcomPinNumber.Undefined)
            {
                throw new InvalidOperationException("Attempt to perform action on an undefined pin");
            }

            // Check that the output is configured
            if (!this.directions.ContainsKey(pinNumber))
            {
                throw new InvalidOperationException("Attempt to read value from un-configured pin");
            }

            // Check that the channel is not being used incorrectly
            if (this.directions[pinNumber] == Direction.Output)
            {
                throw new InvalidOperationException("Attempt to read value form pin configured for output");
            }

            var gpioId = string.Format("gpio{0}", (int)pinNumber);
            var filePath = Path.Combine(gpioId, "value");
            using (var fileStream = new FileStream(Path.Combine(this.GetGpioPath(), filePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    var rawValue = streamReader.ReadToEnd().Trim();
                    Debug.WriteLine(string.Format("[PiSharp.LibGpio] Broadcom GPIO number '{0}', has input value '{1}'", pinNumber, rawValue));
                    if (rawValue == "1")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the Broadcom GPIO pin number form the physical pin number
        /// </summary>
        /// <param name="pinNumber">The physical pin number</param>
        /// <returns>The equivalent Broadcom ID</returns>
        private static BroadcomPinNumber ConvertToBroadcom(PhysicalPinNumber pinNumber)
        {
            GpioLayout Layout = GetRaspType();

            switch (pinNumber)
            {
                case PhysicalPinNumber.Three:
                    if (Layout == GpioLayout.LayoutOne)
                        return BroadcomPinNumber.Zero;
                    else
                        return BroadcomPinNumber.Two;

                case PhysicalPinNumber.Five:
                    if (Layout == GpioLayout.LayoutOne)
                        return BroadcomPinNumber.One;
                    else
                        return BroadcomPinNumber.Three;

                case PhysicalPinNumber.Seven:
                    return BroadcomPinNumber.Four;

                case PhysicalPinNumber.Eight:
                    return BroadcomPinNumber.Fourteen;

                case PhysicalPinNumber.Ten:
                    return BroadcomPinNumber.Fifteen;

                case PhysicalPinNumber.Eleven:
                    return BroadcomPinNumber.Seventeen;

                case PhysicalPinNumber.Twelve:
                    return BroadcomPinNumber.Eighteen;

                case PhysicalPinNumber.Thirteen:
                    if (Layout == GpioLayout.LayoutOne)
                        return BroadcomPinNumber.TwentyOne;
                    else
                        return BroadcomPinNumber.TwentySeven;

                case PhysicalPinNumber.Fifteen:
                    return BroadcomPinNumber.TwentyTwo;

                case PhysicalPinNumber.Sixteen:
                    return BroadcomPinNumber.TwentyThree;

                case PhysicalPinNumber.Eighteen:
                    return BroadcomPinNumber.TwentyFour;

                case PhysicalPinNumber.Nineteen:
                    return BroadcomPinNumber.Ten;

                case PhysicalPinNumber.TwentyOne:
                    return BroadcomPinNumber.Nine;

                case PhysicalPinNumber.TwentyTwo:
                    return BroadcomPinNumber.TwentyFive;

                case PhysicalPinNumber.TwentyThree:
                    return BroadcomPinNumber.Eleven;

                case PhysicalPinNumber.TwentyFour:
                    return BroadcomPinNumber.Eight;

                case PhysicalPinNumber.TwentySix:
                    return BroadcomPinNumber.Seven;

                case PhysicalPinNumber.TwentyNine:
                    if (Layout == GpioLayout.LayoutThree)
                        return BroadcomPinNumber.Five;
                    else
                        return BroadcomPinNumber.Undefined;

                case PhysicalPinNumber.ThirtyOne:
                    if (Layout == GpioLayout.LayoutThree)
                        return BroadcomPinNumber.Six;
                    else
                        return BroadcomPinNumber.Undefined;

                case PhysicalPinNumber.ThirtyTwo:
                    if (Layout == GpioLayout.LayoutThree)
                        return BroadcomPinNumber.Twelve;
                    else
                        return BroadcomPinNumber.Undefined;

                case PhysicalPinNumber.ThirtyThree:
                    if (Layout == GpioLayout.LayoutThree)
                        return BroadcomPinNumber.Thirteen;
                    else
                        return BroadcomPinNumber.Undefined;

                case PhysicalPinNumber.ThirtyFive:
                    if (Layout == GpioLayout.LayoutThree)
                        return BroadcomPinNumber.Nineteen;
                    else
                        return BroadcomPinNumber.Undefined;

                case PhysicalPinNumber.ThirtySix:
                    if (Layout == GpioLayout.LayoutThree)
                        return BroadcomPinNumber.Sixteen;
                    else
                        return BroadcomPinNumber.Undefined;

                case PhysicalPinNumber.ThirtySeven:
                    if (Layout == GpioLayout.LayoutThree)
                        return BroadcomPinNumber.TwentySix;
                    else
                        return BroadcomPinNumber.Undefined;

                case PhysicalPinNumber.ThirtyEight:
                    if (Layout == GpioLayout.LayoutThree)
                        return BroadcomPinNumber.Twenty;
                    else
                        return BroadcomPinNumber.Undefined;

                case PhysicalPinNumber.Fourty:
                    if (Layout == GpioLayout.LayoutThree)
                        return BroadcomPinNumber.TwentyOne;
                    else
                        return BroadcomPinNumber.Undefined;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Unexports a given GPIO pin number
        /// </summary>
        /// <param name="pinNumber">The pin number to unexport</param>
        private void UnExport(BroadcomPinNumber pinNumber)
        {
            if (pinNumber == BroadcomPinNumber.Undefined)
            {
                throw new InvalidOperationException("Attempt to perform action on an undefined pin");
            }

            using (var fileStream = new FileStream(Path.Combine(this.GetGpioPath(), "unexport"), FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write((int)pinNumber);
                }
            }

            Debug.WriteLine(string.Format("[PiSharp.LibGpio] Broadcom GPIO number '{0}', un-exported", pinNumber));
        }

        /// <summary>
        /// Exports a given GPIO pin
        /// </summary>
        /// <param name="pinNumber">The pin number to export</param>
        private void Export(BroadcomPinNumber pinNumber)
        {
            if (pinNumber == BroadcomPinNumber.Undefined)
            {
                throw new InvalidOperationException("Attempt to perform action on an undefined pin");
            }


            // The simulator requires directories to be created first, but the RasPi does not and throws an exception.
            if (this.TestMode || Environment.OSVersion.Platform != PlatformID.Unix)
            {
                var exportDir = string.Format("gpio{0}", (int)pinNumber);
                var exportPath = Path.Combine(this.GetGpioPath(), exportDir);
                if (!Directory.Exists(exportPath))
                {
                    Directory.CreateDirectory(exportPath);
                    File.Create(Path.Combine(exportPath, "value")).Close();
                }
            }

            using (var fileStream = new FileStream(Path.Combine(this.GetGpioPath(), "export"), FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite))
            {

                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write((int)pinNumber);
                    streamWriter.Flush();
                }
            }

            Debug.WriteLine(string.Format("[PiSharp.LibGpio] Broadcom GPIO number '{0}', been exported", pinNumber));
        }

        /// <summary>
        /// Sets the direction of a GPIO channel
        /// </summary>
        /// <param name="pinNumber">The pin number to set the direction of</param>
        /// <param name="direction">The direction to set it to</param>
        private void SetDirection(BroadcomPinNumber pinNumber, Direction direction)
        {
            if (pinNumber == BroadcomPinNumber.Undefined)
            {
                throw new InvalidOperationException("Attempt to perform action on an undefined pin");
            }


            var gpioId = string.Format("gpio{0}", (int)pinNumber);
            var filePath = Path.Combine(gpioId, "direction");
            using (var streamWriter = new StreamWriter(Path.Combine(this.GetGpioPath(), filePath), false))
            {
                if (direction == Direction.Input)
                {
                    streamWriter.Write("in");
                }
                else
                {
                    streamWriter.Write("out");
                }
            }

            // Internally log the direction of this pin, so we can perform safety checks later.
            if (this.directions.ContainsKey(pinNumber))
            {
                this.directions[pinNumber] = direction;
            }
            else
            {
                this.directions.Add(pinNumber, direction);
            }

            Debug.WriteLine(string.Format("[PiSharp.LibGpio] Broadcom GPIO number '{0}', now set to direction '{1}'", pinNumber, direction));
        }

        /// <summary>
        /// Gets the GPIO path to write to
        /// </summary>
        /// <returns>The correct path for the IO and mode</returns>
        private string GetGpioPath()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform == PlatformID.Win32Windows
                || Environment.OSVersion.Platform == PlatformID.Win32S || Environment.OSVersion.Platform == PlatformID.WinCE)
            {
                // If we're running under Windows, use a Windows format test path
                return "C:\\RasPiGpioTest";
            }
            else if (this.TestMode || Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                // If we're in Test mode or running on a Mac, use a Unix style test path
                return "/tmp/RasPiGpioTest";
            }
            else
            {
                // Otherwise use the the GPIO folder for the Raspberry Pi
                return "/sys/class/gpio";
            }
        }

        /// <summary>
        /// Gets the Raspberry Pi revision number (see : http://elinux.org/RPi_HardwareHistory#Board_Revision_History)
        /// </summary>
        /// <returns>The revision number of the rasp based on /proc/cpuinfo </returns>
        private static string GetRevNumber()
        {
            string[] lines = File.ReadAllLines(@"/proc/cpuinfo");

            foreach (string line in lines)
            {
                //"Revision\t: xxxx".Length = 15 (Note : Pi 2 now has 6 digit rev number)
                if (line.Length < 15)
                    continue;

                if (line.Substring(0, 8) == "Revision")
                    return line.Substring(11);
            }

            return null;
        }

        /// <summary>
        /// Convenience method that gets the type of gpio numbering (See : http://1.bp.blogspot.com/-SRF0TOvP2xo/VIKNV_Yk1hI/AAAAAAAAVMk/EJW1gPAZrc4/s1600/Raspberry-Pi-AllModelGPIO-pinouts.png)
        /// </summary>
        /// <returns>The revision number of the rasp based on /proc/cpuinfo </returns>
        private static GpioLayout GetRaspType()
        {
            string Rev = GetRevNumber();

            switch(Rev)
            {
                case "0002":
                    return GpioLayout.LayoutOne;

                case "0003":
                    return GpioLayout.LayoutOne;

                case "0004":
                    return GpioLayout.LayoutTwo;

                case "0005":
                    return GpioLayout.LayoutTwo;

                case "0006":
                    return GpioLayout.LayoutTwo;

                case "0007":
                    return GpioLayout.LayoutTwo;

                case "0008":
                    return GpioLayout.LayoutTwo;

                case "0009":
                    return GpioLayout.LayoutTwo;

                case "000d":
                    return GpioLayout.LayoutTwo;

                case "000e":
                    return GpioLayout.LayoutTwo;

                case "000f":
                    return GpioLayout.LayoutTwo;

                case "0010":
                    return GpioLayout.LayoutThree;

                case "0011":
                    return GpioLayout.LayoutThree;

                case "0012":
                    return GpioLayout.LayoutThree;

                case "1041":
                    return GpioLayout.LayoutThree;

                default:
                    return GpioLayout.LayoutThree;

            }
        }

    }
}
