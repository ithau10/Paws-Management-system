# PAWS Management System

## Overview

PAWS Management System is a comprehensive software solution designed to streamline operations for pet stores and animal shelters. It provides a user-friendly interface for managing inventory, customers, and transactions effectively.

## Screenshots

![Screenshot 1](https://github.com/ithau10/Paws-System/assets/107179973/ac5b0eb1-e9f8-4e40-99f2-73306643f3b4)
![Screenshot 2](https://github.com/ithau10/Paws-System/assets/107179973/b42078ba-5e7a-4a08-bb05-fbf94b2a9cbb)
![Screenshot 3](https://github.com/ithau10/Paws-System/assets/107179973/ee5b96fb-b565-4738-90b9-869c5fd13989)
![Screenshot 4](https://github.com/ithau10/Paws-System/assets/107179973/a2fec668-02fb-4415-af50-a466cf5e1575)
![Screenshot 5](https://github.com/ithau10/Paws-System/assets/107179973/619b4df0-c918-48e3-9a3b-94a7fb378380)
![Screenshot 6](https://github.com/ithau10/Paws-System/assets/107179973/3c93fdc4-8e5e-4d32-9fc1-2f24a570b0ff)
![Screenshot 7](https://github.com/ithau10/Paws-System/assets/107179973/8ec876bc-330c-475f-85d6-d221d27ae955)

## Dependencies

- Visual Studio 2019 (Community Version)
- Microsoft SQL Server Tool 18

## NuGet Packages

Restore these packages by right-clicking on the solution in Solution Explorer and selecting "Restore NuGet Packages"

- microsoft.sqlserver.Types 160.1000.6
- microsoft.reportviewer.Winforms 10.0.40219.1
- microsoft.reportingservicesReportViewerControl.Winforms 160.1000.6
- gunaui 2.0.2.5

## Installation and Running

Follow these steps to install and run the PAWS Management System:

1. **Download and Extract:**
   - Download the Zip file of the PAWS Management System and extract its contents to your desired location on your computer.

2. **Update Connection String:**
   - Open DbConnect.cs file located in the project directory.
   - Update the connection string to match the one in your database. This ensures that the system connects to your database properly.

3. **Open in Visual Studio:**
   - Open Visual Studio 2019 (Community Version) on your computer.
   - Navigate to the location where you extracted the PAWS Management System files and open the solution (.sln) file.

4. **Restore NuGet Packages:**
   - Right-click on the solution in Solution Explorer and select "Restore NuGet Packages". This will ensure that all necessary dependencies are installed correctly.

5. **Build Solution:**
   - Build the solution by clicking on the "Build" menu and selecting "Build Solution". This will compile the code and ensure that everything is set up properly.

6. **Run the Application:**
   - Once the solution has been built successfully, you can run the application by clicking on the "Start" button in Visual Studio or by pressing F5.
   - This will launch the PAWS Management System, allowing you to explore its functionalities and features.

7. **Usage:**
   - Upon launching the application, you can log in using the provided login credentials for admins and cashiers.
   - Admins have the privilege to register users, while cashiers have limited access.
   - You can then register customers and pets, process payments, view reports, and utilize the search functionality as needed.

8. **Feedback and Support:**
   - If you encounter any issues during installation or while running the application, feel free to reach out for assistance.
   - Additionally, we welcome any feedback or suggestions for improving the PAWS Management System.


## Project Description

**PAWS Management System** facilitates the following functionalities:

- User login for admins and cashiers.
- Admins can register users, while cashiers cannot.
- Users can register customers and pets, capturing details such as species, category, vaccination status, transaction mode (buy or donate), quantity, and photo.
- Payment processing allows users to control inventory, select payment method (cash or donation), and generate payment receipts or donation agreement forms.
- Reporting feature enables users to filter information by dates.
- Search functionality allows users to search across the entire project.




Enjoy using the PAWS Management System to streamline operations for your pet store or animal shelter!

