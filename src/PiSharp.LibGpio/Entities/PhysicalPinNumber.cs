//-----------------------------------------------------------------------
// <copyright file="PhysicalPinNumber.cs" company="Andrew Bradford">
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

namespace PiSharp.LibGpio.Entities
{
    /// <summary>
    /// The physical GPIO pin ID
    /// </summary>
    public enum PhysicalPinNumber
    {
        /// <summary>
        /// Invalid pin number - used to established a known invalid value to un-initialised variables.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// GPIO Pin 3
        /// </summary>
        Three = 3,

        /// <summary>
        /// GPIO Pin 5
        /// </summary>
        Five = 5,

        /// <summary>
        /// GPIO Pin 7
        /// </summary>
        Seven = 7,

        /// <summary>
        /// GPIO Pin 8
        /// </summary>
        Eight = 8,

        /// <summary>
        /// GPIO Pin 10
        /// </summary>
        Ten = 10,

        /// <summary>
        /// GPIO Pin 11
        /// </summary>
        Eleven = 11,

        /// <summary>
        /// GPIO Pin 12
        /// </summary>
        Twelve = 12,

        /// <summary>
        /// GPIO Pin 13
        /// </summary>
        Thirteen = 13,

        /// <summary>
        /// GPIO Pin 15
        /// </summary>
        Fifteen = 15,

        /// <summary>
        /// GPIO Pin 16
        /// </summary>
        Sixteen = 16,

        /// <summary>
        /// GPIO Pin 18
        /// </summary>
        Eighteen = 18,

        /// <summary>
        /// GPIO Pin 19
        /// </summary>
        Nineteen = 19,

        /// <summary>
        /// GPIO Pin 21
        /// </summary>
        TwentyOne = 21,

        /// <summary>
        /// GPIO Pin 22
        /// </summary>
        TwentyTwo = 22,

        /// <summary>
        /// GPIO Pin 23
        /// </summary>
        TwentyThree = 23,

        /// <summary>
        /// GPIO Pin 24
        /// </summary>
        TwentyFour = 24,

        /// <summary>
        /// GPIO Pin 26
        /// </summary>
        TwentySix = 26,

        /// <summary>
        /// GPIO Pin 29 (B+ and above)
        /// </summary>
        TwentyNine = 29,

        /// <summary>
        /// GPIO Pin 31 (B+ and above)
        /// </summary>
        ThirtyOne = 31,

        /// <summary>
        /// GPIO Pin 32 (B+ and above)
        /// </summary>
        ThirtyTwo = 32,

        /// <summary>
        /// GPIO Pin 33 (B+ and above)
        /// </summary>
        ThirtyThree = 33,

        /// <summary>
        /// GPIO Pin 35 (B+ and above)
        /// </summary>
        ThirtyFive = 35,

        /// <summary>
        /// GPIO Pin 36 (B+ and above)
        /// </summary>
        ThirtySix = 36,

        /// <summary>
        /// GPIO Pin 37 (B+ and above)
        /// </summary>
        ThirtySeven = 37,

        /// <summary>
        /// GPIO Pin 38 (B+ and above)
        /// </summary>
        ThirtyEight = 38,

        /// <summary>
        /// GPIO Pin 40 (B+ and above)
        /// </summary>
        Fourty = 40

    }
}
