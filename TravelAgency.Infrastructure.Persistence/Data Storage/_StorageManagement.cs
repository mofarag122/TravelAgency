using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TravelAgency.Core.Domain.Entities._Common;
using TravelAgency.Core.Domain.Entities.Identity;

namespace TravelAgency.Infrastructure.Persistence.Data_Storage
{
    // CRUD [Create - Return - Update - Delete]
    internal class _StorageManagement<T> where T : Entity
    {
        private readonly string FilePath;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public _StorageManagement(string filePath)
        {
            FilePath = filePath;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            FileInfo fileInfo = new FileInfo(FilePath);

            if (!fileInfo.Exists)
            {
                var emptyList = new List<T>();
                string jsonContent = JsonSerializer.Serialize(emptyList, _jsonSerializerOptions);
                File.WriteAllText(FilePath, jsonContent);
            }
        }

        public List<T> GetAll()
        {
            var jsonData = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<T>>(jsonData, _jsonSerializerOptions) ?? new List<T>();
        }

        public T GetById(int id)
        {
            List<T> entities = GetAll();
            return entities.FirstOrDefault(e => e.Id == id) ?? null!;
        }

        public bool Add(T entity)
        {
            if (entity is null)
                return false;

            List<T> entities = GetAll();

            if (entities == null || !entities.Any())
            {
                entities = new List<T>();
                entity.Id = 1;
            }
            else
            {
                T lastEntity = entities.Last();
                entity.Id = lastEntity.Id + 1;
            }

            entities.Add(entity);
            Save(entities);

            return true;
        }

        public bool Update(T entity)
        {
            if (entity is null)
                return false;

            List<T> entities = GetAll();

            if (entities is null || !entities.Any())
                return false;

            int index = entities.FindIndex(e => e.Id == entity.Id);
            if (index == -1)
                return false;

            entities[index] = entity;
            Save(entities);

            return true;
        }

        public bool Delete(int id)
        {
            List<T> entities = GetAll();

            if (entities is null || !entities.Any())
                return false;

            T entityToRemove = entities.FirstOrDefault(e => e.Id == id) ?? null!;

            if (entityToRemove is null)
                return false;

            entities.Remove(entityToRemove);
            Save(entities);

            return true;
        }

        private void Save(List<T> entities)
        {
            var jsonData = JsonSerializer.Serialize(entities, _jsonSerializerOptions);
            File.WriteAllText(FilePath, jsonData);
        }
    }
}
