using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TechresStandaloneSale.Helpers
{
    public class WebImageFallBackBehaviour: System.Windows.Interactivity.Behavior<Image>
    {
        public static readonly DependencyProperty FallBackImageSourceProperty
          = DependencyProperty.Register("FallBackImageSource", typeof(string), typeof(WebImageFallBackBehaviour), new PropertyMetadata(string.Empty));

        public string FallBackImageSource
        {
            get { return (string)GetValue(FallBackImageSourceProperty); }
            set { SetValue(FallBackImageSourceProperty, value); }
        }
       
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.ImageFailed += AssociatedObject_ImageFailed;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.ImageFailed -= AssociatedObject_ImageFailed;
        }
        private void AssociatedObject_ImageFailed(object sender, System.Windows.ExceptionRoutedEventArgs e)
        {
            Image self = sender as Image;

            if (self != null)
            {
                self.Source = new BitmapImage(new Uri(FallBackImageSource, UriKind.Relative));
            }
        }
    }
}
