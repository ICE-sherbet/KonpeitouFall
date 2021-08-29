using System;

public static class Animator
{
    public static void Animate(int interval, float frequency, Func<int, float, bool> callback,Action complate = null)
    {
        var timer = new System.Windows.Forms.Timer();
        timer.Interval = interval;
        int frame = 0;
        timer.Tick += (sender, e) =>
        {
            if (callback(frame, frequency) == false || frame >= frequency)
            {
                complate?.Invoke();
                timer.Stop();
            }
            frame++;
        };
        timer.Start();
    }

    public static void Animate(float duration, Func<int, float, bool> callback, Action complate = null)
    {
        const float interval = 10;
        if (duration < interval) duration = interval;
        Animate((int)interval, duration / interval, callback,complate);
    }
}
