# Normalization Report: Hospital Management System

## Overview
This document explains the database normalization process applied to the Hospital Management System to achieve 3rd Normal Form (3NF) / Boyce-Codd Normal Form (BCNF).

## Unnormalized Form (UNF)
Initially, data might be recorded in a single table with repeating groups and redundant data:
`PatientName, PatientDOB, DoctorName, DoctorSpecialization, AppointmentDate, TreatmentDiagnosis, TotalBillAmount`

## First Normal Form (1NF)
**Rule:** Eliminate repeating groups. Ensure each column contains atomic (indivisible) values and each row has a unique identifier.
**Action:** 
- Separated entities into distinct tables with primary keys.
- Ensured names are split into `FirstName` and `LastName`.

## Second Normal Form (2NF)
**Rule:** Must be in 1NF. All non-key attributes must be fully functionally dependent on the primary key (no partial dependency).
**Action:**
- An appointment involves both a Patient and a Doctor. If stored together, Patient details would depend only on `PatientID` and Doctor details only on `DoctorID`, leading to partial dependency.
- Created `Patients` (PK: PatientID), `Doctors` (PK: DoctorID), and `Appointments` (PK: AppointmentID, FKs: PatientID, DoctorID). Now all attributes in `Patients` strictly depend on `PatientID`.

## Third Normal Form (3NF) / BCNF
**Rule:** Must be in 2NF. There must be no transitive dependencies (non-key attributes depending on other non-key attributes).
**Action:**
- If `Treatments` and `Billing` were in `Appointments`, Billing information would depend on Treatment details rather than the Appointment itself.
- Created separate `Treatments` table (PK: TreatmentID, FK: AppointmentID).
- Created separate `Billing` table (PK: BillID, FK: TreatmentID).
- Now, `TotalAmount` depends strictly on the `BillID`, and `Diagnosis` depends strictly on `TreatmentID`. 

The final schema is normalized up to 3NF, ensuring data integrity, minimizing redundancy, and preventing insert/update/delete anomalies.
