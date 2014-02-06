using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Box.V2.Services;
using System.Threading.Tasks;
using Box.V2.Auth;
using System.Collections.Generic;
using Box.V2.Exceptions;
using System.Linq;
using Box.V2.Request;
using Box.V2.Config;
using Box.V2.Managers;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxResourcePluginsTest : BoxResourceManagerTest
    {
    
        [TestMethod]
        public void InitializePlugins_ValidResource_ValidPlugins()
        {
            // Arrange
            BoxClient client = new BoxClient(_config.Object);

            // Act
            client
                .AddResourcePlugin<BoxFilesManager>()
                .AddResourcePlugin<BoxFoldersManager>()
                .AddResourcePlugin<BoxCommentsManager>()
                .AddResourcePlugin<BoxGroupsManager>();

            var fm = client.ResourcePlugins.Get<BoxFilesManager>();
            var dm = client.ResourcePlugins.Get<BoxFoldersManager>();
            var cm = client.ResourcePlugins.Get<BoxCommentsManager>();
            var gm = client.ResourcePlugins.Get<BoxGroupsManager>();

            // Assert
            Assert.IsNotNull(fm);
            Assert.IsNotNull(dm);
            Assert.IsNotNull(cm);
            Assert.IsNotNull(gm);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InitializePlugins_UnregisteredResource_InvalidOperationException()
        {
            // Arrange
            BoxClient client = new BoxClient(_config.Object);

            // Act
            client.AddResourcePlugin<BoxFilesManager>();

            // Assert
            var dm = client.ResourcePlugins.Get<BoxFoldersManager>();
        }

    }
}
