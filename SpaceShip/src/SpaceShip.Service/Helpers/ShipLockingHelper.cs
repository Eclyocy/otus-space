namespace SpaceShip.Service.Helpers;

public static class ShipLockingHelper
{
    public static bool WaitLockShip(Guid shipId)
    {
        Mutex? mutex = null;
        if (!Mutex.TryOpenExisting(shipId.ToString(), out mutex))
        {
            mutex = new Mutex(false, shipId.ToString());
        }

        mutex.WaitOne(0, false);
        return true;
    }

    public static void ReleaseShip(Guid shipId)
    {
        Mutex? mutex = null;
        if (Mutex.TryOpenExisting(shipId.ToString(), out mutex))
        {
            mutex.ReleaseMutex();
        }
    }
}
