using System;
using System.Linq;

namespace Okuma.PanelMode.Common
{
    /// <summary>Extension methods</summary>
    public static class ExtensionMethods
    {
        /// <summary>Translates one enum to another by attempting to match names.</summary>
        /// <typeparam name="TIn">The input enum type</typeparam>
        /// <typeparam name="TOut">The output enum type</typeparam>
        /// <param name="in">The input enum value</param>
        /// <returns>The input enum value, converted to the output enum type based on name.</returns>
        public static TOut Translate<TIn, TOut>(this TIn @in) 
            where TIn: System.Enum
            where TOut : System.Enum
        {
            var inName = @in.ToString();
            try
            {
                return (TOut)System.Enum.Parse(typeof(TOut), inName);
            }
            catch
            {
                throw new ArgumentException($"Enum type {typeof(TOut).FullName} does not contain a definition for {inName}");
            }
        }
    }
}
