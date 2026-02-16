using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShatteredRealms.Web.Services;

public class AuthStateService
{
    public event Func<Task>? OnAuthStateChanged;

    public void NotifyAuthStateChanged()
    {
        if (OnAuthStateChanged != null)
        {
            foreach (var handler in OnAuthStateChanged.GetInvocationList().Cast<Func<Task>>())
            {
                _ = handler.Invoke();
            }
        }
    }
}