using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaNgi
{
    public struct Theme
    {
        public string Name { get; set; }
        public Color GradientStart { get; set; }
        public Color GradientEnd { get; set; }
        public Color AccentColor { get; set; }
        public Color TextColor { get; set; }
        public Color SubtleColor { get; set; }

        public Theme(string name, Color gradStart, Color gradEnd, Color accent, Color text, Color subtle)
        {
            Name = name;
            GradientStart = gradStart;
            GradientEnd = gradEnd;
            AccentColor = accent;
            TextColor = text;
            SubtleColor = subtle;
        }
    }

    public static class ThemeManager
    {
        private static readonly List<Theme> PredefinedThemes = new List<Theme>
        {
            new Theme("HanGi",
                Color.FromArgb(44, 44, 84),    
                Color.FromArgb(25, 25, 56), 
                Color.FromArgb(255, 0, 127),
                Color.FromArgb(240, 240, 240),
                Color.FromArgb(58, 58, 90)), 

            new Theme("Sky",
                Color.FromArgb(78, 142, 221),  
                Color.FromArgb(29, 68, 115),   
                Color.FromArgb(255, 193, 7),    
                Color.White,                  
                Color.FromArgb(49, 112, 185)), 

            new Theme("Sunset",
                Color.FromArgb(243, 156, 18), 
                Color.FromArgb(211, 84, 0),    
                Color.FromArgb(255, 235, 59), 
                Color.White,                  
                Color.FromArgb(230, 126, 34))  
        };

        public static Theme GetTheme(string themeName)
        {
            var theme = PredefinedThemes.FirstOrDefault(t => t.Name == themeName);
            if (theme.Name == null) // Nếu không tìm thấy
            {
                return PredefinedThemes.First(t => t.Name == "HanGi");
            }
            return theme;
        }

        public static List<string> GetThemeNames()
        {
            return PredefinedThemes.Select(t => t.Name).ToList();
        }
    }
}
