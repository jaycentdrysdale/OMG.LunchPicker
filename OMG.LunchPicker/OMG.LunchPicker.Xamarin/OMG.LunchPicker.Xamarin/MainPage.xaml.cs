using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OMG.LunchPicker.Xamarin
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async void OnClickedLoad(object sender, EventArgs e)
        {
            string uri = "https://jsonplaceholder.typicode.com/todos";
            lblMessage.Text = "Loading Items";
            HttpClient client;
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            var endPoint = new Uri(uri);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<dynamic>>(content);
                this.lblMessage.Text = Items.Count().ToString();
            }
        }

    }
}
