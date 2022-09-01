using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class EditorPage : Page
    {
        #region Data Property

        private NoteData _data;
        public NoteData Data
        {
            get { return _data; }
            set {
                txtTitle.Text = value.Title;
                txtContent.Text = value.Content;
                CanSave = false;
                _data = value;
            }
        }

        #endregion

        bool CanSave = false;

        #region Constructors

        public EditorPage(NoteData data)
        {
            InitializeComponent();
            Data = data;
        }
        public EditorPage():this(new NoteData()){}

        #endregion

        #region Home Button

        private void Home_Clicked(object sender, RoutedEventArgs e)
        {
            if (CanSave)
            {
                new DialogBox(MainWindow.DialogNavigator)
                {
                    Message = $"Do you wan't to save changes?",
                    ButtonsMode = DialogBox.DialogMode.ThreeButton

                }.OnDialogEnded += (s, r) => {

                    switch (r)
                    {
                        case DialogBox.DialogResault.Accept:
                            SaveCommand_Excuted(null, null);
                            OpenHomePage();
                            break;

                        case DialogBox.DialogResault.Reject:
                            OpenHomePage();
                            break;

                    }
                };
            }else
            {
                OpenHomePage();
            }
        }
        private void OpenHomePage()
        {
            Home.OpenHome();
        }

        #endregion

        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.TextAlignment != TextAlignment.Center && textBox.Text.Length == 1)
            {

                bool isRTL = Regex.IsMatch(textBox.Text[0].ToString(), @"\p{IsArabic}");
                textBox.TextAlignment = isRTL ? TextAlignment.Right : TextAlignment.Left;
            }

            CanSave = true;
            
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtContent.Focus();
        }

        #region Save Global Command

        private void SaveCommand_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            Data.Title = txtTitle.Text;
            Data.Content = txtContent.Text;

            Data.Save();
            CanSave = false;
        }

        private void SaveCommand_Checked(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanSave;
        }
        #endregion
    }
}
