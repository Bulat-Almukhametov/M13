using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M13.InterviewProject.Controllers;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace M13.InterviewProject.Test
{
    [TestClass]
    public class ControllerTest
    {
        #region методы, используемые внутри тестовых методов
        private ValuesController CreateValuesController()
        {
            var clientFactory = new HttpClientFactory();
            var valuesController = new ValuesController(clientFactory);

            valuesController.ControllerContext = new ControllerContext();
            
            
            return valuesController;
        }

        private void AddRuleToSite(ValuesController valuesController,
            string site = "yandex.ru", string rule = "//ol")
        {
            valuesController.Add(site, rule);
        }
        #endregion


        #region ValuesController.Add
        [TestMethod]
        public void CanAddRuleToSite()
        {
            // Arrange
            var valuesController = CreateValuesController();

            // Act
            AddRuleToSite(valuesController);

            // Assert
            // без ошибок
        }

        [TestMethod]
        public void CanAddIncorrectRuleToSite()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string incorrectRule = @"\\ol";

            // Act
            AddRuleToSite(valuesController, rule:incorrectRule);

            // Assert
            // без ошибок
        }
        #endregion

        #region ValuesController.Get

        #endregion


        #region ValuesController.Test
        [TestMethod]
        public void TestRulesForAddedSite()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";
            AddRuleToSite(valuesController);

            // Act
            valuesController.Test(site);

            // Assert
            // без ошибок
        }

        [TestMethod]
        public void TestRulesWhenNoSiteAdded()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "vk.com";

            // Act
            valuesController.Test(site);

            // Assert
            // без ошибок
        }

        [TestMethod]
        public void TestRulesForNotAddedSite()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "vk.com";
            AddRuleToSite(valuesController);

            // Act
            valuesController.Test(site);

            // Assert
            // без ошибок
        }

        [TestMethod]
        public void TestIncorrectRulesForAddedSite()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";
            string rule = @"\\ol";
            AddRuleToSite(valuesController, site, rule);

            // Act
            valuesController.Test(site);

            // Assert
            // без ошибок
        }
        #endregion
    }
}
