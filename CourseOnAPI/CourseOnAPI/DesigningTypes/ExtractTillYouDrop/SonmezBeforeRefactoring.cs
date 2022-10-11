using System;

namespace DesigningTypes.ExtractTillYouDrop {
    public class PathHelpers {
        public static void SplitDirectoryFile(string path, 
                                              out string directory, out string file) {
            directory = null;
            file = null;

            // assumes a validated full path
            if (path != null) {
                int length = path.Length;
                int rootLength = GetRootLength(path);

                // ignore a trailing slash
                if (length > rootLength && EndsInDirectorySeparator(path)) {
                    length--;
                }

                // find the pivot index between end of string and root
                for (int pivot = length - 1; pivot >= rootLength; pivot--) {
                    if (IsDirectorySeparator(path[pivot])) {
                        directory = path.Substring(0, pivot);
                        file = path.Substring(pivot + 1, length - pivot - 1);
                        return;
                    }
                }

                // no pivot, return just the trimmed directory
                directory = path.Substring(0, length);
            }
        }

        #region Some irrelevant from the learning point of view Helper Methods
        private static bool IsDirectorySeparator(char c) {
            throw new NotImplementedException();
        }

        private static bool EndsInDirectorySeparator(string path) {
            throw new NotImplementedException();
        }

        private static int GetRootLength(string path) {
            throw new NotImplementedException();
        }
        #endregion
    }
}