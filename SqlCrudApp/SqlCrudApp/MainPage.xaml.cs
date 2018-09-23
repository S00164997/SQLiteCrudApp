﻿using SqlCrudApp.Persistence;
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


        void OnDelete(object sender, System.EventArgs e)
        {
            var selection = mylistview.SelectedItem as test;

            if (selection == null)
            {
                DisplayAlert("Select Item", "Please select Item to Delete", "Ok");
            }
            else
            {
                


                _connection.DeleteAsync(selection);
                _test.Remove(selection);
            }
        }

        void OnUpdate(object sender, System.EventArgs e)
        {

            var selection = mylistview.SelectedItem as test;

            if (selection == null)
            {
                DisplayAlert("Select Item", "Please select Item to Edit", "Ok");
            }
            else
            {
                selection.Title = Title.Text;
                selection.Desc = Description.Text;

                    var edit = new  test { Title = Title.Text, Desc = Description.Text };

                _connection.DeleteAsync(selection);
                _test.Remove(selection);
            }
            

            var test = new test { Title = Title.Text, Desc = Description.Text };
            _connection.UpdateAsync(test);
            _test.Add(test);

            //var selection = listView.SelectedItem as Product;
            //if (selection == null)
            //{
            //    DisplayAlert("Select Item", "Please select Item to repeat", "Ok");
            //}
            //else
            //{
            //    //await DisplayAlert("mm", "Product: " + selection.Id, "ok");
            //    Product temp = new Product { Id = selection.Id, ProductName = selection.ProductName, Barcode = selection.Barcode, Price = selection.Price, Quantity = selection.Quantity };
            //    TillPage.Products2.Add(temp);
            //}
        }
    }
}

