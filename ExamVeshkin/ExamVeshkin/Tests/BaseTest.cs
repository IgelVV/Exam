﻿using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using ExamVeshkin.Forms;
using ExamVeshkin.Extensions;

namespace ExamVeshkin.Tests
{
    public abstract class BaseTest
    {
        protected static string ScenarioName
            => TestContext.CurrentContext.Test.Properties.Get("Description")?.ToString()
            ?? TestContext.CurrentContext.Test.Name.Replace("_", string.Empty);

        private static Logger Logger => Logger.Instance;

        private API.API? _api;
        private HomePage? _homePage;
        private ProjectPage? _projectPage;
        private AddProjectPage? _addProjectPage;
        private string? _sid;

        protected API.API Api => _api ??= new();
        protected HomePage HomePage => _homePage ??= new();
        protected ProjectPage ProjectPage => _projectPage ??= new();
        protected AddProjectPage AddProjectPage => _addProjectPage ??= new();
        protected string Sid => _sid ??= DateTime.Now.ToUnixTimeMilliSeconds();

        [SetUp]
        public void Setup()
        {
            Logger.Info($"Start scenario [{ScenarioName}]");
        }

        [TearDown]
        public virtual void AfterEach()
        {
            Logger.Info($"Finished scenario [{ScenarioName}]");

            if (AqualityServices.IsBrowserStarted)
            {
                AqualityServices.Browser.Quit();
            }
        }
    }
}