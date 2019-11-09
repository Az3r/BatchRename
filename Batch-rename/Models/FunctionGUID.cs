using StringLibrary;
namespace BatchRename.Models
{
    public sealed class FunctionGUID : BatchFunction
    {
        public FunctionGUID()
        {
            Name = "GUID";
        }
        public override string GetString(string src)
        {
            return StringExtension.GetGUID();
        }
        public string GetString()
        {
            return StringExtension.GetGUID();
        }
    }
}
