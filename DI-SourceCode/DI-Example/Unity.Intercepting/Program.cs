using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Unity.Intercepting
{
    public class LoggingWrapper : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input,
            GetNextInterceptionBehaviorDelegate getNext)
        {
            // Before invoking the method on the original target.
            WriteLog($"Invoking method {input.MethodBase} at {DateTime.UtcNow}");

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            WriteLog(result.Exception != null
                ? $"Method {input.MethodBase} threw exception {result.Exception.Message} at {DateTime.Now.ToLongTimeString()}"
                : $"Method {input.MethodBase} returned {result.ReturnValue} at {DateTime.Now.ToLongTimeString()}");

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute => true;

        private void WriteLog(string message)
        {
            Console.WriteLine(message);
        }
    }
    public interface IDevice
    {
        void Open();
        void SendCommand(int code);
        void Close();
    }

    public class Device : IDevice
    {
        private readonly StreamWriter _serialPort;

        public Device()
        {
            _serialPort = new StreamWriter("PortStub.txt");
        }

        public void Open()
        {
            SendCommand(0x16);
        }

        public void SendCommand(int code)
        {
            _serialPort.Write($"Eat the command {code}!");
        }

        public void Close()
        {
            SendCommand(0x32);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container.RegisterType<IDevice, Device>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingWrapper>());

            var device = container.Resolve<IDevice>();
            device.Open();
            device.Close();

            Console.ReadLine();
        }
    }
}
