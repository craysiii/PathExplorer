using System.ComponentModel;
using System.IO;

namespace PathExplorer
{
    public class Path
    {
        private string value;

        [DisplayName("Path Variable")]
        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (File.Exists(value) || Directory.Exists(value))
                {
                    this.value = value;
                }

                else
                {
                    throw new FileNotFoundException(@value + "does not exist.");
                }
            }
        }

        public Path(string value)
        {
            this.Value = value;
        }

    }
}