using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Activities;

public sealed class LongRunningActivity : CodeActivity
{

    // Define an activity input argument of type String
    public OutArgument<string> Ret { get; set; }

    // If your activity returns a value, derive from CodeActivity(Of TResult)
    // and return the value from the Execute method.
    protected override void Execute(CodeActivityContext context)
    {
        // Obtain the runtime value of the Text input argument
        // Dim client As New ServiceCalculate.CalculateClient
        // Dim str = client.Calculate(New ServiceCalculate.Calculate With {.parameter1 = 1})
        string str = "Result is: ";
        for (int i = 0; i <= 120; i++)
        {
            System.Threading.Thread.Sleep(1000);
            CommonWorkflow.WfLongRunningCount = i;
        }
        this.Ret.Set(context, str);
    }
}
