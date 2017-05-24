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

            return valuesController;
        }

        private void AddRuleToSite(ValuesController valuesController,
            string site = "yandex.ru", string rule = "//ol")
        {
            valuesController.Add(site, rule);
        }
        
        #endregion

        #region конструктор
        [TestMethod]
        public void CanCreateValuesController()
        {
            // Arrange
            var clientFactory = new HttpClientFactory(); ;

            // Act
            var valuesController = new ValuesController(clientFactory);

            // Assert
            // без ошибок
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

        public void TwiceAddRuleToSite()
        {
            // Arrange
            var valuesController = CreateValuesController();

            // Act
            AddRuleToSite(valuesController);
            AddRuleToSite(valuesController);

            // Assert
            // без ошибок
        }
        #endregion

        #region ValuesController.Get
        [TestMethod]
        public void GetAddedRule()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";
            string rule = "//ol";

            // Act
            valuesController.Add(site, rule);
            object result = valuesController.Get(site);

            // Assert
            Assert.AreEqual(rule, result);
        }

        [TestMethod]
        public void GetNotAddedRule()
        {
            // Arrange
            var valuesController = CreateValuesController();

            // Act
            object result = valuesController.Get("vk.com");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TwiceGetAddedRule()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";
            string rule = "//ol";

            // Act
            valuesController.Add(site, rule);
            valuesController.Get(site);
            object result = valuesController.Get(site);

            // Assert
            Assert.AreEqual(rule, result);
        }
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

        #region ValuesController.Delete
        [TestMethod]
        public void DeleteAddedRule()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";
            string rule = "//ol";

            // Act
            valuesController.Add(site, rule);
            valuesController.Delete(site);

            // Assert
            // без ошибок
        }


        [TestMethod]
        public void DeleteNotAddedRule()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";

            // Act
            valuesController.Delete(site);

            // Assert
            // без ошибок
        }

        [TestMethod]
        public void DeleteRuleAfterGet()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";
            string rule = "//ol";

            // Act
            valuesController.Add(site, rule);
            valuesController.Get(site);
            valuesController.Delete(site);

            // Assert
            // без ошибок
        }

        [TestMethod]
        public void TwiceDeleteRule()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";
            string rule = "//ol";

            // Act
            valuesController.Add(site, rule);
            valuesController.Delete(site);
            valuesController.Delete(site);

            // Assert
            // без ошибок
        }
        #endregion

        #region ValuesController.SpellErrors
        [TestMethod]
        public void GetSpellErrorsFromNameOfSite()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";
            AddRuleToSite(valuesController);

            // Act
            var errors = valuesController.SpellErrors(site);

            // Assert
            // без ошибок
        }

        [TestMethod]
        public void GetSpellErrorsFromUrlOfSite()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "http://news.yandex.ru";
            AddRuleToSite(valuesController);

            // Act
            var errors = valuesController.SpellErrors(site);

            // Assert
            // без ошибок
        }

        [TestMethod]
        public void GetSpellErrorsFromUnreachableSite()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string unreachableSite = "slahjdfdshfl";
            AddRuleToSite(valuesController, site: unreachableSite);

            // Act
            var errors = valuesController.SpellErrors(unreachableSite);

            // Assert
            // без ошибок
        }

        [TestMethod]
        public void GetSpellErrorsFromNotAddedSite()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";
            string notAddedSite = "vk.com";
            AddRuleToSite(valuesController, site);

            // Act
            var errors = valuesController.SpellErrors(notAddedSite);

            // Assert
            Assert.IsNull(errors, "Метод не должен возвращать значение для недобавленного сайта");
        }

        [TestMethod]
        public void GetSpellErrorsFromIncorrectRule()
        {
            // Arrange
            var valuesController = CreateValuesController();
            string site = "yandex.ru";
            string incorrectRule = ";sdjfdshjf";
            AddRuleToSite(valuesController, site, incorrectRule);

            // Act
            var errors = valuesController.SpellErrors(site);

            // Assert
            Assert.IsNull(errors, "Метод не должен возвращать значение для непраильного правила");
        }
        #endregion

        #region  ValuesController.SpellErrorsCount
        [TestMethod]
        public void GetSpellErrorsCount()
        {
            // Arrange
            var valuesController = CreateValuesController();
            var site = "yandex.ru";
            AddRuleToSite(valuesController, site);

            // Act
            int errorsCount = valuesController.SpellErrorsCount(site);

            // Assert
            Assert.IsTrue(errorsCount >= 0, "Число ошибок не может быть отрицательным");
        }
        #endregion
    }
}
