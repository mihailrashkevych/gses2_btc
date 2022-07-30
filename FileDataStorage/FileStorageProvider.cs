using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDataStorage
{
    public class FileStorageProvider<T> where T : class
    {
        private readonly string _connectionString;
        public FileStorageProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<T>?> GetValues()
        {
            if (!File.Exists(_connectionString))
            {
                return null;
                throw new FileNotFoundException($"{typeof(FileStorageProvider<T>)} : Data was not found..");
            }
            string text = await File.ReadAllTextAsync(_connectionString);
            List<T>? list = JsonConvert.DeserializeObject<List<T>>(text);
            return list;
        }

        public async Task<bool> AddValue(T value)
        {
            try
            {
                List<T>? list = new();
                if (File.Exists(_connectionString))
                {
                    string text = await File.ReadAllTextAsync(_connectionString);
                    list = JsonConvert.DeserializeObject<List<T>>(text);
                }
                list.Add(value);
                File.WriteAllText(_connectionString, JsonConvert.SerializeObject(list));
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
