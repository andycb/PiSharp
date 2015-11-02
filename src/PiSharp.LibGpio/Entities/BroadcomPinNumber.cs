//-----------------------------------------------------------------------
// <copyright file="BroadcomPinNumber.cs" company="Andrew Bradford">
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
    public partial class LibGpio
    {
        /// <summary>
        /// The Broadcom GPIO ID
        /// </summary>
        public enum BroadcomPinNumber
        {
            /// <summary>
            /// GPIO Pin 0 (B Rev1 only)
            /// </summary>   
            Zero = 0,

            /// <summary>
            /// GPIO Pin 1 (B Rev1 only)
            /// </summary>
            One = 1,

            /// <summary>
            /// GPIO Pin 2 (A/B Rev2 and above)
            /// </summary>
            Two = 2,

            /// <summary>
            /// GPIO Pin 3 (A/B Rev2 and above)
            /// </summary>
            Three = 3,

            /// <summary>
            /// GPIO Pin 4
            /// </summary>
            Four = 4,

            /// <summary>
            /// GPIO Pin 5 (B+ and above)
            /// </summary>
            Five = 5,

            /// <summary>
            /// GPIO Pin 6 (B+ and above)
            /// </summary>
            Six = 6,

            /// <summary>
            /// GPIO Pin 7
            /// </summary>
            Seven = 7,

            /// <summary>
            /// GPIO Pin 8
            /// </summary>
            Eight = 8,

            /// <summary>
            /// GPIO Pin 9
            /// </summary>
            Nine = 9,

            /// <summary>
            /// GPIO Pin 10
            /// </summary>
            Ten = 10,

            /// <summary>
            /// GPIO Pin 11
            /// </summary>
            Eleven = 11,

            /// <summary>
            /// GPIO Pin 12 (B+ and above)
            /// </summary>
            Twelve = 12,

            /// <summary>
            /// GPIO Pin 13 (B+ and above)
            /// </summary>
            Thirteen = 13,

            /// <summary>
            /// GPIO Pin 14
            /// </summary>
            Fourteen = 14,

            /// <summary>
            /// GPIO Pin 15
            /// </summary>
            Fifteen = 15,

            /// <summary>
            /// GPIO Pin 16 (B+ and above)
            /// </summary>
            Sixteen = 16,

            /// <summary>
            /// GPIO Pin 17
            /// </summary>
            Seventeen = 17,

            /// <summary>
            /// GPIO Pin 18
            /// </summary>
            Eighteen = 18,

            /// <summary>
            /// GPIO Pin 19 (B+ and above)
            /// </summary>
            Nineteen = 19,

            /// <summary>
            /// GPIO Pin 20 (B+ and above)
            /// </summary>
            Twenty = 20,

            /// <summary>
            /// GPIO Pin 21 (Rev1)
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
            /// GPIO Pin 25
            /// </summary>
            TwentyFive = 25,

            /// <summary>
            /// GPIO Pin 26 (B+ and above)
            /// </summary>
            TwentySix = 26,

            /// <summary>
            /// GPIO Pin 27 (A/B Rev2 and above)
            /// </summary>
            TwentySeven = 27,

            /// <summary>
            /// Undefined pin number
            /// </summary>
            Undefined = -1
        }
    }
}
