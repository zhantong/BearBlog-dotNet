using System;
using System.Text.Json;

namespace BearBlog.Models
{
    public static class Events
    {
        public static event EventHandler<RestoreEventArgs> Restore;

        public static void OnRestore(RestoreEventArgs e)
        {
            Restore?.Invoke(null, e);
        }

        public static event EventHandler<NavbarInitEventArgs> NavbarInit;

        public static void OnNavbarInit(NavbarInitEventArgs e)
        {
            NavbarInit?.Invoke(null, e);
        }

        public static void Init()
        {
            Plugins.Article.Models.Events.Register();
        }
    }

    public class RestoreEventArgs : EventArgs
    {
        public JsonElement Root;
    }

    public class NavbarInitEventArgs : EventArgs
    {
        public Navbar Navbar;
    }
}