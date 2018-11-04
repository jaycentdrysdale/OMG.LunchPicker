using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Xamarin
{
    public class Dashboard : INotifyPropertyChanged
    {
        #region Private Variables
        private int _restaurantCount;
        private int _peopleCount;
        private int _cuisineCount;
        private int _avgRating;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Ctors
        public Dashboard()
        {
            LoadRestaurants();
        }
        #endregion

        #region Methods
        private async void LoadRestaurants()
        {
            string uri = "http://omglunchpickerweb20181104111136.azurewebsites.net/api/Dashboard/GetDashboard";
            HttpClient client;
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            var endPoint = new Uri(uri);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<dynamic>>(content);
                await Task.Run(() =>
                    RestaurantCount = Items.Count
                );

            }
        }
        #endregion

        #region Public Properties
        public int RestaurantCount
        {
            set
            {
                if (_restaurantCount != value)
                {
                    _restaurantCount = value;
                    OnPropertyChanged("RestaurantCount");
                }
            }
            get
            {
                return _restaurantCount;
            }
        }

        public int PeopleCount
        {
            set
            {
                if (_peopleCount != value)
                {
                    _peopleCount = value;
                    OnPropertyChanged("PeopleCount");
                }
            }
            get
            {
                return _peopleCount;
            }
        }

        public int CuisineCount
        {
            set
            {
                if (_cuisineCount != value)
                {
                    _cuisineCount = value;
                    OnPropertyChanged("CuisineCount");
                }
            }
            get
            {
                return _cuisineCount;
            }
        }

        public int AvgRating
        {
            set
            {
                if (_avgRating != value)
                {
                    _avgRating = value;
                    OnPropertyChanged("AvgRating");
                }
            }
            get
            {
                return _avgRating;
            }
        }

        #endregion;

        #region INotifyPropertyChanged
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
