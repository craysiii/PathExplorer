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
        
        public bool isValid()
        {
            return Directory.Exists(this.Value);
        }
    }
}