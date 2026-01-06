-- Insert default roles into the Roles table
INSERT INTO "Roles" ("Name", "IsActive") VALUES 
('Owner', true),
('Admin', true),
('Manager', true),
('Customer', true);

-- View all roles
SELECT "Id", "Name", "IsActive" FROM "Roles";
