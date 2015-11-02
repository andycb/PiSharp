namespace PiSharp.LibGpio.Entities
{
    /// <summary>
    /// The Raspberry Pi GPIO ID
    /// </summary>
    public enum GpioLayout
    {
        /// <summary>
        /// Revision numbers : [Beta, 0003]
        /// </summary>
        LayoutOne = 1,

        /// <summary>
        /// Revision numbers : [0004, 000f]
        /// </summary>
        LayoutTwo = 2,

        /// <summary>
        /// Revision numbers : [0010, ...]
        /// </summary>
        LayoutThree = 3
    }
}
