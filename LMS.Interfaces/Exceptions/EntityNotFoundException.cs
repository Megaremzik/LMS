﻿using System;

namespace LMS.Interfaces
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entryName)
            : base($"Entry {entryName} has not found")
        {

        }

        public EntityNotFoundException(string entryName, int id)
            : base($"Entry {entryName} has not found by id={id}")
        {

        }
    }
    public class EntityNotFoundException<T> : EntityNotFoundException
    {
        public EntityNotFoundException()
            : base(typeof(T).Name)
        {

        }

        public EntityNotFoundException(int id)
            : base(typeof(T).Name, id)
        {

        }
    }
}
