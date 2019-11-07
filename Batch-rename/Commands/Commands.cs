using System.Windows.Input;

namespace BatchRename.Commands
{
    public sealed class Commands
    {
        static public RoutedCommand AddFiles { get; private set; } = new RoutedCommand();
        static public RoutedCommand AddFunctions { get; private set; } = new RoutedCommand();
        static public RoutedCommand DeleteFiles { get; private set; } = new RoutedCommand();
        static public RoutedCommand DeleteFunctions { get; private set; } = new RoutedCommand();
        static public RoutedCommand ExecuteSelectedFiles { get; private set; } = new RoutedCommand();
        static public RoutedCommand ExecuteAllFiles { get; private set; } = new RoutedCommand();
        static public RoutedCommand ExpandControl { get; private set; } = new RoutedCommand();
        static public RoutedCommand Create { get; private set; } = new RoutedCommand();
        static public RoutedCommand Refresh { get; private set; } = new RoutedCommand();
    }
}

