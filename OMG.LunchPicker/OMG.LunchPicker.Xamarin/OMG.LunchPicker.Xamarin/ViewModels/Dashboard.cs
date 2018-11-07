using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            Task.Run(() => Execute());
        }
        #endregion

        #region Methods
        private async void Execute()
        {
            MyDashboard model = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://omglunchpickerweb20181104111136.azurewebsites.net/api/Dashboard/GetDashboard")
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode)
                  {
                      var jsonString = response.Content.ReadAsStringAsync();
                      jsonString.Wait();
                      var rw = jsonString.Result;
                      try
                      {
                          model = JsonConvert.DeserializeObject<MyDashboard>(rw);
                          AvgRating = model.averageRating;
                          CuisineCount = model.cuisines.Count;
                          PeopleCount = model.users.Count;
                          RestaurantCount = model.restaurants.Count;
                      }
                      catch (Exception ex)
                      {
                          var ms = ex.Message;
                          int x = 1;
                      }
                  }
              });
            task.Wait();
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

    public class MyDashboard
    {
        public int averageRating { get; set; }
        public List<Cuisine> cuisines { get; set; }
        public List<User> users { get; set; }
        public List<dynamic> restaurants { get; set; }
    }

    public class Cuisine
    {
        public string name { get; set; }
    }

    public class User
    {
        public string userName { get; set; }
    }

    public class Restaurant
    {
        //public int id { get; set; }
        //public string name { get; set; }
        //public string street { get; set;  }
        //public string city { get; set; }
        //public string state { get; set; }
        //public string zip { get; set; }
        //public int rating { get; set; }
        //public List<string> cuisines { get; set; }
    }

}
