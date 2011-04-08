using System.Threading;

namespace PasovaVyroba
{
    public class WorkplaceThread
    {
        private Workplace _workplace;
        private Thread thread;

        public Workplace Workplace
        {
            get { return _workplace; }
        }
        public WorkplaceThread(Workplace workplace)
        {
            _workplace = workplace;
        }
        private void InnerRun()
        {
            // Actually, nothing should be done...
            do
            {
                Workplace.FireUpdate();
                Thread.Sleep(100);
            } while (Workplace.Progress < 1);
            Workplace.FireUpdate();
            Workplace.FireFinished();
        }
        public void Run()
        {
            if (thread != null) thread.Abort();
            thread = new Thread(new ParameterizedThreadStart(
                (self) => { ((WorkplaceThread)self).InnerRun(); }
                    ));
            thread.Start(this);
        }
        public void Stop()
        {
            thread.Abort();
        }
    };
};