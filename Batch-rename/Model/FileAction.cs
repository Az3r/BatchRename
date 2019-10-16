namespace BatchRename.Model
{
    /// <summary>
    /// Apply action on specific <seealso cref="BatchFile"/> instance, does not change the instance itself
    /// </summary>
    public class FileAction
    {
        public FileAction() 
        {
            Name = "Do Nothing";
            ActionHandler = new ActionDelegate((target) => { });
        }
        /// <summary>
        /// Perform an action defined in <see cref="ActionHandler"/> on <paramref name="target"/>,
        /// <paramref name="target"/> is guaranteed not being modified
        /// </summary>
        /// <param name="target"></param>
        /// <returns> a new instance after applying action</returns>
        public BatchFile Execute(BatchFile target)
        {
            BatchFile result = target.Clone();
            ActionHandler?.Invoke(result);
            return result;
        }
        public delegate void ActionDelegate(BatchFile target);
        public ActionDelegate ActionHandler { get; set; }
        public string Name { get; set; }
    }

}
