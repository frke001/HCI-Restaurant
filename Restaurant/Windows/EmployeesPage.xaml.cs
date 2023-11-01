﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant.Windows
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            new AddEmployeeWindow().ShowDialog();
        }

        private void ModifyEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            new ModifyEmployeeWindow().ShowDialog();
        }
    }
}
