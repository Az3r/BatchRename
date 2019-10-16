using System.Threading.Tasks;
namespace BatchRename
{
    public class FileViewModel
    {
        public FileViewModel(SimpleFile file)
        {
            source = file;
        }

        public static FileViewModel[] CreateArray(SimpleFile[] collection)
        {
            FileViewModel[] array = new FileViewModel[collection.Length];
            for (int i = 0; i < collection.Length; ++i)
                array[i] = new FileViewModel(collection[i]);
            return array;
        }

        public string TxtOldName => source.Name;
        public string TxtNewName
        {
            get
            {
                SimpleFile preview = new SimpleFile(source.FullPath);
                return source?.Action?.Execute(preview).Result.Name;
            }
        }

        private readonly SimpleFile source;
    }
}
