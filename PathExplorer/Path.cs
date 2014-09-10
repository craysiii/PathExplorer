using System.ComponentModel;
using System.IO;

namespace PathExplorer
{
    public class Path
    {
        public Path(string value)
        {
            Value = value;
        }

        [DisplayName("Path Variable")]
        public string Value
        {
            get;
            set;
        }
        
        public static bool isValid(string value)
        {
            return Directory.Exists(value);
        }
    }
}