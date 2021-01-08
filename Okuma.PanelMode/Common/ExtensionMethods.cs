using System;
using System.Linq;

namespace Okuma.PanelMode.Common
{
    public static class ExtensionMethods
    {
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
