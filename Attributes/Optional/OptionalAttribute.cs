using System;
using UnityEngine;

namespace EditorKit
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class OptionalAttribute : PropertyAttribute
    {
        public enum Format
        {
            Inline,
            NewLine
        }

        public string boolPath = String.Empty;
        public Format type = Format.Inline;

        /// <summary>
        /// Conditional boolean value managed by the editor. Defaults to false.
        /// 
        /// The editor interprets the boolean based on whether the property is null.
        /// 
        /// <example><code>
        /// [Optional]
        /// public float? data;  // values must be nullable to interpret conditional state
        /// </code></example>
        /// 
        /// Note: Property under the attribute MUST BE AN OBJECT REFERENCE.
        /// </summary>
        /// <param name="type">Whether attributed property should be below the optional property or inline.</param>
        public OptionalAttribute(Format type = Format.Inline, bool @default = false)
        {
            this.type = type;
        }

        /// <summary>
        /// Conditional boolean value held by script and referenced by editor.
        /// 
        /// The editor uses the referenced boolean to control the conditional.
        /// </summary>
        /// <param name="boolPath">Path to boolean.</param>
        /// <param name="displayOnNewLine">Whether attributed property should be below the conditional or inline.</param>
        public OptionalAttribute(string boolPath, Format type = Format.Inline)
        {
            this.boolPath = boolPath;
            this.type = type;
        }
    }
}
