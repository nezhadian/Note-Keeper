using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Note_Keeper
{
    /// <summary>
    /// Interaction logic for DialogBox.xaml
    /// </summary>
    public partial class DialogBox : Page
    {

        #region Dialog Resault

        public enum DialogResault
        {
            None, Accept, Reject
        }

        public DialogResault Resault { set; get; }
        #endregion

        #region Message Property
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(DialogBox), new PropertyMetadata("Hello World !!!"));

        #endregion

        #region Dialog Mode

        public enum DialogMode
        {
            TwoButton, ThreeButton
        }

        private DialogMode _mode;

        public DialogMode ButtonsMode
        {
            get { return _mode; }
            set
            {

                _mode = value;

                switch (value)
                {
                    case DialogMode.TwoButton:
                        dlgDeleteBtns.Visibility = Visibility.Visible;
                        dlgSaveBtns.Visibility = Visibility.Collapsed;
                        break;

                    case DialogMode.ThreeButton:
                        dlgDeleteBtns.Visibility = Visibility.Collapsed;
                        dlgSaveBtns.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        #endregion

        #region Event Handlers

        public delegate void DialogResaultHandler(object sender, DialogResault resault);

        public event DialogResaultHandler OnDialogEnded, OnAccept, OnReject, OnCancel;

        #endregion

        #region Events

        private void Close_Clicked(object sender, RoutedEventArgs e)
        {
            EndDialog(DialogResault.None);
        }

        private void Accept_Clicked(object sender, RoutedEventArgs e)
        {
            EndDialog(DialogResault.Accept);
        }

        private void Outside_Clicked(object sender, MouseButtonEventArgs e)
        {
            EndDialog(DialogResault.None);
        }

        private void Reject_Clicked(object sender, RoutedEventArgs e)
        {
            EndDialog(DialogResault.Reject);
        }

        #endregion

        public DialogBox(NavigationService navigator)
        {
            InitializeComponent();
            navigator.Content = this;
        }

        private void EndDialog(DialogResault r)
        {
            Resault = r;
            switch (r)
            {
                case DialogResault.None:
                    OnCancel?.Invoke(this, r);
                    break;

                case DialogResault.Accept:
                    OnAccept?.Invoke(this, r);
                    break;

                case DialogResault.Reject:
                    OnReject?.Invoke(this, r);
                    break;
            }
            OnDialogEnded?.Invoke(this, r);
            NavigationService.Content = null;
        }
        public DialogBox()
        {
            InitializeComponent();
        }
    }
}
