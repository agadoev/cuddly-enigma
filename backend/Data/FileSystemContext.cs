using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using Core;
using Core.UseCases;

namespace Data {

    public interface IFileSystem {
        bool FileExists(string path);

        void CreateFile(string path);

        string[] ReadAllLines(string path);

        void WriteAllText(string path, string text);
    }

    public class FileSystem : IFileSystem {

        public bool FileExists(string path) => File.Exists(path);

        public void CreateFile(string path) {
            using (var fs = File.Create(path)) {}
        }

        public string[] ReadAllLines(string path) {
            var lines = new List<string>();
            using (var sr = new StreamReader(path)) {
                string line;

                while ((line = sr.ReadLine()) != null)
                    lines.Add(line);
            }

            return lines.ToArray();
        }

        public void WriteAllText(string path, string text) {
            File.WriteAllText(path, text);
        }
    }


    public class FileSystemContext<T> : IDbContext<T> where T : Entity {

        private readonly string _fileName;
        private readonly IFileSystem _fs;
        private readonly string _path;
        private List<T> items{get; set;}

        public FileSystemContext(
            IFileSystem fileSystem
        ) {
            items = new List<T>();
            _fs = fileSystem;
            _path = Path.Join("/Users/gadoevalex/wishlist/backend/Data", typeof(T).ToString());

            if (!_fs.FileExists(_path))
                _fs.CreateFile(_path);

            LoadData();
        }

        public void LoadData() {
            var lines = _fs.ReadAllLines(_path);
            foreach(var line in lines) {
                var obj = JsonSerializer.Deserialize<T>(line);
                items.Add(obj);
            }
        }

        public T[] GetAll() {
            return items.ToArray();
        }

        public void Add(IEnumerable<T> i) {
            items = items.Concat<T>(i).ToList<T>();
            Commit();
        }

        public void Add(T item) {
            items.Add(item);
            Commit();
        }

        public T? GetById(int id) {
            return items.Find(item => item.Id == id);
        }

        public void Commit () {
            var usersString = items.Select(user => JsonSerializer.Serialize(user)).Aggregate((a, b) => a + "\n" + b);
            _fs.WriteAllText(_path, usersString);
        }

        public string GetPath() => _path;
    }
}