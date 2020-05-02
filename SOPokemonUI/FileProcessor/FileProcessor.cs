﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;

namespace SOPokemonUI.FileProcessor
{
    public static class FileProcessor
    {
        /// <summary>
        /// Loads csv file from local drive to a generic List
        /// </summary>
        /// <typeparam name="T"> Needs a generic List </typeparam>
        /// <param name="filePath"> Needs a parameter where file is located as string (path) </param>
        /// <returns> Generic List filled with data from csv file without headline </returns>
        public static BindableCollection<T> LoadFromCsvFile<T>(string filePath) where T : class, new()
        {
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                BindableCollection<T> output = new BindableCollection<T>();
                T entry = new T();
                var cols = entry.GetType().GetProperties();

                // Checks to be sure we have at least one header row and one data row
                if (lines.Count < 2)
                {
                    throw new IndexOutOfRangeException("The file was either empty or missing.");
                }

                // Splits the header into one column header per entry
                var headers = lines[0].Split(';');

                // Removes the header row from the lines so we don't
                // have to worry about skipping over that first row.
                lines.RemoveAt(0);

                foreach (var row in lines)
                {
                    entry = new T();

                    // Splits the row into individual columns. Now the index
                    // of this row matches the index of the header so the
                    // FirstName column header lines up with the FirstName
                    // value in this row.
                    var vals = row.Split(';');

                    // Loops through each header entry so we can compare that
                    // against the list of columns from reflection. Once we get
                    // the matching column, we can do the "SetValue" method to 
                    // set the column value for our entry variable to the vals
                    // item at the same index as this particular header.
                    for (var i = 0; i < headers.Length; i++)
                    {
                        foreach (var col in cols)
                        {
                            if (col.Name == headers[i])
                            {
                                col.SetValue(entry, Convert.ChangeType(vals[i], col.PropertyType));
                            }
                        }
                    }

                    output.Add(entry);

                }

                return output;
            }
            else
            {
                return null;
            }
        }

        // To save Pokemon-list
        public static void SaveToCsvFile<T>(BindableCollection<T> data, string filePath) where T : class
        {
            // Get only Path
            string path = System.IO.Path.GetDirectoryName(filePath);

            // Create Folder if doesn't exists
            if (!System.IO.Directory.Exists(System.IO.Path.GetFullPath(path)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetFullPath(path));
            }

            BindableCollection<string> lines = new BindableCollection<string>();
            StringBuilder line = new StringBuilder();

            if (data == null || data.Count == 0)
            {
                //throw new ArgumentNullException("data", "You must populate the data parameter with at least one value.");
                return;
            }
            var cols = data[0].GetType().GetProperties();

            // Loops through each column and gets the name so it can comma 
            // separate it into the header row.
            foreach (var col in cols)
            {
                line.Append(col.Name);
                line.Append(";");
            }

            // Adds the column header entries to the first line (removing
            // the last comma from the end first).
            lines.Add(line.ToString().Substring(0, line.Length - 1));

            foreach (var row in data)
            {
                line = new StringBuilder();

                foreach (var col in cols)
                {
                    line.Append(col.GetValue(row));
                    line.Append(";");
                }

                // Adds the row to the set of lines (removing
                // the last comma from the end first).
                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }

            System.IO.File.WriteAllLines(filePath, lines);
        }

    }
}
