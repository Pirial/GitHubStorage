using System;
using System.Windows;
using System.Windows.Controls;

namespace OnlinePlayer.Controls
{
    public class TabStackPanel : StackPanel
    {
        private Size newSize;
        private Size panelRealSize;

        public Size TabSize { get; set; }
        public Size AddTabSize { get; set; }
        public Size AllTabSize { get; set; }
        // Override the default Measure method of Panel 
        protected override Size MeasureOverride(Size availableSize)
        {
            Size panelDesiredSize = new Size();

            // In our example, we just have one child.  
            // Report that our panel requires just the size of its only child. 
            foreach (UIElement child in this.InternalChildren)
            {
                child.Measure(availableSize);
                // Change size and add position 
                switch (Convert.ToInt32((child as Button).Tag))
                {
                    case -1:
                        panelDesiredSize.Width += 20;
                        break;
                    case -2:
                        panelDesiredSize.Width += 25;
                        break;
                    default:
                        panelDesiredSize.Width += child.DesiredSize.Width;
                        break;
                }
            }
            this.newSize.Width = availableSize.Width;
            this.panelRealSize = panelDesiredSize;
            return panelDesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double posX = 0;
            if (this.newSize.Width > this.panelRealSize.Width && (this.newSize.Width - this.AllTabSize.Width - this.TabSize.Height) / (this.Children.Count - 2) >= this.TabSize.Width)//(this.Children[0] as Button).Width >= 220)
            {
                foreach (UIElement child in this.InternalChildren)
                {
                    switch (Convert.ToInt32((child as Button).Tag))
                    {
                        case -1:
                            child.Arrange(new Rect(new Point(posX, -this.TabSize.Height), this.AllTabSize));
                            posX += this.AllTabSize.Width;
                            break;
                        case -2:
                            child.Arrange(new Rect(new Point(posX, -this.TabSize.Height), this.AddTabSize));
                            posX += this.AddTabSize.Width;
                            break;
                        default:
                            child.Arrange(new Rect(new Point(posX, -this.TabSize.Height), this.TabSize));
                            (child as Button).Width = this.TabSize.Width;
                            posX += this.TabSize.Width;
                            break;
                    }
                }
            }
            else
            {
                foreach (UIElement child in this.InternalChildren)
                {
                    // Change size and add position 
                    switch (Convert.ToInt32((child as Button).Tag))
                    {
                        case -1:
                            child.Arrange(new Rect(new Point(posX, -this.TabSize.Height), this.AllTabSize));
                            posX += this.AllTabSize.Width;
                            break;
                        case -2:
                            child.Arrange(new Rect(new Point(posX, -this.TabSize.Height), this.AddTabSize));
                            posX += this.AddTabSize.Width;
                            break;
                        default:
                            Size size = new Size((this.newSize.Width - this.AllTabSize.Width - this.AddTabSize.Width) / (base.Children.Count - 2), this.TabSize.Height);
                            (child as Button).Width = size.Width;
                            child.Arrange(new Rect(new Point(posX, -this.TabSize.Height), size));
                            posX += size.Width;
                            break;
                    }
                }
            }
            this.UpdateLayout();
            return finalSize; // Returns the final Arranged size
        }
    }
}
