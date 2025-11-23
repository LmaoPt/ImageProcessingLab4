using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ImageProcessingLab4
{
    
    public partial class MainWindow : Window
    {
        private BitmapImage initialImage;
        private BitmapSource toGrayImage;
        private BitmapSource contrastImage;
        private BitmapSource filter;
        public MainWindow()
        {
            InitializeComponent();
            
        }
        public void selectedImage_Click(Object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp";

            if(openFileDialog.ShowDialog() == true)
            {
                initialImage = new BitmapImage();
                initialImage.BeginInit();
                initialImage.UriSource = new Uri(openFileDialog.FileName);
                initialImage.EndInit();

                selectedImage.Source = initialImage;
            }
            
        }
        public void transformToGray_Click(Object sender, RoutedEventArgs e)
        {
            if(initialImage != null)
            {
                toGrayImage = Algoritmic.ConvertRGBToGray(initialImage);
                toGray.Source = toGrayImage;
            }
            else
            {
                MessageBox.Show("Изначального изображения нет! Загрузите его");
            }
        }
        public void contrasting_Click(Object sender, RoutedEventArgs e)
        {
            if(toGrayImage != null)
            {
                contrastImage = Algoritmic.contrastingImage(toGrayImage);
                contrasting.Source = contrastImage;
            }
            else
            {
                MessageBox.Show("Нечего контрастировать! Загрузите его");
            }
        }
        public void maska_Click(Object sender, RoutedEventArgs e)
        {
            if (toGrayImage != null)
            {
                filter = Algoritmic.Filter(toGrayImage);
                maska.Source = filter;
            }
            else
            {
                MessageBox.Show("Нечего замасить! Загрузите его");
            }
        }
        public void exitProgram_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        
    }


}
