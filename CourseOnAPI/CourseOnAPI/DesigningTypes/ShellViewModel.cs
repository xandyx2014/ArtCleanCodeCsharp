using System;

namespace DesigningTypes {
    public class ShellViewModel {
        public void Handle(ChangePageMessage message) {            
            SetMode(message);
            
            SimulateUserActivity();
        }

        private static void SimulateUserActivity() {
            UINativeMethods.SimulateMouseMove(1, 1);
            UINativeMethods.SimulateMouseMove(0, 0);
        }

        private void SetMode(ChangePageMessage message) {
            if (message.ViewModelType == typeof (MainServiceMenuViewModel)) {
                Mode = Mode.Service;
            }
            if (message.ViewModelType == typeof (MainScreen)) {
                Mode = Mode.WorkIdle;
            }
            else if (Mode == Mode.WorkIdle) {
                Mode = Mode.Work;
            }
        }

        public Mode Mode { get; private set; }
    }

    public class MainScreen {}

    public enum Mode {
        Service,
        WorkIdle,
        Work
    }

    public class MainServiceMenuViewModel {}

    public class UINativeMethods {
        public static void SimulateMouseMove(int x, int y) {
            throw new NotImplementedException();
        }
    }

    public class ChangePageMessage {
        public readonly Type ViewModelType;

        public ChangePageMessage(Type viewModelType) {
            ViewModelType = viewModelType;
        }
    }
}