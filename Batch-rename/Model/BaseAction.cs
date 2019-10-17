namespace BatchRename.Model
{
    /// <summary>
    /// Apply action on specific <seealso cref="BatchFile"/> instance, does not change the instance itself
    /// </summary>
    public class BaseAction
    {
        public BaseAction() 
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
        public PathInfo Execute(PathInfo target)
        {
            PathInfo result = target.Clone();
            ActionHandler?.Invoke(result);
            return result;
        }
        public delegate void ActionDelegate(PathInfo target);
        public ActionDelegate ActionHandler { get; set; }
        public string Name { get; set; }
    }

}
