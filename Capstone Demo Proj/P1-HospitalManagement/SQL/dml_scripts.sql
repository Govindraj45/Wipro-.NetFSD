-- ==========================================
-- DML Scripts for Hospital Management System
-- ==========================================

-- Insert sample Doctors
INSERT INTO Doctors (FirstName, LastName, Specialization, ContactNumber, Email, JoiningDate)
VALUES 
('Alice', 'Smith', 'Cardiology', '9876543210', 'alice.smith@hospital.com', '2020-01-15'),
('Bob', 'Jones', 'Neurology', '9876543211', 'bob.jones@hospital.com', '2019-03-22'),
('Charlie', 'Brown', 'Pediatrics', '9876543212', 'charlie.brown@hospital.com', '2021-07-10');
GO

-- Insert sample Patients
INSERT INTO Patients (FirstName, LastName, DateOfBirth, Gender, ContactNumber, Email, Address)
VALUES 
('John', 'Doe', '1985-06-15', 'M', '1112223333', 'john.doe@example.com', '123 Main St, NY'),
('Jane', 'Roe', '1990-09-20', 'F', '4445556666', 'jane.roe@example.com', '456 Elm St, NY'),
('Sam', 'Wilson', '2010-12-05', 'M', '7778889999', 'sam.wilson@example.com', '789 Oak St, NY');
GO

-- Insert sample Appointments (Dates must be >= current date as per CHECK constraint, using GETDATE() + x)
INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Status, ReasonForVisit)
VALUES 
(1, 1, DATEADD(day, 1, GETDATE()), 'Completed', 'Chest pain'),
(2, 2, DATEADD(day, 2, GETDATE()), 'Scheduled', 'Frequent headaches'),
(3, 3, DATEADD(day, 3, GETDATE()), 'Completed', 'Routine checkup');
GO

-- Insert sample Treatments (Assume Appointment 1 and 3 are completed)
INSERT INTO Treatments (AppointmentID, Diagnosis, Prescription, DoctorNotes)
VALUES 
(1, 'Mild Angina', 'Rest, Aspirin 75mg daily', 'Patient advised to avoid heavy lifting.'),
(3, 'Healthy', 'Vitamins', 'All vitals normal.');
GO

-- Insert sample Billing
INSERT INTO Billing (TreatmentID, TotalAmount, PaymentStatus, PaymentDate)
VALUES 
(1, 1500.00, 'Paid', GETDATE()),
(2, 200.00, 'Pending', NULL);
GO
