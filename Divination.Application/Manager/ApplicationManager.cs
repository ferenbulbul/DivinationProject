using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Divination.Application.Manager
{
    public class ApplicationManager : IApplicationService
    {
        private readonly ICategoryRepository _categoryservice;
        private readonly IApplicationRepository _applicationService;
        private readonly IAnswerRepository _answerService;
        private readonly IFortuneTellerRepository _fortuneService;

        public ApplicationManager(IApplicationRepository service, ICategoryRepository categoryservice, IAnswerRepository answerService, IFortuneTellerRepository fortuneService)
        {
            _applicationService = service;
            _categoryservice = categoryservice;
            _answerService = answerService;
            _fortuneService = fortuneService;
        }
        public async Task AddAplication(ApplicationDto applicationDto)
        {
            var images = new List<byte[]>();

            foreach (var photo in new List<string> { applicationDto.Photo1, applicationDto.Photo2, applicationDto.Photo3 })
            {
                if (!string.IsNullOrEmpty(photo))
                {
                    try
                    {
                        byte[] imageBytes = Convert.FromBase64String(photo);
                        images.Add(imageBytes);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Invalid base64 string: " + ex.Message);
                    }
                }
            }
            var application = new Applications
            {
                ImageData1 = images.ElementAtOrDefault(0),
                ImageData2 = images.ElementAtOrDefault(1),
                ImageData3 = images.ElementAtOrDefault(2),
                ClientId = applicationDto.ClientId,
                FortunetellerId = applicationDto.FortunetellerId,
                Categories = new List<Category>(),
                IsAnswer = false,
                CreatedDate = DateTime.Now

            };

            foreach (var categoryId in applicationDto.CategoryIds)
            {
                var category = await _categoryservice.GetByIdAsync(categoryId);
                if (category != null)
                {
                    application.Categories.Add(category);
                }
            }
            try
            {
                await _applicationService.AddAsync(application);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }

                Console.WriteLine(ex.Message);
            }

        }


        public async Task AddAnswer(int id, string answer)
        {
            var applicationIsAnswer = await _applicationService.GetByIdAsync(id);
            applicationIsAnswer.IsAnswer = true;

            var ans = new Answer
            {
                ApplicationsId = id,
                Answers = answer
            };

            await _answerService.AddAsync(ans);
            await _applicationService.IsAnswerTrue(id);

        }

        public async Task<IEnumerable<GetApplicationDto>?> GetApplications(int fortuneTellerId)
        {
            try
            {

                var appList = await _applicationService.GetApplicationsIsAnswerFalseAsync(fortuneTellerId);

                var resaultList = new List<GetApplicationDto>();

                foreach (var app in appList)
                {
                    var application = new GetApplicationDto
                    {
                        ImageData1 = Convert.ToBase64String(app.ImageData1),
                        ImageData2 = Convert.ToBase64String(app.ImageData2),
                        ImageData3 = Convert.ToBase64String(app.ImageData3),
                        Id = app.Id,
                        FirstName = app.Client.FirstName,
                        LastName = app.Client.LastName,
                        MaritalStatus = app.Client.Occupation,
                        Gender = app.Client.Gender,
                        Categories = app.Categories.Select(ac => ac.CategoryName).ToList(),
                        CreateDate = app.CreatedDate,
                        Occupation = app.Client.Occupation,
                        BirthDate = app.Client.DateofBirth
                    };
                    resaultList.Add(application);
                }
                return resaultList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem fetching the applications.", ex);
            }
        }


        public async Task<IEnumerable<GetApplicationAnsweredTrueDto>?> GetApplicationAnsweredTrue(int fortuneTellerId)
        {
            try
            {

                var appList = await _applicationService.GetApplicationsIsAnswerTrueAsync(fortuneTellerId);

                var resaultList = new List<GetApplicationAnsweredTrueDto>();

                foreach (var app in appList)
                {
                    var application = new GetApplicationAnsweredTrueDto
                    {
                        ImageData1 = Convert.ToBase64String(app.ImageData1),
                        ImageData2 = Convert.ToBase64String(app.ImageData2),
                        ImageData3 = Convert.ToBase64String(app.ImageData3),
                        Id = app.Id,
                        FirstName = app.Client.FirstName,
                        LastName = app.Client.LastName,
                        MaritalStatus = app.Client.Occupation,
                        Gender = app.Client.Gender,
                        Categories = app.Categories.Select(ac => ac.CategoryName).ToList(),
                        CreateDate = app.CreatedDate,
                        Occupation = app.Client.Occupation,
                        BirthDate = app.Client.DateofBirth,
                        Answer = app.Answer.Answers

                    };
                    resaultList.Add(application);
                }
                return resaultList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem fetching the applications.", ex);
            }
        }


        public async Task<IEnumerable<GetApplicationByClientIdDto>> GetApplicationsByClientIdIsAnsweredTrue(int clientId)
        {
            try
            {
                var appList = await _applicationService.GetApplicationByClientIdIsAnsweredTrueAsync(clientId);
                var resaultList = new List<GetApplicationByClientIdDto>();

                foreach (var app in appList)
                {
                    var application = new GetApplicationByClientIdDto
                    {
                        Id = app.Id,
                        Answer = app.Answer.Answers,
                        CreateDate = app.CreatedDate,
                        FortunetellerFirstName = app.FortuneTeller.FirstName,
                        FortunetellerLastName = app.FortuneTeller.LastName,
                        Categories = app.Categories.Select(ac => ac.CategoryName).ToList()
                    };
                    resaultList.Add(application);
                }
                return resaultList;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem fetching the applications.", ex);
            }
        }


        public async Task<IEnumerable<GetApplicationByClientIsAnsweredFalseDto>> GetApplicationsByClientIdIsAnsweredFalse(int clientId)
        {
            try
            {
                var appList = await _applicationService.GetApplicationByClientIdIsAnsweredFalseAsync(clientId);
                var resaultList = new List<GetApplicationByClientIsAnsweredFalseDto>();

                foreach (var app in appList)
                {
                    var application = new GetApplicationByClientIsAnsweredFalseDto
                    {
                        Id = app.Id,
                        CreateDate = app.CreatedDate,
                        FortunetellerFirstName = app.FortuneTeller.FirstName,
                        FortunetellerLastName = app.FortuneTeller.LastName,
                        Categories = app.Categories.Select(ac => ac.CategoryName).ToList()
                    };
                    resaultList.Add(application);
                }
                return resaultList;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem fetching the applications.", ex);
            }
        }

        public async Task ScoreFotrune(int applicationId, float score)
        {
            try
            {
                await _answerService.ScoreFortuneAsync(applicationId, score);
                var fortunetellerId=await _applicationService.GetFortuneTellerIdByApplicationId(applicationId);
                var fortuneTellerRating = await _answerService.GetAverageScoreForFortuneTellerAsync(fortunetellerId);
                await _fortuneService.UpdateRating(fortunetellerId, fortuneTellerRating);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem .", ex);
            }
        }
    }


}