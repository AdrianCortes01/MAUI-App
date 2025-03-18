namespace Nutrition_Assistant.Pages
{
    public partial class LogFoodPage : ContentPage
    {
        public LogFoodPage()
        {
            InitializeComponent();
        }

        private async void OnTakePictureClicked(object sender, EventArgs e)
        {
            try
            {
                // Check if camera permission is granted
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Camera>();
                    if (status != PermissionStatus.Granted)
                    {
                        await DisplayAlert("Permission Denied", "Camera access is required to take pictures.", "OK");
                        return;
                    }
                }

                // Capture photo
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo == null)
                    return;

                // Save photo to a memory stream
                using var stream = await photo.OpenReadAsync();
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                // Set image source
                capturedImage.Source = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
