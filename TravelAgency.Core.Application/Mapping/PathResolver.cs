using System;
using Microsoft.Extensions.Configuration;

namespace TravelAgency.Core.Application.Mapping
{
    public static class PathResolver
    {
        public static string ResolvePath(IConfiguration configuration, string fullPath)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (string.IsNullOrWhiteSpace(fullPath))
                throw new ArgumentException("Full path cannot be null or empty.", nameof(fullPath));

            // Get the base URL from configuration
            var baseUrl = configuration["Urls:ApiBaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                throw new InvalidOperationException("Base URL is not configured in appsettings.json.");

            // Strip 'file://' prefix if it's present
            if (fullPath.StartsWith("file://"))
            {
                fullPath = fullPath.Substring(7); // Remove 'file://' from the beginning
            }

            // Map the local file system path to the relative URL by replacing the root path with the wwwroot path
            // Example: D:/SDA Project/TravelAgency/TravelAgency.APIs/images/hotels/Sleep-siwa.jpg
            // Should become: /images/hotels/Sleep-siwa.jpg

            // This assumes that 'wwwroot' is in the project directory and your images are inside it
            string imageRelativePath = fullPath.Replace("D:/SDA Project/TravelAgency/TravelAgency.APIs/wwwroot", "");
            imageRelativePath = imageRelativePath.Replace("\\", "/"); // Ensure forward slashes

            // Combine the base URL with the relative path
            var resolvedPath = new Uri(new Uri(baseUrl), imageRelativePath).ToString();
            return resolvedPath;
        }
    }
}
