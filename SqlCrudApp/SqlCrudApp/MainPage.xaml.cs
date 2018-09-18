using SqlCrudApp.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SqlCrudApp
{
    public partial class MainPage : ContentPage
    {
        private SQLite.SQLiteAsyncConnection _connection;
        private System.Collections.ObjectModel.ObservableCollection<test> _test;

        public MainPage()
        {
            InitializeComponent();
            //mc  https://www.c-sharpcorner.com/article/setting-up-sqlite-in-xamarin-forms/
            //2   https://www.c-sharpcorner.com/article/data-persistence-using-sqlite-in-xamarin-forms/
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();



        }

        protected async override void OnAppearing()
        {
            await _connection.CreateTableAsync<test>();
            var abc = await _connection.Table<test>().ToListAsync();
            _test = new ObservableCollection<test>(abc);
            mylistview.ItemsSource = _test;

            base.OnAppearing();
        }

        void OnAdd(object sender, System.EventArgs e)
        {
            var test = new test { Title = Title.Text, Desc = Description.Text };
            _connection.InsertAsync(test);
            _test.Add(test);
        }
    }
}

