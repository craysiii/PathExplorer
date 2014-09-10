using System.ComponentModel;
using System.IO;

namespace PathExplorer
{
    public class Path
    {
        private string _value;

        public Path(string value)
        {
            Value = value;
        }

        [DisplayName("Path Variable")]
        public string Value
        {
            get { return _value; }
            set
            {
                if (File.Exists(value) || Directory.Exists(value))
                {
                    this._value = value;
                }

                else
                {
                    throw new FileNotFoundException(@value + "does not exist.");
                }
            }
        }
    }
}