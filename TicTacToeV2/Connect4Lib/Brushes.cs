using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Connect4Lib
{
    public class Brushes
    {
        // brushes for the checker colours
        private SolidColorBrush yellow = new SolidColorBrush(Color.FromArgb(255, 246, 183, 40));
        private SolidColorBrush red = new SolidColorBrush(Color.FromArgb(255, 229, 43, 58));

        // brushes for the winning checker colours
        private SolidColorBrush winningYellow = new SolidColorBrush(Color.FromArgb(255, 255, 246, 101));
        private SolidColorBrush winningRed = new SolidColorBrush(Color.FromArgb(255, 254, 73, 107));

        // brush for neutral/grey text
        private SolidColorBrush grey = new SolidColorBrush(Color.FromArgb(255, 96, 90, 87));

        public SolidColorBrush getYellow() { return yellow; }

        public SolidColorBrush getRed() { return red; }

        public SolidColorBrush getGrey() { return grey; }

        public SolidColorBrush getWinningYellow() { return winningYellow; }

        public SolidColorBrush getWinningRed() { return winningRed; }
    }
}
