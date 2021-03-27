using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // synchronously freezes the app until it finishes
            //SetName();

            // Gives the illusion everything works at the same time

            // takes a delegate
            Thread t = new Thread(SetName);

            // creating a new thread is expensive
            // every thread is on own stack, defined on its own scope, when its done it gets destroyed and the whole process repeats for Thread Scheduler
            // working directly with Thread class is not the best way of having async code
            t.Start();
        }

        private void SetLabel(string name)
        {
            label1.Content = name;
        }

        private void SetName()
        {
            var name = GetNameFromDb();
            this.Invoke(new Action<string>(SetLabel), name);
        }

        private string GetNameFromDb()
        {
            Thread.Sleep(5000);
            return "Jess";
        }
    }
}
