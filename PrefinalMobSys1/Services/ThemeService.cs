using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace PrefinalMobSys1.Services
{
    public class ThemeService
    {
        private const string ThemeKey = "app_theme";
        private const string DefaultTheme = "light";

        public string CurrentTheme { get; private set; }

        public event Action<string> OnThemeChanged;

        public ThemeService()
        {
            // Load saved theme or default
            CurrentTheme = Preferences.Get(ThemeKey, DefaultTheme);
        }

        public void SetTheme(string theme)
        {
            if (CurrentTheme != theme)
            {
                CurrentTheme = theme;
                Preferences.Set(ThemeKey, theme);
                OnThemeChanged?.Invoke(theme);
            }
        }
    }

}
