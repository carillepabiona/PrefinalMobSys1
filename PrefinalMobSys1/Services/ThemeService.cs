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
        public string CurrentTheme { get; private set; } = "light";
        public event Action<string> OnThemeChanged;

        public void SetTheme(string theme)
        {
            CurrentTheme = theme;
            OnThemeChanged?.Invoke(theme);
        }
    }

}
