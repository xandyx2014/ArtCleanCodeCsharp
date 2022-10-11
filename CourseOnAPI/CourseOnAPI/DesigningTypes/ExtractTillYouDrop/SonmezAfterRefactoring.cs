using System;

namespace DesigningTypes.ExtractTillYouDrop {
    public class DirectoryFileSplitter {
        private readonly string validatedFullPath;
        private int length;
        private readonly int rootLength;
        private bool pivotFound;
        public string Directory { get; set; }
        public string File { get; set; }

        public DirectoryFileSplitter(string validatedFullPath) {
            this.validatedFullPath = validatedFullPath;
            length = validatedFullPath.Length;
            rootLength = GetRootLength(validatedFullPath);
        }

        public void Split() {
            if (validatedFullPath != null) {
                IgnoreTrailingSlash();

                FindPivotIndexBetweenEndOfStringAndRoot();
                if (!pivotFound) {
                    TrimDirectory();
                }
            }
        }

        private void TrimDirectory() {
            Directory = validatedFullPath.Substring(0, length);
        }

        private void FindPivotIndexBetweenEndOfStringAndRoot() {
            for (int pivot = length - 1; pivot >= rootLength; pivot--) {
                if (IsDirectorySeparator(validatedFullPath[pivot])) {
                    Directory = validatedFullPath.Substring(0, pivot);
                    File = validatedFullPath.Substring(pivot + 1, length - pivot - 1);
                    pivotFound = true;
                }
            }
        }

        private void IgnoreTrailingSlash() {
            if (length > rootLength && EndsInDirectorySeparator(validatedFullPath)) {
                length--;
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