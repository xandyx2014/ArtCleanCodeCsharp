using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace DesigningTypes.DisposalAndFinalization {
    /// <summary>
    /// Нарушение SRP - управление ресурсами и логика.
    /// Обернуть IntPtr
    /// </summary>
    public class ResourceHolder : IDisposable {
        private readonly IntPtr _unmanagedResource;
        private readonly SafeHandle _managedResource;

        public ResourceHolder() {
            _unmanagedResource = Marshal.AllocHGlobal(sizeof (int));
            _managedResource = new SafeFileHandle(new IntPtr(), true);
        }

        /// <summary>
        /// tempral coupling between calls
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isManualDisposing) {
            ReleaseUnmanagedResourse(_unmanagedResource);
            if (isManualDisposing) {
                ReleaseManagedResources(_managedResource);
            }
        }

        private void ReleaseManagedResources(SafeHandle safeHandle) {
            safeHandle?.Dispose();
        }

        private void ReleaseUnmanagedResourse(IntPtr intPtr) {
            Marshal.FreeHGlobal(intPtr);
        }

        ~ResourceHolder() {
            Dispose(false);
        }
    }
}