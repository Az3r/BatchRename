using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.Shared
{
    public static class Error
    {
        public const int SUCCESS = 0;
        public const int FILE_EXISTS = 1;
        public const int FILE_NOT_FOUND = 2;
        public const int DESTINATION_FILE_NOT_FOUND = 3;
        public const int FOLDER_EXISTS = -1;
        public const int FOLDER_NOT_FOUND = -2;
        public const int DESTINATION_FOLDER_NOT_FOUND = -3;
        public const int FOLDER_NOT_EMPTY = -10;
        public static string GetMessage(int errCode)
        {
            return $"string message for {errCode}";
        }
        public static bool IsGood(int errCode)
        {
            return errCode == SUCCESS;
        }
    }
}
