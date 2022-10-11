using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DesigningTypes {
public class InvalidUserException : Exception, ISerializable {
    public InvalidUserException() {}
    public InvalidUserException(string message) : base(message) {}
    public InvalidUserException(string message, Exception inner) : base(message, inner) {}

    //for serialization purposes.
    protected InvalidUserException(SerializationInfo info, StreamingContext context) {
        
    }
}
}