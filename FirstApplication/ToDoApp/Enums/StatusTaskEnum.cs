using System.ComponentModel;

namespace ToDoApp.Enums
{
    public enum StatusTaskEnum
    {
        [Description("Pending")]
        Pending = 1,
        [Description("In Progres")]
        InProgress = 2,
        [Description("Finished")]
        Finished = 3
    }
}
