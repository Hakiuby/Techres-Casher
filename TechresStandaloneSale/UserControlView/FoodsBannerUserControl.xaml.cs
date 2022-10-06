using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.ViewModels;

namespace TechresStandaloneSale.UserControlView
{
    /// <summary>
    /// Interaction logic for FoodsBannerUserControl.xaml
    /// </summary>
    public partial class FoodsBannerUserControl : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int i = 1;
        // 
        private DispatcherTimer timerImageChange;
        private Image[] ImageControls;
        private List<ImageSource> Images = new List<ImageSource>();
        private static string[] TransitionEffects = new[] { "Fade" };
        private int CurrentSourceIndex, CurrentCtrlIndex, EffectIndex = 0, IntervalTimer = 7;
        private string TransitionType;
        private List<FoodMenuItem> FoodsList = MainSecondViewModel.FoodList;
        public FoodsBannerUserControl()
        {
            InitializeComponent();
            //Initialize Image control, Image directory path and Image timer.
            //ImageControls = new[] { myImage, myImage2 };

            LoadImages();
            PlaySlideShow();

            timerImageChange = new DispatcherTimer();
            timerImageChange.Interval = new TimeSpan(0, 0, IntervalTimer);
            timerImageChange.Tick += new EventHandler(timerImageChange_Tick);
            timerImageChange.IsEnabled = true;

            #region Test ads 

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

            #endregion
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            i++;
            PicHolder.Source = new BitmapImage(new Uri(@"/ImageAdv/" + i + ".png", UriKind.Relative));

            if (i == 2)
            {
                i = 0;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PlaySlideShow();
            timerImageChange.IsEnabled = true;
        }

        private void LoadImages()
        {
            if (FoodsList!= null && FoodsList.Count > 0)
            {
                List<string> ListFoodsAvatar = FoodsList.Select(x => x.ImageFood.UriSource.AbsoluteUri).ToList();
                Images.Clear();
                foreach (string food in ListFoodsAvatar)
                {
                    int originals = food.LastIndexOf("/");
                    string imgLarge = food.Insert(originals + 1, "originals_");

                    Images.Add(CreateImageSource(imgLarge, true));
                }
            }           
        }

        private ImageSource CreateImageSource(string file, bool forcePreLoad)
        {
            if (forcePreLoad)
            {
                var src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(file, UriKind.Absolute);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                //src.Freeze();
                return src;
            }
            else
            {
                var src = new BitmapImage(new Uri(file, UriKind.Absolute));
                src.Freeze();
                return src;
            }
        }

        private void timerImageChange_Tick(object sender, EventArgs e)
        {
            PlaySlideShow();
        }

        private void PlaySlideShow()
        {
            try
            {
                var oldCtrlIndex = CurrentCtrlIndex;
                CurrentCtrlIndex = (CurrentCtrlIndex + 1) % 2;

                Random rd = new Random();
                int i = rd.Next(0, Images.Count);

                CurrentSourceIndex = (i + 1) % Images.Count;

                Image imgFadeOut = ImageControls[oldCtrlIndex];
                Image imgFadeIn = ImageControls[CurrentCtrlIndex];
                ImageSource newSource = Images[CurrentSourceIndex];
                imgFadeIn.Source = newSource;

                //FoodName.Content = FoodsList[CurrentSourceIndex].ContentFoodName;
                //PriceFood.Content = FoodsList[CurrentSourceIndex].PriceFood;

                TransitionType = TransitionEffects[EffectIndex].ToString();

                Storyboard StboardFadeOut = (Resources[string.Format("{0}Out", TransitionType.ToString())] as Storyboard).Clone();
                StboardFadeOut.Begin(imgFadeOut);
                Storyboard StboardFadeIn = Resources[string.Format("{0}In", TransitionType.ToString())] as Storyboard;
                StboardFadeIn.Begin(imgFadeIn);
            }
            catch (Exception ex) {
                WriteLog.logs(ex.ToString());
            }
        }
    }
}
