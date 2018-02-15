using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GlazerCalcGUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        const double MAX_WIDTH = 5.0;
        const double MIN_WIDTH = 0.5;
        double pw = 0;
        double ph = 0;

        public void Width_KeyUp(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (double.TryParse(Width.Text.Trim(), out pw))
            {
                if (pw <= MAX_WIDTH && pw >= MIN_WIDTH)
                {
                    //SolidColorBrush brush = Width.Background as SolidColorBrush;
                    //brush.Color = Colors.Aquamarine;
                    width_indicator.Glyph = "\uE73E";
                }

                else
                {
                    //SolidColorBrush brush = Width.Background as SolidColorBrush;
                    //brush.Color = Colors.PaleVioletRed;
                    width_indicator.Glyph = "\uE711";

                }
            }

            else
            {
                width_indicator.Glyph = "\uE711";
            }
        }

        const double MAX_HEIGHT = 3.0;
        const double MIN_HEIGHT = 0.75;

        public void Height_KeyUp(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (double.TryParse(Height.Text.Trim(), out ph))
            {
                if (ph <= MAX_HEIGHT && ph >= MIN_HEIGHT)
                {
                    //SolidColorBrush brush = Width.Background as SolidColorBrush;
                    //brush.Color = Colors.Aquamarine;
                    height_indicator.Glyph = "\uE73E";
                }

                else
                {
                    //SolidColorBrush brush = Width.Background as SolidColorBrush;
                    //brush.Color = Colors.PaleVioletRed;
                    height_indicator.Glyph = "\uE711";

                }
            }

            else
            {
                height_indicator.Glyph = "\uE711";
            }
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check to see which radio button was selected and create a variable from the data
                string tint_color = "None";
                if (Black.IsChecked == true)
                {
                    tint_color = "Black";
                }

                if (Brown.IsChecked == true)
                {
                    tint_color = "Brown";
                }

                if (Blue.IsChecked == true)
                {
                    tint_color = "Blue";
                }

                //Check that each field is filled, if not tell the user to try again
                if ((Width.Text == "") || (Height.Text == "") || (tint_color == "None"))
                {
                    var error = new MessageDialog("There are one or more fields that have not been completed." +
                        " Please try again.", "Error Calculating Data");

                    error.Commands.Add(new UICommand("OK") { Id = 0 });

                    error.CancelCommandIndex = 0;

                    var result = await error.ShowAsync();
                }

                else if (((ph > MAX_HEIGHT) || (ph < MIN_HEIGHT)) && ((pw > MAX_WIDTH) || (pw < MIN_WIDTH)))
                {
                    var error = new MessageDialog("The height and width you entered are outside of the ranges specified." +
                        " Please try again.", "Error Calculating Data");

                    error.Commands.Add(new UICommand("OK") { Id = 0 });

                    error.CancelCommandIndex = 0;

                    var result = await error.ShowAsync();
                }

                else if ((pw > MAX_WIDTH) || (pw < MIN_WIDTH))
                {
                    var error = new MessageDialog("The width you entered is outside of the range specified." +
                        " Please try again.", "Error Calculating Data");

                    error.Commands.Add(new UICommand("OK") { Id = 0 });

                    error.CancelCommandIndex = 0;

                    var result = await error.ShowAsync();
                }

                else if ((ph > MAX_HEIGHT) || (ph < MIN_HEIGHT))
                {
                    var error = new MessageDialog("The height you entered is outside of the range specified." +
                        " Please try again.", "Error Calculating Data");

                    error.Commands.Add(new UICommand("OK") { Id = 0 });

                    error.CancelCommandIndex = 0;

                    var result = await error.ShowAsync();
                }

                else
                {
                    //Calculate a few things
                    double woodLength = 2 * (pw * ph) * 3.25;
                    double glassArea = 2 * (pw * ph);

                    //Get today's date and show it in the format we want
                    string Today = DateTime.Now.ToString("MM/dd/yyyy");

                    //Create message dialog
                    var msg = new MessageDialog("Date: " + Today + Environment.NewLine +
                        "Width: " + pw + Environment.NewLine +
                        "Height: " + ph + Environment.NewLine +
                        "Color: " + tint_color + Environment.NewLine +
                        "Number of Windows: " + Quantity.Value + Environment.NewLine +
                        "Wood Length: " + woodLength + Environment.NewLine +
                        "Glass Area: " + glassArea,
                        "Glazer Calc Results");

                    //Display OK button to user when dialog is brought up
                    msg.Commands.Add(new UICommand("OK") { Id = 0 });

                    //Set OK button as cancel and close the dialog when clicked
                    msg.CancelCommandIndex = 0;

                    //Reset field values
                    Width.Text = "";
                    Height.Text = "";
                    Black.IsChecked = false;
                    Blue.IsChecked = false;
                    Brown.IsChecked = false;
                    Quantity.Value = 1;


                    var result = await msg.ShowAsync();

                }
            }

            catch(Exception ex)
            {
                //Display error message if there is one
                var messageDialog = new MessageDialog(ex.Message);
                throw;               
            }
        }
    }
}
