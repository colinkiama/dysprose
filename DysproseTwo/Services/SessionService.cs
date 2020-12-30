using DysproseTwo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.Services
{
    public class SessionService
    {
        // Singleton Pattern with "Lazy"
        private static readonly Lazy<SessionService> lazy =
            new Lazy<SessionService>(() => new SessionService());

        public static SessionService Instance => lazy.Value;

        private SessionService() { }

        public event EventHandler<DysproseSessionState> SessionStateChanged;

        public void UpdateSessionState(DysproseSessionState newState)
        {
            SessionStateChanged?.Invoke(this, newState);
        }
    }
}
