﻿using System;
using System.Linq;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TestSessionService : BaseService
    {
        public TestSessionService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public TestSessionDTO GetById(int id)
        {
            var template = unitOfWork.TestSessions.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<TestSession>(id);
            }

            return mapper.Map<TestSession, TestSessionDTO>(template);
        }

        public TestSessionResultsDTO GetResults(int id)
        {
            var template = unitOfWork.TestSessions.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<TestSession>(id);
            }

            return mapper.Map<TestSession, TestSessionResultsDTO>(template);
        }

        public ExameneeResultDTO GetExameneeResult(int sessionId, string exameneeId)
        {
            var template = unitOfWork.TestSessions.Get(sessionId);
            if (template == null)
            {
                throw new EntityNotFoundException<TestSession>(sessionId);
            }

            var user = template.Members.FirstOrDefault(m => m.UserId == exameneeId);
            if (user == null)
            {
                throw new EntityNotFoundException<TestSessionUser>();
            }

            return mapper.Map<TestSessionUser, ExameneeResultDTO>(user);
        }

        public Task DeleteByIdAsync(int id)
        {
            unitOfWork.TestSessions.Delete(id);
            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TestSessionDTO testSessionDTO)
        {
            if (testSessionDTO == null)
            {
                throw new ArgumentNullException(nameof(testSessionDTO));
            }
            if (!testSessionDTO.TestIds.Any())
            {
                throw new ArgumentException("Test session should contains at least one test");
            }
            if (!testSessionDTO.MemberIds.Any())
            {
                throw new ArgumentException("Test session shoul contains at least one examenee");
            }

            var testSession = mapper.Map<TestSessionDTO, TestSession>(testSessionDTO);

            unitOfWork.TestSessions.Update(testSession);

            return unitOfWork.SaveAsync();
        }

        public Task CreateAsync(TestSessionDTO testSessionDTO)
        {
            if (testSessionDTO == null)
            {
                throw new ArgumentNullException(nameof(testSessionDTO));
            }
            if (!testSessionDTO.TestIds.Any())
            {
                throw new ArgumentException("Test session should contains at least one test");
            }
            if (!testSessionDTO.MemberIds.Any())
            {
                throw new ArgumentException("Test session shoul contains at least one examenee");
            }

            var testSession = mapper.Map<TestSessionDTO, TestSession>(testSessionDTO);

            unitOfWork.TestSessions.Create(testSession);

            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TestSessionDTO> GetAll()
        {
            return mapper
                .Map<IEnumerable<TestSession>, IEnumerable<TestSessionDTO>>(
                    unitOfWork.TestSessions.GetAll());
        }
    }
}