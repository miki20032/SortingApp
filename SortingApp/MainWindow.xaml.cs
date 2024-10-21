using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace SortingApp
{
    public partial class MainWindow : Window
    {
        private string filePath;
        private SortingContext sortingContext;

        public MainWindow()
        {
            InitializeComponent();
            sortingContext = new SortingContext();
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                MessageBox.Show("File loaded successfully.");
            }
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            if (filePath == null)
            {
                MessageBox.Show("Please load a file first.");
                return;
            }

            ISortingStrategy sortingStrategy = null;
            if (BubbleSortRadio.IsChecked == true)
            {
                sortingStrategy = new BubbleSort();
            }
            else if (QuickSortRadio.IsChecked == true)
            {
                sortingStrategy = new QuickSort();
            }
            else if (MergeSortRadio.IsChecked == true)
            {
                sortingStrategy = new MergeSort();
            }

            if (sortingStrategy == null)
            {
                MessageBox.Show("Please select a sorting algorithm.");
                return;
            }

            sortingContext.SetStrategy(sortingStrategy);

            List<string> lines = File.ReadAllLines(filePath).ToList();
            bool isNumeric = lines.All(line => int.TryParse(line, out _));

            var stopwatch = Stopwatch.StartNew();
            if (isNumeric)
            {
                var numbers = lines.Select(int.Parse).ToList();
                sortingContext.Sort(numbers);
                File.WriteAllLines(filePath, numbers.Select(n => n.ToString()));
            }
            else
            {
                sortingContext.Sort(lines);
                File.WriteAllLines(filePath, lines);
            }
            stopwatch.Stop();

            SummaryText.Text = $"Sorted {lines.Count} elements in {stopwatch.ElapsedMilliseconds} ms.";
        }
    }
}
