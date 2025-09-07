# HeavyStringFiltering

This project is built on **.NET 8 SDK** and follows a strict **N-Layered** architecture consisting of **Presentation**, **Business**, and **DataAccess** layers.
Its primary purpose is to efficiently handle large text uploads (up to 100 MB) by processing them in chunks and filtering the content asynchronously in the background.

The filtering mechanism is implemented using the Levenshtein algorithm to detect and remove words that are similar to predefined filter terms.
To ensure maximum performance and transparency, the solution avoids the use of third-party libraries, relying solely on in-memory data structures and the .NET runtime.

All unit tests are located in the **HeavyStringFiltering.Business.Tests** project.
These tests cover the filtering logic (including word extraction, bad-word detection, and string regeneration)
The test project uses the **MSTest** framework for assertions and test execution.

To run the application, ensure that the .NET 8 SDK is installed on your system.
Navigate to the **HeavyStringFiltering.WebApi** project directory and start the API.