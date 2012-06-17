//-----------------------------------------------------------------------
// <copyright file="Main.cs" company="Andrew Bradford">
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

namespace PiSharp.GpioSimulator
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// The main simulator form
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// Background image of a powered off LED
        /// </summary>
        private Image ledOffBitmap = new Bitmap(Path.Combine("Images", "LEDOff.png"));

        /// <summary>
        ///  Background image of a lit LED
        /// </summary>
        private Image ledOnBitmap = new Bitmap(Path.Combine("Images", "LEDOn.png"));

        /// <summary>
        /// Background image of a switched off switch
        /// </summary>
        private Image switchOffBitmap = new Bitmap(Path.Combine("Images", "SwitchOff.png"));
    
        /// <summary>
        /// Background image of a switched on switch
        /// </summary>
        private Image switchOnBitmap = new Bitmap(Path.Combine("Images", "SwitchOn.png"));

        /// <summary>
        /// Contains the current states of the GPIO pins
        /// </summary>
        private Dictionary<string, GpioState> chanelStates = new Dictionary<string, GpioState>();

        /// <summary>
        /// Contains the file system watcher, used to detect writes the GPIO
        /// </summary>
        private FileSystemWatcher fileWatcher;

        /// <summary>
        /// Initializes a new instance of the Main class.
        /// </summary>
        public Main()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Executes when the form loads
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The event arguments</param>
        private void Main_Load(object sender, EventArgs e)
        {
            this.fileWatcher = new FileSystemWatcher(this.GetGpioPath());
            this.fileWatcher.IncludeSubdirectories = true;
            this.fileWatcher.Changed += this.FileChanged;
            this.fileWatcher.Created += this.FileChanged;
            this.fileWatcher.Deleted += this.FileChanged;
            this.fileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Executed when there is a detected change to the monitored folder
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The event arguments</param>
        private void FileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.Name.Contains("direction"))
            {
                var value = string.Empty;
                using (var streamReader = new StreamReader(new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    value = streamReader.ReadToEnd();
                }

                var nameSplit = e.Name.Split(Path.DirectorySeparatorChar);
                var id = nameSplit[nameSplit.Length - 2];

                if (value == "out")
                {
                    this.GetPanel(id).BackgroundImage = this.ledOffBitmap;
                    this.ChangeState(id, GpioState.Outout);
                }
                else
                {
                    this.GetPanel(id).BackgroundImage = this.switchOffBitmap;
                    this.ChangeState(id, GpioState.Input);
                }
            }

            if (e.Name.Contains("value"))
            {                
                var value = string.Empty;
                using (var streamReader = new StreamReader(new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    value = streamReader.ReadToEnd();
                }

                var nameSplit = e.Name.Split(Path.DirectorySeparatorChar);
                var id = nameSplit[nameSplit.Length - 2];
                this.SetValue(this.GetPanel(id), value);
            }
        }

        /// <summary>
        /// Sets a simulated output to the correct value
        /// </summary>
        /// <param name="image">The image to change</param>
        /// <param name="value">The value</param>
        private void SetValue(Panel image, string value)
        {
            var id = image.Name.ToLower();
            if (this.chanelStates.ContainsKey(id) && this.chanelStates[id] != GpioState.Outout)
            {
                // Not configured as output, ignore it. This prevents the simulator changing itself when setting inputs
                return;
            }

            if (value == "1")
            {
                image.BackgroundImage = this.ledOnBitmap;
            }
            else
            {
                image.BackgroundImage = this.ledOffBitmap;
            }
        }

        /// <summary>
        /// Retrieves the panel represented a GPIO pin from its Broadcom ID
        /// </summary>
        /// <param name="id">The Broadcom ID, in form "gpio{Number}"</param>
        /// <returns>The corresponding panel</returns>
        private Panel GetPanel(string id)
        {
            switch (id)
            {
                case "gpio4":
                    return this.Gpio4;

                case "gpio17":
                    return this.Gpio17;

                case "gpio18":
                    return this.Gpio18;

                case "gpio21":
                    return this.Gpio21;

                case "gpio22":
                    return this.Gpio22;

                case "gpio23":
                    return this.Gpio23;

                case "gpio24":
                    return this.Gpio24;

                case "gpio25":
                    return this.Gpio25;

                default:
                    throw new ArgumentException();
            }
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
            else
            {
                // Otherwise use a Unix style test path
                return "/tmp/RasPiGpioTest";
            }
        }

        /// <summary>
        /// Runs when an icon is double clicked
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The event arguments</param>
        private void Gpio_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var panel = (Panel)sender;
            if (panel.BackgroundImage == this.switchOffBitmap)
            {
                this.WriteInput(panel.Name, true);
                panel.BackgroundImage = this.switchOnBitmap;
            }
            else if (panel.BackgroundImage == this.switchOnBitmap)
            {
                this.WriteInput(panel.Name, false);
                panel.BackgroundImage = this.switchOffBitmap;
            }
        }

        /// <summary>
        /// Writes an output to the simulated GPIO
        /// </summary>
        /// <param name="gpioId">The GPIO id</param>
        /// <param name="value">The value to write</param>
        private void WriteInput(string gpioId, bool value)
        {
            var filePath = Path.Combine(gpioId.ToLowerInvariant(), "value");
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
        }

        /// <summary>
        /// Changes the internal state of a GPIO channel
        /// </summary>
        /// <param name="id">The GPIO ID</param>
        /// <param name="state">The state to change to</param>
        private void ChangeState(string id, GpioState state)
        {
            if (this.chanelStates.ContainsKey(id))
            {
                this.chanelStates[id] = state;
            }
            else
            {
                this.chanelStates.Add(id, state);
            }
        }

        private void LblAboutLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new FrmAbout().ShowDialog();
        }
    }
}
