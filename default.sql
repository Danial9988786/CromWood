-- Here will go the inital query that we have to run while inisitalizing this project
INSERT INTO dbo.ClaimTypes values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','CT1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','CT2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','CT3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','CT4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','CT5')

INSERT INTO dbo.ComplaintCategories values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','CC1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','CC2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','CC3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','CC4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','CC5')

INSERT INTO dbo.ComplaintNatures values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','CN1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','CN2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','CN3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','CN4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','CN5')

INSERT INTO dbo.ComplaintTypes values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','CT1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','CT2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','CT3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','CT4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','CT5')

INSERT INTO dbo.Concerns values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','C1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','C2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','C3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','C4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','C5')

INSERT INTO dbo.ContractTypes values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','CT1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','CT2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','CT3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','CT4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','CT5')

INSERT INTO dbo.Countries values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','Nepal'),
('c6f007f8-2d30-49a0-a248-add219f4748d','India'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','China'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','Pakistan'),
('01289643-bba0-48d0-b34c-c69b466be7ec','Bhutan')

INSERT INTO dbo.Courts values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','Ktm Court'),
('c6f007f8-2d30-49a0-a248-add219f4748d','Court Bern Court'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','Supreme Court'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','District Court'),
('01289643-bba0-48d0-b34c-c69b466be7ec','High Court')

INSERT INTO dbo.FinancialPrgorams values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','FP1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','FP2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','FP3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','FP4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','FP5')

INSERT INTO dbo.LicenseCertificateTypes values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','LCT1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','LCT2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','LCT3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','LCT4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','LCT5')

INSERT INTO dbo.PaymentMethods values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','Cash'),
('c6f007f8-2d30-49a0-a248-add219f4748d','Credit'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','Cheque'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','Bitcoin'),
('01289643-bba0-48d0-b34c-c69b466be7ec','Crypto')

INSERT INTO dbo.PropertyTypes values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','PT1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','PT2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','PT3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','PT4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','PT5')

