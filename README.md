
-----

# Soft Books Payroll System

## 💰 Low-Cost, Dynamic, and Compliant Payroll for Kenyan Businesses

Soft Books Payroll is a comprehensive, multi-user, Client/Server payroll management system designed to provide small, mid-sized, and large organizations with a powerful, yet low-cost, solution for managing their workforce compensation. It is built to be **dynamic** and **fully compliant** with the ever-changing tax landscape in Kenya, ensuring businesses stay ahead of government policy changes.

## ✨ Key Features

This system integrates all aspects of payroll functions, offering unparalleled flexibility and control:

  * **Unlimited Capacity:** Supports an **unlimited number of employees** and **users**.
  * **Multi-User Client/Server Architecture:** A desktop application built with C\# Windows Forms connects to a central Microsoft SQL Server database for secure, multi-user access.
  * **Comprehensive Financial Management:**
      * Tracking and management of **Benefit Accruals**.
      * Support for multiple **Deduction Types**.
      * Integrated module for **Pension Disbursements** management.
  * **Compliance & Reporting:** Provides essential **compliance reporting tools** necessary for meeting regulatory requirements in Kenya.
  * **Kenyan Tax Focus:** Designed to be **dynamic** and easily updated to keep abreast of rapid tax policy changes implemented by the Kenyan government (KRA).
  * **Versatile Use:** Ideal for in-house use, hosted environments, or for accountants representing multiple client organizations.

-----

## 💻 Installation

The installation process involves two main components: the **Microsoft SQL Server Database** (backend) and the **C\# Windows Forms Application** (client).

### Server Setup (SQL Server)

1.  **Install SQL Server:** Install a supported version of Microsoft SQL Server on a central server.
2.  **Restore Database:** Restore the project database from the provided backup file (`SoftBooksPayroll.bak`) or run the SQL scripts to create the schema and tables.
3.  **Create User:** Create a dedicated SQL Server user with appropriate permissions for the application to access the database.
4.  **Configure Network Access:** Ensure the server is configured to accept remote connections and that the firewall allows incoming traffic on the SQL Server port (default is **1433**).

### Client Setup (C\# Windows Forms Application)

There are two methods for setting up the client application on a user's workstation:

#### **Method 1: Using the Installer**

The easiest way to install the application is by running the provided installer.

1.  Locate the `setup.msi` file in the project's `dist` or `publish` folder.
2.  Double-click the `setup.msi` file to launch the installation wizard.
3.  Follow the on-screen instructions to complete the installation.
4.  After installation, you may need to manually update the database connection string in the application's configuration file.

#### **Method 2: Building from Source**

This method is recommended for developers who need to modify the code.

1.  **Clone the Repository:**
    ```bash
    git clone https://github.com/iProjects/soft_books_payroll.git
    ```
2.  **Open in Visual Studio:** Open the solution file (`SoftBooksPayroll.sln`) in **Visual Studio 2010**.
3.  **Configure Connection String:** Navigate to the application's configuration file (`App.config` or similar) and update the database **connection string** to point to your SQL Server instance.
4.  **Build and Run:** Build the solution and run the application.

-----

## 🚀 Usage

The Soft Books Payroll system provides a user-friendly interface for managing all payroll-related tasks.

### Administration & Configuration

  * **Users Management:** Create, modify, and manage user accounts with secure access credentials.
  * **Roles & Rights Management:** Assign specific roles and permissions to control user access to different parts of the system.
  * **Database Control Panel:** An administrative panel for database maintenance and operations, including:
      * **Database Backup & Restore:** Perform backups and restores of the entire payroll database.
      * **Create Database:** Tools for creating a new database instance.
      * **Create Database Users:** Functionality to create and manage database users directly from the application.
  * **Settings Management:** Configure general system settings, including company policies and payroll rules.

### Employer Management

  * **Company Profile:** Define and manage the company's profile, including details such as KRA PIN, NSSF, and NHIF numbers, which are essential for statutory filings.
  * **Banks & Branches Management:** Manage company bank accounts and their associated branches for payroll disbursements.
  * **Department and Branch Management:** Organize employees into different departments and branches for structured payroll processing and reporting.

### Employee & Payee Management

  * **Employee Profiles:** Add, edit, and manage employee profiles, including salary details, benefits, and deductions.
  * **Payee Management:** Manage various payees, including individuals and organizations, for payments outside of regular salaries.

### Payroll Processing

  * **Payroll Items Management:** Define and manage various payroll items such as allowances, commissions, and bonuses.
  * **Payroll Cycles:** Run periodic payroll cycles (monthly, bi-weekly, etc.). The system automatically calculates **PAYE (Tax)**, **NHIF**, **NSSF**, and other statutory deductions based on current Kenyan legislation.
  * **Tax Calculator:** A built-in tax calculator for accurate tax computations.
  * **NSSF & NHIF Management:** Dedicated modules for managing and reporting on NSSF and NHIF contributions.

### Financial Management

  * **Deduction/Benefit Tracking:** Input and track complex benefit accruals (e.g., leave, sick days) and various customizable deductions.
  * **Disbursements:** Manage and track pension and other disbursement processes.

### Reporting

The system provides a comprehensive suite of reports and statements to meet all business and statutory requirements:

  * **Payroll Reports:**
      * **Payroll Master**
      * **Payroll Statement**
      * **Payslip for All Employees**
      * **Single Employee Payslip**
      * **Netsalary Report**
      * **Advanced Payments**
  * **Statutory Reports (Kenya):**
      * **P9A, P9B, P10A, P10, P11**
      * **NSSF Deductions**
      * **NHIF Deductions**
  * **Financial & Bank Reports:**
      * **Bank Transfer**
      * **Bank Branch Transfer**
      * **SACCO Payments Schedule**
  * **Management & Employee Reports:**
      * **Employees List**
      * **Departments Report**
      * **Payee Report**
      * **Loan Repayment Schedules**
      * **Schedules and Statements** 
  * **Export Options:** All reports can be viewed and exported in various formats, including **PDF** and **Excel**, for easy sharing and analysis.

-----

## 🛠️ Technology Stack

  * **Client:** C\# / .NET Framework (Windows Forms)
  * **Development Environment:** Visual Studio 2010
  * **Database:** Microsoft SQL Server

-----

## 📜 License

This project is released under the **[Insert License Here (e.g., MIT License, GPL)]**. Please see the `LICENSE` file for more details.

-----

## 🤝 Contribution

Contributions are welcome\! If you would like to contribute to the development of this compliant payroll system, please:

1.  Fork the repository.
2.  Create a new feature branch (`git checkout -b feature/AmazingFeature`).
3.  Commit your changes (`git commit -m 'Add amazing feature'`).
4.  Push to the branch (`git push origin feature/AmazingFeature`).
5.  Open a Pull Request.

-----

## 📧 Contact

For support, feature requests, or collaboration, please contact the project maintainers.

| Role | Contact |
| :--- | :--- |
| **Project Lead** | Kevin Mutugi Kithinji |
| **Email** | kevinmutugikithinji254@gmail.com |
| **Website** | [https://kevin-mutugi-kithinji-portfolio.onrender.com/](https://kevin-mutugi-kithinji-portfolio.onrender.com/) |