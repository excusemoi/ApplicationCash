using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;

namespace ApplicationCashImp
{
    public class AppCash<T>
    {
        private int _size;
        private int _currentSize;
        private Dictionary<string, KeyValuePair<T, DateTime>> _data;
        private TimeSpan _notesLifeTime;
        public AppCash(TimeSpan _nlt, int _sz)
        {
            _data = new Dictionary<string, KeyValuePair<T, DateTime>>();
            _notesLifeTime = _nlt;
            _size = _sz;
        }
        public void Save(string key, T data)
        {
            if (_data.ContainsKey(key))
                throw new ArgumentException("The note already exist in the data\n");
            else if (_currentSize + 1 > _size)
            {
                KeyValuePair<string, KeyValuePair<T, DateTime>> tmp = _data.First();
                foreach (var v in _data)
                    if (v.Value.Value < tmp.Value.Value)
                        tmp = v;
                _data.Remove(tmp.Key);
            }
            
            _data.Add(key, new KeyValuePair<T, DateTime>(data, DateTime.Now));
            _currentSize++;
            checkTime(_data);
        }

        public T Get(string key)
        {
            KeyValuePair<T, DateTime> data;
            if (!_data.TryGetValue(key, out data))
                throw new KeyNotFoundException($"Key '{key}' data does not exist\n");
            else if (DateTime.Now - data.Value > _notesLifeTime)
            {
                _data.Remove(key);
                throw new KeyNotFoundException($"Key '{key}' data does not exist\n");
            }
            checkTime(_data);
            return data.Key;
        }
        private void checkTime(Dictionary<string, KeyValuePair<T, DateTime>> dict)
        {
            foreach (var v in dict)
            {
                if (DateTime.Now - v.Value.Value > _notesLifeTime)
                    dict.Remove(v.Key);
            }
        }

        public void printData()
        {
            Console.WriteLine("Lifetime of notes");
            foreach (var v in _data)
            {
                Console.WriteLine($"Key {v.Key} Value {v.Value.Key} Added at {v.Value.Value}");
            }
        }

        
        

    }
}