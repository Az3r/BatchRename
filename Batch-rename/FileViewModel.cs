using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
namespace BatchRename
{
    public class FileViewModel : ICloneable
    {
        public FileViewModel(FileInfo iFile)
        {
            file = iFile;
            FileHandler += (FileViewModel target) => 
            {
                string newPath = Path.Combine(target.file.Directory.FullName, "Abc");
                target.file = new FileInfo(newPath);
                return target;
            };
        }

        public bool Exists => file.Exists;
        public string Name => file.Name;
        public string AbsPath => file.FullName;
        public string PreviewNewName
        {
            get
            {
                FileViewModel preview = Clone() as FileViewModel;
                return FileHandler(preview).Name;
            }
        }
        public string Description
        {
            get
            {
                if (Exists) return string.Empty;
                return "File not found";
            }
        }
        public Brush NameBackground
        {
            get
            {
                if (Exists)
                {
                   return new LinearGradientBrush(Colors.LightGreen, Colors.White, new Point(1, 0), new Point(0, 0));
                }
                else 
                {
                    return new LinearGradientBrush(Colors.Red, Colors.White, new Point(1, 0), new Point(0, 0));
                }
            }
        }

        public object Clone()
        {
            FileInfo copyFile = new FileInfo(file.FullName);
            FileViewModel copycat = new FileViewModel(copyFile);
            return copycat;
        }

        public delegate FileViewModel FileDelegate(FileViewModel target);
        public FileDelegate FileHandler;

        private FileInfo file;


    }
}
