using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PhoneNumberExcercise.ViewModel;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        textInput = "1-555-NETMAUI";
        callButtonText = "Call";
        isCallButtonEnabled = false;
    }

    [ObservableProperty]
    string textInput;

    [ObservableProperty]
    string callButtonText;

    [ObservableProperty]
    bool isCallButtonEnabled;

    [RelayCommand]
    async void Call()
    {
        string translatedNumber = Core.PhonewordTranslator.ToNumber(TextInput);

        if (await Shell.Current.DisplayAlert(
                "Dial a Number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No")
            )
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                    PhoneDialer.Default.Open(translatedNumber);
                else
                    await Shell.Current.DisplayAlert(
                        "Not supported",
                        "Sorry, This device is not supporting Phone call",
                        "Close");
            }
            catch (ArgumentNullException)
            {
                await Shell.Current.DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
            }
            catch (Exception)
            {
                // Other error has occurred.
                await Shell.Current.DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
    }

    [RelayCommand]
    async void Translate()
    {
        string enteredNumber = TextInput;
        string translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

        if (!string.IsNullOrEmpty(translatedNumber))
        {
            IsCallButtonEnabled = true;
            CallButtonText = "Call " + translatedNumber;
        }
        else
        {
            IsCallButtonEnabled = false;
            CallButtonText = "Call";
        }
    }
}