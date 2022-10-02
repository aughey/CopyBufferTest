using System;
using System.Threading;
using System.Threading.Tasks;

class StaHelper
{
    readonly SemaphoreSlim done = new(0);
    readonly Action Work;

    StaHelper(Action work)
    {
        Work = work;
    }

    public static async Task Run(Action work)
    {
        var helper = new StaHelper(work);
        helper.Go();
        await helper.done.WaitAsync();
    }

    public void Go()
    {
        var thread = new Thread(new ThreadStart(DoWork))
        {
            IsBackground = true,
        };
#pragma warning disable CA1416
        thread.SetApartmentState(ApartmentState.STA);
#pragma warning restore CA1416

        thread.Start();
    }

    // Thread entry method
    private void DoWork()
    {
        try
        {
            Work();
        }
        catch (Exception ex)
        {
            // ex from first exception
            System.Console.WriteLine(ex);
        }
        finally
        {
            done.Release();
        }
    }


}
