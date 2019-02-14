namespace Microsoft.IoT.ServiceFabric.Common
{
    using System;
    using System.Diagnostics;

    public class MeasureTime : IDisposable
    {
        private Stopwatch sw;
        public MeasureTime()
        {
            sw = Stopwatch.StartNew();
        }

        public TimeSpan Elapsed
        {
            get
            {
                return sw.Elapsed;
            }
        }

        public void Dispose()
        {
            if(sw != null && sw.IsRunning)
            {
                sw.Stop();
            }
        }
    }
}