INSERT INTO dbo.PropertyKeyTypes (Id, Name) values 
('e40e3062-29b3-49fb-a786-e9523ced99df','Physical Key'),
('4607fce5-af4b-49bf-a977-c27c621a0eb2','Lock Box'),
('4027e368-034c-4854-9cd3-82284797f037','Alarm'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','Entry Code'),
('ef7dc97d-d49b-40ca-8214-09d14f701246','Other')

INSERT INTO dbo.AssetTypes values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','AT1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','AT2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','AT3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','AT4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','AT5')

INSERT INTO dbo.RentFrequencies values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','RF1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','RF2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','RF3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','RF4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','RF5')

INSERT INTO dbo.Salutions values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','Mr'),
('c6f007f8-2d30-49a0-a248-add219f4748d','Miss'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','Miss'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','Jr'),
('01289643-bba0-48d0-b34c-c69b466be7ec','Dr')

INSERT INTO dbo.Sections values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','Section 6'),
('c6f007f8-2d30-49a0-a248-add219f4748d','Section 482'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','Section 9'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','Section 2'),
('01289643-bba0-48d0-b34c-c69b466be7ec','Section 4')

INSERT INTO dbo.TenantTypes values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','Owner'),
('c6f007f8-2d30-49a0-a248-add219f4748d','Lander'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','Oker'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','Parent'),
('01289643-bba0-48d0-b34c-c69b466be7ec','Typer')

INSERT INTO dbo.TransactionTypes values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','TT1'),
('c6f007f8-2d30-49a0-a248-add219f4748d','TT2'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','TT3'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','TT4'),
('01289643-bba0-48d0-b34c-c69b466be7ec','TT5')

INSERT INTO dbo.Amenities  values 
('a8228559-11be-4d9a-9544-8241f2ec59c1','Bills Included','Internet', 'internet'),
('c6f007f8-2d30-49a0-a248-add219f4748d','Bills Included','Phone', 'phone'),
('64432c8e-8cc3-4365-a8f5-e4dc925c42fd','General','Fireplace', 'fireplace'),
('914d1a66-0ee7-45f6-ab77-19c928c5a426','General','Gym', 'gym'),
('01289643-bba0-48d0-b34c-c69b466be7ec','Appliances','Washing Machine', 'washing_machine')


-- Default Roles, Permission, User : At first --

INSERT INTO dbo.Permissions (Id, PermissionKey, PermissionDisplayName) values 
('80341c60-c482-4249-979d-0d10a1e41d9b','dashboard_management', 'Dashboard Management'),
('0c0c5a45-741d-4ae9-8e3c-a976a1d9c3bd','user_management', 'User Management'),
('84533fc1-d782-4d9d-98c8-d558397909b1','role_management', 'Role Management'),
('92b759ee-7da6-49ba-838e-56144187042d','asset_management', 'Asset Management'),
('a3a31140-bdb7-4b82-8742-1547e0c1232a','property_management', 'Property Management'),
('3d6a1f89-ed7f-46d2-8ea8-87cc279dcf32','tenancy_management', 'Tenancy Management'),
('b4b9dfa1-b26a-4c16-98f7-c706fc29a568','finance_management', 'Finance Management'),
('ded2522b-487a-46f1-b452-010c15fd0fce','license_management', 'License/Certification Management');


INSERT INTO dbo.Roles (Id, Name) values
('01289643-bba0-48d0-b34c-c69b466be7ec', 'Administrator')

INSERT INTO dbo.RolePermissions(Id, RoleId, PermissionId, CanRead, CanWrite, CanDelete, CanViewAll) values
('01289643-bba0-48d0-b34c-c69b466be7ec', '01289643-bba0-48d0-b34c-c69b466be7ec', '80341c60-c482-4249-979d-0d10a1e41d9b', 1, 1, 1, 1),
('92b759ee-7da6-49ba-838e-56144187042d', '01289643-bba0-48d0-b34c-c69b466be7ec', '0c0c5a45-741d-4ae9-8e3c-a976a1d9c3bd', 1, 1, 1, 1),
('3d6a1f89-ed7f-46d2-8ea8-87cc279dcf32', '01289643-bba0-48d0-b34c-c69b466be7ec', 'b4b9dfa1-b26a-4c16-98f7-c706fc29a568', 1, 1, 1, 1),
('0c0c5a45-741d-4ae9-8e3c-a976a1d9c3bd', '01289643-bba0-48d0-b34c-c69b466be7ec', '92b759ee-7da6-49ba-838e-56144187042d', 1, 1, 1, 1),
('80341c60-c482-4249-979d-0d10a1e41d9b', '01289643-bba0-48d0-b34c-c69b466be7ec', '3d6a1f89-ed7f-46d2-8ea8-87cc279dcf32', 1, 1, 1, 1),
('a3a31140-bdb7-4b82-8742-1547e0c1232a', '01289643-bba0-48d0-b34c-c69b466be7ec', 'a3a31140-bdb7-4b82-8742-1547e0c1232a', 1, 1, 1, 1),
('b4b9dfa1-b26a-4c16-98f7-c706fc29a568', '01289643-bba0-48d0-b34c-c69b466be7ec', 'b4b9dfa1-b26a-4c16-98f7-c706fc29a568', 1, 1, 1, 1),
('ded2522b-487a-46f1-b452-010c15fd0fce', '01289643-bba0-48d0-b34c-c69b466be7ec', 'ded2522b-487a-46f1-b452-010c15fd0fce', 1, 1, 1, 1);


-- INSERT ADMIN INTO USER TABLE
INSERT INTO dbo.Users (Id, FirstName, LastName, Email, Password,RoleId, IsActive, LastActive) values 
('01289643-bba0-48d0-b34c-c69b466be7ec', 'Cromwood','Admin','admin@cromwood.com','8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918','01289643-bba0-48d0-b34c-c69b466be7ec', 1, GETDATE())