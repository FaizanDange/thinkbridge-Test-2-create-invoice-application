-- SQLite compatible script

CREATE TABLE Invoices (
    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
    CustomerName TEXT
);

CREATE TABLE InvoiceItems (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    InvoiceId INTEGER,
    Name TEXT,
    Price DECIMAL(10,2),
    FOREIGN KEY (InvoiceId) REFERENCES Invoices(Id)
);

INSERT INTO Invoices (Id, CustomerName) VALUES (1, 'John Doe');
INSERT INTO InvoiceItems (InvoiceId, Name, Price) VALUES (1, 'Widget A', 19.99);
INSERT INTO InvoiceItems (InvoiceId, Name, Price) VALUES (1, 'Widget B', 25.50);
