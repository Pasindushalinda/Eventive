using System.Reflection;
namespace Eventive.Modules.Events.Application;

//The purpose of this code is likely to provide a way to access 
//the assembly that contains the AssemblyReference class from other 
//parts of the application.This can be useful in scenarios where 
//you need to dynamically load or inspect the assembly, 
//such as in plugin architectures or dependency injection systems.
public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
