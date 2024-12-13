using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TravelAgency.Core.Domain.Entities._Common;
using TravelAgency.Core.Domain.Entities.Identity;

namespace TravelAgency.Infrastructure.Persistence.Data_Storage
{
    // CRUD [Create - Return - Update - Delete]
    internal class StorageManagement<T> where T : Entity
    {
        private readonly string FilePath;

        public StorageManagement(string filePath)
        {
            FilePath = filePath;

            FileInfo fileInfo = new FileInfo(FilePath);

            if (!fileInfo.Exists)
            {
                
                var emptyList = new List<T>(); 
                string jsonContent = JsonSerializer.Serialize(emptyList, new JsonSerializerOptions
                {
                    WriteIndented = true 
                });

                File.WriteAllText(FilePath, jsonContent);
            }
        }

        public List<T> GetAll()
        {
            var jsonData = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
        }

        public T GetById(int Id)
        {
            List<T> entities = GetAll();
            T entity = entities.FirstOrDefault(e => e.Id == Id) ?? null!;
            return entity;
        }

        public bool Add(T Entity)
        {
            if (Entity is null)
                return false;

            List<T> entities = GetAll();

            // If no entities exist, initialize Id to 1
            if (entities == null || !entities.Any())
            {
                entities = new List<T>(); // Ensure entities is initialized
                Entity.Id = 1;
            }
            else
            {
                T LastEntity = entities.Last(); // Safe because the list is non-empty
                Entity.Id = LastEntity.Id + 1;
            }

            entities.Add(Entity);
            Save(entities);

            return true;
        }

        public bool Update(T Entity)
        {
            if (Entity is null)
                return false;

            List<T> entities = GetAll();

            if (entities is null || !entities.Any())
                return false;

           
            int index = entities.FindIndex(e => e.Id == Entity.Id);
            if (index == -1)
                return false; 
       
            entities[index] = Entity;
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

        public void Save(List<T> Entities)
        {
            var JsonData = JsonSerializer.Serialize(Entities);
            File.WriteAllText(FilePath, JsonData);
        }

    }
}
