using System.ComponentModel;
using Microsoft.Win32;

namespace PathExplorer
{
    public class PathExplorer
    {
        private readonly string PathKey = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment";
        BindingList<Path> paths = new BindingList<Path>();

        public PathExplorer()
        {
            string temp = (string)Registry.GetValue(PathKey, "Path", null);
            var list = temp.Split(';');
            foreach (string item in list)
            {
                this.addPath(item);
            }

        }

        public BindingList<Path> Paths
        {
            get
            {
                return this.paths;
            }
        }

        public void addPath(string value)
        {
            var path = new Path(value);
            this.paths.Add(path);

        }

        public void deletePath(int index)
        {
            this.paths.RemoveAt(index);
        }

        public void commitPath()
        {
            string absolutePath = "";
            foreach (Path path in this.paths)
            {
                absolutePath += (path.Value + ';');
            }

            Registry.SetValue(PathKey, "Path", absolutePath.Substring(0, absolutePath.Length - 1));
        }
    }
}