using System;
using System.Runtime.InteropServices;

namespace EditorKit
{
    [Serializable]
    //[StructLayout(LayoutKind.Sequential)]
    public struct Vector3Bool : IEquatable<Vector3Bool>
    {
        /// <summary>X component.</summary>
        public bool x;
        /// <summary>Y component.</summary>
        public bool y;
        /// <summary>Z component.</summary>
        public bool z;

        /// <summary>Name for `x` component.</summary>
        public string xName;
        /// <summary>Name for `y` component.</summary>
        public string yName;
        /// <summary>Name for `z` component.</summary>
        public string zName;
        
        /// <summary>
        /// Shorthand for `new Vector3Bool(true, true, true)`.
        /// </summary>
        public static readonly Vector3Bool @true = new Vector3Bool(true, true, true);
        /// <summary>
        /// Shorthand for `new Vector3Bool(false, false, false)`.
        /// </summary>
        public static readonly Vector3Bool @false = new Vector3Bool(false, false, false);
        
        /// <summary>
        /// Access the x, y, z components using [0], [1], [2] respectively.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns></returns>
        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3Bool index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3Bool index");
                }
            }
        }

        #region Constructors
        /// <summary>
        /// Creates a new `Vector3Bool` with given x, y, and z boolean 
        /// components. `z` component defaults to false.
        /// </summary>
        /// <param name="x">X component to set.</param>
        /// <param name="y">Y component to set.</param>
        /// <param name="z">Z component to set.</param>
        public Vector3Bool(bool x, bool y, bool z = false)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.xName = String.Empty;
            this.yName = String.Empty;
            this.zName = String.Empty;
        }

        /// <summary>
        /// Creates a new `Vector3Bool` with given x, y, and z names. 
        /// Boolean values default to false.
        /// </summary>
        /// <param name="x">X component to set.</param>
        /// <param name="y">Y component to set.</param>
        /// <param name="z">Z component to set.</param>
        public Vector3Bool(string xName, string yName, string zName)
        {
            this.x = false;
            this.y = false;
            this.z = false;
            this.xName = xName;
            this.yName = yName;
            this.zName = zName;
        }

        /// <summary>
        /// Creates a new `Vector3Bool` with given x, y, and z boolean 
        /// components assigned with given names.
        /// </summary>
        /// <param name="xName">Name of x component.</param>
        /// <param name="x">X component to set.</param>
        /// <param name="yName">Name of y component.</param>
        /// <param name="y">Y component to set.</param>
        /// <param name="zName">Name of z component.</param>
        /// <param name="z">Z component to set.</param>
        public Vector3Bool(string xName, bool x, string yName, bool y, string zName, bool z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.xName = xName;
            this.yName = yName;
            this.zName = zName;
        }

        /// <summary>
        /// Creates a new `Vector3Bool` with the first, second, and third bits 
        /// of the given byte. Note: The forth bit is ignored and names are left
        /// empty.
        /// </summary>
        /// <param name="xyz">Byte containing the bit values.</param>
        public Vector3Bool(byte xyz)
        {
            this.x = (xyz & 0x1) == 0x1;
            this.y = (xyz & 0x2) == 0x2;
            this.z = (xyz & 0x4) == 0x4;
            this.xName = String.Empty;
            this.yName = String.Empty;
            this.zName = String.Empty;
        }
        #endregion

        /// <summary>
        /// Sets x, y, and z boolean components of a an existing `Vector3Bool`. `z` 
        /// component defaults to false.
        /// </summary>
        /// <param name="x">X component to set.</param>
        /// <param name="y">Y component to set.</param>
        /// <param name="z">Z component to set.</param>
        public void Set(bool newX, bool newY, bool newZ = false)
        {
            x = newX;
            y = newY;
            z = newZ;
        }

        #region Operators
        /// <summary>
        /// Creates a `Vector3Bool` with complemented/toggled components from the given `Vector3Bool`.
        /// </summary>
        /// <param name="value">The operand.</param>
        /// <returns>The complemented `Vector3Bool`.</returns>
        public static Vector3Bool operator!(Vector3Bool value)
        {
            return value.toggled;
        }

        /// <summary>
        /// Creates a `Vector3Bool` with complemented/toggled components from the given `Vector3Bool`.
        /// </summary>
        /// <param name="value">The operand.</param>
        /// <returns>The complemented `Vector3Bool`.</returns>
        public static Vector3Bool operator~(Vector3Bool value)
        {
            return value.toggled;
        }

        /// <summary>
        /// Creates a `Vector3Bool` with corresponding components `OR`ed together from the given `Vector3Bool`s.
        /// </summary>
        /// <param name="value1">First operand.</param>
        /// <param name="value2">Second operand.</param>
        /// <returns>`Vector3Bool` with corresponding components `OR`ed together</returns>
        public static Vector3Bool operator|(Vector3Bool value1, Vector3Bool value2)
        {
            return new Vector3Bool(value1.x | value2.x,
                                   value1.y | value2.y,
                                   value1.z | value2.z);
        }

        /// <summary>
        /// Creates a `Vector3Bool` with corresponding components `AND`ed together from the given `Vector3Bool`s.
        /// </summary>
        /// <param name="value1">First operand.</param>
        /// <param name="value2">Second operand.</param>
        /// <returns>`Vector3Bool` with corresponding components `AND`ed together</returns>
        public static Vector3Bool operator &(Vector3Bool value1, Vector3Bool value2)
        {
            return new Vector3Bool(value1.x & value2.x,
                                   value1.y & value2.y,
                                   value1.z & value2.z);
        }

        /// <summary>
        /// Creates a `Vector3Bool` with corresponding components `XOR`ed together from the given `Vector3Bool`s.
        /// </summary>
        /// <param name="value1">First operand.</param>
        /// <param name="value2">Second operand.</param>
        /// <returns>`Vector3Bool` with corresponding components `XOR`ed together</returns>
        public static Vector3Bool operator ^(Vector3Bool value1, Vector3Bool value2)
        {
            return new Vector3Bool(value1.x ^ value2.x,
                                   value1.y ^ value2.y,
                                   value1.z ^ value2.z);
        }
        #endregion

        /// <summary>
        /// Toggles/complements all boolean components.
        /// </summary>
        public void Toggle()
        {
            Set(!x, !y, !z);
        }

        /// <summary>
        /// Returns 
        /// </summary>
        public Vector3Bool toggled
        {
            get { return new Vector3Bool(!x, !y, !z); }
        }

        public bool AllTrue()
        {
            return x && y && z;
        }

        public bool AllFalse()
        {
            return !AllTrue();
        }

        public bool Equals(Vector3Bool other)
        {
            return this.x == other.x &&
                   this.y == other.y &&
                   this.z == other.z;
        }

        public override string ToString()
        {
            return $"{xName}: {x}, {yName}: {y}, {zName}: {z}";
        }
    }
}
