﻿using OMG.LunchPicker.Objects.Entities;
using OMG.LunchPicker.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Services
{
    public class CuisineService : ICuisineService
    {
        #region Private Fields
        /// <summary>
        /// The repository
        /// </summary>
        private readonly ICuisineRepository _repository;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CuisineService(ICuisineRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region ICuisineService Members
        public async Task<IQueryable<dynamic>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<dynamic> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<int> SaveAsync(Cuisine cuisine)
        {
            return await _repository.SaveAsync(cuisine);
        }
        #endregion
    }
}