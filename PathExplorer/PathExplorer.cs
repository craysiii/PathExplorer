using System.ComponentModel;
using System.Linq;
using Microsoft.Win32;

namespace PathExplorer
{
    public class PathExplorer
    {
        private const string PathKey = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment";

        private readonly BindingList<Path> _paths = new BindingList<Path>();

        public PathExplorer()
        {
            var temp = (string) Registry.GetValue(PathKey, "Path", null);
            temp.Split(';').ToList().ForEach(AddPath);
        }

        public BindingList<Path> Paths
        {
            get { return _paths; }
        }

        public void AddPath(string value)
        {
            var path = new Path(value);
            _paths.Add(path);
        }

        public void DeletePath(int index)
        {
            if ( index >= 0 && index < _paths.Count )
            {
                _paths.RemoveAt(index);
            }    
        }

        public void CommitPath()
        {
            var absolutePath = _paths.Aggregate("", (current, path) => current + (path.Value + ';'));
            Registry.SetValue(PathKey, "Path", absolutePath.Substring(0, absolutePath.Length - 1));
        }
    }
}