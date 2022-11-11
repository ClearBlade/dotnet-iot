﻿using ClearBlade.API.dotnet.client.core;
using ClearBlade.API.dotnet.client.core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearBlade.API.dotnet.client
{
    /// <summary>
    /// Helper class to run tests to test the dot net SDK
    /// </summary>
    public static class RunTests
    {
        #region Test Selection
        static bool bAllTests = false;
        static bool bTest001 = false;
        static bool bTest002 = false;
        static bool bTest003 = false;
        static bool bTest004 = false;
        static bool bTest005 = false;
        static bool bTest006 = false;
        static bool bTest007 = false;
        #endregion

        public static bool Execute(ServiceProvider serviceProvider, ILogger logger)
        {
            // Set which tests to run
            //bTest007 = true;
            // bTest004 = true;
            // bTest005 = true;
            // bTest006 = true;
            // bAllTests = true;

            logger.LogInformation("Running selected DotNet SDK tests");

            RunTestsAsync(serviceProvider, logger).GetAwaiter().GetResult();

            logger.LogInformation("All done!");

            return true;
        }


        /// <summary>
        /// Method to run the tests asynchronously
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        static async Task RunTestsAsync(ServiceProvider serviceProvider, ILogger logger)
        {
            IDeviceService? deviceService = serviceProvider.GetService<IDeviceService>();
            if (deviceService != null)
            {
                MainClient mClient = new MainClient(deviceService);

                var data = new
                {
                    binaryData = "QUJD",
                    versionToUpdate = "1"
                };

                // Test-001 - Obtain list of devices for a particular registry
                if (bTest001 || bAllTests)
                {
                    // TBD - Need to obtain the token from authorization service
                    logger.LogInformation("Running Test-001 - Obtain list of devices for a particular registry");

                    // Create a device to verify if result is correct
                    var resultPre = await mClient.CreateDevice(4, "https://iot-sandbox.clearblade.com",
                                            "92e9f2b60cd482c3b6e19984e48401",
                                            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                            "Test-001-Device", "Test-001-Device");
                    if (!resultPre.Item1 || (resultPre.Item2 == null))
                    {
                        logger.LogInformation("Test-001 - Create Device - Failed");
                    }
                    else
                    {

                        var result = await mClient.GetDevicesList(4, "https://iot-sandbox.clearblade.com",
                                                                    "92e9f2b60cd482c3b6e19984e48401",
                                                                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                                    "projects/ingressdevelopmentenv/locations/us-central1/registries/PD-103-Registry");
                        if (!result.Item1)
                            logger.LogInformation("Test-001 - Failed");
                        else
                        {
                            bool bSuccess = false;                            

                            foreach (var deviceItem in result.Item2)
                            {
                                if(string.Compare(deviceItem.name, "Test-001-Device", true) == 0)
                                {
                                    bSuccess = true;
                                    break;
                                }
                            }
                            if (bSuccess)
                                logger.LogInformation("Test-001 - Succeeded");
                            else
                                logger.LogInformation("Test-001 - Failed");
                        }

                        // Delete the newly created device - cleanup
                        await mClient.DeleteDevice(4, "https://iot-sandbox.clearblade.com",
                                            "92e9f2b60cd482c3b6e19984e48401",
                                            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                            "Test-001-Device", "Test-001-Device");
                    }
                }

                // Test-002 - Send Command to Device
                if (bTest002 || bAllTests)
                {
                    // TBD - Need to obtain the token from authorization service
                    logger.LogInformation("Running Test-002 - Send Command to Device");

                    // Create new device to send command to
                    var resultPre = await mClient.CreateDevice(4, "https://iot-sandbox.clearblade.com",
                                                                "92e9f2b60cd482c3b6e19984e48401",
                                                                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                                "Test-002-Device", "Test-002-Device");
                    if (!resultPre.Item1 || (resultPre.Item2 == null))
                        logger.LogInformation("Test-002 - Failed");

                    // Now send the message to newly create device
                    var result002 = await mClient.SendCommandToDevice(4, "https://iot-sandbox.clearblade.com",
                                                                "92e9f2b60cd482c3b6e19984e48401",
                                                                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                                "Test-002-Device", data);
                    if (!result002)
                        logger.LogInformation("Test-002 - Failed");
                    else
                    {
                        logger.LogInformation("Test-002 - Succeeded");
                    }

                    // Delete the newly created device - cleanup
                    await mClient.DeleteDevice(4, "https://iot-sandbox.clearblade.com",
                                        "92e9f2b60cd482c3b6e19984e48401",
                                        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                        "Test-002-Device", "Test-002-Device");
                }

                // Test-003 - Modify Device config
                if (bTest003 || bAllTests)
                {
                    // TBD - Need to obtain the token from authorization service
                    logger.LogInformation("Running Test-003 - Modify device config");
                    data = new
                    {
                        binaryData = "QUJD",
                        versionToUpdate = "19"
                    };
                    var result003 = await mClient.ModifyCloudToDeviceConfig(4, "https://iot-sandbox.clearblade.com",
                                                                "92e9f2b60cd482c3b6e19984e48401",
                                                                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                                "PD-103-Device", data);
                    if (!result003)
                        logger.LogInformation("Test-003 - Failed");
                    else
                    {
                        logger.LogInformation("Test-003 - Succeeded");
                    }
                }

                // Test-004 - Create Device
                if (bTest004 || bAllTests)
                {
                    logger.LogInformation("Running Test-004 - Create Device");
                    // TBD - Need to obtain the token from authorization service

                    // Delete the device with ID "Test-004-Device" if it already existed.
                    await mClient.DeleteDevice(4, "https://iot-sandbox.clearblade.com",
                                            "92e9f2b60cd482c3b6e19984e48401",
                                            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                            "Test-004-Device", "Test-004-Device");

                    // Create new device
                    var result004 = await mClient.CreateDevice(4, "https://iot-sandbox.clearblade.com",
                                                                "92e9f2b60cd482c3b6e19984e48401",
                                                                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                                "Test-004-Device", "Test-004-Device");
                    if (!result004.Item1 || (result004.Item2 == null))
                        logger.LogInformation("Test-004 - Failed");
                    else
                    {
                        // Verify if the device exists
                        var result = await mClient.GetDevicesList(4, "https://iot-sandbox.clearblade.com",
                                                                    "92e9f2b60cd482c3b6e19984e48401",
                                                                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                                    "projects/ingressdevelopmentenv/locations/us-central1/registries/PD-103-Registry");
                        if (!result.Item1)
                            logger.LogInformation("Test-004 - Failed");
                        else
                        {
                            bool bSuccess = false;

                            foreach (var deviceItem in result.Item2)
                            {
                                if (string.Compare(deviceItem.name, "Test-004-Device", true) == 0)
                                {
                                    bSuccess = true;
                                    break;
                                }
                            }
                            if (bSuccess)
                            {
                                logger.LogInformation("Test-004 - Succeeded");

                                // Delete the newly created device - cleanup
                                await mClient.DeleteDevice(4, "https://iot-sandbox.clearblade.com",
                                                    "92e9f2b60cd482c3b6e19984e48401",
                                                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                    "Test-004-Device", "Test-004-Device");
                            }
                            else
                            {
                                logger.LogInformation("Test-004 - Failed");
                            }
                        }
                    }
                }

                // Test-005 - Delete Device
                if (bTest005 || bAllTests)
                {
                    // TBD - Need to obtain the token from authorization service
                    logger.LogInformation("Running Test-005 - Delete Device");

                    // First create a device to delete it
                    var resultPre = await mClient.CreateDevice(4, "https://iot-sandbox.clearblade.com",
                                            "92e9f2b60cd482c3b6e19984e48401",
                                            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                            "Test-005-Device", "Test-005-Device");
                    if (!resultPre.Item1 || (resultPre.Item2 == null))
                        logger.LogInformation("Test-005 - Failed");
                    else
                    {

                        var result005 = await mClient.DeleteDevice(4, "https://iot-sandbox.clearblade.com",
                                                                "92e9f2b60cd482c3b6e19984e48401",
                                                                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                                "Test-005-Device", "Test-005-Device");
                        if (!result005.Item1 || (result005.Item2 == null))
                            logger.LogInformation("Test-005 - Failed");
                        else
                        {
                            // try to get the device
                            var resultPost = await mClient.GetDevice(4, "https://iot-sandbox.clearblade.com",
                                            "92e9f2b60cd482c3b6e19984e48401",
                                            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                            "Test-005-Device");
                            if (!resultPost.Item1 || (resultPost.Item2 == null))
                                logger.LogInformation("Test-005 - Succeeded"); // Device does not exist means it is deleted
                            else
                                logger.LogInformation("Test-005 - Failed"); // Device still exists. Means test case failed.
                        }
                    }
                }

                // Test-006 - Get Device details
                if (bTest006 || bAllTests)
                {
                    // TBD - Need to obtain the token from authorization service
                    logger.LogInformation("Running Test-006 - Get Device");

                    // First create a device to get its details
                    var resultPre = await mClient.CreateDevice(4, "https://iot-sandbox.clearblade.com",
                        "92e9f2b60cd482c3b6e19984e48401",
                        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                        "Test-006-Device", "Test-006-Device");
                    if (!resultPre.Item1 || (resultPre.Item2 == null))
                        logger.LogInformation("Test-006 - Failed");

                    var result006 = await mClient.GetDevice(4, "https://iot-sandbox.clearblade.com",
                                                                "92e9f2b60cd482c3b6e19984e48401",
                                                                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                                "Test-006-Device");
                    if (!result006.Item1 || (result006.Item2 == null))
                        logger.LogInformation("Test-006 - Failed");
                    else
                    {
                        if(string.Compare(result006.Item2.name, "Test-006-Device", true) == 0)
                            logger.LogInformation("Test-006 - Succeeded");
                        else
                            logger.LogInformation("Test-006 - Failed");

                        // Delete the newly created device - cleanup
                        await mClient.DeleteDevice(4, "https://iot-sandbox.clearblade.com",
                                            "92e9f2b60cd482c3b6e19984e48401",
                                            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                            "Test-006-Device", "Test-006-Device");

                    }
                }

                // Test-007 - Get Device configuration details
                if (bTest007 || bAllTests)
                {
                    // TBD - Need to obtain the token from authorization service
                    logger.LogInformation("Running Test-007 - Get Device");

                    // First create a device to get its configuration details
                    var resultPre = await mClient.CreateDevice(4, "https://iot-sandbox.clearblade.com",
                        "92e9f2b60cd482c3b6e19984e48401",
                        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                        "Test-007-Device", "Test-007-Device");
                    if (!resultPre.Item1 || (resultPre.Item2 == null))
                        logger.LogInformation("Test-007 - Failed - Failed while creating new device");

                    // Next set some configuration information
                    data = new
                    {
                        binaryData = "QUJD",
                        versionToUpdate = "1"
                    };
                    var resultPre1 = await mClient.ModifyCloudToDeviceConfig(4, "https://iot-sandbox.clearblade.com",
                                                                "92e9f2b60cd482c3b6e19984e48401",
                                                                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                                "Test-007-Device", data);
                    if (!resultPre1)
                        logger.LogInformation("Test-007 - Failed - Failed while setting configuration");
                    
                    // Actual test
                    var result007 = await mClient.GetDeviceConfig(4, "https://iot-sandbox.clearblade.com",
                                                                "92e9f2b60cd482c3b6e19984e48401",
                                                                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                                                "Test-007-Device", "2");
                    if (!result007.Item1 || (result007.Item2 == null))
                        logger.LogInformation("Test-007 - Failed");
                    else
                    {
                        if (string.Compare(result007.Item2.binaryData, "QUJD", true) == 0)
                            logger.LogInformation("Test-007 - Succeeded");
                        else
                            logger.LogInformation("Test-007 - Failed");

                        // Delete the newly created device - cleanup
                        await mClient.DeleteDevice(4, "https://iot-sandbox.clearblade.com",
                                            "92e9f2b60cd482c3b6e19984e48401",
                                            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI5Y2U5ZjJiNjBjODhlOWZjY2VkZGQ1YTZkZTBjIiwic2lkIjoiNjJmYzBlMTMtZWNkMy00OTUyLTgyN2UtOTI5YWJlODVkMTY2IiwidXQiOjIsInR0IjoxLCJleHAiOi0xLCJpYXQiOjE2NjgxNzY0NjJ9.M_ptSQZ6Y1qCzC8TszsbYo3Y8pjE56lQW9I4psin3JI",
                                            "Test-007-Device", "Test-007-Device");
                    }
                }

                //Console.ReadLine();
            }
        }

    }
}
