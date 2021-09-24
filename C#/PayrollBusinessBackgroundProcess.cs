using PayrollDAL
using Framework.Data
using System.Collections.Generic
using System.Threading

Namespace PayrollBusiness.BackgroundProcess
{
    public class PayrollBusinessBackgroundProcess
    {
    private WithEvents timer = Timers.Timer;
    private int _interval = 60000; // 1min
    private DateTime _date = DateTime.Now;

    public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
            }
        }


    public void Start()
    {
        timer = new System.Timers.Timer(_interval); // 1min
        timer.Start();
    }

    private void Timer_Tick(object sender, System.Timers.ElapsedEventArgs e)
    {
        ThreadPool.QueueUserWorkItem(new PayrollRepository().CheckAndSendPayslip, DateTime.Now);
    }

}
}
