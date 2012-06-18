namespace LibGpioDemo2
{
    using System.Threading;

    // Pi# Namespaces
    using PiSharp.LibGpio;
    using PiSharp.LibGpio.Entities;

    class Program
    {
        static void Main(string[] args)
        {
            // Use test mode
            LibGpio.Gpio.TestMode = true;

            // Setup one pin for input and one for output (using Raspberry Pi numbers)
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Seven, Direction.Input);
            LibGpio.Gpio.SetupChannel(RaspberryPinNumber.Zero, Direction.Output);

            // Loop around and set the value of GPIO 0 to the inverse of GPIO 7
            // ie. If the button is pressed, the LED will go out
            var oldInput = true;
            while (true)
            {
                var newInput = LibGpio.Gpio.ReadValue(RaspberryPinNumber.Seven);
                if (newInput != oldInput)
                {
                    LibGpio.Gpio.OutputValue(BroadcomPinNumber.Seventeen, !newInput);
                    oldInput = newInput;
                }
            }
        }
    }
}
