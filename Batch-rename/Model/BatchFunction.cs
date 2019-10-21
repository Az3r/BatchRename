using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BatchRename.Shared;
namespace BatchRename.Model
{
    /// <summary>
    /// base class for <see cref="FunctionChangeCase"/>, <see cref="FunctionReplace"/>
    /// </summary>
    public class BatchFunction : EventNotifier
    {
        public delegate string StringDelegate(string target, params object[] args);
        public void Invoke(BatchPath target)
        {
            StringDelegate[] delegates = (StringDelegate[])Handler.GetInvocationList();
            string result = target.FullName;
            foreach (StringDelegate func in delegates)
            {
                result = func(result, args);
            }
            target.SetFullName(result);
        }
        public void Combine(StringDelegate other)
        {
            Handler = (StringDelegate)Delegate.Combine(Handler, other);
        }
        public void Combine(Func<string, object[], string> other)
        {
            Handler = (StringDelegate)Delegate.Combine(Handler, other);
        }
        public void Combine(BatchFunction function)
        {
            Handler = (StringDelegate)Delegate.Combine(Handler, function.Handler);
        }
        public void Combine(StringDelegate[] delegates)
        {
            Handler = (StringDelegate)Delegate.Combine(delegates);
        }
        public void Combine(Func<string, object[], string>[] delegates)
        {
            Handler = (StringDelegate)Delegate.Combine(delegates);
        }
        public void Combine(BatchFunction[] functions)
        {
            StringDelegate[] delegates = new StringDelegate[functions.Length];
            int i = 0;
            foreach (var function in functions)
                delegates[i++] = function.Handler;
            Handler = (StringDelegate)Delegate.Combine(delegates);
        }
        public void SetName(string name)
        {
            Name = name;
            NotifyPropertyChanged(nameof(Name));
        }
        public string Name { get; protected set; }

        protected object[] args;
        protected StringDelegate Handler = new StringDelegate((s, objs) => s);  // do nothing and return s
    }
}
