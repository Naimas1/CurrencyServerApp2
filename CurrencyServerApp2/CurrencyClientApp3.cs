namespace CurrencyClientApp
{
    public partial class ClientForm : Form
    {
        private HttpClient httpClient = new HttpClient();

        public ClientForm()
        {
            InitializeComponent();
            LoadImages();
        }

        private Task<List<string>> GetImagesFileNamesAsync()
        {
            // Mock implementation to get image file names from the server
            return Task.FromResult(new List<string> { "image1.jpg", "image2.jpg", "image3.jpg" });
        }

        private async Task UploadFiles(string[] filePaths)
        {
            using (var content = new MultipartFormDataContent())
            {
                foreach (var filePath in filePaths)
                {
                    content.Add(new StreamContent(File.OpenRead(filePath)), "files", Path.GetFileName(filePath));
                }

                var response = await httpClient.PostAsync("http://localhost:5000/api/file/upload", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    AppendText("Files uploaded successfully: " + result);
                }
                else
                {
                    AppendText("Failed to upload files.");
                }
            }
        }

        private void ConfirmDelete(string fileName)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this image?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                DeleteFile(fileName);
            }
        }

        private async void DeleteFile(string fileName)
        {
            var response = await httpClient.DeleteAsync($"http://localhost:5000/api/file/{fileName}");
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

        private void AppendText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendText), new object[] { text });
            }
            else
            {
                textBox1.AppendText(text + Environment.NewLine);
            }
        }
    }
}
