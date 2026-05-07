-- ==========================================
-- DDL Scripts for Hospital Management System
-- ==========================================

-- Create Database
-- CREATE DATABASE HospitalManagementDB;
-- GO
-- USE HospitalManagementDB;
-- GO

-- 1. Patients Table
CREATE TABLE Patients (
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL CHECK (DateOfBirth <= GETDATE()),
    Gender CHAR(1) CHECK (Gender IN ('M', 'F', 'O')),
    ContactNumber VARCHAR(15) UNIQUE NOT NULL,
    Email VARCHAR(100) UNIQUE,
    Address VARCHAR(255),
    RegistrationDate DATETIME DEFAULT GETDATE()
);
GO

-- 2. Doctors Table
CREATE TABLE Doctors (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Specialization VARCHAR(100) NOT NULL,
    ContactNumber VARCHAR(15) UNIQUE NOT NULL,
    Email VARCHAR(100) UNIQUE,
    JoiningDate DATE DEFAULT GETDATE()
);
GO

-- 3. Appointments Table
CREATE TABLE Appointments (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID),
    DoctorID INT NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorID),
    AppointmentDate DATETIME NOT NULL CHECK (AppointmentDate >= GETDATE()),
    Status VARCHAR(20) DEFAULT 'Scheduled' CHECK (Status IN ('Scheduled', 'Completed', 'Cancelled')),
    ReasonForVisit VARCHAR(255)
);
GO

-- 4. Treatments Table
CREATE TABLE Treatments (
    TreatmentID INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentID INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Appointments(AppointmentID),
    Diagnosis VARCHAR(255) NOT NULL,
    Prescription VARCHAR(MAX),
    TreatmentDate DATETIME DEFAULT GETDATE(),
    DoctorNotes VARCHAR(MAX)
);
GO

-- 5. Billing Table
CREATE TABLE Billing (
    BillID INT IDENTITY(1,1) PRIMARY KEY,
    TreatmentID INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Treatments(TreatmentID),
    TotalAmount DECIMAL(10, 2) NOT NULL CHECK (TotalAmount >= 0),
    PaymentStatus VARCHAR(20) DEFAULT 'Pending' CHECK (PaymentStatus IN ('Pending', 'Paid', 'Partial')),
    PaymentDate DATETIME,
    BillingDate DATETIME DEFAULT GETDATE()
);
GO

-- 6. AuditLogs Table (For Triggers)
CREATE TABLE AuditLogs (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    TableName VARCHAR(50),
    Operation VARCHAR(20),
    RecordID INT,
    LogDate DATETIME DEFAULT GETDATE(),
    ChangedBy VARCHAR(50) DEFAULT SYSTEM_USER
);
GO

-- Indexes for Performance
CREATE NONCLUSTERED INDEX IX_Patients_Contact ON Patients(ContactNumber);
CREATE NONCLUSTERED INDEX IX_Appointments_PatientDoctor ON Appointments(PatientID, DoctorID);
CREATE NONCLUSTERED INDEX IX_Billing_Status ON Billing(PaymentStatus);
GO
