namespace BatchRename.Models
{
    public sealed class FunctionReplace : BatchFunction
    {
        public FunctionReplace()
        {
            Name = "Replace";
        }

        public override BatchFunction Clone()
        {
            return new FunctionReplace()
            {
                args = new string[] { OldValue, NewValue },
                Name = Name,
                IsFavorite = IsFavorite
            };
        }
        public override string GetString(string src)
        {
            return src.Replace(OldValue, NewValue);
        }
        public string OldValue
        {
            get => args[0];
            set
            {
                args[0] = value;
                NotifyPropertyChanged();
            }
        }
        public string NewValue
        {
            get => args[1];
            set
            {
                args[1] = value;
                NotifyPropertyChanged();
            }
        }

        private string[] args = new string[2] { string.Empty, string.Empty };
    }
}
