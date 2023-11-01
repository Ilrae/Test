using System;
using System.Configuration;
using System.Diagnostics;

namespace TraceEx
{
    class Program
    {
        private static TraceSource traceSource = new TraceSource("MyTrace");
        static void Main(string[] args)
        {
            // TraceSource 생성
            traceSource.TraceEvent(TraceEventType.Start, 0, "Main Start");
            
            for(int i = 1; i<10; i++)
            {
                Console.WriteLine(i);
                traceSource.TraceInformation("msg#" + i.ToString());
                
            }
            Trace.Listeners.Add(new ConsoleTraceListener());
            Trace.WriteLine("i");
            traceSource.TraceEvent(TraceEventType.Stop, 0, "Main End");
            traceSource.Flush();
        }
    }

}
