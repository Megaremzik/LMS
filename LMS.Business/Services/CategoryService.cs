﻿using System.Collections.Generic;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;
using System.Linq;
using System;
using System.Threading.Tasks;


namespace LMS.Business.Services
{
    public class CategoryService : BaseService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public System.Threading.Tasks.Task DeleteAsync(int categoryId)
        {
            unitOfWork.Categories.Delete(categoryId);   

            return unitOfWork.SaveAsync();
        }

        public System.Threading.Tasks.Task CreateAsync(CategoryDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (!string.IsNullOrEmpty(category.Title))
            {
                var entry = mapper.Map<CategoryDTO, Entities.Category>(category);

                unitOfWork.Categories.Create(entry);
            }

            return unitOfWork.SaveAsync();
        }

        public System.Threading.Tasks.Task UpdateAsync(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryDTO));
            }
            if (!string.IsNullOrEmpty(categoryDTO.Title))
            {             

                if (mapper.Map<CategoryDTO, Entities.Category>(categoryDTO) is Entities.Category category)
                {                    
                    unitOfWork.Categories.Update(category);
                }
                else
                {
                    return CreateAsync(categoryDTO);
                }
            }

        

            return unitOfWork.SaveAsync();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = unitOfWork.Categories.GetAll();
            var categoriesDTO = mapper.Map<IEnumerable<Entities.Category>, IEnumerable<CategoryDTO>>(categories);


            foreach (var category in categoriesDTO)
            {
                category.TasksCount = unitOfWork.Tasks.Filter(b => b.CategoryId == category.Id).Count();
            }

            return categoriesDTO;
        }

        public CategoryDTO GetById(int categoryId)
        {
            var category = unitOfWork.Categories.Get(categoryId);
            if (category == null)
            {
                throw new EntityNotFoundException<Entities.Category>(categoryId);
            }

            return mapper.Map<Entities.Category, CategoryDTO>(category);
        }

        public virtual IEnumerable<CategoryDTO> Filter(System.Linq.Expressions.Expression<Func<Category, bool>> filter)
        {
            return mapper.Map<IEnumerable<Entities.Category>, IEnumerable<CategoryDTO>>(unitOfWork.Categories.Filter(filter));
        }

    }
}
