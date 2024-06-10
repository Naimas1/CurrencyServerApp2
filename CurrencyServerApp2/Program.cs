namespace CurrencyServerApp2
{
    public partial class ServerForm : Form
    {
        private HttpClient httpClient = new HttpClient();

        public bool InvokeRequired { get; private set; }

        public ServerForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private async void buttonUploadLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    await UploadFile(filePath);
                }
            }
        }

        private async Task UploadFile(string filePath)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(File.OpenRead(filePath)), "file", Path.GetFileName(filePath));

                var response = await httpClient.PostAsync("http://localhost:5000/api/file/upload", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    AppendText("File uploaded successfully: " + result);
                }
                else
                {
                    AppendText("Failed to upload file.");
                }
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

        private void Invoke(Action<string> action, object[] objects)
        {
            throw new NotImplementedException();
        }

        private class OpenFileDialog
        {
            public string Filter { get; internal set; }
            public string FileName { get; internal set; }

            internal object ShowDialog()
            {
                throw new NotImplementedException();
            }
        }
    }

    internal class textBox1
    {
        internal static void AppendText(string v)
        {
            throw new NotImplementedException();
        }
    }

    internal class DialogResult
    {
        public static object OK { get; internal set; }
    }

    public class Form
    {
    }
}
