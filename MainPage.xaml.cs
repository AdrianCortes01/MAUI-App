namespace Nutrition_Assistant
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            CheckAndRequestCameraPermission(); // Call only if not granted before
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                DisplayAlert("Error", "Please enter both username and password.", "OK");
                return;
            }

            // Simulated authentication logic
            if (username == "admin" && password == "password123")
            {
                DisplayAlert("Success", "Login successful!", "OK");
                // Navigate to another page if needed, in this case the home page
                Application.Current.MainPage = new HomePage();
            }
            else
            {
                DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }

        private void OnPageLoaded(object sender, EventArgs e)
        {
            CheckAndRequestCameraPermission(); // Call the permission request function
        }


        private async void CheckAndRequestCameraPermission()
        {
            if (Preferences.Get("CameraPermissionAsked", false)) // Has the user been asked before?
                return; // Skip asking again

            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();

                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permission Denied", "Camera access is required to take photos.", "OK");
                }
            }

            Preferences.Set("CameraPermissionAsked", true); // Save that we asked
        }
    }

}
