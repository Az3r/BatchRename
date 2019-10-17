using System;
using System.Text;
namespace BatchRename.Model
{
    public class ChangeCaseAction : BaseAction
    {
        public enum CaseType
        {
            AllUpper = 0, 
            AllLower = 1, 
            Capitalize = 2
        }
        public ChangeCaseAction() : base()
        {
            Name = "Change Case";
            ActionHandler = new ActionDelegate(ChangeCase);
        }

        private void ChangeCase(PathInfo target)
        {
            
            PathInfo result = target.Clone(); // preserved target type
            StringBuilder builder;
            string oldName = target.FullName;
            switch (Type)
            {
                case CaseType.AllUpper:
                    builder = new StringBuilder(oldName.ToUpper());
                    break;
                case CaseType.AllLower:
                    builder = new StringBuilder(oldName.ToLower());
                    break;
                case CaseType.Capitalize:
                    builder = new StringBuilder(oldName.ToLower());
                    builder[0] = char.ToUpper(builder[0]);
                    for (int i = 1; i < oldName.Length; ++i)
                    {
                        if (builder[i] == ' ' && i + 1 < oldName.Length)
                            builder[i + 1] = char.ToUpper(builder[i + 1]);
                    }
                    break;
                default:
                    throw new NotSupportedException($"CaseType {Type} is not supported");
            }
            result.FullName = builder.ToString();
        }
        static public string[] GetCaseTypes()
        {
            return Enum.GetNames(typeof(CaseType));
        }
        public CaseType Type { get; set; }
    }
}
