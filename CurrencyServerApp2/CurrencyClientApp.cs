namespace CurrencyClientApp
{
    public partial class ClientForm : Form
    {
        private List<string> imageFileNames = new List<string>();

        public ClientForm(HttpClient httpClient = null)
        {
            InitializeComponent();
            LoadImages();
            this.httpClient = httpClient;
        }

        private async void LoadImages()
        {
            // Assume GetImagesFileNamesAsync is a method that retrieves the list of image file names.
            imageFileNames = await ImagesFileNamesAsync;
            DisplayImages();
        }

        private async Task<List<string>> GetImagesFileNamesAsync()
        {
            // Mock implementation to get image file names from the server
            return new List<string> { "image1.jpg", "image2.jpg", "image3.jpg" };
        }

        private void DisplayImages()
        {
            // Assume carousel is a control that displays images in a carousel format.
            carousel.Images = imageFileNames;
            carousel.Refresh();
        }

        private async void buttonDeleteImage_Click(object sender, EventArgs e)
        {
            // Assume selectedImageFileName contains the name of the selected image to be deleted.
            string selectedImageFileName = "example.jpg";

            var confirmResult = MessageBox.Show("Are you sure to delete this image?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                var response = await httpClient.DeleteAsync($"http://localhost:5000/api/file/{selectedImageFileName}");
                if (response.IsSuccessStatusCode)
                {
                    AppendText("File deleted successfully.");
                    LoadImages();
                }
                else
                {
                    AppendText("Failed to delete file.");
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is ClientForm form &&
                   EqualityComparer<HttpClient>.Default.Equals(this.httpClient, form.httpClient);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.httpClient);
        }
    }

    internal class carousel
    {
        public static List<string> Images { get; internal set; }

        internal static void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
