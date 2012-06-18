namespace LibGpioDemo1
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

            // Setup all pins for output (using Broadcom numbers)
            LibGpio.Gpio.SetupChannel(BroadcomPinNumber.Four, Direction.Output);
            LibGpio.Gpio.SetupChannel(BroadcomPinNumber.Seventeen, Direction.Output);
            LibGpio.Gpio.SetupChannel(BroadcomPinNumber.Eighteen, Direction.Output);
            LibGpio.Gpio.SetupChannel(BroadcomPinNumber.TwentyOne, Direction.Output);
            LibGpio.Gpio.SetupChannel(BroadcomPinNumber.TwentyTwo, Direction.Output);
            LibGpio.Gpio.SetupChannel(BroadcomPinNumber.TwentyThree, Direction.Output);
            LibGpio.Gpio.SetupChannel(BroadcomPinNumber.TwentyFour, Direction.Output);
            LibGpio.Gpio.SetupChannel(BroadcomPinNumber.TwentyFive, Direction.Output);

            // Loop around doing an LED chase
            while (true)
            {
                // Turn on
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

                // Turn off
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
        }
    }
}
